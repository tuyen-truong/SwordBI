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
    public partial class wcSubMenu_bak : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Chỉ ghi menu sub cho lần đầu tiên tải về client
            // Nếu trang bị gọi lại do Ajax sau khi được tải về client thì không ghi lại menu sub
            if (!Page.IsPostBack)
            {
                var parentCode = GlobalVar.CurMainMnuCode;
                if (string.IsNullOrEmpty(parentCode)) return;
                this.LoadSubMenu(parentCode);

                if (GlobalVar.FirstURL_IsRequested) return;                
                GlobalVar.FirstURL_IsRequested = true;
                if (string.IsNullOrEmpty(GlobalVar.FirstURL)) return;
                Response.Redirect(GlobalVar.FirstURL);
            }
        }
        public string LoadSubMenu(string parentCode)
        {
            try
            {
                var parentObj = MySys.Me.Get_Menu(parentCode);
                var rolePath = string.Format("{0}\\App\\{1}", GlobalVar.RootPath, parentObj.Url);
                if (!Directory.Exists(rolePath)) throw new Exception(string.Format("Directory {0} is not exists!", parentObj.Url));

                var lsSub = MySys.Me.Get_MenuChild(parentCode).ToList();
                var culName = LanguageManager.CurrentCulture.Name;
                var isUS = culName.Equals("en-US");

                // Duyệt qua tất cả các chức năng cha (group)
                var mnSubItems = new List<DevExpress.Web.ASPxMenu.MenuItem>();
                foreach (var sub in lsSub)
                {
                    var url = string.Format(@"/App/{0}/{1}.aspx?submnucode={2}", parentObj.Url, parentObj.Url, sub.Code);
                    var caption = isUS ? sub.NameEN : sub.NameVI;
                    var mnItem = Helpers.CreateMenuObject(caption, sub.Code, url);
                    //mnItem.Selected = sub.IsDefault;
                    mnSubItems.Add(mnItem);
                }
                this.mnuSubH.Items.AddRange(mnSubItems);

                // Lưu url mặc định vào session khi tạo mới 1 submenu từ một parent menu.
                // Áp dụng cho menu chỉ có 2 cấp.                
                //var firstItem = mnSubItems.FirstOrDefault();
                DevExpress.Web.ASPxMenu.MenuItem firstItem = null;
                var defaultSubItem = lsSub.FirstOrDefault(p => p.IsDefault);
                if (defaultSubItem != null)
                    firstItem = mnSubItems.FirstOrDefault(p => p.Name == defaultSubItem.Code);

                //var firstItem = mnSubItems.FirstOrDefault(p => p.Selected);
                if (firstItem != null)
                    GlobalVar.FirstURL = firstItem.NavigateUrl;
                // Hủy các đối tượng cũ không còn dc tham khảo tới
                GC.Collect();
            }
            catch (Exception ex)
            {
                GlobalVar.FirstURL = string.Empty;
                return ex.Message;
            }
            return string.Empty;
        }
    }
}