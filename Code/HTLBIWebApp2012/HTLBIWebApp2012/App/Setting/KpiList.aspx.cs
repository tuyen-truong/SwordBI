﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class KpiList : PageBase
    {
        private String WHCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            WHCode = Get_Param(PageArgs.WHCode);

            if (!IsPostBack)
            {
                // Load data warehouse
                Helpers.SetDataSource(cboDataDW, MyBI.Me.GetDW(), "Value", "Text");
                cboDataDW.Value = WHCode;
                cboDataDW_ValueChanged(this.cboDataDW, new EventArgs());
            }
        }

        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            WHCode = Lib.NTE(cboDataDW.Value);
            lstKpiList.DataSource = MyBI.Me.Get_DashboardKPI_ByWH(WHCode);
            lstKpiList.DataBind();
        }
    }
}