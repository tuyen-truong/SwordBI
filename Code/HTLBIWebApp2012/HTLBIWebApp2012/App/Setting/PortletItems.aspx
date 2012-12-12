<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true" CodeBehind="PortletItems.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.PortletItems" %>
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
    <span style="font-weight: bold; float: left; padding: 5px 0 5px">Portlet List&nbsp;&nbsp;</span>
    <div style="float: left; padding: 5px; background: #E7EBEF"><asp:HyperLink runat="server" NavigateUrl="~/App/Setting/PortletSetting.aspx" Text="Add New" ID="lnkAddNew"></asp:HyperLink></div>
    <div style="clear:both"></div>
    <dx:ASPxGridView ID="gridPortletList" runat="server" AutoGenerateColumns="false" Width="80%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Portlet Name" FieldName="Name" VisibleIndex="0" Width="25%">
                <DataItemTemplate>
                    <asp:HyperLink runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("Code", "~/App/Setting/PortletSetting.aspx?wgcode={0}") %>' ID="ctl"></asp:HyperLink>
                </DataItemTemplate>
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="DS Name" FieldName="DSName" VisibleIndex="1" Width="25%">
                <Settings AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="KPI Name" FieldName="KPIName" VisibleIndex="2"  Width="25%">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Layout" VisibleIndex="3" Width="25%">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsPager Mode="ShowAllRecords" />
    </dx:ASPxGridView>
</div>
</asp:Content>
