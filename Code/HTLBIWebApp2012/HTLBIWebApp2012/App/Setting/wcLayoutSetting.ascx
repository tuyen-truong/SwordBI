<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcLayoutSetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcLayoutSetting" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxMenu" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

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

<script type="text/javascript" language="javascript">
    function Get_CurLayoutCount() {
        var ret = Layout_cboLayout.GetItemCount();
        return ret;
    }
    function Get_CurLayoutCode() {
        var ret = Layout_cboLayout.GetValue();
        return (ret == null) ? '' : ret.toString();
    }

    function IsValid_PropChart() {
        if (Layout_PropChart_cboAxisXField1.GetValue() == null || Layout_PropChart_cboAxisXField1.GetValue().toString().length == 0) {
            alert('please select X-field!');
            Layout_PropChart_cboAxisXField1.Focus();
            return false;
        }
        if (Layout_PropChart_lbxAxisYFieldSel.GetItemCount() == 0) {
            alert('please select at least a Y-field!');
            Layout_PropChart_lbxAxisYFieldSel.Focus();
            return false;
        }
        return true;
    }
    function IsValid_PropGauge() {
        if (Layout_PropGauge_cboMeasure.GetValue() == null || Layout_PropGauge_cboMeasure.GetValue().toString().length == 0) {
            alert('please select measure!');
            Layout_PropGauge_cboMeasure.Focus();
            return false;
        }
        return true;
    }
    function IsValid_PropGrid() {
        return true;
    }
    function IsValidInput() {
        var ctrlType = Layout_cboCtrlType.GetValue();
        return (
            (ctrlType == 'chart' && IsValid_PropChart()) ||
            (ctrlType == 'gauge' && IsValid_PropGauge()) ||
            (ctrlType == 'grid' && IsValid_PropGrid())
        );
    }
</script>

<!--General-->
<dx:ASPxCallbackPanel ID="cbp_Header" ClientInstanceName="Layout_cbp_Header" 
    runat="server" Width="100%" oncallback="cbp_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent_Header" runat="server" SupportsDisabledAttribute="True">                
            <asp:UpdatePanel ID="upp_Header" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td colspan="4" class="titleBox_AgLeft">LAYOUT DEFINATION</td>
                        </tr>
                        <tr>
                            <td>Display Name</td>
                            <td style="width:250px;">
                                <dx:ASPxTextBox ID="txtDisplayName" ClientInstanceName="Layout_txtWidgetDisplayName" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Layouts</td>
                            <td>
                                <dx:ASPxComboBox ID="cboLayout" ClientInstanceName="Layout_cboLayout" runat="server" Width="100%"
                                     AutoPostBack="true" onvaluechanged="cbo_ValueChanged">
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnNewLayout" runat="server" Text="New" Width="50px" onclick="btn_Click">
                                </dx:ASPxButton>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Control type</td>
                            <td>
                                <dx:ASPxComboBox ID="cboCtrlType" ClientInstanceName="Layout_cboCtrlType" runat="server" Width="100%"
                                    AutoPostBack="true" onvaluechanged="cbo_ValueChanged" ShowImageInEditBox="True">
                                </dx:ASPxComboBox>
                            </td>
                            <td>Control</td>
                            <td style="width:250px;">
                                <dx:ASPxComboBox ID="cboCtrl" ClientInstanceName="Layout_cboCtrl" runat="server" Height="20px" 
                                    AutoPostBack="True" onvaluechanged="cbo_ValueChanged" ShowImageInEditBox="True" Width="100%">
                                    <ItemStyle Height="20px" />
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>

<!--Properties by control-->
<fieldset style="padding:3px">
    <legend>Settings</legend>
    <asp:UpdatePanel ID="upp_Setting" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <div id="propCtrl" runat="server"></div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>

<!--Button command-->
<table>
    <tr>
        <td>
            <dx:ASPxButton ID="btnPreview" runat="server" Text="Preview" Width="100px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
                    try
                    {
                        if(IsValidInput())
                        {
                            Layout_cbpPreView.PerformCallback();
                        }
                    }
                    catch(err){alert(err);}
                }" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {                        
                    try
                    {
                        if(IsValidInput())
                            Layout_cbpSavingMsg.PerformCallback();
                    }
                    catch(err){alert(err);}
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
            <dx:ASPxCallbackPanel ID="cbpSavingMsg" ClientInstanceName="Layout_cbpSavingMsg" 
                runat="server" Width="100%" oncallback="cbp_Callback">
                <ClientSideEvents EndCallback="function(s, e){
                    Layout_cbp_Header.PerformCallback();
                }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLabel ID="lblSavingMsg" runat="server" ></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </td>
    </tr>        
</table>

<dx:ASPxPopupControl ID="frmPreView" ClientInstanceName="Layout_frmPreView" runat="server" AppearAfter="100" 
    CloseAction="CloseButton" DisappearAfter="100" Font-Names="Arial" AllowResize="true"
    Font-Size="9pt" HeaderText="Preview layout" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" AllowDragging="true" 
    ShowFooter="true" Width="900px" Height="100%" ShowLoadingPanel="true" >
    <FooterTemplate>
        <div style="float:right; padding:5px 5px 5px 5px">
            <dx:ASPxButton ID="btnClose" runat="server" Text="Close" Width="70px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                Layout_frmPreView.Hide();                        
                }" />
            </dx:ASPxButton>
        </div>
    </FooterTemplate>
    <ContentStyle><Paddings Padding="5px" /></ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="popupPreView" runat="server">
            <dx:ASPxCallbackPanel ID="cbpPreView" ClientInstanceName="Layout_cbpPreView" runat="server" Width="100%" oncallback="cbp_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
	                Layout_frmPreView.Show();                 
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