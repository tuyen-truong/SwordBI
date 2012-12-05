<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcKPIMeasure.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcKPIMeasure" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<table>
    <tr>
        <td>Measure</td>
        <td>
            <dx:ASPxComboBox ID="cboField" ClientInstanceName="KPIMeasure_cboField" runat="server" Width="150px" ToolTip="Measure field"
                AutoPostBack="true" OnValueChanged="ctrl_ValueChanged" />
        </td>
        <td>
            <dx:ASPxTextBox ID="txtDisplayName" runat="server" Width="150px" ToolTip="Display name" />
        </td>
        <td>Aggregator</td>
        <td>
            <dx:ASPxComboBox ID="cboAggregator" runat="server" Width="70px" />
        </td>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnRemove" runat="server" Text="X" Width="20px"
                ToolTip="Remove this measure" onclick="Remove_Click">
            </dx:ASPxButton> 
        </td>
    </tr>
</table>