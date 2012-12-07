using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012
{
    /// <summary>
    /// Trang này làm nhiệm vụ điều hướng cho các trang asp khác trong ứng dụng hiện hành
    /// </summary>
    public partial class Index : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //var mnuObj = MySys.Me.Get_MenuRoot().FirstOrDefault(p => p.IsDefault);
                //var urlDefault = string.Format("/App/{0}/{0}.aspx?mmnucode={1}", mnuObj.Url, mnuObj.Code);
                //Response.Redirect(urlDefault);

                // Test data.........
                //var obj = GlobalSsn.Get_CalcField();
                //var expressionStr = obj.To_Expression();


                //var obj = GlobalSsn.Get_InqDefineSourceMDX();
                // Filter lại theo mục đích sử dụng.
                //obj.ReFilter_Fields(p => new[] { "ItemGroupName", "ItemName" }.Contains(p.ColName));
                //obj.ReFilter_Summaries(p => p.Field.ColName == "Quantity");
                //obj.ReFilter_Filters(new[] { new InqFilterInfoMDX(new InqFieldInfoMDX("DimItem", "ItemGroupName", "NTEXT"), "=", "NB") });
                //var a = obj.ToMDX(true);
                //var b = obj.ToMDX();
                ////


                //var cat = Request.QueryString["cat"];
                //var url = Request.QueryString["url"];
                //var isnullCat = string.IsNullOrWhiteSpace(cat);
                //var isnullUrl = string.IsNullOrWhiteSpace(url);
                //if (isnullCat && isnullUrl) return;
                //if (!isnullCat && !isnullUrl) // ứng với menu item
                //{
                //    var leftPart = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                //    var rightPart = string.Format("App/{0}/{1}.aspx", cat, url);
                //    if (Lib.Web.IsWebUrlExits(string.Format("{0}/{1}", leftPart, rightPart)))
                //        //Server.Transfer(string.Format("~/{0}", rightPart));
                //        Response.Redirect(string.Format("{0}/{1}", leftPart, rightPart));
                //}
                //else if (!isnullCat) // ứng với menu category ở ngoài cùng
                //{
                //    var leftPart = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                //    var rightPart = string.Format("App/{0}/{0}.aspx", cat);
                //    if (Lib.Web.IsWebUrlExits(string.Format("{0}/{1}", leftPart, rightPart)))
                //        //Server.Transfer(string.Format("~/{0}", rightPart));
                //        Response.Redirect(string.Format("{0}/{1}", leftPart, rightPart));
                //}
            }
            catch (Exception ex) { /*Response.Write(ex.Message);*/ }
        }
    }
}
