using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using CECOM;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcPropGrid_Column : PartPlugCtrlBase
    {
        protected void ctrl_ValueChanged(object sender, EventArgs e)
        {
            var curCol = GlobalSsn.WidgetGrid_SsnModel.Columns
                .FirstOrDefault(p => p.Name == this.ID);
            if (curCol == null) return;

            if (sender is TextBox)
            {
                var ctrl = sender as TextBox;
                if (ctrl == null) return;
                if (ctrl.ID == this.txtVisibleIndex.ID)
                {
                    curCol.VisibleIndex = int.Parse(ctrl.Text);
                    this.txtWidth.Focus();
                }
                else if (ctrl.ID == this.txtWidth.ID)
                {
                    curCol.Width = int.Parse(ctrl.Text);
                }
            }
            else if (sender is ASPxTextBox)
            {
                var ctrl = sender as ASPxTextBox;
                if (ctrl == null) return;
                if (ctrl.ID == this.txtCaption.ID)
                {
                    curCol.Caption = ctrl.Text;
                    this.txtFormatStr.Focus();
                }
            }
            else if (sender is ASPxComboBox)
            {
                var ctrl = sender as ASPxComboBox;
                if (ctrl == null) return;
                if (ctrl.ID == this.cboField.ID)
                {
                    curCol.FieldName = Lib.NTE(ctrl.Value);
                    this.txtCaption.Focus();
                    var myInqDS = GlobalSsn.WidgetGrid_SsnModel.Get_BIDatasource().JsonObjMDX;
                    if (myInqDS != null)
                    {
                        var obj = myInqDS.Fields.FirstOrDefault(p => p.ColName == curCol.FieldName);
                        if (obj == null)
                            obj = myInqDS.Summaries.FirstOrDefault(p => p.Field.ColName == curCol.FieldName).Field;
                        if (obj != null)
                        {
                            this.txtCaption.Text = obj.ColAliasVI;
                            curCol.Caption = obj.ColAliasVI;
                        }
                    }
                }
                else if (ctrl.ID == this.cboAlign.ID)
                {
                    curCol.Align = Lib.ToEnum<System.Web.UI.WebControls.HorizontalAlign>(ctrl.Value);
                    this.txtVisibleIndex.Focus();
                }
            }
        }
        public void Set_DataSource(lsttbl_DashboardSource ds)
        {
            try
            {
                this.Reset_Info();
                if (ds == null) return;
                if (ds.SettingCat == GlobalVar.SettingCat_DS)
                {
                    var obj = ds.JsonObjMDX;
                    var dsUnion = obj.Fields.Union(obj.Summaries.Select(p => p.Field)).Distinct();
                    Helpers.SetDataSource(this.cboField, dsUnion, "ColName", "ColAliasVI", this.cboField.Value);
                    this.txtVisibleIndex.Text = (GlobalSsn.WidgetGrid_SsnModel.Columns.Count + 1).ToString();
                }
                else if (ds.SettingCat == GlobalVar.SettingCat_KPI)
                {
                    var obj = ds.JsonObjKPI;
                    var dsX = obj.Dimensions.Select(p => new COMCodeNameObj(p.FieldName, p.DisplayName)).ToList();
                    var dsY = obj.Get_SummaryFields();
                    var dsUnion = dsX.Union(dsY);
                    Helpers.SetDataSource(this.cboField, dsUnion, "Code", "Name", this.cboField.Value);
                    this.txtVisibleIndex.Text = (GlobalSsn.WidgetGrid_SsnModel.Columns.Count + 1).ToString();
                }
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            this.cboField.Items.Clear();
            this.cboField.Text = string.Empty;
        }
        public void Set_GridColumn(WebGirdColumn info)
        {
            this.cboField.Value = info.FieldName;
            this.txtCaption.Text = info.Caption;
            this.txtFormatStr.Text = info.DisplayF;
            this.cboAlign.Value = info.Align.ToString();
            this.txtVisibleIndex.Text = info.VisibleIndex.ToString();
            this.txtWidth.Text = info.Width.ToString();
        }
        public WebGirdColumn Get_GridColumn()
        {
            try
            {
                var ret = new WebGirdColumn
                {
                    Name = this.ID,
                    FieldName = Lib.NTE(this.cboField.Value),
                    Caption = this.txtCaption.Text,
                    DisplayF = this.txtFormatStr.Text,
                    Align = Lib.ToEnum<System.Web.UI.WebControls.HorizontalAlign>(this.cboAlign.Value),
                    VisibleIndex = int.Parse(this.txtVisibleIndex.Text),
                    Width = int.Parse(this.txtWidth.Text)
                };
                return ret;
            }
            catch { }
            return null;
        }
    }
}