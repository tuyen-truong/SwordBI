<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcSwitchDashboard.ascx.cs" Inherits="HTLBIWebApp2012.App.Dashboard.wcSwitchDashboard" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!-- DASHBOARD LIST -->
<table>
    <tr>
        <td>Switch to dashboard:</td>
        <td>                
            <dx:ASPxComboBox ID="cboDashboard" ClientInstanceName="DasboardMaster_cboDashboard" 
                runat="server" AutoPostBack="true" OnValueChanged="cboDashboard_ValueChanged">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxButton ID="btnSetDefaultDashboard" runat="server" Text="Set default" 
                AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                    var valStr = DasboardMaster_cboDashboard.GetValue().toString();
                    callback_SetDefaultDashboard.PerformCallback(valStr);
                }" />
            </dx:ASPxButton>
            <dx:ASPxCallback ID="callback_SetDefaultDashboard" 
                ClientInstanceName="callback_SetDefaultDashboard" runat="server" 
                oncallback="callback_SetDefaultDashboard_Callback">
                <ClientSideEvents CallbackComplete="function(s,e){
                    var valStr = DasboardMaster_cboDashboard.GetValue().toString();
                    alert('The dashboard [' + valStr + '] has been set default!');
                }" />
            </dx:ASPxCallback>
        </td>
        <td>&nbsp;&nbsp;|&nbsp;</td>
        <td>
            <asp:Label ID="lblGotoDashboardSetting" runat="server" />
        </td>            
    </tr>
</table>