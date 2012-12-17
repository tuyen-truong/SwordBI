using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.Web.ASPxEditors;

namespace HTLBIWebApp2012.Shared.UserControl
{
    public partial class wcPortletPicker : System.Web.UI.UserControl
    {
        protected void Page_Init()
        {
            m_portletCandidate.Width = 200;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(WHCode)) 
            {
#if DEBUG
                throw new Exception("Invalid WHCode"); 
#else
                return;
#endif
            }
            Helpers.SetDataSource(AvailablePortlet, MyBI.Me.Get_Widget(WHCode), "Code", "Name");
        }

        public string WHCode { get; set; }
        public ListEditItem SelectedItem
        {
            get
            {
                return this.m_portletCandidate.SelectedItem;
            }
        }
    }
}