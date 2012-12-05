using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace HTLBIWebApp2012.Account
{
    public partial class BILogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtUserName.Focus();
            if (IsPostBack)
            {
                string userName = this.txtUserName.Text;
                string password = this.txtPassword.Text;

                // Validate again
                try
                {
                    if (Membership.ValidateUser(userName, password))
                    {
                        // Phải có lệnh này để lưu lại thông tin đăng nhập hiện hành vào cookie, 
                        // cung cấp thông tin cho LoginView, LoginStatus...
                        FormsAuthentication.SetAuthCookie(userName, true);
                        if (this.RememberMe.Checked)
                        {
                            Response.Cookies["RememberMe"]["userName"] = userName;
                            Response.Cookies["RememberMe"]["check"] = "true";
                            Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(2d);
                        }
                        else
                            Response.Cookies["RememberMe"].Value = null;

                        Response.Redirect("Analysis.aspx");
                    }
                }
                catch { }
            }
        }

        #region Property of RememberMe option
        public string RCheck
        {
            get
            {
                if (Request.Cookies["RememberMe"] != null && Request.Cookies["RememberMe"]["check"] != null)
                    return "true";
                else
                    return "false";
            }
        }
        public string RUserName
        {
            get
            {
                if (Request.Cookies["RememberMe"] != null && Request.Cookies["RememberMe"]["userName"] != null)
                    return Request.Cookies["RememberMe"]["userName"];
                else
                    return string.Empty;
            }
        }
        public string RPassword
        {
            get
            {
                return !string.IsNullOrEmpty(this.RUserName) ? Membership.GetUser(this.RUserName).GetPassword() : string.Empty;
            }
        }
        #endregion
    }
}