using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class Setting : PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var subCode = this.Get_Param_SubMenuCode();
            if (!string.IsNullOrEmpty(subCode))
            {
                var objMnu = MySys.Me.Get_Menu(subCode);
                if (objMnu == null) return;
                Response.Redirect(string.Format("{0}.aspx?whcode={1}", objMnu.Url, this.Get_Param("whcode")));
            }
        }
    }
}