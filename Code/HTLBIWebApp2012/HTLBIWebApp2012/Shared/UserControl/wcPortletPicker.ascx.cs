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

            String csname = "PortletPicker";
            Type cstype = this.GetType();
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptBlockRegistered(csname))
            {
                csm.RegisterClientScriptBlock(cstype, csname, wcPortletPicker.PortletPickerScript);
            }
            //csm.RegisterStartupScript()
            m_portletCandidate.ClientInstanceName = m_portletCandidate.ClientID;
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

        public static string PortletPickerScript
        {
            get
            {
                return @"
<script type=""text/javascript"">
    function ShowModalPopup(name) {
        var popup = window[name];
        if( popup ) {
            popup.Show();
        }
        //PopupPicker.Show();
    }

    function PopupPicker_btnOK_Click() {
        alert(AvailablePortlet);
    }
</script>
";
            }
        }
    }
}