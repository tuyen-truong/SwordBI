using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.Web.ASPxEditors;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class DashboardEdit : PageBase
    {
        private string DashboardId { get; set; }
        private lsttbl_Dashboard Dashboard { get; set; }
        private string WHCode { get; set; }
        private List<string> CtrlDashboardFilterIDs
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Edit Dashboard";
            DashboardId = Get_Param(PageArgs.DashboardId);
            if (string.IsNullOrEmpty(DashboardId))
            {
                // TODO: New Dashboard
            }
            else
            {
                Dashboard = MyBI.Me.Get_Dashboard().FirstOrDefault(db => db.ID == Int32.Parse(DashboardId));
                txtDashboardName.Text = Dashboard.JsonObj.DisplayName;
                WHCode = Dashboard.WHCode;

                RadioButton radio = Helpers.FindControlRecur(Page, Dashboard.JsonObj.Template) as RadioButton;
                if (radio != null)
                {
                    radio.Checked = true;
                }
            }
            if (IsPostBack)
            {
                Add_FilterControl(true);
            }
        }

        protected void btnAddDashboardFilter_Click(object sender, EventArgs e)
        {
            Add_FilterControl(false);
        }

        protected void FilterCtrl_Remove(object sender, EventArgs e)
        {
            string ctrlID = (sender.GetVal("Parent") as Control).ID;
            CtrlDashboardFilterIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
            ctrl_DashboardFilters.Controls.RemoveAll(p => p.ID == ctrlID);
        }

        private wcInteractionFilter Add_FilterControl(bool isReCreate)
        {
            var dsFields = MyBI.Me.Get_DWColumn(this.WHCode)
                .Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime"))
                .Select(p => new COMCodeNameObj(p.ColName, p.ColAliasVI)).ToList();
            wcInteractionFilter ctrl = null;
            if (isReCreate)
            {
                foreach (string ctrlID in CtrlDashboardFilterIDs)
                {
                    ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.FilterCtrl_Remove;
                    ctrl.Set_Source(dsFields, "Code", "Name");
                    ctrl_DashboardFilters.Controls.Add(ctrl);
                }
                return null;
            }
            var guiID = Guid.NewGuid().ToString().Replace("-", "_");
            ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
            ctrl.ID = string.Format("gen_{0}", guiID);
            ctrl.OnRemove += this.FilterCtrl_Remove;
            ctrl.Set_Source(dsFields, "Code", "Name");
            ctrl_DashboardFilters.Controls.Add(ctrl);
            CtrlDashboardFilterIDs.Add(ctrl.ID);
            return ctrl;
        }


    }
}