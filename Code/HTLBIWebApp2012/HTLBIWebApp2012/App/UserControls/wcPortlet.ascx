<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcPortlet.ascx.cs" Inherits="HTLBIWebApp2012.App.UserControls.wcPortlet" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register src="wcChart.ascx" tagname="wcChart" tagprefix="uc1" %>

<%@ Register src="wcThreeFourCGauge.ascx" tagname="wcThreeFourCGauge" tagprefix="uc2" %>

<center class="title">
    <asp:ImageButton  runat="server" ID="imgBtnSetting"            
        ImageUrl="~/Content/Images/setting_16.png" ToolTip="Setting." ImageAlign="Right" 
        OnClientClick="Set_ProcessState('0');" CommandArgument="1" 
        CommandName="Setting" oncommand="imgBtnSetting_Command"/>
    <asp:ImageButton  runat="server" ID="imgBtn_PortletFilter"            
        ImageUrl="~/Content/Images/icon_table_16.png" ToolTip="Show portlet filter." ImageAlign="Right" 
        OnClientClick="ShowPortletFilter();" CommandArgument="1" 
        CommandName="Edit" />
    <dx:ASPxLabel ID="lbl_PortletTitle" runat="server" Text=""></dx:ASPxLabel>            
</center>
<div class="content">
    <center>
        <div id="tabPage_1_1" style="width:100%">
        <!--Trình bài dạng biểu đồ-->   
                 
            <%--<uc1:wcChart ID="wcChart1" runat="server" />--%>
                 
            <uc2:wcThreeFourCGauge ID="wcThreeFourCGauge1" runat="server" />
                 
        </div>                   
    </center>
</div>