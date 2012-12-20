using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;
using CECOM;

namespace HTLBIWebApp2012.App.Dashboard
{
    public partial class wcSwitchDashboard : PartPlugCtrlBase
    {
        public PageBase MyPage { get { return this.Page as PageBase; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // tải danh sách các dashboard theo whCode hiện hành (chính là url ứng với submnuCode).
                var subMnuCode = GlobalVar.CurSubMnuCode;
                if (!string.IsNullOrEmpty(subMnuCode))
                {
                    var objMnuSub = MySys.Me.Get_Menu(subMnuCode);
                    if (objMnuSub == null) return;
                    var whcode = objMnuSub.Url;
                    var ds = MyBI.Me.Get_Dashboard(whcode);

                    // Nếu là lần đầu tiên được gọi thì sẽ tải dashboard đã set mặc định (nếu có)
                    if (!this.IsPostBack)
                    {
                        var dbrdDefault = ds.FirstOrDefault(p => p.IsDefault);
                        if (dbrdDefault != null)
                        {
                            Helpers.SetDataSource(this.cboDashboard, ds.ToList(), "Code", "Name", dbrdDefault.Code);
                            this.MyPage.Raise_OnChange(new { WcCtrl = this, Ctrl = sender }, new HTLBIEventArgs(dbrdDefault.Code));
                        }
                        else
                            Helpers.SetDataSource(this.cboDashboard, ds.ToList(), "Code", "Name", this.cboDashboard.Value);
                    }
                    else
                        Helpers.SetDataSource(this.cboDashboard, ds.ToList(), "Code", "Name", this.cboDashboard.Value);

                    // Tạo link đến trang setting cho các dashboard.
                    var url = string.Format("/App/Setting/Setting.aspx?mmnucode=sett&submnucode=sett_Dashboard&whcode={0}", objMnuSub.Url);
                    this.lblGotoDashboardSetting.Text = "<a href=\"" + url + "\">Go to dashboard setting<a/>";
                }
            }
            catch { }
        }
        protected void cboDashboard_ValueChanged(object sender, EventArgs e)
        {
            this.MyPage.Raise_OnChange(new { WcCtrl = this, Ctrl = sender }, new HTLBIEventArgs(this.cboDashboard.Value));
        }
        protected void callback_SetDefaultDashboard_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.Parameter)) return;
                    MyBI.Me.Set_DashboardDefault(e.Parameter);
            }
            catch { }
        }
    }
}