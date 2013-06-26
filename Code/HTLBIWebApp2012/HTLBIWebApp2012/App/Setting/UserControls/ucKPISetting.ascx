<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucKPISetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.ucKPISetting" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>

<asp:UpdatePanel ID="KpiUpdatePanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table cellpadding="0" cellspacing="0">
	<tr>
		<th style="font-size:90%;">KPI DEFINATION</th>
	</tr>
</table>
<fieldset style="min-width:1024px;width:1024px">
<legend>General</legend>
<table style="border-collapse:collapse;">
	<colgroup>
	<col width="170px" />
	<col width="250px" />
	<col />
	<col width="250px" />
	</colgroup>
	<tr>
		<th style="font-weight:normal;text-align:left;">Display Name</th>
		<td style="padding-bottom: 3px">
			<dx:ASPxTextBox ID="txtKPIDisplayName" runat="server" Width="100%">
			</dx:ASPxTextBox>
		</td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<th style="font-weight:normal;text-align:left;">KPIs</th>
		<td valign="top">
			<dx:ASPxComboBox ID="cbKPI" runat="server" Width="100%" AutoPostBack="true"	ClientInstanceName="cbKPI"
				onvaluechanged="cbKPI_ValueChanged">
			</dx:ASPxComboBox>
		</td>
		<td style="padding-left:3px; padding-bottom:3px">
			<dx:ASPxButton ID="btnNew" runat="server" Text="New" Width="60px" 
				onclick="btnNew_Click">
			</dx:ASPxButton>
		</td>
		<td></td>
	</tr>
	<tr>
		<th style="font-weight:normal;text-align:left;">Preferred Visualization</th>
		<td>
			<dx:ASPxComboBox ID="cbCtrlType" runat="server" Width="100%" 
				onvaluechanged="cbCtrlType_ValueChanged" ShowImageInEditBox="True" AutoPostBack="true">
			</dx:ASPxComboBox>
		</td>
		<td style="padding-left:3px">Control</td>
		<td>
			<dx:ASPxComboBox ID="cbCtrl" runat="server" Width="100%" ShowImageInEditBox="true">
				<ItemStyle Height="20px" />
			</dx:ASPxComboBox>
		</td>
	</tr>
	<tr>
		<td colspan="4">
			<fieldset>
			<legend>Range</legend>
			<table style="border-collapse:collapse">
				<tr>
					<td>Min value</td>
					<td>
						<asp:TextBox ID="txtMinValue" runat="server" AutoPostBack="true" Width="80px" CssClass="numericInput" Text="0" />
					</td>
					<td>
						<dx:ASPxCheckBox ID="chkNoMinValue" runat="server" Text="No min value">
						</dx:ASPxCheckBox>
					</td>
					<td style="width: 10px">&nbsp;</td>
					<td>Max value</td>
					<td>
						<asp:TextBox ID="txtMaxValue" runat="server" AutoPostBack="true" Width="80px" CssClass="numericInput" Text="1000" />
					</td>
					<td>
						<dx:ASPxCheckBox ID="chkNoMaxValue" runat="server" Text="No max value">
						</dx:ASPxCheckBox>
					</td>
				</tr>
			</table>
			</fieldset>
		</td>
	</tr>
</table>
</fieldset>
<dx:ASPxPageControl ID="tabKPISetting" runat="server" ActiveTabIndex="0" 
	Width="1024px">
<Border BorderStyle="None" />
<ContentStyle>
	<Paddings Padding="0px" />
</ContentStyle>
<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
	<Paddings Padding="0px" />
</TabStyle>
<Paddings Padding="0px" />
<TabPages>
<dx:TabPage Name="tabPageDimensions" Text="Dimensions" ToolTip="Define dimensions.">
	<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
		<Paddings Padding="3px" />
	</TabStyle>
	<ContentCollection>
		<dx:ContentControl ID="ContentControl1" runat="server">
			<asp:Panel ID="tabPageDimensionsContainer" runat="server"></asp:Panel>
			<dx:ASPxButton ID="btnAddDimension" runat="server" Text="Add dimension" 
				Width="120px" OnClick="btnAddDimension_Click" />
		</dx:ContentControl>
	</ContentCollection>
</dx:TabPage>
<dx:TabPage Name="tabPageMeasuresFilters" Text="Measures and Filters" ToolTip="Define measures and filters.">
<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
	<Paddings Padding="3px" />
</TabStyle>
<ContentCollection>
	<dx:ContentControl ID="ContentControl2" runat="server">
		<asp:Panel ID="measureContainer" runat="server"></asp:Panel>
		<dx:ASPxButton ID="btnAddMeasure" runat="server" Text="Add measures" 
			Width="120px" OnClick="btnAddMeasure_Click" />
		<hr />		
		<asp:Panel ID="kpiFilterContainer" runat="server"></asp:Panel>
		<dx:ASPxButton ID="btnAddKpiFilter" runat="server" Text="Add KPI Filter" 
			Width="120px" AutoPostBack="false" />
		<dx:ASPxPopupMenu ID="popMenAddFilter" runat="server" PopupElementID="btnAddKpiFilter"
				OnItemClick="popMenAddFilter_ItemClick" PopupAction="LeftMouseClick">
			<Items>
				<dx:MenuItem Text="Normal" Name="NORMAL">
				</dx:MenuItem>
				<dx:MenuItem Text="Numeric" Name="NUM">
				</dx:MenuItem>
				<dx:MenuItem Text="Time" Name="DATE">
				</dx:MenuItem>
			</Items>
		</dx:ASPxPopupMenu>
	</dx:ContentControl>
</ContentCollection>
</dx:TabPage>
<dx:TabPage Name="tabPageContextMetrics" Text="Contextual Metrics" ToolTip="Define contextual metrics.">
<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
	<Paddings Padding="3px" />
</TabStyle>
<ContentCollection>
	<dx:ContentControl ID="ContentControl3" runat="server">
		<asp:Panel ID="kpiContextMetricContainer" runat="server"></asp:Panel>
		<dx:ASPxButton ID="btnAddContextMetric" runat="server" Text="Add contextual metric" 
			Width="160px" AutoPostBack="false" />	
		<dx:ASPxPopupMenu ID="popMenAddCalcField" runat="server" PopupElementID="btnAddContextMetric"
			OnItemClick="popMenAddCalcField_ItemClick" PopupAction="LeftMouseClick">
			<Items>
				<dx:MenuItem Text="Normal contextual metric" Name="Normal">
				</dx:MenuItem>
				<dx:MenuItem Text="Calculation contextual metric" Name="Calc">
				</dx:MenuItem>
			</Items>
		</dx:ASPxPopupMenu>
	</dx:ContentControl>
</ContentCollection>
</dx:TabPage>
</TabPages>
</dx:ASPxPageControl>
<table style="border-collapse:collapse;margin-top: 3px" cellpadding="0">
<tr>
	<td>
		<dx:ASPxButton ID="btnPreview" runat="server" Text="Preview" Width="80px" AutoPostBack="false">
			<ClientSideEvents Click="function(s, e){ gridPreviewData.PerformCallback(); }" />
		</dx:ASPxButton>
	</td>
	<td style="padding-left: 3px; padding-right: 3px">
		<dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="80px" 
            onclick="btnSave_Click">
		</dx:ASPxButton>
	</td>
	<td>
		<dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" Width="80px">
		</dx:ASPxButton>
	</td>
</tr>
</table>
<!-- Popup Content -->
<dx:ASPxPopupControl ID="frmPreview" runat="server" AllowDragging="True" ClientInstanceName="frmPreview"
	AllowResize="True" AppearAfter="100" CloseAction="CloseButton" 
	DisappearAfter="100" HeaderText="Preview KPI" 
	PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" PopupVerticalOffset="20"
	Width="900px" ShowFooter="True" Height="500px">
	<FooterTemplate>
		<div style="float: right; padding: 5px 5px 5px 5px">
			<dx:ASPxButton ID="btnClose" runat="server" Text="Close" Width="70px" AutoPostBack="False">
				<ClientSideEvents Click="function(s, e) {
					frmPreview.Hide();
				}" />
			</dx:ASPxButton>
		</div>
	</FooterTemplate>
	<ContentStyle>
		<Paddings Padding="5px" />
	</ContentStyle>
	<ContentCollection>
		<dx:PopupControlContentControl ID="previewContent" runat="server">
			<dx:ASPxPageControl ID="previewTabs" runat="server" ActiveTabIndex="0" 
				Width="890px" Height="100%">
				<ContentStyle>
					<Paddings Padding="0" />
				</ContentStyle>
				<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
					<Paddings Padding="0" />
				</TabStyle>
				<Paddings Padding="0" />
				<TabPages>
					<dx:TabPage Name="tabPreviewData" Text="Data" ToolTip="Data">
						<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
							<Paddings Padding="3px" />
						</TabStyle>
						<ContentCollection>
							<dx:ContentControl ID="ContentControl4" runat="server" Height="500px">
								<dx:ASPxGridView ID="gridPreviewData" runat="server" 
									ClientInstanceName="gridPreviewData" AutoGenerateColumns="False" 
									OnCustomCallback="gridPreviewData_CustomCallback" 
									OnCustomUnboundColumnData="gridPreviewData_CustomUnboundColumnData" 
									OnPageIndexChanged="gridPreviewData_PageIndexChanged"
									Width="100%">
								<ClientSideEvents EndCallback="function(s, e){ frmPreview.Show(); }" />
								<Columns>
									<dx:GridViewDataTextColumn Caption="#" FieldName="#" Name="colLine" PropertiesTextEdit-DisplayFormatString="#,##0"
										ShowInCustomizationForm="True" VisibleIndex="0" UnboundType="Integer">
										<PropertiesTextEdit DisplayFormatString="#,##0">
										</PropertiesTextEdit>
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<CellStyle HorizontalAlign="Center">
										</CellStyle>
									</dx:GridViewDataTextColumn>
								</Columns>
								<SettingsBehavior AllowSort="false" />
								<SettingsPager PageSize="20"></SettingsPager>
								</dx:ASPxGridView>
							</dx:ContentControl>
						</ContentCollection>
					</dx:TabPage>
					<dx:TabPage Name="tabPreviewMDX" Text="MDX Query" ToolTip="MDX Query">
						<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
							<Paddings Padding="3px" />
						</TabStyle>
						<ContentCollection>
							<dx:ContentControl ID="ContentControl5" runat="server">
								<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server">
									<PanelCollection>
										<dx:PanelContent ID="a" runat="server">
											<dx:ASPxMemo ID="txtPreviewMDX" runat="server" Height="500px" Width="100%" ReadOnly="true">
											</dx:ASPxMemo>
										</dx:PanelContent>
									</PanelCollection>
								</dx:ASPxCallbackPanel>
							</dx:ContentControl>
						</ContentCollection>
					</dx:TabPage>
				</TabPages>
			</dx:ASPxPageControl>
		</dx:PopupControlContentControl>
	</ContentCollection>
</dx:ASPxPopupControl>
</ContentTemplate>
</asp:UpdatePanel>

