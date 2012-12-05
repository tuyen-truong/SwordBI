using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcKPIDimension : KPIPartCtrlBase
    {
        public override void Set_Source(object ds)
        {
            try
            {
                this.Reset_Info();
                var myDs = ds as lsttbl_DashboardSource;
                var obj = myDs.JsonObjMDX;
                Helpers.SetDataSource(this.cboField, obj.Fields, "ColName", "ColAliasVI", this.cboField.Value);
            }
            catch { }
        }
        public override void Set_Info(KPIField info)
        {
            try
            {
                var myInfo = info as KPIDimension;
                this.cboField.Value = myInfo.FieldName;
                this.txtDisplayName.Text = myInfo.DisplayName;
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            this.cboField.Items.Clear();
            this.cboField.Text = string.Empty;
            this.txtDisplayName.Text = "";
        }
        public override KPIField Get_KPIPartInfo()
        {
            try
            {
                var ret = new KPIDimension
                {
                    DisplayName = this.txtDisplayName.Text,
                    FieldName = Lib.NTE(this.cboField.Value)
                };
                return ret;
            }
            catch { }
            return null;
        }
        protected void ctrl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is ASPxComboBox)
                {
                    var ctrl = sender as ASPxComboBox;
                    if (ctrl.ID == this.cboField.ID)
                    {
                        this.txtDisplayName.Text = ctrl.Text;
                        this.txtDisplayName.Focus();
                    }
                }
            }
            catch { }
        }        
    }
}