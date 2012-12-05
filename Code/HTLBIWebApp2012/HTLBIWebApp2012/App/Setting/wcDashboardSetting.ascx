<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcDashboardSetting.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcDashboardSetting" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx1" %>

<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<style type="text/css">
    *{margin:0; padding:0}
</style>

<script type="text/javascript" language="javascript">
    var isNeedValid = false;
    function Valid_OnSubmit() {
        if (!isNeedValid) return true;
        isNeedValid = false;
        try {
            var errMsg = '';
            if (DashboardSetting_cboTemplate.GetValue() == null || DashboardSetting_cboTemplate.GetValue().toString().length == 0) {
                errMsg = 'Please select at least a template for the dashboard presentation.\r\n';
                DashboardSetting_cboTemplate.Focus();
            }
            if (DashboardSetting_lbxUsingPortlet.GetItemCount() == 0) {
                errMsg += 'Please get using portlets from available portlets list.';
                DashboardSetting_lbxUsingPortlet.Focus();
            }
            if (errMsg != '') {
                alert(errMsg);
                return false;
            }
            else
                return true;
        }
        catch (err) { alert(err); }
        return false;
    }
</script>

<div id="divNavigation">
    <ul>
        <li style="display:inline"><dx:ASPxHyperLink runat="server" ID="DataSource" ClientInstanceName="DataSource" Text="Data Source" /></li>
        <li style="display:inline">&nbsp;|&nbsp;</li>
        <li style="display:inline"><dx:ASPxHyperLink runat="server" ID="ASPxHyperLink1" ClientInstanceName="DataSource" Text="KPIs" /></li>
        <li style="display:inline">&nbsp;|&nbsp;</li>
        <li style="display:inline"><dx:ASPxHyperLink runat="server" ID="ASPxHyperLink2" ClientInstanceName="DataSource" Text="Layouts" /></li>
    </ul>
</div>

<div id="divWarehouseFilter">
    <dx1:ASPxComboBox runat="server" ID="cboDataDW" AutoPostBack="true" 
        onvaluechanged="cboDataDW_ValueChanged" />
</div>

<table>
    <tr>
        <td style="width:250px">
            <dx:ASPxLabel ID="lblLstTitle" runat="server" Text="Dashboard list" 
                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <dx:ASPxListBox ID="lbxDashboard" runat="server" AutoPostBack="false" 
                        ClientInstanceName="DashboardSetting_lbxDashboard" Height="326px"
                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"                                     
                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100%">
                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                        </LoadingPanelImage>
                        <LoadingPanelStyle ImageSpacing="5px">
                        </LoadingPanelStyle>
                    </dx:ASPxListBox>
                </ContentTemplate>
            </asp:UpdatePanel>       
        </td>
        <td style="vertical-align:top">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <dx:ASPxButton ID="btnView" runat="server" Text="View" OnClick="btn_Click" Width="70px"></dx:ASPxButton>
                    <dx:ASPxButton ID="btnNew" runat="server" Text="New" OnClick="btn_Click" Width="70px"></dx:ASPxButton>
                    <dx:ASPxButton ID="btnEdit" runat="server" Text="Edit" OnClick="btn_Click" Width="70px"></dx:ASPxButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td></td>
        <td style="vertical-align:top">        
            <dx:ASPxPageControl ID="tabCtrl_DashboardSetting" ClientInstanceName="tabCtrl_DashboardSetting"
                runat="server" ActiveTabIndex="0" Width="100%" Font-Names="Arial" 
                Font-Size="9pt" ClientIDMode="AutoID">
	            <Border BorderStyle="None" />
	            <ContentStyle><Paddings Padding="5px" /></ContentStyle>
	            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="0px" /></TabStyle>
	            <Paddings Padding="0px" />
	            <TabPages>
		            <dx:TabPage Name="tabPage_Templates" Text="Templates">
			            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			            <ContentCollection>
				            <dx:ContentControl ID="ContentControl1" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
				                        <table>
                                            <tr>
                                                <td>Display name</td>
                                                <td style="width:200px">
                                                    <dx:ASPxTextBox ID="txtDisplayName" ClientInstanceName="DashboardSetting_txtDisplayName" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>                            
                                                </td>
                                                <td style="width:30px"></td>
                                                <td style="width:200px"></td>
                                            </tr>
                                            <tr>
                                                <td>Template</td>
                                                <td>
                                                    <dx:ASPxComboBox ID="cboTemplate" ClientInstanceName="DashboardSetting_cboTemplate" 
                                                        runat="server" Height="20px" Width="100%">
                                                    </dx:ASPxComboBox>                            
                                                </td>
                                                <td></td>
                                                <td>
                                                    <dx:ASPxCheckBox ID="chkIsDefault" runat="server" Font-Names="Arial" 
                                                        Font-Size="9pt" Text="Is Default">
                                                    </dx:ASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>Available Portlet</td>
                                                <td></td>
                                                <td>Using Portlet</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <dx:ASPxListBox ID="lbxAvailablePortlet" runat="server" ClientIDMode="AutoID" 
                                                        ClientInstanceName="DashboardSetting_lbxAvailablePortlet" 
                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                        Height="180px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                                                        Width="100%">
                                                        <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif">
                                                        </LoadingPanelImage>
                                                        <LoadingPanelStyle ImageSpacing="5px">
                                                        </LoadingPanelStyle>
                                                    </dx:ASPxListBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnIn" runat="server" Text=">" OnClick="btn_Click" Width="30px">
                                                    </dx:ASPxButton>
                                                    <dx:ASPxButton ID="btnOut" runat="server" Text="<" OnClick="btn_Click" Width="30px">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxListBox ID="lbxUsingPortlet" runat="server" ClientIDMode="AutoID" 
                                                        ClientInstanceName="DashboardSetting_lbxUsingPortlet" 
                                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                                        Height="180px" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" 
                                                        Width="100%">
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
				            </dx:ContentControl>
			            </ContentCollection>
		            </dx:TabPage>
		            <dx:TabPage Name="tabPage_Filter" Text="Filters">
			            <TabStyle HorizontalAlign="Center" VerticalAlign="Middle"><Paddings Padding="3px" /></TabStyle>
			            <ContentCollection>
				            <dx:ContentControl ID="ContentControl2" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td><div id="ctrl_DashboardFilters" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnAddDashboardFilter" runat="server" 
                                                        Text="Add Dashboard Filter" OnClick="btn_Click" >
                                                    </dx:ASPxButton>
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
            <!--Button command-->
            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btn_Click">
                            <ClientSideEvents Click="function(s,e){isNeedValid=true;}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers><asp:AsyncPostBackTrigger ControlID="btnSave" /></Triggers>
                            <ContentTemplate>
                                <dx:ASPxLabel ID="lblSavingMsg" runat="server" ></dx:ASPxLabel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>        
            </table>            
        </td>
    </tr>
</table>