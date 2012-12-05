using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;

namespace HTLBIWebApp2012
{
    public class PageBase : Page
    {
        public const string SESSION_KEY_LANGUAGE = "CURRENT_LANGUAGE";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                this.Init_SqlDataSource();
                // Lưu vào session những thông tin về menu đang chọn.
                if (Context == null || Context.Session == null) return;
                if (this.Has_Param_MainMenuCode())
                {
                    var curMainCode = this.Get_Param_MainMenuCode();
                    if (curMainCode == GlobalVar.CurMainMnuCode) return;
                    GlobalVar.CurMainMnuCode = curMainCode;
                    GlobalVar.FirstURL_IsRequested = false;
                }
                if (this.Has_Param_SubMenuCode())
                {
                    var curSubCode = this.Get_Param_SubMenuCode();
                    if (curSubCode == GlobalVar.CurSubMnuCode) return;
                    GlobalVar.CurSubMnuCode = curSubCode;
                }
            }
            catch { }
        }
        protected override void InitializeCulture()
        {
            base.InitializeCulture();


            //If you would like to have DefaultLanguage changes to effect all users,
            // or when the session expires, the DefaultLanguage will be chosen, do this:
            // (better put in somewhere more GLOBAL so it will be called once)
            //LanguageManager.DefaultCulture = ...

            //Change language setting to user-chosen one
            if (Session[SESSION_KEY_LANGUAGE] != null)
            {
                ApplyNewLanguage((CultureInfo)Session[SESSION_KEY_LANGUAGE]);
            }
            else
                ApplyNewLanguage(LanguageManager.DefaultCulture);

            //if (Session["MyTheme"] == null)
            //{
            //    Session.Add("MyTheme", "Organe");
            //    Page.Theme = ((string)Session["MyTheme"]);
            //}
            //else
            //{
            //    Page.Theme = ((string)Session["MyTheme"]);
            //}
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
                this.Load_InitData();
            else
                this.Load_OnPostPack();
        }

        private void ApplyNewLanguage(CultureInfo culture)
        {
            LanguageManager.CurrentCulture = culture;
            //Keep current language in session
            Session.Add(SESSION_KEY_LANGUAGE, LanguageManager.CurrentCulture);
        }
        protected void ApplyNewLanguageAndRefreshPage(CultureInfo culture)
        {
            ApplyNewLanguage(culture);
            //Refresh the current page to make all control-texts take effect
            Response.Redirect(Request.Url.AbsoluteUri);


        }

        protected bool Has_Param_QueryString()
        {
            return Request.QueryString != null && Request.QueryString.Count > 0;
        }
        protected bool Has_Param(string paramName)
        {
            return this.Has_Param_QueryString() && Request.QueryString.AllKeys.Contains(paramName);
        }
        protected string Get_Param(string paramName)
        {
            return this.Has_Param(paramName) ? Request.QueryString[paramName] : "";
        }
        protected bool Has_Param_MainMenuCode()
        {
            return this.Has_Param("mmnucode");
        }
        protected string Get_Param_MainMenuCode()
        {
            return Get_Param("mmnucode");
        }
        protected bool Has_Param_SubMenuCode()
        {
            return this.Has_Param("submnucode");
        }
        protected string Get_Param_SubMenuCode()
        {
            return Get_Param("submnucode");
        }

        public virtual bool RegisterStartupScript(Control updatePanel, string javaScriptCode)
        {
            try
            {
                if (string.IsNullOrEmpty(javaScriptCode)) return true;
                if (updatePanel == null) return true;
                ScriptManager.RegisterStartupScript(updatePanel, typeof(string), string.Format("{0}_Script", this.GetType().Name), javaScriptCode, true);
                return true;
            }
            catch { }
            return false;
        }
        public virtual bool RegisterStartupScript(string javaScriptCode)
        {
            try
            {
                var udp = this.Page.Controls.At("ctl00")
                                   .Controls.At("aspnetForm")
                                   .Controls.At("ctl00$MainContent")
                                   .Controls.At("ctl00$MainContent$UpdatePanel1");
                return this.RegisterStartupScript(udp, javaScriptCode);
            }
            catch { return false; }
        }
        public virtual void Init_SqlDataSource() { }
        public virtual void Load_InitData() { }
        public virtual void Load_OnPostPack() { }

        public event EventHandler OnChange;
        public void Raise_OnChange(object sender, EventArgs e)
        {
            if (this.OnChange != null)
                this.OnChange(sender, e);
        }
    }
}