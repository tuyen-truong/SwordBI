<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPropGrid_Column.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcPropGrid_Column" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<table>
    <tr>
        <td>Field</td>
        <td>
            <dx:ASPxComboBox ID="cboField" runat="server" Width="90px" AutoPostBack="true"
                OnValueChanged="ctrl_ValueChanged" />
        </td>
        <td>Caption</td>
        <td>
            <dx:ASPxTextBox ID="txtCaption" runat="server" Width="90px" AutoPostBack="true" 
                OnValueChanged="ctrl_ValueChanged" />
        </td>
        <td>Format</td>
        <td>
            <dx:ASPxTextBox ID="txtFormatStr" runat="server" Width="60px" />
        </td>
        <td>Align</td>
        <td>
            <dx:ASPxComboBox ID="cboAlign" runat="server" Width="80px" AutoPostBack="True" 
                OnValueChanged="ctrl_ValueChanged" ClientIDMode="AutoID" SelectedIndex="0" 
                ValueType="System.String">
                <Items>
                    <dx:ListEditItem Selected="True" Text="NotSet" Value="NotSet" />
                    <dx:ListEditItem Text="Left" Value="Left" />
                    <dx:ListEditItem Text="Center" Value="Center" />
                    <dx:ListEditItem Text="Right" Value="Right" />
                    <dx:ListEditItem Text="Justify" Value="Justify" />
                </Items>
            </dx:ASPxComboBox>
        </td>
        <td>Visible index</td>
        <td>
            <asp:TextBox ID="txtVisibleIndex" runat="server" Width="50px" 
                AutoPostBack="true" OnTextChanged="ctrl_ValueChanged" 
                ToolTip="Number value" CssClass="numericInput" Text="1" />
        </td>
        <td>Width</td>
        <td>
            <asp:TextBox ID="txtWidth" runat="server" Width="50px" 
                AutoPostBack="true" OnTextChanged="ctrl_ValueChanged"
                ToolTip="Number value" CssClass="numericInput" Text="100" />
        </td>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnRemove" runat="server" Text="X" Width="20px"
                ToolTip="Remove this column" onclick="Remove_Click">
            </dx:ASPxButton> 
        </td>
    </tr>
</table>