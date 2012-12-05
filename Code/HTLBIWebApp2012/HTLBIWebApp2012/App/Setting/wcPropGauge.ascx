<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPropGauge.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcPropGauge" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx1" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2.Web, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<table>
    <tr>
        <td rowspan="6" style="width:170px; height:170px; vertical-align:top;">
            <div ID="ctrlPreView" runat="server" title="The illustration of gauge"></div>
        </td>
        <td style="width:20px"></td>
        <td>Measure</td>
        <td style="width:150px;">
            <dx:ASPxComboBox ID="cboDimension" runat="server" AutoPostBack="false" 
                ClientInstanceName="Layout_PropGauge_cboDimension" Width="100%">
            </dx:ASPxComboBox>
        </td>
        <td>Measure</td>
        <td style="width:150px;">
            <dx:ASPxComboBox ID="cboMeasure" runat="server" AutoPostBack="true" 
                ClientInstanceName="Layout_PropGauge_cboMeasure" Width="100%"
                OnValueChanged="ctrl_ValueChanged">
            </dx:ASPxComboBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>Min value</td>
        <td>
            <asp:TextBox ID="txtMinValue" runat="server" ToolTip="Number value" 
                Width="145px" AutoPostBack="true" OnTextChanged="ctrl_ValueChanged"
                CssClass="numericInput" Text="0" />
        </td>
        <td>Max value</td>
        <td>
            <asp:TextBox ID="txtMaxValue" runat="server" ToolTip="Number value" 
                Width="145px" AutoPostBack="true" OnTextChanged="ctrl_ValueChanged"
                CssClass="numericInput" Text="5000" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>Format value</td>
        <td>
            <dx:ASPxTextBox ID="txtFormatValue" runat="server" Width="100%" 
                AutoPostBack="true" OnValueChanged="ctrl_ValueChanged" Text="N0" />
        </td>
        <td ></td>
        <td>
            <dx:ASPxCheckBox ID="chkShowCurValueText" runat="server" Font-Names="Arial" AutoPostBack="true"
                Font-Size="10pt" Text="Show value text" OnValueChanged="ctrl_ValueChanged">
            </dx:ASPxCheckBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>Width</td>
        <td>
            <asp:TextBox ID="txtWidth" runat="server" ToolTip="Number value" 
                Width="145px" CssClass="numericInput" Text="170" />
        </td>
        <td>Height</td>
        <td>
            <asp:TextBox ID="txtHeight" runat="server" ToolTip="Number value" 
                Width="145px" CssClass="numericInput" Text="170" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>
            &nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td colspan="7">
            <div ID="ctrlStateRange" runat="server"></div>
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <dx:ASPxButton ID="btnAddStateRange" runat="server" Text="Add state range" 
                Width="110px" OnClick="btn_Click" >
            </dx:ASPxButton> 
        </td>
    </tr>
</table>