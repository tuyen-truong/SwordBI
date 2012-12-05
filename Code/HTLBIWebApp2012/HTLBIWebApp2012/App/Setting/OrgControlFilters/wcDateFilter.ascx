<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcDateFilter.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcDateFilter" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<table width="420px">
    <tr>
        <td>
            <dx:ASPxComboBox ID="cbbKeyField" runat="server" Width="120px">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbOperator" runat="server" Width="70px">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxDateEdit ID="deValue" runat="server" Width="146px" 
                DisplayFormatString="dd/MM/yyyyy" EditFormatString="dd/MM/yyyyy" 
                ToolTip="dd/mm/yyyyy">
            </dx:ASPxDateEdit>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbAndOr" runat="server" Width="70px">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxButton ID="btnDelFilter" runat="server" Text="X" 
                ToolTip="Remove this filter" Width="20px" onclick="btnDelFilter_Click">
            </dx:ASPxButton>
        </td>
    </tr>
</table>