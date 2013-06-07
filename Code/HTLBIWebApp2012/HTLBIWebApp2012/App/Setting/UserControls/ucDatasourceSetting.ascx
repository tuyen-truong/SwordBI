<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDatasourceSetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.ucDatasourceSetting" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<style type="text/css">
.ds-defination th
{
	 font-weight:normal;
	 text-align: left;
	 width: 150px;
	 white-space:nowrap;
	 padding-bottom: 5px;
}
.uc-title
{
	font-weight:bold !important;
	font-size: 90%;
}
</style>
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
						<dx:ASPxTextBox ID="txtFieldDispName" runat="server" Width="250px">
						</dx:ASPxTextBox>
					</td>
					<td style="width:80px; padding-left:3px">
						<dx:ASPxComboBox ID="cbFieldSort" runat="server" Width="80px">
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
				Width="100%" Height="212px">
				<Columns>
					<dx:ListBoxColumn Caption="Field Name" FieldName="Caption" />
					<dx:ListBoxColumn Caption="Display Name" FieldName="Caption" />
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
						<dx:ASPxTextBox ID="txtMetricDispName" runat="server" Width="250px">
						</dx:ASPxTextBox>
					</td>
					<td style="width:80px; padding-left:3px">
						<dx:ASPxComboBox ID="cbFuncs" runat="server" Width="80px">
						</dx:ASPxComboBox>
					</td>
					<td style="width:80px; padding-left:3px">
						<dx:ASPxComboBox ID="cbMetricSort" runat="server" Width="80px">
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
				Width="100%" Height="120px">
				<Columns>
					<dx:ListBoxColumn Caption="Field Name" FieldName="Caption" />
					<dx:ListBoxColumn Caption="Display Name" FieldName="Caption" />
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
