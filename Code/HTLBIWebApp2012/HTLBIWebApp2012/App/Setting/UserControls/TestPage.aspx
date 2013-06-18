<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.UserControls.TestPage" EnableViewState="true" %>

<%@ Register TagName="ucDatasourceSetting" TagPrefix="uc" Src="~/App/Setting/UserControls/ucDatasourceSetting.ascx" %>
<%@ Register TagName="ucKPISetting" TagPrefix="uc" Src="~/App/Setting/UserControls/ucKPISetting.ascx" %>

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
		<uc:ucDatasourceSetting runat="server" ID="uc1" />

	</div>
	</form>
</body>
</html>
