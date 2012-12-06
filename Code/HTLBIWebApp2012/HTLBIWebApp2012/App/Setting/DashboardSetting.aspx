<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="DashboardSetting.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.DashboardSetting" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        ul.menu
        {
        }
        ul.menu li
        {
            display: inline-block;
            list-style-type: none;
        }
    </style>
    <div id="divNavigation">
        <ul class="menu">
            <li>
                <dx:ASPxHyperLink runat="server" ID="DataSource" ClientInstanceName="DataSource"
                    Text="Data Source" />
            </li>
            <li>&nbsp;|&nbsp;</li>
            <li>
                <dx:ASPxHyperLink runat="server" ID="ASPxHyperLink1" ClientInstanceName="DataSource"
                    Text="KPIs" />
            </li>
            <li>&nbsp;|&nbsp;</li>
            <li>
                <dx:ASPxHyperLink runat="server" ID="ASPxHyperLink2" ClientInstanceName="DataSource"
                    Text="Layouts" />
            </li>
        </ul>
    </div>
    <div id="divWarehouseFilter" style="margin-top: 10px;">
        <dx:ASPxComboBox ID="cboDataDW" runat="server" AutoPostBack="true" OnValueChanged="cboDataDW_ValueChanged">
        </dx:ASPxComboBox>
    </div>
    <div id="divDashboardList" style="margin-top: 5px">
        <asp:Label runat="server" ID="lblLstTitle" Text="Dashboard List" Font-Bold="true"></asp:Label>
        <dx:ASPxGridView ID="lstDashboard" runat="server" AutoGenerateColumns="false" Width="80%">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" Width="50%">
                    <Settings AllowDragDrop="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Template" VisibleIndex="1" Width="40%">
                    <Settings AllowDragDrop="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Default" FieldName="IsDefault" VisibleIndex="2">
                    <Settings AllowDragDrop="False" AllowSort="False" />
                </dx:GridViewDataCheckColumn>
            </Columns>
            <SettingsPager Mode="ShowAllRecords" />
        </dx:ASPxGridView>
    </div>
</asp:Content>
