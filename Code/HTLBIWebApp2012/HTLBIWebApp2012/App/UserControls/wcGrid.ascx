<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcGrid.ascx.cs" Inherits="HTLBIWebApp2012.App.UserControls.wcGrid" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<dx:ASPxGridView ID="gvData" ClientInstanceName="gvData" runat="server"
    AutoGenerateColumns="false" Font-Names="Arial" Font-Size="9pt"
    OnCustomCallback="gvData_CustomCallback"
    OnPageIndexChanged="gvData_PageIndexChanged"
    OnCustomUnboundColumnData="gvData_CustomUnboundColumnData">
    <SettingsBehavior AllowSort="false" />
    <SettingsPager PageSize="20"></SettingsPager>
</dx:ASPxGridView>