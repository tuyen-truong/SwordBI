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
        private string WGCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsAsync) { return; }
            WGCode = Get_Param(PageArgs.WidgetCode);
            if (string.IsNullOrEmpty(WGCode))
            {
                // TODO: add new
            }
            else
            {
                MyBI.Me.Get_Widget().FirstOrDefault(wg => wg.ID == Int32.Parse(WGCode));
            }
        }
    }
}