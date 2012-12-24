using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class LayoutSetting : PageBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            wcLayoutSetting wc = LoadControl("wcLayoutSetting.ascx") as wcLayoutSetting;
            if (!IsPostBack)
            {
                wc.WHCode = Get_Param(PageArgs.WHCode);
                wc.LayoutCode = Get_Param(PageArgs.LayoutCode);
            }
            LayoutSettingHolder.Controls.Add(wc);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}