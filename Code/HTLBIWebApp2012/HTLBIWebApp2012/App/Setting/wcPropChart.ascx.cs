using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts.Web;
using DevExpress.Web.ASPxEditors;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.XtraCharts;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcPropChart : PropCtrlBase
    {
        #region Properties
        public bool IsPie { get; set; }
        #endregion

        #region Members
        public override void Load_InitData()
        {
            this.cboAppearance.Items.Clear();
            var obj_Sel = Lib.IfNOE(this.cboAppearance.Value, Helpers.AppearanceNameDefault);
            Helpers.InitAppearanceComboBox(this.cboAppearance, obj_Sel, true);

            this.cboPalette.Items.Clear();
            obj_Sel = Lib.IfNOE(this.cboPalette.Value, Helpers.PaletteNameDefault);
            Helpers.InitPaletteComboBox(this.cboPalette, obj_Sel);
        }
        public override void Set_ViewType(string viewType)
        {
            this.chartPreview.SetChartType(viewType, true);
        }
        public override void Set_Info(object info)
        {
            try
            {
                var myInfo = info as lsttbl_Widget;
                var wgObj = myInfo.JsonObj_Chart;                
                // Reset old infor
                this.Reset_Text();

                // Set new infor
                this.cboAppearance.Value = wgObj.AppearanceName;
                this.cboPalette.Value = wgObj.PaletteName;
                this.cboAxisXField1.Value = wgObj.XFields.FirstOrDefault();
                if (wgObj.XFields.Count > 1)
                    this.cboAxisXField2.Value = wgObj.XFields.LastOrDefault();
                this.txtTitleX.Text = wgObj.XTitle;

                var lstYFields = new List<COMCodeNameObj>();
                var dsObj = MyBI.Me.Get_DashboardSourceBy(wgObj.DatasourceID);
                //this.lbxAxisYFieldSel
                if (dsObj.SettingCat == GlobalVar.SettingCat_DS)
                    lstYFields = dsObj.JsonObjMDX.Summaries
                        .Where(p => wgObj.YFields.Contains(p.Field.ColName))
                        .Select(p => new COMCodeNameObj(p.Field.ColName, p.FieldAlias)).ToList();
                else if (dsObj.SettingCat == GlobalVar.SettingCat_KPI)
                    lstYFields = dsObj.JsonObjKPI.Get_SummaryFields()
                        .Where(p => wgObj.YFields.Contains(p.Code)).ToList();
                MySession.LayoutDefine_Chart_SelAxisY.AddRange(lstYFields);
                Helpers.SetDataSource(this.lbxAxisYFieldSel, lstYFields, "Code", "Name");

                this.txtTitleY.Text = wgObj.YTitle;
                this.txtYUnit.Text = wgObj.YUnitName;
                this.chkRotatedXY.Checked = wgObj.RotatedXY;
                this.chkShowSeriesLabel.Checked = wgObj.ShowSeriesLabel;
                this.txtWidth.Text = wgObj.Width.ToString();
                this.txtHeight.Text = wgObj.Height.ToString();

                //////
                this.chartPreview.AppearanceName = wgObj.AppearanceName;
                this.chartPreview.PaletteName = wgObj.PaletteName;
                //
                var xyDia = this.chartPreview.Diagram as XYDiagram;
                if (wgObj.RotatedXY)
                {
                    xyDia.Rotated = true;
                    xyDia.PaneLayoutDirection = PaneLayoutDirection.Horizontal;
                }
                else
                {
                    xyDia.Rotated = false;
                    xyDia.PaneLayoutDirection = PaneLayoutDirection.Vertical;
                }
                //
                foreach (SeriesBase series in this.chartPreview.Series)
                    series.Label.Visible = wgObj.ShowSeriesLabel;
            }
            catch { }
        }
        public override void Set_Source(object ds)
        {
            try
            {
                this.Reset_Info();
                var myDs = ds as lsttbl_DashboardSource;
                if (myDs == null) return;

                if (myDs.SettingCat == GlobalVar.SettingCat_DS)
                {
                    var obj = myDs.JsonObjMDX;
                    var dsX = obj.Fields.Select(p => new { p.ColName, p.ColAliasVI }).ToList();
                    dsX.Add(new { ColName = "", ColAliasVI = "" });
                    Helpers.SetDataSource(this.cboAxisXField1, dsX, "ColName", "ColAliasVI", this.cboAxisXField1.Value);
                    Helpers.SetDataSource(this.cboAxisXField2, dsX, "ColName", "ColAliasVI", this.cboAxisXField2.Value);
                    Helpers.SetDataSource(this.lbxAxisYField, obj.Summaries, "Field.ColName", "FieldAlias", this.lbxAxisYField.Value);
                }
                else if (myDs.SettingCat == GlobalVar.SettingCat_KPI)
                {
                    var obj = myDs.JsonObjKPI;
                    var dsX = obj.Dimensions.Select(p => new { Code = p.FieldName, Name = p.DisplayName }).ToList();
                    dsX.Add(new { Code = "", Name = "" });
                    var dsY = obj.Get_SummaryFields();
                    Helpers.SetDataSource(this.cboAxisXField1, dsX, "Code", "Name", this.cboAxisXField1.Value);
                    Helpers.SetDataSource(this.cboAxisXField2, dsX, "Code", "Name", this.cboAxisXField2.Value);
                    Helpers.SetDataSource(this.lbxAxisYField, dsY, "Code", "Name", this.lbxAxisYField.Value);                    
                }
            }
            catch { }
        }        
        public override void Reset_Info(params object[] param)
        {
            try
            {
                // Clear List field
                this.cboAxisXField1.Items.Clear();
                this.cboAxisXField2.Items.Clear();
                this.lbxAxisYField.Items.Clear();
                this.Reset_Text();
            }
            catch { }
        }
        public void Reset_Text()
        {
            // Clear List field
            this.cboAppearance.Value = Helpers.AppearanceNameDefault;
            this.cboPalette.Value = Helpers.PaletteNameDefault;
            this.cboAxisXField1.Text = "";
            this.cboAxisXField2.Text = "";
            this.txtTitleX.Text = "";
            this.lbxAxisYFieldSel.Items.Clear();
            MySession.LayoutDefine_Chart_SelAxisY.Clear();
            this.txtTitleY.Text = "";
            this.txtYUnit.Text = "";
            this.chkRotatedXY.Checked = false;
            this.chkShowSeriesLabel.Checked = false;
            this.txtWidth.Text = "485";
            this.txtHeight.Text = "400";
        }
        public List<string> Get_XFields()
        {
            return new[] 
            { 
                Lib.NTE(this.cboAxisXField1.Value), 
                Lib.NTE(this.cboAxisXField2.Value)
            }.Where(p => !string.IsNullOrEmpty(p)).ToList();
        }
        public List<string> Get_YFields()
        {
            return this.lbxAxisYFieldSel.Items.OfType<ListEditItem>().Select(p => Lib.NTE(p.Value)).ToList();
        }
        public override WidgetBase GetInputSett()
        {
            var obj = new WidgetChart
            {
                AppearanceName = Lib.NTE(this.cboAppearance.Value),
                PaletteName = Lib.NTE(this.cboPalette.Value),
                RotatedXY = this.chkRotatedXY.Checked,
                XTitle = this.txtTitleX.Text,
                XFields = this.Get_XFields(),
                YTitle = this.txtTitleY.Text,
                YFields = this.Get_YFields(),
                YUnitName = this.txtYUnit.Text,
                Width = int.Parse(this.txtWidth.Text),
                Height = int.Parse(this.txtHeight.Text),
                ShowSeriesLabel = this.chkShowSeriesLabel.Checked
            };
            return obj;
        }
        #endregion

        #region Events
        protected void chartPreview_CustomCallback(object sender, CustomCallbackEventArgs e)
        {
            try
            {
                if (e.Parameter == this.cboAppearance.ID || e.Parameter == this.cboPalette.ID)
                {
                    this.chartPreview.AppearanceName = this.cboAppearance.SelectedItem.Text;
                    this.chartPreview.PaletteName = this.cboPalette.SelectedItem.Text;
                }
                else if (e.Parameter == this.chkRotatedXY.ID)
                {
                    var xyDia = this.chartPreview.Diagram as XYDiagram;
                    if (this.chkRotatedXY.Checked)
                    {
                        xyDia.Rotated = true;
                        xyDia.PaneLayoutDirection = PaneLayoutDirection.Horizontal;
                    }
                    else
                    {
                        xyDia.Rotated = false;
                        xyDia.PaneLayoutDirection = PaneLayoutDirection.Vertical;
                    }
                }
                else if (e.Parameter == this.chkShowSeriesLabel.ID)
                {
                    var isChecked = this.chkShowSeriesLabel.Checked;
                    foreach (SeriesBase series in this.chartPreview.Series)
                        series.Label.Visible = isChecked;
                }
            }
            catch { }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as DevExpress.Web.ASPxEditors.ASPxButton;
                if (btn == null) return;

                if (btn.ID == this.btnIn.ID)
                {
                    var item = this.lbxAxisYField.SelectedItem;
                    if (item == null) return;
                    var colName = Lib.NTE(item.Value);
                    var info = new COMCodeNameObj(colName, item.Text);
                    var sel_LayoutDefine_AxisY = MySession.LayoutDefine_Chart_SelAxisY;
                    if (sel_LayoutDefine_AxisY.ToArray().Exists(p => p.GetStr("Code") == info.Code)) return;
                    sel_LayoutDefine_AxisY.Add(info);
                    Helpers.SetDataSource(this.lbxAxisYFieldSel, sel_LayoutDefine_AxisY, "Code", "Name");
                }
                else if (btn.ID == this.btnOut.ID)
                {
                    var itemRemove = lbxAxisYFieldSel.SelectedItem;
                    this.lbxAxisYFieldSel.Items.Remove(itemRemove);
                    var objRemove = MySession.LayoutDefine_Chart_SelAxisY.ToArray()
                        .FirstOrDefault(p => p.GetStr("Code") == Lib.NTE(itemRemove.Value));
                    MySession.LayoutDefine_Chart_SelAxisY.Remove(objRemove);
                }
            }
            catch { }
        }
        #endregion
    }
}