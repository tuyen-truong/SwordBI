<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.UserControls.TestPage" EnableViewState="true" %>

<%@ Register Assembly="DevExpress.XtraCharts.v10.2.Web, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>

<%@ Register TagName="ucDatasourceSetting" TagPrefix="uc" Src="~/App/Setting/UserControls/ucDatasourceSetting.ascx" %>
<%@ Register TagName="ucKPISetting" TagPrefix="uc" Src="~/App/Setting/UserControls/ucKPISetting.ascx" %>

<%@ Register assembly="DevExpress.XtraCharts.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<div>
		<dxchartsui:WebChartControl ID="WebChartControl1" runat="server" 
			ClientIDMode="AutoID" Height="200px" IndicatorsPaletteName="Default" 
			Width="300px">
		</dxchartsui:WebChartControl>
	</div>
	</form>
</body>
</html>
