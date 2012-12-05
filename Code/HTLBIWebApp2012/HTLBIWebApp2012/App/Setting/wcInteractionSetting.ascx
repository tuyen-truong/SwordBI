<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcInteractionSetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcInteractionSetting" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>

<%@ Register src="wcInteractionFieldHierarchy.ascx" tagname="wcInteractionFieldHierarchy" tagprefix="uc1" %>

<style type="text/css">
    .dxeListBox_DevEx 
    {
        font-size: 8pt;
        font-family: Verdana;
	    background-color: white;
	    border: 1px solid;
	    border-color: #9da0aa #c2c4cb #d9dae0;
        width: 70px;
        height: 109px;
    }
    .dxeListBoxItemRow_DevEx
    {
        cursor: default;
    }

    .dxeListBoxItemSelected_DevEx     /* inherits dxeListBoxItem */
    {    
        color: #201f35;
        background: #eff0f2;
    }
    .dxeListBoxItem_DevEx
    {
        font-family: Verdana;
        font-weight: normal;
        font-size: 8pt;
        color: #201f35;
        padding: 2px 5px;
        white-space: nowrap;
        text-align: left;
        border-width: 0;
    }
</style>

<script type="text/javascript" language="javascript">
    //ctrl_HierarchyField.dis
</script>

<div style="text-align:left;font: normal bolder 15pt Arial;">INTERACTION DEFINATION</div>

<!--Interaction Filter And DrillDown-->
<dx:ASPxPageControl ID="tabCtrl_InteractionSetting" ClientInstanceName="tabCtrl_InteractionSetting"
	runat="server" ActiveTabIndex="1" Width="100%" Font-Names="Arial"
    Font-Size="9pt" ClientIDMode="AutoID">
	<Border BorderStyle="None" />
	<Paddings Padding="0px" />
	<TabPages>
		<dx:TabPage Name="tabPage_Filters" Text="Filters" ToolTip="Define interaction filters.">
			<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" />
            <Paddings Padding="3px"></Paddings>
            </TabStyle>
			<ContentCollection>
				<dx:ContentControl ID="ContentControl4" runat="server">
                    <asp:UpdatePanel ID="upp_Filter" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td><div id="ctrl_InteractionFilters" runat="server"></div></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddInteractionFilter" runat="server" 
                                            Text="Add Interaction Filter" AutoPostBack="true" OnClick="btn_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
				</dx:ContentControl>
			</ContentCollection>
		</dx:TabPage>
		<dx:TabPage Name="tabPage_DrillDown" Text="Drilldown" ToolTip="Define Drilldown.">
			<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" />
            <Paddings Padding="3px"></Paddings>
            </TabStyle>
			<ContentCollection>
				<dx:ContentControl ID="ContentControl5" runat="server">
                    <asp:UpdatePanel ID="upp_DrillDown" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>Drilldown category</td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="radListDrilldownCat" runat="server" 
                                            ClientIDMode="AutoID" Width="100%" RepeatDirection="Horizontal">
                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                var valSel = s.GetValue();
                                                var valSelStr = valSel == null ? '' : valSel.toString();
                                                Interaction_cbbDrilldownPortlet.SetEnabled(valSelStr == 'Other');
                                                //InteractionFilter_cbbFieldHierarchy1.SetEnabled(valSelStr == 'In' || valSelStr == 'Popup');
                                                SetEnableHierarchy(valSelStr == 'In' || valSelStr == 'Popup');
                                            }" />
                                            <Items>
                                                <dx:ListEditItem Text="None" Value="None" Selected="true" />
                                                <dx:ListEditItem Text="In" Value="In" />
                                                <dx:ListEditItem Text="Popup" Value="Popup" />
                                                <dx:ListEditItem Text="Other" Value="Other" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Hierarchy Fields</td>
                                    <td>
                                        <uc1:wcInteractionFieldHierarchy ID="wcInteractionFieldHierarchy1" 
                                           runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Drilldown Portlet</td>
                                    <td>
                                        <dx:ASPxComboBox ID="cbbDrilldownPortlet" runat="server" 
                                            ClientInstanceName="Interaction_cbbDrilldownPortlet" 
                                            ToolTip="Drilldown Portlet." Width="200px">
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
				</dx:ContentControl>
			</ContentCollection>
		</dx:TabPage>
	</TabPages>

<Paddings Padding="0px"></Paddings>

	<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="0px" />
    <Paddings Padding="0px"></Paddings>
    </TabStyle>
	<ContentStyle><Paddings Padding="0px" />
<Paddings Padding="0px"></Paddings>
    </ContentStyle>

<Border BorderStyle="None"></Border>
</dx:ASPxPageControl>

<!--Buttons Command-->
<table>
    <tr>
        <td>
            <dx:ASPxButton ID="btnPreview" runat="server" Text="Preview" Width="100px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
                    var errMsg = '';
                }" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {                        
                    var errMsg = '';
	                Interaction_cbpSavingMsg.PerformCallback();
                }" />
            </dx:ASPxButton>
        </td>        
        <td>
            <dx:ASPxCallbackPanel ID="cbpSavingMsg" ClientInstanceName="Interaction_cbpSavingMsg" 
                runat="server" Width="100%" oncallback="cbp_Callback">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">                            
                        <asp:UpdatePanel ID="upp_SavingMsg" runat="server">
                            <ContentTemplate>
                                <dx:ASPxLabel ID="lblSavingMsg" runat="server" ></dx:ASPxLabel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </td>
    </tr>        
</table>

<!--Content Of Preview-->
<dx:ASPxPopupControl ID="frmPreView" ClientInstanceName="Interaction_frmPreView" runat="server" AppearAfter="100" 
    CloseAction="CloseButton" DisappearAfter="100" Font-Names="Arial" AllowResize="true"
    Font-Size="9pt" HeaderText="Preview layout" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" AllowDragging="true" 
    ShowFooter="true" Width="900px" Height="100%" ShowLoadingPanel="true" >
    <FooterTemplate>
        <div style="float:right; padding:5px 5px 5px 5px">
            <dx:ASPxButton ID="btnClose" runat="server" Text="Close" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                Interaction_frmPreView.Hide();                        
                }" />
            </dx:ASPxButton>
        </div>
    </FooterTemplate>
    <ContentStyle><Paddings Padding="5px" /></ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="popupPreView" runat="server">
            <dx:ASPxCallbackPanel ID="cbpPreView" ClientInstanceName="Interaction_cbpPreView" runat="server" Width="100%" oncallback="cbp_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
	                Interaction_frmPreView.Show();                 
                }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent_PreView" runat="server" SupportsDisabledAttribute="True">
                        <center><div id="ctrlPreView" runat="server"></div></center>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl> 