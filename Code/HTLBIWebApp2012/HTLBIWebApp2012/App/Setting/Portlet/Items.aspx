<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.PortletItems" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<br />
<table style="margin-left: 10px">
    <tr>
        <td>Data Warehouse</td>
        <td>
            <dx:ASPxComboBox ID="cboDataDW" runat="server" AutoPostBack="true" OnValueChanged="cboDataDW_ValueChanged">
            </dx:ASPxComboBox>
        </td>
    </tr>
</table>
<br />
<div id="divDashboardList" style="margin-left: 10px">
    <asp:Label runat="server" ID="lblLstTitle" Text="Portlet List" Font-Bold="true"></asp:Label>
    <dx:ASPxGridView ID="gridPortletList" runat="server" AutoGenerateColumns="false" Width="80%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="DS Name" FieldName="Name" VisibleIndex="0" Width="40%">
                <DataItemTemplate>
                    <asp:HyperLink runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("ID", "~/App/Setting/Portlet/Edit.aspx?plid={0}") %>' ID="ctl"></asp:HyperLink>
                </DataItemTemplate>
                <Settings AllowDragDrop="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="KPI Name" VisibleIndex="1" Width="30%">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Layout" VisibleIndex="2">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsPager Mode="ShowAllRecords" />
    </dx:ASPxGridView>
</div>
</asp:Content>
