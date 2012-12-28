<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPortletPicker.ascx.cs" Inherits="HTLBIWebApp2012.Shared.UserControl.wcPortletPicker" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:PlaceHolder runat="server" ID="PortletPickerPlaceHolder">

<div style="padding-right: 30px">
    <dx:ASPxListBox runat="server" ID="m_portletCandidate" Width="100%"></dx:ASPxListBox>
    <dx:ASPxButton runat="server" ID="btnShowModal" AutoPostBack="false" Text="Add Portlet">
        <ClientSideEvents Click="function(s, e) { 
                var pn = s.name.replace('btnShowModal', '');
                
                ShowModalPopup(pn + 'PopupPicker'); 
            }" />
    </dx:ASPxButton>
</div>
<dx:ASPxPopupControl runat="server" ID="PopupPicker" CloseAction="CloseButton" Modal="true"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    HeaderText="Available Portlets" AllowDragging="true" EnableAnimation="false" EnableViewState="false">
    <ClientSideEvents PopUp="function(s, e){}" />
    <ContentStyle>
        <Paddings Padding="5px" />
    </ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxPanel runat="server" ID="Panel1">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table width="100%">
                            <tr>
                                <td><dx:ASPxListBox runat="server" ID="AvailablePortlet" Width="100%"></dx:ASPxListBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxButton runat="server" ID="btnOK" Text="OK" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                                            var popupname = s.name.replace('_Panel1_btnOK', '');
                                            var AvailablePortlet = window[popupname + '_Panel1_AvailablePortlet'];
                                            var selecteditem = AvailablePortlet.GetSelectedItem();
                                            if (selecteditem)
                                            {
                                                var n = popupname.replace('PopupPicker', '');
                                                //console.log(eval(n + 'm_portletCandidate');
                                                var CandidatePortlet = window[n + 'm_portletCandidate'];
                                                if( CandidatePortlet ) {
                                                    CandidatePortlet.ClearItems();
                                                    CandidatePortlet.AddItem(selecteditem.text, selecteditem.value);
                                                    var el = document.getElementById('selectedPortlet');
                                                    if (el) {
                                                        if (el.value.length == 0) {
                                                            el.value = selecteditem.value;
                                                        }
                                                        else {
                                                            el.value += ',';
                                                            el.value += selecteditem.value;
                                                        }
                                                    }
                                                }
                                            }
                                            window[popupname].Hide(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
</asp:PlaceHolder>
