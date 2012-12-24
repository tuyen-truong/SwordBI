using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class DatasourceSetting : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            wcDatasourceSetting wcDSSetting = LoadControl("wcDatasourceSetting.ascx") as wcDatasourceSetting;
            DatasourcePlaceHolder.Controls.Add(wcDSSetting);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}