<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HTLBIWebApp2012.App.Dashboard.Dashboard" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register src="wcSwitchDashboard.ascx" tagname="wcSwitchDashboard" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">        
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <asp:UpdatePanel ID="udp_Dashboard" runat="server">
        <ContentTemplate>
            <uc1:wcSwitchDashboard ID="wcSwitchDashboard1" runat="server" />
            <div ID="ctrl_Dashboard" runat="server" style="width:100%"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>