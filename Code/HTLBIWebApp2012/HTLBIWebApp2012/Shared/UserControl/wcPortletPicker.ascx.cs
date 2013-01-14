using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.Shared.UserControl
{
    public partial class wcPortletPicker : System.Web.UI.UserControl
    {
        public Unit Width = Unit.Percentage(100);
        public Unit Height = Unit.Pixel(100);

        protected void Page_Init()
        {
            String csname = "PortletPicker";
            Type cstype = this.GetType();
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsClientScriptBlockRegistered(cstype, csname))
            {
                csm.RegisterClientScriptBlock(cstype, csname, wcPortletPicker.PortletPickerScript);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(WHCode)) 
            {
                return;
            }
            Helpers.SetDataSource(AvailablePortlet, MyBI.Me.Get_Widget(WHCode), "Code", "Name");
            ClientScriptManager csm = Page.ClientScript;
            csm.RegisterStartupScript(this.GetType(), "BtnScript", "");
        }

        protected override void OnPreRender(EventArgs e)
        {
            m_portletCandidate.Width = this.Width;
            m_portletCandidate.Height = this.Height;
            
            btnShowModal.Style.Add(HtmlTextWriterStyle.Position, "relative");
            btnShowModal.Style.Add("float", "right");
            btnShowModal.Style.Add("right", "-20px");
            btnShowModal.Style.Add("top", string.Format("-{0}px", this.Height.Value + 10));

            base.OnPreRender(e);
            m_portletCandidate.ClientInstanceName = m_portletCandidate.ClientID;
        }

        public string WHCode { get; set; }
        private ListEditItem _selectedItem = null;
        public ListEditItem SelectedItem
        {
            get
            {
                _selectedItem = this.m_portletCandidate.SelectedItem;
                if (_selectedItem == null
                    && this.m_portletCandidate.Items.Count > 0)
                {
                    _selectedItem = this.m_portletCandidate.Items[0];
                }
                return _selectedItem;
            }
        }

        public ListEditItemCollection Items { get { return m_portletCandidate.Items; } }

        public int SelectedIndex
        {
            get { return this.m_portletCandidate.SelectedIndex; }
            set { this.m_portletCandidate.SelectedIndex = value; }
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