
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