<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="DashboardEdit.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.DashboardEdit" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 5px">
        <asp:Label runat="server" Text="Display Name"></asp:Label></div>
    <fieldset style="margin: 5px">
        <legend>Layout Style</legend>
        <div style="margin: 5px">
            <span style="float: left; width: 150px;">Two Pane</span>
            <div style="float: left;">
                fdfd</div>
        </div>
        <div style="clear:both; height: 1px;"></div>
        <div style="margin: 5px;">
            <span style="float:left; width:150px;">Three Pane</span>
            <div>fdfdf</div>
        </div>
        <div style="clear:both; height: 1px;"></div>
        <div style="margin: 5px">
            <span style="float:left; width:150px;">Four Pane</span>
            <div>fdfdf</div>
        </div>
    </fieldset>
    <br />
    <br />
    <fieldset style="margin: 5px">
        <legend>Filters</legend>
        <dx:ASPxButton ID="btnAddDashboardFilter" runat="server" Text="Add Filter">
        </dx:ASPxButton>
    </fieldset>
</asp:Content>
