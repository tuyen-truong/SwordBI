<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcCalcField.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcCalcField" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<table>
    <tr>
        <td>
            <asp:Label ID="lblName" runat="server" Text="phamduy" ToolTip="Calculation identity name." />
        </td>
        <td>
            <dx:ASPxComboBox ID="cboMember1" runat="server" Width="150px"
                ToolTip="Calculation member1 : field type." />
        </td>
        <td>
            <asp:TextBox ID="txtMember1" runat="server" Width="144px"
                ToolTip="Calculation member1 : numeric type." CssClass="numericInput" Text="0" />
        </td>
        <td>
            <dx:ASPxComboBox ID="cboOperator" runat="server" Width="50px" HorizontalAlign="Center"
                ToolTip="Calculation operator." />
        </td>
        <td>        
            <dx:ASPxComboBox ID="cboMember2" runat="server" Width="150px" 
                ToolTip="Calculation member2 : field type." />
        </td>
        <td>
            <asp:TextBox ID="txtMember2" runat="server" Width="144px"
                ToolTip="Calculation member2 : numeric type." CssClass="numericInput" Text="0" />
        </td>
        <td>
            <dx:ASPxButton ID="btnRemove" runat="server" Text="X" Width="20px"
                ToolTip="Remove this calculation field." onclick="Remove_Click">
            </dx:ASPxButton> 
        </td>
    </tr>
</table>