<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPropGauge_StateRange.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcPropGauge_StateRange" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<style type="text/css">

.dxeBase
{
    font-family: Tahoma;
    font-size: 9pt;
}
</style>

<table>
    <tr>
        <td>From</td>
        <td>
            <asp:TextBox ID="txtFromValue" runat="server" Width="120px" 
                AutoPostBack="true" OnTextChanged="ctrl_ValueChanged"
                ToolTip="Number value" CssClass="numericInput" Text="0" />
        </td>
        <td>To</td>
        <td>
            <asp:TextBox ID="txtToValue" runat="server" Width="120px" 
                AutoPostBack="true" OnTextChanged="ctrl_ValueChanged"
                ToolTip="Number value" CssClass="numericInput" Text="0" />
        </td>
        <td>Color</td>
        <td>
            <dx:ASPxColorEdit ID="cboColor" runat="server" Width="100px" AutoPostBack="true"
                OnValueChanged="ctrl_ValueChanged" />
        </td>
        <td>Description</td>
        <td>
            <dx:ASPxTextBox ID="txtDes" runat="server" Width="120px"></dx:ASPxTextBox>
        </td>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnRemove" runat="server" Text="X" Width="20px"
                ToolTip="Remove this state range" onclick="Remove_Click">
            </dx:ASPxButton> 
        </td>
    </tr>
</table>