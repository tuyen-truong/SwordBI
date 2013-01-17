using System;
using DevExpress.Web.ASPxEditors;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                //string errReturn = OLAPConnector.TryConnect(this.ASPxPivotGrid1, OLAPConnector.OLAPConnectionString, "ARCube");
            }
            catch (Exception ex) { /*Response.Write(ex.Message);*/ }
            

           //DisableDefaultButton(this.Page);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //ASPxTextBox t = new ASPxTextBox();
            TextBox t = new TextBox();
            t.AutoPostBack = false;
            PlaceHolder1.Controls.Add(t);

            ASPxButton bt = new ASPxButton();
            bt.ID = "btTest";
            bt.Text = "ASPxButton";
            bt.Click += new EventHandler(EventHandler1);
            bt.UseSubmitBehavior = false;

            PlaceHolder1.Controls.Add(bt);

            Button bt1 = new Button();
            bt1.Text = "Button";
            bt1.UseSubmitBehavior = false;
            bt1.Click += new EventHandler(EventHandler1);
            PlaceHolder1.Controls.Add(bt1);
        }

        void DisableDefaultButton(Control c)
        {
            if (c.Controls.Count == 0) { return; }
            foreach (Control c1 in c.Controls)
            {
                if (c1 is Button
                    || c1 is ASPxButton)
                {
                    if (c1 is Button) { (c1 as Button).UseSubmitBehavior = false; }
                    if (c1 is ASPxButton) { (c1 as ASPxButton).UseSubmitBehavior = false; }
                }
                else
                {
                    DisableDefaultButton(c1);
                }
            }
        }

        protected void EventHandler1(object sender, EventArgs e)
        {
            string str = string.Empty;
        }
    }
}
