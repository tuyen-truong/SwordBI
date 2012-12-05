using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class DashboardSetting : PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                var whCode = this.Get_Param("whcode");
                if (!string.IsNullOrEmpty(whCode))
                {
                    this.wcDashboardSetting1.WHCode = whCode;
                }
            }
            catch { }
        }
    }
}