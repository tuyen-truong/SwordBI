using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.Web.ASPxCallbackPanel;
using CECOM;
using System.Drawing;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcDashboardSetting : PartPlugCtrlBase
    {
        #region Declares
        public string MenuCode { get; set; }
        public string WHCode { get; set; }
        protected List<string> CtrlDashboardFilterIDs
        {
            get
            {
                if (ViewState["CtrlDashboardFilterIDs"] == null)
                    ViewState["CtrlDashboardFilterIDs"] = new List<string>();
                return ViewState["CtrlDashboardFilterIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlDashboardFilterIDs"] = value;
                if (value == null) GC.Collect();
            }
        }        
        #endregion

        #region Members
        /*
        private DashboardDefine Get_DefineInfo()
        {
            try
            {
                var ret = new DashboardDefine()
                {
                    DisplayName = this.txtDisplayName.Text,
                    Template = Lib.NTE(this.cboTemplate.Value)
                };
                ret.Filters = this.ctrl_DashboardFilters.Controls.OfType<wcInteractionFilter>()
                    .Select(p => p.Get_FilterInfo()).ToList();
                ret.UsingPortlets = MySession.DashboardDefine_UsingPortlet
                    .OfType<COMCodeNameObj>().Select(p => p.Code).ToList();
                return ret;
            }
            catch { return null; }
        }
        private wcInteractionFilter Add_FilterControl(bool isReCreate)
        {
            var guiID = Guid.NewGuid().ToString().Replace('-', '_');
            wcInteractionFilter ctrl = null;
            var dsFields = MyBI.Me.Get_DWColumn(this.WHCode)
                .Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime"))
                .Select(p => new COMCodeNameObj(p.ColName, p.ColAliasVI)).ToList();
            // ReCreate...
            if (isReCreate)
            {
                foreach (var ctrlID in this.CtrlDashboardFilterIDs)
                {
                    ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.FilterCtrl_Remove;
                    ctrl.Set_Source(dsFields, "Code", "Name");
                    this.ctrl_DashboardFilters.Controls.Add(ctrl);
                }
                return null;
            }
            // Add new...
            ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
            ctrl.ID = string.Format("gen_{0}", guiID);
            ctrl.OnRemove += this.FilterCtrl_Remove;
            ctrl.Set_Source(dsFields, "Code", "Name");
            this.ctrl_DashboardFilters.Controls.Add(ctrl);
            this.CtrlDashboardFilterIDs.Add(ctrl.ID);
            return ctrl;
        }
        private void Set_SaveMsgText(string msg, bool isError)
        {
            this.lblSavingMsg.Font.Bold = true;
            this.lblSavingMsg.Font.Italic = true;
            this.lblSavingMsg.Font.Name = "Arial";
            this.lblSavingMsg.ForeColor = isError ? Color.Red : Color.Blue;
            this.lblSavingMsg.Text = msg;
        }
        public override void Reset_Info(params object[] param)
        {
            this.lblSavingMsg.Text = "";
            this.txtDisplayName.Text = "";
            this.cboTemplate.Value = "";
            MySession.DashboardDefine_CurEditing = null;

            this.lbxUsingPortlet.Items.Clear();
            MySession.DashboardDefine_UsingPortlet.Clear();

            this.ctrl_DashboardFilters.Controls.Clear();
            this.CtrlDashboardFilterIDs.Clear();
        }
        */
        #endregion        

        #region Events
        public override void Load_InitData()
        {
            if (!IsPostBack)
            {
                // Load Data WareHouse
                Helpers.SetDataSource(this.cboDataDW, MyBI.Me.GetDW(), "Value", "Text");
            }
        }
        protected void cbp_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            var cbp = sender as ASPxCallbackPanel;
            if (cbp == null) return;
        }
        #endregion

        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            this.WHCode = Lib.NTE(this.cboDataDW.Value);
            // Load Available dashboard...
            lstDashboard.DataSource = MyBI.Me.Get_Dashboard(WHCode);
            lstDashboard.DataBind();

            // Load availavle dashboard
            /*
            lstBox.DataSource = MyBI.Me.Get_Widget(WHCode);
            lstBox.DataBind();
            */
            var ds = MyBI.Me.Get_Widget(WHCode).ToList();
            Helpers.SetDataSource(this.lstBox, ds, "Code", "Name");
        }
    }
}