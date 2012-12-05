<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcKPISetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcKPISetting" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $(".numericInput").autoNumeric({ aSep: ',', aDec: '.', mDec: '0', vMax: '999999999999999999' });
        $(".numericInput").css("text-align", "right");
    });
    // Lấy Mã KPI hiện tại đang được chọn.
    function Get_CurKPICode() {
        var ret = KPI_cboKPI.GetValue();
        return (ret == null) ? '' : ret.toString();
    }
    function NormalFilter_Get_ValidMsg() {
//        try {
//            alert(typeof (NormalFilter_cbbKeyField));
//            if (typeof (NormalFilter_cbbKeyField) != 'undefined' && CStr(NormalFilter_cbbKeyField.GetValue()).length == 0) {
//                NormalFilter_cbbKeyField.Focus();
//                return 'Please select key field of normal filter!\r\n';
//            }
//        } catch (err) { }
        return '';
    }
    function NumFilter_Get_ValidMsg() {
//        try {
//            if (typeof (NumFilter_cbbKeyField) != 'undefined' && CStr(NumFilter_cbbKeyField.GetValue()).length == 0) {
//                NumFilter_cbbKeyField.Focus();
//                return 'Please select key field of num filter!\r\n';
//            }
//        } catch (err) { }
        return '';
    }
    function TimeFilter_Get_ValidMsg() {
//        try {
//            if (typeof (TimeFilter_cbbKeyField) != 'undefined' && CStr(TimeFilter_cbbKeyField.GetValue()).length == 0) {
//                TimeFilter_cbbKeyField.Focus();
//                return 'Please select key field of time filter!\r\n';
//            }
//        } catch (err) { }
        return '';
    }
    function KPIDim_Get_ValidMsg() {
//        try {
//            if (typeof(KPIDim_cboField) == 'undefined')
//                return 'Dimensions is Required!\r\n';
//            if (CStr(KPIDim_cboField.GetValue()).length == 0) {
//                KPIDim_cboField.Focus();
//                return 'Please select a dimension field!\r\n';
//            }
//        } catch (err) { }
        return '';
    }
    function KPIMeasure_Get_ValidMsg() {
//        try {
//            if (typeof(KPIMeasure_cboField) == 'undefined')
//                return 'Measures is Required!\r\n';
//            if (CStr(KPIMeasure_cboField.GetValue()).length == 0) {
//                KPIMeasure_cboField.Focus();
//                return 'Please select a measure field!\r\n';
//            }
//        } catch (err) { }
        return '';
    }
    function KPIContext_Get_ValidMsg() {
//        try {
//            if (typeof (KPIContext_cboField) != 'undefined' && CStr(KPIContext_cboField.GetValue()).length == 0) {
//                KPIContext_cboField.Focus();
//                return 'Please select a context measure field!\r\n';
//            }
//        } catch (err) { }
        return '';
    }
    // Gọi các hàm valid từ các usercontrol bên trong.
    function KPI_Get_RefValidMsg() {
        var msg = KPIDim_Get_ValidMsg() + KPIMeasure_Get_ValidMsg() + KPIContext_Get_ValidMsg();
        msg += NormalFilter_Get_ValidMsg() + NumFilter_Get_ValidMsg() + TimeFilter_Get_ValidMsg();
        return msg;
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

<table>
    <tr>
        <td colspan="4" class="titleBox_AgLeft">KPI DEFINATION</td>
    </tr>        
</table>

<!--General-->
<fieldset style="padding:3px">
    <legend>General</legend>
    <dx:ASPxCallbackPanel ID="cbp_Header" ClientInstanceName="KPI_cbp_Header" 
        runat="server" Width="100%" oncallback="cbp_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent_Header" runat="server" SupportsDisabledAttribute="True">                
                <asp:UpdatePanel ID="upp_Header" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Display Name</td>
                                <td style="width:250px;">
                                    <dx:ASPxTextBox ID="txtKPIDisplayName" ClientInstanceName="KPI_txtKPIDisplayName" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td >KPIs</td>
                                <td>
                                    <dx:ASPxComboBox ID="cboKPI" runat="server" AutoPostBack="true" 
                                        ClientInstanceName="KPI_cboKPI" 
                                        onvaluechanged="cbo_ValueChanged" Width="100%">
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnNewKPI" runat="server" AutoPostBack="true"
                                        onclick="btn_Click" Text="New" Width="50px" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Preferred Visualization</td>
                                <td>
                                    <dx:ASPxComboBox ID="cboCtrlType" runat="server" AutoPostBack="true" 
                                        ClientInstanceName="KPI_cboCtrlType" onvaluechanged="cbo_ValueChanged" 
                                        Width="100%" ShowImageInEditBox="True">
                                    </dx:ASPxComboBox>
                                </td>
                                <td>Control</td>
                                <td style="width:250px;">
                                    <dx:ASPxComboBox ID="cboCtrl" runat="server" AutoPostBack="false" 
                                        ClientInstanceName="KPI_cboCtrl" Height="20px" ShowImageInEditBox="True" Width="100%">
                                        <ItemStyle Height="20px" />
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <fieldset style="padding:3px">
                                        <legend>Range</legend>
                                        <table>
                                            <tr>
                                                <td>Min value</td>
                                                <td>
                                                    <asp:TextBox ID="txtMinValue" runat="server" AutoPostBack="true"
                                                        Width="80px" CssClass="numericInput" Text="0"/>
                                                </td>
                                                <td>
                                                    <dx:ASPxCheckBox ID="chkNoMinValue" runat="server" AutoPostBack="false"
                                                        Font-Names="Arial" Font-Size="10pt" Text="No min value">
                                                        <ClientSideEvents CheckedChanged="function(s,e){
                                                            //chartPreview.PerformCallback('chkNoMinValue');
                                                        }" />
                                                    </dx:ASPxCheckBox>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td>Max value</td>
                                                <td>
                                                    <asp:TextBox ID="txtMaxValue" runat="server" AutoPostBack="true" 
                                                        Width="80px" CssClass="numericInput" Text="1000"/>
                                                </td>
                                                <td>
                                                    <dx:ASPxCheckBox ID="chkNoMaxValue" runat="server" AutoPostBack="false" 
                                                        Font-Names="Arial" Font-Size="10pt" Text="No max value">
                                                        <ClientSideEvents CheckedChanged="function(s,e){
                                                            //chartPreview.PerformCallback('chkNoMaxValue');
                                                        }" />
                                                    </dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</fieldset>

<!--Dimensions, Measures and Filters, Contextual Metrics-->
<dx:ASPxPageControl ID="tabCtrl_PortletSetting" ClientInstanceName="tabCtrl_PortletSetting"
	runat="server" ActiveTabIndex="0" Width="100%" Font-Names="Arial" 
    Font-Size="9pt" ClientIDMode="AutoID">
	<Border BorderStyle="None" />
	<ContentStyle><Paddings Padding="0px" /></ContentStyle>
	<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="0px" /></TabStyle>
	<Paddings Padding="0px" />
	<TabPages>
		<dx:TabPage Name="tabPage_Dimensions" Text="Dimensions" ToolTip="Define dimensions.">
			<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			<ContentCollection>
				<dx:ContentControl ID="ContentControl3" runat="server">
                    <asp:UpdatePanel ID="upp_Dimension" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <div id="ctrl_Dimensions" runat="server"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddDimension" runat="server" Text="Add dimension" 
                                            Width="120px" OnClick="btn_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
				</dx:ContentControl>
			</ContentCollection>
		</dx:TabPage>
		<dx:TabPage Name="tabPage_MeasuresFilters" Text="Measures and Filters" ToolTip="Define measures and filters.">
			<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			<ContentCollection>
				<dx:ContentControl ID="ContentControl4" runat="server">
                    <asp:UpdatePanel ID="upp_Measures" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <div id="ctrl_Measures" runat="server"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddMeasure" runat="server" Text="Add measures" 
                                            Width="120px" OnClick="btn_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><hr /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="ctrl_KPIFilters" runat="server"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddKPIFilter" runat="server" Text="Add KPI Filter" AutoPostBack="False"/>
                                        <dx:ASPxPopupMenu ID="popMenAddFilter" runat="server" PopupElementID="btnAddKPIFilter" 
                                            onitemclick="popMenAddFilter_ItemClick" PopupAction="LeftMouseClick">
                                            <Items>
                                                <dx:MenuItem Text="Normal" Name="NORMAL"></dx:MenuItem>
                                                <dx:MenuItem Text="Numeric" Name="NUM"></dx:MenuItem>
                                                <dx:MenuItem Text="Time" Name="DATE"></dx:MenuItem>
                                            </Items>
                                        </dx:ASPxPopupMenu>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
				</dx:ContentControl>
			</ContentCollection>
		</dx:TabPage>
		<dx:TabPage Name="tabPage_ContextMetrics" Text="Contextual Metrics" ToolTip="Define contextual metrics.">
			<TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			<ContentCollection>
				<dx:ContentControl ID="ContentControl5" runat="server">
                    <asp:UpdatePanel ID="upp_ContextMetrics" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <div id="ctrl_ContextMetric" runat="server"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddContextMetric" runat="server" Text="Add contextual metric" 
                                            Width="150px" AutoPostBack="false" />
                                        <dx:ASPxPopupMenu ID="popMenAddCalcField" runat="server" PopupElementID="btnAddContextMetric" 
                                            onitemclick="popMenAddCalcField_ItemClick" PopupAction="LeftMouseClick">
                                            <Items>
                                                <dx:MenuItem Text="Normal contextual metric" Name="Normal"></dx:MenuItem>
                                                <dx:MenuItem Text="Calculation contextual metric" Name="Calc"></dx:MenuItem>
                                            </Items>
                                        </dx:ASPxPopupMenu>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
				</dx:ContentControl>
			</ContentCollection>
		</dx:TabPage>
	</TabPages>
</dx:ASPxPageControl>
    
<!--Buttons Command-->
<table>
    <tr>
        <td>
            <dx:ASPxButton ID="btnPreview" runat="server" Text="Preview" Width="100px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
                    var errMsg = '';
                    var dsCode = Get_CurDSCode();
                    var kpiCode = Get_CurKPICode();                    
                    if(dsCode.length == 0 && kpiCode.length == 0) // Reference validation...
                    {                        
                        errMsg = 'Please select a datasource on tabpage [Data source]!\r\n';
                        Portlet_SetActiveTab(0);
                    }
                    else // Internal validation...
                    {
                        errMsg += KPI_Get_RefValidMsg();
                    }
                    // Hiển thị thông diệp lỗi (nếu có)...
                    if(errMsg.length != 0)
                        alert(errMsg);
                    else
                    {
                        KPI_cbpPreViewSQL.PerformCallback();
                        KPI_gvPreViewData.PerformCallback();
                        KPI_tabCtrl_PreView.SetActiveTab(KPI_tabCtrl_PreView.GetTab(0));
                    }
                }" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {                        
                    var errMsg = '';
                    var dsCode = Get_CurDSCode();
                    var kpiCode = Get_CurKPICode();
                    if(dsCode.length == 0 && kpiCode.length == 0) // Reference validation...
                    {
                        errMsg = 'Please select a datasource on tabpage [Data source]!';
                        Portlet_SetActiveTab(0);
                    }
                    else // Internal validation...
                    {
                        var displayName = KPI_txtKPIDisplayName.GetValue();
                        if(displayName == null || displayName.toString().length == 0)
                        {
                            errMsg += 'Please enter for your KPI display name!\r\n';
                            KPI_txtKPIDisplayName.Focus();
                        }
                        errMsg += KPI_Get_RefValidMsg();
                    }
                    // Hiển thị thông diệp lỗi (nếu có)...
                    if(errMsg.length != 0)
                        alert(errMsg);
                    else
	                    KPI_cbpSavingMsg.PerformCallback();
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
            <dx:ASPxCallbackPanel ID="cbpSavingMsg" ClientInstanceName="KPI_cbpSavingMsg" 
                runat="server" Width="100%" oncallback="cbp_Callback">
                <ClientSideEvents EndCallback="function(s, e){
                    KPI_cbp_Header.PerformCallback();
                }" />
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
<dx:ASPxPopupControl ID="frmPreView" ClientInstanceName="KPI_frmPreView" runat="server" AppearAfter="100" 
    CloseAction="CloseButton" DisappearAfter="100" Font-Names="Arial" AllowResize="true"
    Font-Size="9pt" HeaderText="Preview KPI" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" AllowDragging="true" 
    ShowFooter="true" Width="900px" Height="100%" ShowLoadingPanel="true" >
    <FooterTemplate>
        <div style="float:right; padding:5px 5px 5px 5px">
            <dx:ASPxButton ID="btnClose" runat="server" Text="Close" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                KPI_frmPreView.Hide();
                }" />
            </dx:ASPxButton>
        </div>
    </FooterTemplate>
    <ContentStyle><Paddings Padding="5px" />
    <Paddings Padding="5px"></Paddings>
    </ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="popupPreView" runat="server">                            
            <dx:ASPxPageControl ID="tabCtrl_PreView" runat="server" ActiveTabIndex="1" Width="100%" 
		        Font-Names="Arial" Font-Size="9pt" ClientInstanceName="KPI_tabCtrl_PreView">
	            <Border BorderStyle="None" />        
	            <ContentStyle><Paddings Padding="0px" /></ContentStyle>
	            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="0px" /></TabStyle>
	            <Paddings Padding="0px" />
	            <TabPages>              
		            <dx:TabPage Name="tabCtrl_PreViewData" Text="Data" ToolTip="Data">
			            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            <Paddings Padding="3px" />
                        </TabStyle>
			            <ContentCollection>
				            <dx:ContentControl ID="ContentControl1" runat="server">				
                                <dx:ASPxGridView ID="gvPreViewData" ClientInstanceName="KPI_gvPreViewData" runat="server"
                                    AutoGenerateColumns="false" Font-Names="Arial" Font-Size="9pt" Width="100%"
                                    OnCustomCallback="gvPreViewData_CustomCallback"
                                    OnPageIndexChanged="gvPreViewData_PageIndexChanged" 
                                    OnCustomUnboundColumnData="gvPreViewData_CustomUnboundColumnData">
                                    <ClientSideEvents EndCallback="function(){
                                        KPI_frmPreView.Show();
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
                                <dx:ASPxCallbackPanel ID="cbpPreViewSQL" ClientInstanceName="KPI_cbpPreViewSQL" runat="server" Width="100%" oncallback="cbp_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent_PreViewSQL" runat="server" SupportsDisabledAttribute="True">
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