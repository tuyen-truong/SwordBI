using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.Shared
{
    public partial class FullMaster : System.Web.UI.MasterPage
    {
        private static string[] JavascriptLibs
        {
            get
            {
                return new string[] {
                    "~/Scripts/JQuery/jquery-1.7.2.min.js",
                    "~/Scripts/JQuery/autoNumeric-1.7.4.js",
                    "~/Scripts/JQuery/jshashtable-2.1.js",
                    "~/Scripts/JQuery/jquery.numberformatter-1.2.3.min.js",
                    "~/Scripts/JQuery/jquery.maskedinput-1.2.2.min.js",
                    "~/Scripts/JQuery/jquery-ui-1.8.2.custom.min.js",
                    "~/Scripts/JQuery/Json-ende.js",
                    "~/Scripts/common.js"
                };
            }
        }

        private static string[] CSS
        {
            get
            {
                return new string[] {
                    "~/Content/CSS/css.css",
                    "~/Content/CSS/css_002.css",
                    "~/Scripts/JQuery/themes/cupertino/jquery-ui-1.8.2.custom.css"
                };
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Controls.Add(new LiteralControl(@"<script type=""text/javascript"">var g_WebAppPath = """ + ResolveUrl("~") + @"""</script>"));
            foreach (string css in CSS)
            {
                Page.Header.Controls.Add(new LiteralControl(@"<link href=""" + Page.ResolveUrl(css) + @""" rel=""stylesheet"" type=""text/css""/>"));
            }
            foreach (string lib in JavascriptLibs)
            {
                Page.Header.Controls.Add(new LiteralControl("<script type=\"text/javascript\" src=\"" + Page.ResolveUrl(lib) + "\"></script>"));
            }
        }
    }
}