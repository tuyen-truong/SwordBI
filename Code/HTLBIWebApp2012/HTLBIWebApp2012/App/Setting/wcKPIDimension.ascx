<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcKPIDimension.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcKPIDimension" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<table>
    <tr>
        <td>Dimension</td>
        <td>
            <dx:ASPxComboBox ID="cboField" ClientInstanceName="KPIDim_cboField" runat="server" Width="150px" ToolTip="Dimension field"
                AutoPostBack="true" OnValueChanged="ctrl_ValueChanged" />
        </td>
        <td>
            <dx:ASPxTextBox ID="txtDisplayName" runat="server" Width="150px" ToolTip="Display name" />
        </td>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnRemove" runat="server" Text="X" Width="20px"
                ToolTip="Remove this dimension" onclick="Remove_Click">
            </dx:ASPxButton> 
        </td>
    </tr>
</table>