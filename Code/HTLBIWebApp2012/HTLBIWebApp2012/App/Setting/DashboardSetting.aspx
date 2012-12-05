<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true" CodeBehind="DashboardSetting.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.DashboardSetting" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register src="../Setting/wcDashboardSetting.ascx" tagname="wcDashboardSetting" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>      
    <uc1:wcDashboardSetting ID="wcDashboardSetting1" runat="server" />        
</asp:Content>
