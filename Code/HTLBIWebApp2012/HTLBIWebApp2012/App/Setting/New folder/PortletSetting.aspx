<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/TwoContentZone.Master" AutoEventWireup="true" CodeBehind="PortletSetting.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.PortletSetting" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register src="wcNumFilter.ascx" tagname="wcNumFilter" tagprefix="uc1" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register src="wcDatasourceSetting.ascx" tagname="wcDatasourceSetting" tagprefix="uc2" %>
<%@ Register src="wcKPISetting.ascx" tagname="wcKPISetting" tagprefix="uc3" %>
<%@ Register src="wcLayoutSetting.ascx" tagname="wcLayoutSetting" tagprefix="uc4" %>

<%@ Register src="wcInteractionSetting.ascx" tagname="wcInteractionSetting" tagprefix="uc5" %>

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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <dx:ASPxPageControl ID="tabCtrl_PortletSetting" ClientInstanceName="tabCtrl_PortletSetting"
        runat="server" ActiveTabIndex="3" Width="100%" Font-Names="Arial" 
        Font-Size="9pt" ClientIDMode="AutoID">
	    <Border BorderStyle="None" />
	    <ContentStyle><Paddings Padding="5px" /></ContentStyle>
	    <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="0px" /></TabStyle>
	    <ClientSideEvents Init="function(s, e) { Portlet_SetActiveTab(0); }"
            ActiveTabChanging="function(s, e) {
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
			    <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			    <ContentCollection>
				    <dx:ContentControl ID="ContentControl2" runat="server">
				        <uc2:wcDatasourceSetting ID="wcDatasourceSetting1" runat="server" />
				    </dx:ContentControl>
			    </ContentCollection>
		    </dx:TabPage>
		    <dx:TabPage Name="tabPage_KPISetting" Text="KPIs" ToolTip="KPI defination.">
			    <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			    <ContentCollection>
				    <dx:ContentControl ID="ContentControl1" runat="server">
				        <uc3:wcKPISetting ID="wcKPISetting1" runat="server" />
				    </dx:ContentControl>
			    </ContentCollection>
		    </dx:TabPage>
		    <dx:TabPage Name="tabPage_LayoutSetting" Text="Layout" ToolTip="Layout defination.">
			    <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			    <ContentCollection>
				    <dx:ContentControl ID="ContentControl3" runat="server">
				        <uc4:wcLayoutSetting ID="wcLayoutSetting1" runat="server" />
				    </dx:ContentControl>
			    </ContentCollection>
		    </dx:TabPage>
		    <dx:TabPage Name="tabPage_InteractionSetting" Text="Interaction" ToolTip="Interaction defination.">
			    <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			    <ContentCollection>
				    <dx:ContentControl ID="ContentControl4" runat="server">
				        <uc5:wcInteractionSetting ID="wcInteractionSetting1" runat="server" />
				    </dx:ContentControl>
			    </ContentCollection>
		    </dx:TabPage>
	    </TabPages>
    </dx:ASPxPageControl>    

</asp:Content>
