<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcKPIContextMetric_Calc.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcKPIContextMetric_Calc" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<table>
    <tr>
        <td>Context</td>
        <td>
            <dx:ASPxTextBox ID="txtField" runat="server" Width="180px" ToolTip="Field identity name" />
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
            <div id="ctrl_CalcFields" runat="server"></div>
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <dx:ASPxButton ID="btnAddCalcField" runat="server" Text="Add calc field" AutoPostBack="False"/>
            <dx:ASPxPopupMenu ID="popMen" runat="server" PopupElementID="btnAddCalcField" 
                onitemclick="popMen_ItemClick" PopupAction="LeftMouseClick">
                <Items>
                    <dx:MenuItem Text="Member1:Field, Member2:Number" Name="FieldAndNum"></dx:MenuItem>
                    <dx:MenuItem Text="Member1:Field, Member2:Field" Name="FieldAndField"></dx:MenuItem>
                    <dx:MenuItem Text="Member1:Number, Member2:Field" Name="NumAndField"></dx:MenuItem>
                    <dx:MenuItem Text="Member1:Number, Member2:Number" Name="NumAndNum"></dx:MenuItem>
                </Items>
            </dx:ASPxPopupMenu>
        </td>
    </tr>  
    <tr>
        <td colspan="7"><hr /></td>
    </tr> 
</table>