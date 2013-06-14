<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDatasourceSetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.ucDatasourceSetting" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>	
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<style type="text/css">
.ds-defination th
{
	 font-weight:normal !important;
	 text-align: left;
	 width: 150px;
	 white-space:nowrap;
	 padding-bottom: 5px !important;
}
th.uc-title
{
	font-weight:bold !important;
	font-size: 90% !important;
}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellpadding="0" cellspacing="0" class="ds-defination">
	<tr>
		<th colspan="3" class="uc-title">DATASOURCE DEFINATION</th>
	</tr>
	<tr>
		<th>Display Name</th>
		<td>
			<dx:ASPxTextBox ID="txtDataSourceName" runat="server" Width="100%">
			</dx:ASPxTextBox>
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<th>Data Warehouse</th>
		<td style="width:250px">
			<dx:ASPxComboBox ID="cbDataWarehouse" runat="server" Width="100%" AutoPostBack="true" OnValueChanged="cbDataWarehouse_ValueChanged">
			</dx:ASPxComboBox>
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<th>Data Source</th>
		<td style="width:250px">
			<dx:ASPxComboBox ID="cbDataSource" runat="server" Width="100%" AutoPostBack="true" OnValueChanged="cbDataSource_ValueChanged">
			</dx:ASPxComboBox>
		</td>
		<td style="padding-left:3px">
			<dx:ASPxButton ID="btnNewDataSource" runat="server" Text="New">
			</dx:ASPxButton>
		</td>
	</tr>
</table>

<fieldset style="min-width:1024px">
<legend>Query Infomation</legend>
<table cellpadding="0" cellspacing="0" class="ds-defination" width="1024px">
	<colgroup>
		<col width="350px" />
		<col width="70px" />
		<col width="604px" />
	</colgroup>
	<tr>
		<th class="uc-title">Fields</th>
		<td></td>
		<td>
			<table cellpadding="0" cellspacing="0" style="float:right; margin-bottom:3px">
				<tr>
					<td>
						<dx:ASPxTextBox ID="txtFieldDispName" runat="server" Width="250px" 
							onvaluechanged="txtFieldDispName_ValueChanged" AutoPostBack="true">
						</dx:ASPxTextBox>
					</td>
					<td style="width:80px; padding-left:3px">
						<dx:ASPxComboBox ID="cbFieldSort" runat="server" Width="80px" 
							AutoPostBack="true" onvaluechanged="cbFieldSort_ValueChanged">
						</dx:ASPxComboBox>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<dx:ASPxListBox ID="lbFields" runat="server" ClientIDMode="AutoID" Width="100%" 
				Height="212px">
				<Columns>
					<dx:ListBoxColumn Caption="Field Name" FieldName="Caption" />
					<dx:ListBoxColumn Caption="Display Name" FieldName="Caption" />
				</Columns>
			</dx:ASPxListBox>
		</td>
		<td style="width:70px;">
			<div style="margin:5px 5px;">
				<dx:ASPxButton ID="btnFieldAdd" runat="server" Text=">" Width="60px">
				</dx:ASPxButton>
				<span style="line-height:3px">&nbsp;</span>
				<dx:ASPxButton ID="btnFieldRemove" runat="server" Text="<" Width="60px">
				</dx:ASPxButton>
			</div>
		</td>
		<td>
			<dx:ASPxListBox ID="lbSelectedFields" runat="server" ClientIDMode="AutoID" 
				Width="100%" Height="212px" 
				onselectedindexchanged="lbSelectedFields_SelectedIndexChanged" AutoPostBack="true">
				<Columns>
					<dx:ListBoxColumn Caption="Field Name" FieldName="Caption" />
					<dx:ListBoxColumn Caption="Display Name" FieldName="DisplayName" />
					<dx:ListBoxColumn Caption="Sort" FieldName="Sort" Width="30px" />
				</Columns>
			</dx:ASPxListBox>
		</td>
	</tr>
	<tr>
		<th class="uc-title">Metrics</th>
		<td></td>
		<td  style="padding-top: 5px">
			<table cellpadding="0" cellspacing="0" style="float:right; margin-bottom:3px">
				<tr>
					<td>
						<dx:ASPxTextBox ID="txtMetricDispName" runat="server" Width="250px" 
							AutoPostBack="True" onvaluechanged="txtMetricDispName_ValueChanged">
						</dx:ASPxTextBox>
					</td>
					<td style="width:80px; padding-left:3px">
						<dx:ASPxComboBox ID="cbFuncs" runat="server" Width="80px" AutoPostBack="True" 
							onselectedindexchanged="cbFuncs_SelectedIndexChanged">
						</dx:ASPxComboBox>
					</td>
					<td style="width:80px; padding-left:3px">
						<dx:ASPxComboBox ID="cbMetricSort" runat="server" Width="80px" 
							AutoPostBack="True" onselectedindexchanged="cbMetricSort_SelectedIndexChanged">
						</dx:ASPxComboBox>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<dx:ASPxListBox ID="lbMetricFields" runat="server" ClientIDMode="AutoID" Width="100%" 
				Height="120px">
				<Columns>
					<dx:ListBoxColumn Caption="Field Name" FieldName="Caption" />
					<dx:ListBoxColumn Caption="Display Name" FieldName="Caption" />
				</Columns>
			</dx:ASPxListBox>
		</td>
		<td style="width:70px;">
			<div style="margin:5px 5px;">
				<dx:ASPxButton ID="btnMeasureFieldAdd" runat="server" Text=">" Width="60px" OnClick="MeasureFieldAdd_Click">
				</dx:ASPxButton>
				<span style="line-height:3px">&nbsp;</span>
				<dx:ASPxButton ID="btnMeasureFieldRemove" runat="server" Text="<" Width="60px" OnClick="MeasureFieldRemove_Click">
				</dx:ASPxButton>
			</div>
		</td>
		<td>
			<dx:ASPxListBox ID="lbSelectedMetricFields" runat="server" ClientIDMode="AutoID" 
				Width="100%" Height="120px" AutoPostBack="True" 
				onselectedindexchanged="lbSelectedMetricFields_SelectedIndexChanged">
				<Columns>
					<dx:ListBoxColumn Caption="Field Name" FieldName="Caption" />
					<dx:ListBoxColumn Caption="Display Name" FieldName="DisplayName" />
					<dx:ListBoxColumn Caption="Calc" FieldName="Calc" />
					<dx:ListBoxColumn Caption="Sort" FieldName="Sort" Width="40px" />
				</Columns>
			</dx:ASPxListBox>
		</td>
	</tr>
</table>
</fieldset>
<fieldset style="min-width:1024px">
<legend>Filters</legend>
<asp:Panel ID="filterContainer" runat="server" style="margin-left:-3px"></asp:Panel>
<dx:ASPxButton ID="btnAddFilter" runat="server" Text="Add Filter" Width="90px" AutoPostBack="false"></dx:ASPxButton>
<dx:ASPxPopupMenu ID="popFilterMenu" runat="server" PopupElementID="btnAddFilter" PopupAction="LeftMouseClick" OnItemClick="popFilterMenu_Click">
<Items>
	<dx:MenuItem Text="Normal" Name="NORMAL"></dx:MenuItem>
	<dx:MenuItem Text="Numeric" Name="NUM"></dx:MenuItem>
	<dx:MenuItem Text="Time" Name="DATE"></dx:MenuItem>
</Items>
</dx:ASPxPopupMenu>
</fieldset>

<table cellpadding="0" cellspacing="0" style="margin-top:3px">
	<tr>
		<td>
			<dx:ASPxButton ID="btnPreview" runat="server" Text="Preview" Width="80px" AutoPostBack="false">
			<ClientSideEvents Click="function(s, e) { dsGridPreviewData.PerformCallback(); dsTabPreviewMDX.PerformCallback(); frmDSPreview.Show(); }" />
			</dx:ASPxButton>
		</td>
		<td style="padding-left: 3px;padding-right: 3px;">
			<dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="80px" 
				onclick="btnSave_Click">
			</dx:ASPxButton>
		</td>
		<td>
			<dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" Width="80px" >
			</dx:ASPxButton>
		</td>
	</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
<dx:ASPxLoadingPanel runat="server" ID="LoadingPanel1">
</dx:ASPxLoadingPanel>
<!-- Popup Content -->
<dx:ASPxPopupControl ID="frmDSPreview" runat="server" AllowDragging="True" ClientInstanceName="frmDSPreview"
	AllowResize="True" AppearAfter="100" CloseAction="CloseButton" 
	DisappearAfter="100" HeaderText="Preview Data Source" 
	PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" PopupVerticalOffset="20"
	Width="900px" ShowFooter="True" Height="500px">
	<FooterTemplate>
		<div style="float: right; padding: 5px 5px 5px 5px">
			<dx:ASPxButton ID="btnClose" runat="server" Text="Close" Width="70px" AutoPostBack="False">
				<ClientSideEvents Click="function(s, e) {
					frmDSPreview.Hide();
				}" />
			</dx:ASPxButton>
		</div>
	</FooterTemplate>
	<ContentStyle>
		<Paddings Padding="5px" />
	</ContentStyle>
	<ContentCollection>
		<dx:PopupControlContentControl ID="dsPreviewContent" runat="server">
			<dx:ASPxPageControl ID="dsPreviewTabs" runat="server" ActiveTabIndex="0" 
				Width="890px" Height="100%">
				<ContentStyle>
					<Paddings Padding="0" />
				</ContentStyle>
				<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
					<Paddings Padding="0" />
				</TabStyle>
				<Paddings Padding="0" />
				<TabPages>
					<dx:TabPage Name="dsTabPreviewData" Text="Data" ToolTip="Data">
						<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
							<Paddings Padding="3px" />
						</TabStyle>
						<ContentCollection>
							<dx:ContentControl ID="ContentControl4" runat="server" Height="500px">
								<dx:ASPxGridView ID="dsGridPreviewData" runat="server" 
									ClientInstanceName="dsGridPreviewData" AutoGenerateColumns="False" 
									OnCustomCallback="dsGridPreviewData_CustomCallback" 
									OnCustomUnboundColumnData="dsGridPreviewData_CustomUnboundColumnData" 
									OnPageIndexChanged="dsGridPreviewData_PageIndexChanged"
									Width="100%">
								<ClientSideEvents EndCallback="function(s, e){ frmDSPreview.Show(); }" />
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
					<dx:TabPage Name="dsTabPreviewMDX" Text="MDX Query" ToolTip="MDX Query">
						<TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
							<Paddings Padding="3px" />
						</TabStyle>
						<ContentCollection>
							<dx:ContentControl ID="ContentControl5" runat="server">
								<dx:ASPxCallbackPanel ID="dsCallbackPanel" runat="server">
									<PanelCollection>
										<dx:PanelContent ID="dsPanelContent" runat="server">
											<dx:ASPxMemo ID="txtDSPreviewMDX" runat="server" Height="500px" Width="100%" ReadOnly="true">
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
<script type="text/javascript">
	var prm = Sys.WebForms.PageRequestManager.getInstance();
	prm.add_initializeRequest(InitializeRequest);
	prm.add_endRequest(EndRequest);
	var loadingPanel = <%=LoadingPanel1.ClientID %>;
	function InitializeRequest(sender, args) {
		if (prm.get_isInAsyncPostBack()) {
			args.set_cancel(true);
		}
		if (loadingPanel)
		{
			loadingPanel.Show();
		}
	}
	function EndRequest(sender, args) {
		if (loadingPanel)
		{
			loadingPanel.Hide();
		}
	}
</script>
