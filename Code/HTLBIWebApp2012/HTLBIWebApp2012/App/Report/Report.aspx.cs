using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Report
{
    public partial class Report : PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //var curMnuCode="dbrd_AR";
            //this.wcDashboardSetting1.MenuCode = curMnuCode;
            //this.wcDashboardSetting1.WHCode = MySys.Me.Get_Menu(curMnuCode).Url;            
        }
    }
}