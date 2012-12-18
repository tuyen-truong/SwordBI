<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPortletPicker.ascx.cs" Inherits="HTLBIWebApp2012.Shared.UserControl.wcPortletPicker" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:PlaceHolder runat="server" ID="PortletPickerPlaceHolder">
<script type="text/javascript">
// <![CDATA[
    function ShowModalPopup() {
        PopupPicker.Show();
    }

    function PopupPicker_btnOK_Click() {
        alert(AvailablePortlet);
    }
// ]]
</script>
<style type="text/css">
.htl-button-showmodal
{
    right: -20px;
    top: -10px;
    position:absolute;
}
</style>
<div style="position:absolute;">
    <dx:ASPxListBox runat="server" ID="m_portletCandidate" Width="100%" ClientInstanceName="CandidatePortlet"></dx:ASPxListBox>
    <dx:ASPxButton runat="server" ID="btnShowModal" AutoPostBack="false" Text="Add Portlet" CssClass="htl-button-showmodal">
        <ClientSideEvents Click="function(s, e) { ShowModalPopup(); }" />
    </dx:ASPxButton>
</div>
<dx:ASPxPopupControl runat="server" ID="PopupPicker" CloseAction="CloseButton" Modal="true"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopupPicker"
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
                                <td><dx:ASPxListBox runat="server" ID="AvailablePortlet" Width="100%" ClientInstanceName="AvailablePortlet"></dx:ASPxListBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxButton runat="server" ID="btnOK" Text="OK" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) { 
                                            var item = AvailablePortlet.GetSelectedItem();
                                            if (item)
                                            {
                                                CandidatePortlet.ClearItems();
                                                CandidatePortlet.AddItem(item.text, item.value);
                                            }
                                            PopupPicker.Hide(); }" />
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
