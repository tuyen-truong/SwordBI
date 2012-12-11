<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="DashboardEdit.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.DashboardEdit" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
<style type="text/css">
table th
{
    white-space: nowrap;
}
th.normal
{
    font-weight:normal;
}
fieldset
{
    padding: 5px 10px 5px;
    margin: 10px;
}
fieldset.dashboard-layout-style, fieldset.dashboard-filter
{
    width: 50%;
}
</style>
    <table style="margin: 10px;">
        <tr>
            <td>Display Name</td>
            <td><dx:ASPxTextBox ID="txtDashboardName" runat="server" ClientInstanceName="txtDashboardName" Width="250px"></dx:ASPxTextBox></td>
        </tr>
    </table>
    <fieldset class="dashboard-layout-style">
        <legend>Layout Style</legend>
        <table>
            <tr>
                <th style="vertical-align: middle; width: 150px;">
                    Two Pane
                </th>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="TwoPane_1" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image6" ImageUrl="~/Images/TwoPane_1.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="TwoPane_2" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image1" ImageUrl="~/Images/TwoPane_2.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
            </tr>
            <tr>
                <th style="vertical-align: middle; width: 150px">Three Pane</th>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_1" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image2" ImageUrl="~/Images/ThreePane_1.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_2" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image3" ImageUrl="~/Images/ThreePane_2.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_3" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image4" ImageUrl="~/Images/ThreePane_3.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_4" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image5" ImageUrl="~/Images/ThreePane_4.png" runat="server"
                        Width="96" Height="96" />
                </td>
            </tr>
            <tr style="padding-top: 50px;">
                <th style="vertical-align: middle; width: 150px;">Four Pane</th>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="FourPane_1" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image7" ImageUrl="~/Images/FourPane_1.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
                <td valign="middle">
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="margin: 10px; width:450px">
        <legend>Portlets</legend>
        <asp:UpdatePanel ID="upPortlet" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="margin: 5px">
                    <colgroup>
                        <col width="200px" />
                        <col width="30px" />
                        <col width="200px" />
                    </colgroup>
                    <tr>
                        <td>Available Portlets</td>
                        <td></td>
                        <td>Using Porlets</td>
                    </tr>
                    <tr>
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
                            <dx:ASPxButton runat="server" ID="btnPortletAdd" Text=">" Width="30px" OnClick="BtnAddRemovePortlet_Click" />
                            <dx:ASPxButton runat="server" ID="btnPortletRemove" Text="<" Width="30px" OnClick="BtnAddRemovePortlet_Click" />
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
    </fieldset>
    <fieldset class="dashboard-filter">
        <legend>Filters</legend>
        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel runat="server" ID="ctrl_DashboardFilters"></asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAddDashboardFilter" />
            </Triggers>
        </asp:UpdatePanel>
        <dx:ASPxButton ID="btnAddDashboardFilter" runat="server" Text="Add Filter" 
                onclick="btnAddDashboardFilter_Click">
        </dx:ASPxButton>
        <asp:UpdateProgress ID="UpdateProcess" AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0" DynamicLayout="false" runat="server">
            <ProgressTemplate>
                <asp:Label runat="server" ID="lbUpdateProcess" Text="..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </fieldset>
    <div style="margin-left: 10px;"><dx:ASPxCheckBox runat="server" ID="chkDefault" Text="Is Default" /></div>
    <asp:UpdatePanel ID="toolbarAction" runat="server">
        <ContentTemplate>
            <table style="margin: 5px 10px">
                <tr>
                    <td><dx:ASPxButton runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save"></dx:ASPxButton></td>
                    <td><dx:ASPxButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" Text="Delete" Visible="false"></dx:ASPxButton></td>
                    <td><dx:ASPxButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"></dx:ASPxButton></td>
                </tr>
            </table>        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
