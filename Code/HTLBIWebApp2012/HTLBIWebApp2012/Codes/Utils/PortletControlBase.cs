using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web.ASPxPopupControl;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.Codes.Utils
{
    public class PortletControlBase : UserControl
    {
        protected ASPxPopupControl m_portletPicker;

        public PortletControlBase()
            : base()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_portletPicker = new ASPxPopupControl()
            {
                ID = "PorletPicker",
                ClientInstanceName = this.ClientID,
                Modal = true,
                PopupHorizontalAlign = DevExpress.Web.ASPxClasses.PopupHorizontalAlign.WindowCenter,
                PopupVerticalAlign = DevExpress.Web.ASPxClasses.PopupVerticalAlign.WindowCenter,
                CloseAction = DevExpress.Web.ASPxClasses.CloseAction.CloseButton,
                HeaderText = "Cadidate Portlets",
                AllowDragging = true
            };
            m_portletPicker.ContentStyle.Paddings.Padding = Unit.Pixel(5);
            PopupControlContentControl p = new PopupControlContentControl();
        }
    }
}