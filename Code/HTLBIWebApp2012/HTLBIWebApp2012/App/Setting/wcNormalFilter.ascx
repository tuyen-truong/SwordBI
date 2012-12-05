<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcNormalFilter.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcNormalFilter" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>

<table>
    <tr>
        <td>
            <dx:ASPxComboBox ID="cbbKeyField" ClientInstanceName="NormalFilter_cbbKeyField" runat="server" Width="120px" ToolTip="Key filter">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbOperator" runat="server" Width="50px" HorizontalAlign="Center" ToolTip="Operator">
            </dx:ASPxComboBox>
        </td>
        <td>
            <asp:TextBox ID="txtValue" runat="server" Width="140px" ToolTip="Text value" />
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