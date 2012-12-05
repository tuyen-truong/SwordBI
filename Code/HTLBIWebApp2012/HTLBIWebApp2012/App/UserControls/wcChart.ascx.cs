using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using CECOM;

namespace HTLBIWebApp2012.App.UserControls
{
    public partial class wcChart : ChartCtrlBase
    {
        protected void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            var drv = (e.SeriesPoint.Tag as System.Data.DataRowView);
            if (drv == null) return;
            var valueObj = drv.Row.ItemArray.Last();
            var valueStr = Convert.ToDecimal(valueObj).ToString("#,##0") + this.MySett.GetYUnitName("(", ")");
            var arr = drv.Row.ItemArray
                .Where(p => !Lib.IsNOE(p) && Lib.NTE(valueObj) != Lib.NTE(p))
                .Select(p => Lib.NTE(p)).ToArray();

            e.LegendText = Lib.ParseCollection2Str(arr, " | ") + " : " + valueStr;
        }
        protected void chart_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            //Session["pointSel"] = new ChartSeriesPointSelection(1, e.AdditionalObject, e.Object);
            //GC.Collect();
        }
        protected void chart_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Parameter))
                {
                    var filters = Lib.FromJsonStr<List<WidgetFilter>>(e.Parameter);
                    this.Sett.WidgetFiltersRuntime = filters;
                    this.InitCustom_Charts();
                }
            }
            catch { }
        }
    }
}