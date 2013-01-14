﻿using System;
using System.Linq;
using System.Web.UI;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.Shared.UserControl
{
    public partial class wcMainMenu : System.Web.UI.UserControl
    {
        public string CurTabID { get { return string.Format("li_{0}", GlobalVar.CurMainMnuCode); } }
        public string ImagesFolder
        {
            get
            {
                return Page.ResolveUrl("~/Content/Images");
            }
        }
        public string WriteMenuToHTML()
        {
            // Cần lấy theo UserMenu của user hiện hành...
            var lstMnuObj = MySys.Me.Get_Menu().Where(p => p.ParentCode == null || p.ParentCode == "").ToArray();
            var ret = "";
            foreach (var item in lstMnuObj)
            {
                var li_idname = string.Format("li_{0}", item.Code);
                var a_id = string.Format("a_{0}", item.Code);
                var a_href = ResolveUrl(String.Format("~/App/{0}/{0}.aspx?mmnucode={1}",item.Url, item.Code));
                ret +=
                "<li name=\"" + li_idname + "\" id=\"" + li_idname + "\">" +
                    "<a id=\"" + a_id + "\" href=\"" + a_href + "\">" +
                        "<span>" + item.NameEN + "</span>" +
                    "</a>" +
                "</li>\r\n";
            }
            return "<ul>\r\n" + ret + "</ul>";
        }
    }
}