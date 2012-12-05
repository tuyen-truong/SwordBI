<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcDatasourceSetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcDatasourceSetting" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<script language="javascript" type="text/javascript">
    function Get_CurDSCode() {
        var ret = DS_cboDatasource.GetValue();
        return (ret == null) ? '' : ret.toString();
    }
</script>

<style type="text/css">
    .titleBox
    {
        text-align:center;
        font: normal bolder 15pt Arial;
    }
    .titleBox_AgLeft
    {
        text-align:left;
        font: normal bolder 15pt Arial;
    }    
    .titleBox1
    {
        text-align:center;
        font: normal bolder 10pt Arial;
    }    
    .titleBox1_AgLeft
    {
        text-align:left;
        font: normal bolder 10pt Arial;
    }      
</style>

<!--General Informations-->
<dx:ASPxCallbackPanel ID="cbp_Header" ClientInstanceName="DS_cbp_Header" 
runat="server" Width="100%" oncallback="cbp_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                
            <asp:UpdatePanel ID="upp_Header" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td colspan="3" class="titleBox_AgLeft">DATASOURCE DEFINATION</td>
                        </tr>
                        <tr>
                            <td >Display Name</td>
                            <td style="width:250px;">
                                <dx:ASPxTextBox ID="txtDisplayNameDS" ClientInstanceName="DS_txtDisplayNameDS" 
                                    runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Data WareHouse</td>
                            <td>
                                <dx:ASPxComboBox ID="cboDataDW" runat="server" AutoPostBack="true" 
                                    ClientInstanceName="DS_cboDataDW" onvaluechanged="cbo_ValueChanged" 
                                    Width="100%">
                                </dx:ASPxComboBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Data Source</td>
                            <td>
                                <dx:ASPxComboBox ID="cboDatasource" runat="server" AutoPostBack="true" 
                                    ClientInstanceName="DS_cboDatasource" onvaluechanged="cbo_ValueChanged" 
                                    Width="100%">
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnNewDS" runat="server" AutoPostBack="true" Text="New" 
                                    Width="50px" ClientIDMode="AutoID" onclick="btn_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>

<!--Content Of Select Clause-->
<fieldset style="padding:3px">
    <legend>Query Information</legend>
    <asp:UpdatePanel ID="upp_SelectClause" runat="server">
        <ContentTemplate>
            <table style="padding:0px; border-width:0px">
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td><b>Fields</b></td>
                    <td></td>
                    <td style="width:120px"></td>
                    <td colspan="2">
                        <dx:ASPxTextBox ID="txtDisplayName0" runat="server" AutoPostBack="true" 
                            onvaluechanged="txt_ValueChanged" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width:80px">
                        <dx:ASPxComboBox ID="cboOrderBy0" runat="server" AutoPostBack="True" 
                            onvaluechanged="cbo_ValueChanged" SelectedIndex="0" Width="100%">
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px; vertical-align:top">
                        <dx:ASPxListBox ID="lbxField" runat="server" ClientInstanceName="DS_lbxField" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            Height="90px" Width="100%"
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" >
                            <Columns>
                                <dx:ListBoxColumn Caption="Field name" FieldName="ColName" />
                                <dx:ListBoxColumn Caption="Display name" FieldName="ColAliasVI" />
                            </Columns>
                            <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                        </dx:ASPxListBox>
                    </td>
                    <td style="width:50px">
                        <dx:ASPxButton ID="btnIn_1" runat="server" onclick="btn_Click" Text="&gt;" 
                            Width="100%">
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnOut_1" runat="server" onclick="btn_Click" 
                            Text="&lt;" Width="100%">
                        </dx:ASPxButton>
                    </td>
                    <td colspan="4" style="width:400px">
                        <dx:ASPxListBox ID="lbxFieldSelected" runat="server" Width="100%" Height="90px"
                            ClientInstanceName="DS_lbxFieldSelected" AutoPostBack="True"
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"                                                                 
                            OnSelectedIndexChanged="lbx_SelectedIndexChanged">
                            <Columns>
                                <dx:ListBoxColumn Caption="Field name" FieldName="ColName" Width="120px" />
                                <dx:ListBoxColumn Caption="Display name" FieldName="ColAliasVI" Width="200px" />
                                <dx:ListBoxColumn Caption="Sort" FieldName="OrderName" Width="80px" />
                            </Columns>
                            <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                        </dx:ASPxListBox>
                    </td>
                </tr>
                <tr>
                    <td><b>Metrics</b></td>
                    <td></td>
                    <td style="width:120px"></td>
                    <td style="width:120px">
                        <dx:ASPxTextBox ID="txtDisplayName" runat="server" AutoPostBack="true" 
                            onvaluechanged="txt_ValueChanged" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width:80px">
                        <dx:ASPxComboBox ID="cboFuncs" runat="server" Width="100%" SelectedIndex="0"
                            AutoPostBack="true" onvaluechanged="cbo_ValueChanged">
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width:80px">
                        <dx:ASPxComboBox ID="cboOrderBy1" runat="server" AutoPostBack="True" 
                            onvaluechanged="cbo_ValueChanged" SelectedIndex="0" Width="100%">
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px">
                        <dx:ASPxListBox ID="lbxMetricField" runat="server" 
                            ClientInstanceName="DS_lbxMetricField" 
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            Height="80px" Width="100%"
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" >
                            <Columns>
                                <dx:ListBoxColumn Caption="Field name" FieldName="ColName" />
                                <dx:ListBoxColumn Caption="Display name" FieldName="ColAliasVI" />
                            </Columns>
                            <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                        </dx:ASPxListBox>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnIn_2" runat="server" onclick="btn_Click" Text="&gt;" 
                            Width="100%">
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnOut_2" runat="server" onclick="btn_Click" 
                            Text="&lt;" Width="100%">
                        </dx:ASPxButton>
                    </td>
                    <td colspan="4" style="width:400px">
                        <dx:ASPxListBox ID="lbxMetricFieldSelected" runat="server" Width="100%" Height="80px"
                            ClientInstanceName="DS_lbxMetricFieldSelected" AutoPostBack="true"
                            CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                            OnSelectedIndexChanged="lbx_SelectedIndexChanged"
                            SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                            <Columns>                                    
                                <dx:ListBoxColumn Caption="Field name" FieldName="Field.ColName" Width="120px" />
                                <dx:ListBoxColumn Caption="Display name" FieldName="FieldAlias" Width="120px" />
                                <dx:ListBoxColumn Caption="Calc" FieldName="FuncName" Width="80px" />
                                <dx:ListBoxColumn Caption="Sort" FieldName="Field.OrderName" Width="80px" />
                            </Columns>
                            <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                        </dx:ASPxListBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>

<!--Filter Control-->
<fieldset style="padding:3px">
    <legend>Filters</legend>
    <asp:UpdatePanel ID="upp_Filter" runat="server">
        <ContentTemplate>
            <div id="ctrlCollect" runat="server"></div>
            <dx:ASPxButton ID="btnAddFilter" runat="server" Text="Add filter" Width="70px" AutoPostBack="False"></dx:ASPxButton>
            <dx:ASPxPopupMenu ID="popMen" runat="server" PopupElementID="btnAddFilter" 
                onitemclick="popMen_ItemClick" PopupAction="LeftMouseClick">
                <Items>
                    <dx:MenuItem Text="Normal" Name="NORMAL"></dx:MenuItem>
                    <dx:MenuItem Text="Numeric" Name="NUM"></dx:MenuItem>
                    <dx:MenuItem Text="Time" Name="DATE"></dx:MenuItem>
                </Items>
            </dx:ASPxPopupMenu>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>

<!--Buttons Command-->
<table>
    <tr>
        <td>
            <dx:ASPxButton ID="btnPreview" runat="server" Text="Preview" Width="100px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
                    var errMsg = '';
                    var fieldSelCount = DS_lbxFieldSelected.GetItemCount();
                    var fieldMetricCount = DS_lbxMetricField.GetItemCount();
                    if(fieldSelCount==0)
                        errMsg = errMsg + 'Please select at least a field!\r\n';
                    if(fieldMetricCount==0)
                        errMsg = errMsg + 'The '+DS_cboDataDW.GetValue()+' Fact data unavailable!\r\n';

                    if(errMsg.length != 0)
                    {
                        alert(errMsg);
                        DS_txtDisplayNameDS.Focus();
                    }
                    else
                    {
                        DS_cbpPreViewSQL.PerformCallback();
                        DS_gvPreViewData.PerformCallback();
                        DS_tabCtrl_PreView.SetActiveTab(DS_tabCtrl_PreView.GetTab(0));
                    }
                }" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
                    var errMsg = '';
                    var displayName = DS_txtDisplayNameDS.GetValue();
                    if(displayName == null || displayName.toString().length == 0)
                        errMsg = errMsg + 'Please enter for your datasouse display name!\r\n';
                    var fieldSelCount = DS_lbxFieldSelected.GetItemCount();
                    var fieldMetricCount = DS_lbxMetricField.GetItemCount();
                    if(fieldSelCount==0)
                        errMsg = errMsg + 'Please select at least a field!\r\n';
                    if(fieldMetricCount==0)
                        errMsg = errMsg + 'The '+DS_cboDataDW.GetValue()+' Fact data unavailable!\r\n';

                    if(errMsg.length != 0)
                    {
                        alert(errMsg);
                        DS_txtDisplayNameDS.Focus();
                    }
                    else
	                    DS_cbpSavingMsg.PerformCallback();
                         
                }" />
            </dx:ASPxButton>
        </td>
        <td style="text-align:right; float:right" align="right">
            <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                //alert('Chưa cài đặt.');
                }" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxCallbackPanel ID="cbpSavingMsg" ClientInstanceName="DS_cbpSavingMsg" 
                runat="server" Width="100%" oncallback="cbp_Callback">
                <ClientSideEvents EndCallback="function(s, e){
                    DS_cbp_Header.PerformCallback();
                }" />
                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
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
<dx:ASPxPopupControl ID="frmPreView" ClientInstanceName="DS_frmPreView" runat="server" AppearAfter="100" 
    CloseAction="CloseButton" DisappearAfter="100" Font-Names="Arial" AllowResize="true"
    Font-Size="9pt" HeaderText="Preview Datasource" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" AllowDragging="true" 
    ShowFooter="true" Width="900px" Height="100%" ShowLoadingPanel="true" >
    <FooterTemplate>
        <div style="float:right; padding:5px 5px 5px 5px">
            <dx:ASPxButton ID="btnClose" runat="server" Text="Close" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                DS_frmPreView.Hide();
                }" />
            </dx:ASPxButton>
        </div>
    </FooterTemplate>
    <ContentStyle>
        <Paddings Padding="5px" />
    </ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="popupPreView" runat="server">                            
            <dx:ASPxPageControl ID="tabCtrl_PreView" runat="server" ActiveTabIndex="1" Width="100%" 
		        Font-Names="Arial" Font-Size="9pt" ClientInstanceName="DS_tabCtrl_PreView">
	            <Border BorderStyle="None" />        
	            <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
	            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings Padding="0px" />
                </TabStyle>
	            <Paddings Padding="0px" />
	            <TabPages>
		            <dx:TabPage Name="tabCtrl_PreViewData" Text="Data" ToolTip="Data">
			            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            <Paddings Padding="3px" />
                        </TabStyle>
			            <ContentCollection>
				            <dx:ContentControl ID="ContentControl1" runat="server">				
                                <dx:ASPxGridView ID="gvPreViewData" ClientInstanceName="DS_gvPreViewData" runat="server"
                                    AutoGenerateColumns="false" Font-Names="Arial" Font-Size="9pt" Width="100%"
                                    OnCustomCallback="gvPreViewData_CustomCallback"
                                    OnPageIndexChanged="gvPreViewData_PageIndexChanged" 
                                    OnCustomUnboundColumnData="gvPreViewData_CustomUnboundColumnData">
                                    <ClientSideEvents EndCallback="function(){
                                        frmPreView.Show();
                                    }"></ClientSideEvents>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="#" FieldName="#" Name="colLine" PropertiesTextEdit-DisplayFormatString="#,##0"
                                            ShowInCustomizationForm="True" VisibleIndex="0" UnboundType="Integer">
                                            <PropertiesTextEdit DisplayFormatString="#,##0"></PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="false" />
                                    <SettingsPager PageSize="20"></SettingsPager>                                        
                                    <ClientSideEvents EndCallback="function(){
                                        DS_frmPreView.Show();
                                    }" />
                                </dx:ASPxGridView>
				            </dx:ContentControl>
			            </ContentCollection>
		            </dx:TabPage>
		            <dx:TabPage Name="tabCtrl_PreViewSQL" Text="MDX Query" ToolTip="MDX Query">
			            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            <Paddings Padding="3px" />
                        </TabStyle>
			            <ContentCollection>
				            <dx:ContentControl ID="ContentControl2" runat="server">
                                <dx:ASPxCallbackPanel ID="cbpPreViewSQL" ClientInstanceName="DS_cbpPreViewSQL" runat="server" Width="100%" oncallback="cbp_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxMemo ID="txtPreViewSQL" ReadOnly="true" runat="server" Height="500px" Width="100%"></dx:ASPxMemo>
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