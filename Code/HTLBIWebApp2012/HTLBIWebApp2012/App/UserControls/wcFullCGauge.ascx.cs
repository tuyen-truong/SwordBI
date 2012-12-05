using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGauges.Gauges;
using DevExpress.Web.ASPxGauges.Gauges.Circular;
using DevExpress.Web.ASPxGauges.Base;
using DevExpress.Web.ASPxGauges;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
using System.Drawing;
using DevExpress.Web.ASPxEditors;
using System.IO;
using System.Text;
using DevExpress.XtraGauges.Core.Base;

namespace HTLBIWebApp2012.App.UserControls
{
    public partial class wcFullCGauge : GaugeCtrlBase
    {
        protected void myGauge_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            this.MySett.ReFilter_InqMDX();
            this.Lock_SetValue = true;
            this.myGauge.Value = "3500";
            this.CustomGauge();
            this.Lock_SetValue = false;
        }
    }
}