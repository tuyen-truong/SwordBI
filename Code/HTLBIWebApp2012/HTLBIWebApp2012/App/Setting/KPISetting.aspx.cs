using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class KPISetting : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            wcKPISetting wcSetting = LoadControl("wcKPISetting.ascx") as wcKPISetting;
            KPIPlaceHolder.Controls.Add(wcSetting);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}