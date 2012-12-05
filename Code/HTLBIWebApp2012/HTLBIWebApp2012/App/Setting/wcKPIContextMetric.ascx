<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcKPIContextMetric.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcKPIContextMetric" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<table>
    <tr>
        <td>Context</td>
        <td>
            <dx:ASPxComboBox ID="cboField" ClientInstanceName="KPIContext_cboField" runat="server" Width="180px" ToolTip="Context measure field"
                AutoPostBack="true" OnValueChanged="ctrl_ValueChanged" />
        </td>
        <td>
            <dx:ASPxTextBox ID="txtDisplayName" runat="server" Width="180px" ToolTip="Display name" />
        </td>
        <td>Aggregator</td>
        <td>
            <dx:ASPxComboBox ID="cboAggregator" runat="server" Width="70px" />
        </td>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnRemove" runat="server" Text="X" Width="20px"
                ToolTip="Remove this context metric" onclick="Remove_Click">
            </dx:ASPxButton> 
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <div id="ctrl_Filters" runat="server"></div>
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <dx:ASPxButton ID="btnAddFilter" runat="server" Text="Add filter" AutoPostBack="False"/>
            <dx:ASPxPopupMenu ID="popMen" runat="server" PopupElementID="btnAddFilter" 
                onitemclick="popMen_ItemClick" PopupAction="LeftMouseClick">
                <Items>
                    <dx:MenuItem Text="Normal" Name="NORMAL"></dx:MenuItem>
                    <dx:MenuItem Text="Numeric" Name="NUM"></dx:MenuItem>
                    <dx:MenuItem Text="Time" Name="DATE"></dx:MenuItem>
                    <dx:MenuItem Text="Prev Time" Name="PREVDATE"></dx:MenuItem>
                </Items>
            </dx:ASPxPopupMenu>
        </td>
    </tr>  
    <tr>
        <td colspan="7"><hr /></td>
    </tr> 
</table>