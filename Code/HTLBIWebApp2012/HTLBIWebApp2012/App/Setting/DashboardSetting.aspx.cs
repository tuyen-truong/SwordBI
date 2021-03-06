﻿using System;
using System.Web.UI;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;

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
                cboDataDW_ValueChanged(this.cboDataDW, new EventArgs());
            }
        }

        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            WHCode = Lib.NTE(cboDataDW.Value);
            GetDashboardList(WHCode);
            // create add new link
            SetAddNewUrl();
            // Set Navigation Url
            DataSource.NavigateUrl = String.Format("DatasourceList.aspx?whcode={0}", WHCode);
            KpiList.NavigateUrl = String.Format("KpiList.aspx?whcode={0}", WHCode);
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
        }
        /// <summary>
        /// Make add new url
        /// </summary>     
        private void SetAddNewUrl()
        {
            if (String.IsNullOrEmpty(WHCode))
            {
                lnkAddNew.Enabled = false;
                return;
            }
            lnkAddNew.Enabled = true;
            lnkAddNew.NavigateUrl = String.Format("DashboardEdit.aspx?whcode={0}", WHCode);
        }
    }
}