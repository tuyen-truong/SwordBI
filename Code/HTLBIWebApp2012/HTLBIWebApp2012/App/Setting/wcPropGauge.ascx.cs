using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.UserControls;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    /*
     * Nếu Load động các các 'UserControl' thì nên để vào sự kiện 'OnLoadComplete' or 'OnPreRender'
     * Không nên để trong các hàm sự kiện của nơi gọi 'Button' vì 'OnLoadComplete' or 'OnPreRender' sẽ
     * được gọi sau cùng so với sự kiện của nơi gọi. nếu không tuân thủ, có thể 1 'UserControl' có thể
     * được load lại 2 lần: 1 trong hàm sự kiện và 2 là trong 'OnLoadComplete' or 'OnPreRender' => làm
     * cho các thành phần đồ họa trên UserControl có thể bị mất do tính And - bit của màu.
     */
    public partial class wcPropGauge : PropCtrlBase
    {
        #region Declares
        protected List<string> CurCtrlStateRange
        {
            get
            {
                if (ViewState["CurCtrlStateRange"] == null)
                    ViewState["CurCtrlStateRange"] = new List<string>();
                return ViewState["CurCtrlStateRange"] as List<string>;
            }
            set
            {
                ViewState["CurCtrlStateRange"] = value;
                if (value == null) GC.Collect();
            }
        }
        protected string CurCtrlPreView
        {
            get
            {
                if (ViewState["CurCtrlPreView"] == null)
                    ViewState["CurCtrlPreView"] = "";
                return ViewState["CurCtrlPreView"] as string;
            }
            set
            {
                ViewState["CurCtrlPreView"] = value;
                if (value == null) GC.Collect();
            }
        }
        #endregion

        #region Members
        public override void Load_InitData()
        {
            // Tạo lại control stateRange....
            this.AddNew_StageRange(false, true);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Tạo lại control preview....
            try
            {
                if (!string.IsNullOrEmpty(this.CurCtrlPreView))
                {
                    var arr = this.CurCtrlPreView.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    string ctrlID = arr.First(), ctrlType = arr.Last();
                    WidgetCtrlBase ctrl = null;
                    var gaugeType = Lib.ToEnum<GaugeType>(ctrlType);
                    switch (gaugeType)
                    {
                        case GaugeType.CircleFull:
                            ctrl = this.LoadControl("../UserControls/wcFullCGauge.ascx") as wcFullCGauge;
                            break;
                        case GaugeType.CircleThreeFour:
                            ctrl = this.LoadControl("../UserControls/wcThreeFourCGauge.ascx") as wcThreeFourCGauge;
                            break;
                        case GaugeType.CircleHalf:
                            ctrl = this.LoadControl("../UserControls/wcHalfCGauge.ascx") as wcHalfCGauge;
                            break;
                        case GaugeType.CircleQuaterLeft:
                        case GaugeType.CircleQuaterRight:
                            ctrl = this.LoadControl("../UserControls/wcQuaterCGauge.ascx") as wcQuaterCGauge;
                            break;
                        case GaugeType.LinearHorizontal:
                        case GaugeType.LinearVertical:
                            ctrl = this.LoadControl("../UserControls/wcLGauge.ascx") as wcLGauge;
                            break;
                    }
                    GlobalSsn.WidgetGauge_SsnModel.VisibleType = gaugeType;
                    ctrl.ID = ctrlID;
                    ctrl.IsDemo = true;
                    ctrl.Sett = GlobalSsn.WidgetGauge_SsnModel;
                    this.ctrlPreView.Controls.Clear();
                    this.ctrlPreView.Controls.Add(ctrl);
                }
            }
            catch { }
        }
        public wcPropGauge_StateRange AddNew_StageRange(bool isAddOnSetInfo,bool isReCreate)
        {
            wcPropGauge_StateRange ctrl = null;
            try
            {
                // Tạo lại...
                if (isReCreate)
                {
                    if (this.CurCtrlStateRange == null || this.CurCtrlStateRange.Count == 0) return null;
                    this.ctrlStateRange.Controls.Clear();
                    foreach (var ctrlID in this.CurCtrlStateRange)
                    {
                        ctrl = this.LoadControl("wcPropGauge_StateRange.ascx") as wcPropGauge_StateRange;
                        ctrl.ID = ctrlID;
                        ctrl.OnRemove += this.RemoveStateRange;
                        this.ctrlStateRange.Controls.Add(ctrl);
                    }
                    return null;
                }
                // Tạo mới...
                var newCtrlID = string.Format("StateRange_{0}", Guid.NewGuid().ToString());
                ctrl = this.LoadControl("wcPropGauge_StateRange.ascx") as wcPropGauge_StateRange;
                ctrl.ID = newCtrlID;
                ctrl.OnRemove += this.RemoveStateRange;

                if (!isAddOnSetInfo)
                {
                    if (this.ctrlStateRange.Controls.Count > 0)
                    {
                        var lastCtrlAdded = this.ctrlStateRange.Controls.OfType<wcPropGauge_StateRange>().LastOrDefault();
                        var lastVal = lastCtrlAdded.Get_StateRange();
                        if (lastVal != null)
                            ctrl.Set_StateRange(new StatusRange<float, float>("", lastVal.RangeVal.End, float.Parse(this.txtMaxValue.Text), System.Drawing.Color.White, ""));
                    }
                    else
                        ctrl.Set_StateRange(new StatusRange<float, float>("", 0, float.Parse(this.txtMaxValue.Text), System.Drawing.Color.White, ""));
                    GlobalSsn.WidgetGauge_SsnModel.StateRanges.Add(ctrl.Get_StateRange());
                }
                this.ctrlStateRange.Controls.Add(ctrl);
                this.CurCtrlStateRange.Add(newCtrlID);
            }
            catch { }
            return ctrl;
        }
        /// <summary>
        /// Tải usercontrol gauge tương ứng với viewType
        /// </summary>
        public override void Set_ViewType(string viewType)
        {
            // Tạo mới một ID cho gauge tương tứng với viewType.(Control sẽ được Add vào tập hợp ở hàm sự kiện 'OnPreRender')
            var ctrlID = string.Format("gaugePreView_{1}_{0}", Guid.NewGuid().ToString(), viewType);
            this.CurCtrlPreView = string.Format("{0},{1}", ctrlID, viewType);
        }
        public override void Set_Info(object info)
        {
            try
            {
                var myInfo = info as lsttbl_Widget;
                var wgObj = myInfo.JsonObj_Gauge;
                GlobalSsn.WidgetGauge_SsnModel = wgObj.Copy();
                GlobalSsn.WidgetGauge_SsnModel.StateRanges.Clear();
                // Reset old infor
                this.Reset_Text();

                // Set new infor
                this.cboDimension.Value = wgObj.Dimension;
                this.cboMeasure.Value = wgObj.Measure;
                this.txtMinValue.Text = wgObj.MinValue.ToString();
                this.txtMaxValue.Text = wgObj.MaxValue.ToString();
                this.txtFormatValue.Text = wgObj.FormatString;
                this.chkShowCurValueText.Checked = wgObj.ShowCurValueText;
                this.txtWidth.Text = wgObj.Width.ToString();
                this.txtHeight.Text = wgObj.Height.ToString();
                //// Gọi hàm add StageRanges...
                foreach (var obj in wgObj.StateRanges)
                {
                    var ctrl = this.AddNew_StageRange(true, false);
                    ctrl.Set_StateRange(obj);
                    GlobalSsn.WidgetGauge_SsnModel.StateRanges.Add(ctrl.Get_StateRange());
                }
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
                    Helpers.SetDataSource(this.cboDimension, dsX, "ColName", "ColAliasVI", this.cboDimension.Value);
                    Helpers.SetDataSource(this.cboMeasure, obj.Summaries, "Field.ColName", "FieldAlias", this.cboMeasure.Value);
                }
                else if (myDs.SettingCat == GlobalVar.SettingCat_KPI)
                {
                    var obj = myDs.JsonObjKPI;
                    var dsX = obj.Dimensions.Select(p => new { Code = p.FieldName, Name = p.DisplayName }).ToList();
                    Helpers.SetDataSource(this.cboDimension, dsX, "Code", "Name", this.cboDimension.Value);
                    var dsY = obj.Get_SummaryFields();
                    Helpers.SetDataSource(this.cboMeasure, dsY, "Code", "Name", this.cboMeasure.Value);
                }
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            try
            {
                this.cboDimension.Items.Clear();
                this.cboMeasure.Items.Clear();
                this.Reset_Text();
            }
            catch { }
        }
        public void Reset_Text()
        {
            this.ctrlStateRange.Controls.Clear();
            this.CurCtrlStateRange.Clear();
            this.cboDimension.Text = "";
            this.cboMeasure.Text = "";
            this.txtMinValue.Text = "0";
            this.txtMaxValue.Text = "0";
            this.txtFormatValue.Text = "N0";
            this.chkShowCurValueText.Checked = false;
            this.txtWidth.Text = "170";
            this.txtHeight.Text = "170";
        }
        public override WidgetBase GetInputSett()
        {
            try
            {
                var ret = new WidgetGauge()
                {
                    MinValue = float.Parse(this.txtMinValue.Text),
                    MaxValue = float.Parse(this.txtMaxValue.Text),
                    Width = int.Parse(this.txtWidth.Text),
                    Height = int.Parse(this.txtHeight.Text),
                    Dimension = Lib.NTE(this.cboDimension.Value),
                    Measure = Lib.NTE(this.cboMeasure.Value),
                    FormatString = this.txtFormatValue.Text,
                    ShowCurValueText = this.chkShowCurValueText.Checked
                };
                foreach (wcPropGauge_StateRange ctrl in this.ctrlStateRange.Controls)
                    ret.StateRanges.Add(ctrl.Get_StateRange());
                return ret;
            }
            catch { return new WidgetGauge(); }
        }
        #endregion

        #region Events
        protected void ctrl_ValueChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                var ctrl = sender as TextBox; if (ctrl == null) return;
                if (ctrl.ID == this.txtMinValue.ID)
                    GlobalSsn.WidgetGauge_SsnModel.MinValue = float.Parse(ctrl.Text);
                else if (ctrl.ID == this.txtMaxValue.ID)
                    GlobalSsn.WidgetGauge_SsnModel.MaxValue = float.Parse(ctrl.Text);
            }
            //cboMeasure
            else if (sender is ASPxComboBox)
            {
                var ctrl = sender as ASPxComboBox; if (ctrl == null) return;
                if (ctrl.ID == this.cboMeasure.ID)
                {
                    var ctx = GlobalSsn.WidgetGauge_SsnModel;
                    ctx.Dimension = Lib.NTE(this.cboDimension.Value);
                    ctx.Measure = Lib.NTE(this.cboMeasure.Value);
                    this.txtMaxValue.Text = ctx.Get_ActualValue();
                    ctx.MaxValue = float.Parse(this.txtMaxValue.Text);
                }
            }
            else if (sender is ASPxTextBox)
            {
                var ctrl = sender as ASPxTextBox; if (ctrl == null) return;
                if (ctrl.ID == this.txtFormatValue.ID)
                    GlobalSsn.WidgetGauge_SsnModel.FormatString = ctrl.Text;
            }
            else if (sender is ASPxCheckBox)
            {
                var ctrl = sender as ASPxCheckBox; if (ctrl == null) return;
                if (ctrl.ID == this.chkShowCurValueText.ID)
                    GlobalSsn.WidgetGauge_SsnModel.ShowCurValueText = ctrl.Checked;
            }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            this.AddNew_StageRange(false, false);
        }
        private void RemoveStateRange(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CurCtrlStateRange.RemoveAll(p => p == ctrlID);
                this.ctrlStateRange.Controls.RemoveAll(p => p.ID == ctrlID);
                GlobalSsn.WidgetGauge_SsnModel.StateRanges.RemoveAll(p => p.Name == ctrlID);
            }
            catch { }
        }
        #endregion
    }
}