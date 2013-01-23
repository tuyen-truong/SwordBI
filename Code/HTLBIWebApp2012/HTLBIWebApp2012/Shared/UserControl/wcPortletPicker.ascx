<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPortletPicker.ascx.cs" Inherits="HTLBIWebApp2012.Shared.UserControl.wcPortletPicker" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

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
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="450px"
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
                                <td><dx:ASPxListBox runat="server" ID="AvailablePortlet" Width="100%" Height="300px" ></dx:ASPxListBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="float:left">
									<dx:ASPxButton runat="server" ID="btnOK" Text="<%$ Resources:BI, Button_btnSelect %>" AutoPostBack="false" Width="100px">
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
                                                    CandidatePortlet.SetSelectedIndex(0);
                                                }
                                            }
                                            window[popupname].Hide(); }" />
                                    </dx:ASPxButton>
									</span>
									<span style="float:left;margin-left:4px;">
										<dx:ASPxButton ID="btnNew" runat="server" 
										Text="<%$ Resources:BI, Button_btnNew %>" Width="100px" 
										OnClick="btnNew_Click"  />
									</span>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
