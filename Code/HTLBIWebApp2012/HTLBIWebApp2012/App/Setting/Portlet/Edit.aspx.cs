using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class PortletEdit : PageBase
    {
        private string PortletId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsAsync) { return; }
            PortletId = Get_Param(PageArgs.PortletId);
            if (string.IsNullOrEmpty(PortletId))
            {
                // TODO: add new
            }
            else
            {
                MyBI.Me.Get_Widget().FirstOrDefault(wg => wg.ID == Int32.Parse(PortletId));
            }
        }
    }
}