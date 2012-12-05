using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using System.Web.Security;
using HTLBIWebApp2012.Codes.BLL;
using System.IO;

namespace HTLBIWebApp2012.Shared.UserControl
{
    public partial class wcSubMenu : System.Web.UI.UserControl
    {
        public string CurTabID { get { return string.Format("a_{0}", GlobalVar.CurSubMnuCode); } }
        public string WriteMenuToHTML()
        {
            try
            {
                var parentCode = GlobalVar.CurMainMnuCode;
                if (string.IsNullOrEmpty(parentCode)) return "";
                var parentObj = MySys.Me.Get_Menu(parentCode);
                var lsSub = MySys.Me.Get_MenuChild(parentCode).ToList();
                var ret = "";
                var count = lsSub.Count;
                for (int i = 0; i < count; i++)
                {
                    var item = lsSub[i];
                    var txt = item.NameEN;
                    var a_id = string.Format("a_{0}", item.Code);
                    var a_href = string.Format("/App/{0}/{0}.aspx?submnucode={1}", parentObj.Url, item.Code);
                    ret += "<a id=\"" + a_id + "\" href=\"" + a_href + "\" >" + txt + "</a>\r\n";
                    if (i < count - 1) ret += " | ";
                }
                // Lưu mã của menu sẽ mặc định chọn đầu tiên, theo thiết lập lưu trong table systbl_Menu ở database quản lý.
                var firstDefault = lsSub.FirstOrDefault(p => p.IsDefault);
                if (firstDefault != null)
                    GlobalVar.FirstURL = string.Format("/App/{0}/{0}.aspx?submnucode={1}", parentObj.Url, firstDefault.Code);
                else
                    GlobalVar.FirstURL = string.Empty;
                return ret;
            }
            catch { }
            return "";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                this.lblSubMenu.Text = this.WriteMenuToHTML();
                if (GlobalVar.FirstURL_IsRequested) return;
                GlobalVar.FirstURL_IsRequested = true;
                if (string.IsNullOrEmpty(GlobalVar.FirstURL)) return;
                Response.Redirect(GlobalVar.FirstURL);
            }
        }
    }
}