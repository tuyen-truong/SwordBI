using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class PortletItems : PageBase
    {
        private string WHCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load Data WareHouse
                Helpers.SetDataSource(cboDataDW, MyBI.Me.GetDW(), "Value", "Text");
                
                WHCode = Get_Param(PageArgs.WHCode);
                if (!string.IsNullOrEmpty(WHCode))
                {
                    cboDataDW.Value = WHCode;
                    gridPortletList.DataSource = MyBI.Me.Get_Widget(WHCode);
                    gridPortletList.DataBind();
                    IQueryable<lsttbl_Widget> widgets = MyBI.Me.Get_Widget(WHCode);
                    foreach (lsttbl_Widget wg in widgets)
                    {
                        if (wg.WidgetType == "gauge")
                        {
                            WidgetGauge wgGauge = wg.JsonObj_Gauge;
                            
                        }
                        else if (wg.WidgetType == "chart")
                        {
                            WidgetChart wgChart = wg.JsonObj_Chart;
                        }
                        else
                        {
                            // grid
                            WidgetGrid wgGrid = wg.JsonObj_Grid;
                        }
                    }
                }
            }

        }
        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            WHCode = Lib.NTE(cboDataDW.Value);
            gridPortletList.DataSource = MyBI.Me.Get_Widget(WHCode);
            gridPortletList.DataBind();
        }
    }
}