using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.Dashboard.Base;

namespace HTLBIWebApp2012.App.Dashboard
{
    public partial class Dashboard : PageBase
    {
        private string Ctrl_DashboardID
        {
            get
            {
                return Lib.NTE(ViewState["Ctrl_DashboardID"]);
            }
            set
            {
                ViewState["Ctrl_DashboardID"] = value;
                if (value == null) GC.Collect();
            }
        }
        private LayoutPortletCtrlBase Load_Dashboard(bool isReCreate, string newDbrdCode)
        {
            this.ctrl_Dashboard.Controls.Clear();
            LayoutPortletCtrlBase ctrl = null;
            lsttbl_Dashboard dbrdObj = null;
            DashboardDefine dbrdDef = null;

            // ReCreate...
            if (isReCreate)
            {
                if (string.IsNullOrEmpty(this.Ctrl_DashboardID)) return null;
                var tmpl = this.Ctrl_DashboardID.Split('-').Last();
                ctrl = this.LoadControl(string.Format("wc{0}.ascx", tmpl)) as LayoutPortletCtrlBase;
                ctrl.ID = this.Ctrl_DashboardID;
            }
            else // Add new...
            {                
                dbrdObj = MyBI.Me.Get_DashboardBy(newDbrdCode);
                if (dbrdObj == null) return null; dbrdDef = dbrdObj.JsonObj;
                ctrl = this.LoadControl(string.Format("wc{0}.ascx", dbrdDef.Template)) as LayoutPortletCtrlBase;
                ctrl.ID = string.Format("genDashboard-{0}-{1}", dbrdObj.Code, dbrdDef.Template);
                this.Ctrl_DashboardID = ctrl.ID;
            }
            ctrl.ContainerUpdatePanel = this.udp_Dashboard;            
            this.ctrl_Dashboard.Controls.Add(ctrl);
            return ctrl;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnChange += this_OnChange;
            //Tạo lại control....
            if (this.IsPostBack) this.Load_Dashboard(true, null);
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            try
            {
                (this.ctrl_Dashboard.Controls[0] as LayoutPortletCtrlBase).RegisterStartupScript();
            }
            catch { }
        }
        protected void this_OnChange(object sender, EventArgs e)
        {
            try
            {
                this.Ctrl_DashboardID = null;
                var myArg = e as HTLBIEventArgs;
                this.Load_Dashboard(false, Lib.NTE(myArg.Values));
            }
            catch { }
        }
    }
}