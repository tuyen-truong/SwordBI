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
        protected wcLayoutSetting CtrlLayoutSetting;

        protected void Page_Init(object sender, EventArgs e)
        {
            CtrlLayoutSetting = LoadControl("wcLayoutSetting.ascx") as wcLayoutSetting;
            LayoutSettingHolder.Controls.Add(CtrlLayoutSetting);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                CtrlLayoutSetting.WHCode = Get_Param(PageArgs.WHCode);
                CtrlLayoutSetting.LayoutCode = Get_Param(PageArgs.LayoutCode);
            }
        }
    }
}