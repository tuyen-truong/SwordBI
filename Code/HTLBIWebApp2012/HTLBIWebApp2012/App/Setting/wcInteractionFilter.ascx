<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcInteractionFilter.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcInteractionFilter" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>

<table>
    <tr>
        <td>Control</td>
        <td>
            <dx:ASPxComboBox ID="cbbControl" ClientInstanceName="InteractionFilter_cbbControl" 
                runat="server" Width="150px" ToolTip="Control filter." 
                AutoPostBack="true" OnValueChanged="cbo_ValueChanged">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbSourceField" ClientInstanceName="InteractionFilter_cbbSourceField" 
                runat="server" Width="180px" ToolTip="Source field.">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtCaption" runat="server" Width="150px" ToolTip="Caption."></dx:ASPxTextBox>
        </td>
        <td>
            <dx:ASPxCheckBox ID="chkEnableRange" ClientInstanceName="InteractionFilter_chkEnableRange"
                runat="server" Text="Enable Range">
            </dx:ASPxCheckBox>
        </td>
        <td>
            <dx:ASPxButton ID="btnDelFilter" runat="server" Text="X" 
                ToolTip="Remove this filter" Width="20px" onclick="Remove_Click">
            </dx:ASPxButton>
        </td>
    </tr>
</table>