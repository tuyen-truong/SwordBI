<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPropGrid.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcPropGrid" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx1" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2.Web, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<%@ Register src="../UserControls/wcGrid.ascx" tagname="wcGrid" tagprefix="uc1" %>

<table>
    <tr>
        <td style="vertical-align:top;">
            <div ID="ctrlPreView" runat="server" title="The illustration of grid"></div>
        </td>
    </tr>
    <tr>
        <td>
            <div ID="ctrlColumn" runat="server"></div>
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxButton ID="btnAddColumn" runat="server" Text="Add column" 
                Width="90px" OnClick="btn_Click" />
        </td>
    </tr>
</table>