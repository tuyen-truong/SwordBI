<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true" CodeBehind="PortletItems.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.PortletItems" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="divNavigation" style="margin-top: 10px; margin-left: 10px">
    <ul class="menu">
        <li>
            <dx:ASPxHyperLink runat="server" ID="DataSource" ClientInstanceName="DataSource"
                Text="Data Source" NavigateUrl="~/App/Setting/DatasourceList.aspx" />
        </li>
        <li>&nbsp;|&nbsp;</li>
        <li>
            <dx:ASPxHyperLink runat="server" ID="KpiList" ClientInstanceName="KpiList"
                Text="KPIs" NavigateUrl="~/App/Setting/KpiList.aspx" />
        </li>
    </ul>
</div>
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
<div id="divPortletList" style="margin-left: 10px">
    <span style="font-weight: bold; float: left; padding: 5px 0 5px">Portlet List&nbsp;&nbsp;</span>
    <div style="float: left; padding: 5px; background: #E7EBEF"><asp:HyperLink runat="server" NavigateUrl="~/App/Setting/PortletSetting.aspx" Text="Add New" ID="lnkAddNew"></asp:HyperLink></div>
    <div style="clear:both"></div>
    <dx:ASPxGridView ID="gridPortletList" runat="server" AutoGenerateColumns="false" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="DS Name" FieldName="DSName" VisibleIndex="0" Width="40%">
                <DataItemTemplate>
                    <asp:HyperLink runat="server" Text='<%# Eval("DSName") %>' NavigateUrl='<%# Eval("Code", "~/App/Setting/PortletSetting.aspx?wgcode={0}") %>' ID="ctl"></asp:HyperLink>
                </DataItemTemplate>
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="KPI Name" FieldName="KPIName" VisibleIndex="0"  Width="40%">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn Caption="Layout" FieldName="LayoutImageUrl" VisibleIndex="0">
                <DataItemTemplate>
                    <asp:Image ImageUrl='<%# Eval("LayoutImageUrl") %>' runat="server" AlternateText="" />
                </DataItemTemplate>
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataColumn>
        </Columns>
        <SettingsPager Mode="ShowAllRecords" />
    </dx:ASPxGridView>
</div>
</asp:Content>
