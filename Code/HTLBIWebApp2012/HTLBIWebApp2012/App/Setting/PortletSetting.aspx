<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
	CodeBehind="PortletSetting.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.PortletSetting" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Src="wcNumFilter.ascx" TagName="wcNumFilter" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Src="wcDatasourceSetting.ascx" TagName="wcDatasourceSetting" TagPrefix="uc2" %>
<%@ Register Src="wcKPISetting.ascx" TagName="wcKPISetting" TagPrefix="uc3" %>
<%@ Register Src="wcLayoutSetting.ascx" TagName="wcLayoutSetting" TagPrefix="uc4" %>
<%@ Register Src="wcInteractionSetting.ascx" TagName="wcInteractionSetting" TagPrefix="uc5" %>
<%@ Register Src="~/App/Setting/UserControls/ucDatasourceSetting.ascx" TagName="ucDatasourceSetting"
	TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			$(".numericInput").autoNumeric({ aSep: ',', aDec: '.', mDec: '0', vMax: '999999999999999999' });
			$(".numericInput").css("text-align", "right");
		});

		function Portlet_SetActiveTab(tab_index) {
			tabCtrl_PortletSetting.SetActiveTab(tabCtrl_PortletSetting.GetTab(tab_index));
		}
	</script>
	<dx:ASPxPageControl ID="tabCtrl_PortletSetting" ClientInstanceName="tabCtrl_PortletSetting"
		runat="server" ActiveTabIndex="0" Width="100%" Font-Names="Arial" Font-Size="9pt"
		ClientIDMode="AutoID">
		<Border BorderStyle="None" />
		<ClientSideEvents Init="function(s, e) { Portlet_SetActiveTab(0); }" ActiveTabChanging="function(s, e) {
			if(e.tab.name == 'tabPage_LayoutSetting')
			{
				var dsCode = Get_CurDSCode();
				var kpiCode = Get_CurKPICode();
				var layoutCount = Get_CurLayoutCount();
				if(dsCode.length==0 && kpiCode.length==0 && layoutCount==0)
				{
					alert('Please select a datasource or KPI on tabpage [Data source] or [KPIs].');
					e.cancel = true;
				}
			}
			else if(e.tab.name == 'tabPage_InteractionSetting')
			{
				var layoutCode = Get_CurLayoutCode();
				if(layoutCode.length==0)
				{
					alert('Please select a layout.');
					e.cancel = true;
				}
			}
		}" />
		<Paddings Padding="0px" />
		<TabPages>
			<dx:TabPage Name="tabPage_DatasourceSetting" Text="Data source" ToolTip="Data source defination.">
				<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
					<Paddings Padding="3px" />
					<Paddings Padding="3px"></Paddings>
				</TabStyle>
				<ContentCollection>
					<dx:ContentControl ID="ContentControl2" runat="server">
						<uc2:ucDatasourceSetting ID="ucDatasourceSetting1" runat="server" />
					</dx:ContentControl>
				</ContentCollection>
			</dx:TabPage>
			<dx:TabPage Name="tabPage_KPISetting" Text="KPIs" ToolTip="KPI defination.">
				<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
					<Paddings Padding="3px" />
					<Paddings Padding="3px"></Paddings>
				</TabStyle>
				<ContentCollection>
					<dx:ContentControl ID="ContentControl1" runat="server">
						<uc3:wcKPISetting ID="wcKPISetting1" runat="server" />
					</dx:ContentControl>
				</ContentCollection>
			</dx:TabPage>
			<dx:TabPage Name="tabPage_LayoutSetting" Text="Layout" ToolTip="Layout defination.">
				<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
					<Paddings Padding="3px" />
					<Paddings Padding="3px"></Paddings>
				</TabStyle>
				<ContentCollection>
					<dx:ContentControl ID="ContentControl3" runat="server">
						<uc4:wcLayoutSetting ID="wcLayoutSetting1" runat="server" />
					</dx:ContentControl>
				</ContentCollection>
			</dx:TabPage>
			<dx:TabPage Name="tabPage_InteractionSetting" Text="Interaction" ToolTip="Interaction defination.">
				<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
					<Paddings Padding="3px" />
					<Paddings Padding="3px"></Paddings>
				</TabStyle>
				<ContentCollection>
					<dx:ContentControl ID="ContentControl4" runat="server">
						<uc5:wcInteractionSetting ID="wcInteractionSetting1" runat="server" />
					</dx:ContentControl>
				</ContentCollection>
			</dx:TabPage>
		</TabPages>
		<ClientSideEvents ActiveTabChanging="function(s, e) {
			if(e.tab.name == &#39;tabPage_LayoutSetting&#39;)
			{
				var dsCode = Get_CurDSCode();
				var kpiCode = Get_CurKPICode();
				var layoutCount = Get_CurLayoutCount();
				if(dsCode.length==0 &amp;&amp; kpiCode.length==0 &amp;&amp; layoutCount==0)
				{
					alert(&#39;Please select a datasource or KPI on tabpage [Data source] or [KPIs].&#39;);
					e.cancel = true;
				}
			}
			else if(e.tab.name == &#39;tabPage_InteractionSetting&#39;)
			{
				var layoutCode = Get_CurLayoutCode();
				if(layoutCode.length==0)
				{
					alert(&#39;Please select a layout.&#39;);
					e.cancel = true;
				}
			}
		}" Init="function(s, e) { Portlet_SetActiveTab(0); }"></ClientSideEvents>
		<Paddings Padding="0px"></Paddings>
		<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
			<Paddings Padding="0px" />
			<Paddings Padding="0px"></Paddings>
		</TabStyle>
		<ContentStyle>
			<Paddings Padding="5px" />
			<Paddings Padding="5px"></Paddings>
		</ContentStyle>
		<Border BorderStyle="None"></Border>
	</dx:ASPxPageControl>
</asp:Content>
