<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcTimeFilter.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcTimeFilter" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<table>
    <tr>
        <td>
            <dx:ASPxComboBox ID="cbbKeyField" ClientInstanceName="TimeFilter_cbbKeyField" runat="server" Width="120px" ToolTip="Key filter"
               onvaluechanged="cbbKeyField_ValueChanged" AutoPostBack="true">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbOperator" runat="server" Width="50px" HorizontalAlign="Center" ToolTip="Operator">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxDateEdit ID="txtValue" runat="server" Width="146px" 
                DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" 
                ToolTip="Date value, format by: dd/mm/yyyy">
            </dx:ASPxDateEdit>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbAndOr" runat="server" Width="70px" ToolTip="Logic combine">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxButton ID="btnDelFilter" runat="server" Text="X" 
                ToolTip="Remove this filter" Width="20px" onclick="Remove_Click">
            </dx:ASPxButton>
        </td>
    </tr>
</table>