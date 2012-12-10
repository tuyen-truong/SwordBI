using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class DashboardSetting : PageBase
    {
        protected string WHCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Dashboard List";
            if (!IsPostBack)
            {
                // Load Data WareHouse
                Helpers.SetDataSource(cboDataDW, MyBI.Me.GetDW(), "Value", "Text");

                WHCode = Get_Param(PageArgs.WHCode);
                cboDataDW.Value = WHCode;
                GetDashboardList(WHCode);
            }
        }

        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            WHCode = Lib.NTE(cboDataDW.Value);
            GetDashboardList(WHCode);
        }

        /// <summary>
        /// Load available dashboard by specified WHCode.
        /// Then output to lstDashboard
        /// </summary>
        /// <param name="WHCode">Warehouse Code</param>
        private void GetDashboardList(string WHCode)
        {
            lstDashboard.DataSource = MyBI.Me.Get_Dashboard(WHCode);
            lstDashboard.DataBind();
            List<Codes.Models.lsttbl_Dashboard> dashboards = MyBI.Me.Get_Dashboard(WHCode).ToList();
            //dashboards.Where(
            foreach (Codes.Models.lsttbl_Dashboard tbl in dashboards)
            {
                //tbl.JsonObj.

            }
        }     
    }
}