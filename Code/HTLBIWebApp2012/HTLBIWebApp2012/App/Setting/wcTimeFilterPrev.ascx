<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcTimeFilterPrev.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcTimeFilterPrev" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<table>
    <tr>
        <td>
            <dx:ASPxComboBox ID="cbbKeyField" runat="server" Width="324px" ToolTip="Previous time infor">
            </dx:ASPxComboBox>
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