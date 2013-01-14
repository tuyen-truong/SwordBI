﻿using System;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class DatasourceList : PageBase
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
            lstDatasource.DataSource = MyBI.Me.Get_DashboardSource(WHCode, "DS");
            lstDatasource.DataBind();
        }
    }
}