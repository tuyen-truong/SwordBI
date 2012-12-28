<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="DashboardEdit.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.DashboardEdit" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="wc" TagName="wcPortletPicket" Src="~/Shared/UserControl/wcPortletPicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="margin: 10px;">
        <tr>
            <td>
                Display Name
            </td>
            <td>
                <dx:ASPxTextBox ID="txtDashboardName" runat="server" ClientInstanceName="txtDashboardName"
                    Width="250px">
                </dx:ASPxTextBox>
            </td>
        </tr>
    </table>
    <fieldset class="dashboard-layout-style">
        <legend>Layout Style</legend>
        <asp:UpdatePanel ID="UPLayoutStyle" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
                    <colgroup>
                        <col width="50%" />
                        <col width="50%" />
                    </colgroup>
                    <tr>
                        <td valign="top">
                            <table>
                                <tr>
                                    <th style="vertical-align: middle; width: 150px;">
                                        Two Pane
                                    </th>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="TwoPane_1" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <label for="bodyContent_MainContent_TwoPane_1">
                                            <asp:Image ID="Image6" ImageUrl="~/Images/TwoPane_1.jpg" runat="server" Width="72"
                                                Height="72" />
                                        </label>
                                    </td>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="TwoPane_2" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <label for="bodyContent_MainContent_TwoPane_2">
                                            <asp:Image ID="Image1" ImageUrl="~/Images/TwoPane_2.jpg" runat="server" Width="72"
                                                Height="72" />
                                        </label>
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
                                    <th style="vertical-align: middle; width: 150px">
                                        Three Pane
                                    </th>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_1" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <asp:Image ID="Image2" ImageUrl="~/Images/ThreePane_1.jpg" runat="server" Width="72"
                                            Height="72" />
                                    </td>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_2" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <asp:Image ID="Image3" ImageUrl="~/Images/ThreePane_2.jpg" runat="server" Width="72"
                                            Height="72" />
                                    </td>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_3" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <asp:Image ID="Image4" ImageUrl="~/Images/ThreePane_3.jpg" runat="server" Width="72"
                                            Height="72" />
                                    </td>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_4" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <asp:Image ID="Image5" ImageUrl="~/Images/ThreePane_4.jpg" runat="server" Width="72"
                                            Height="72" />
                                    </td>
                                </tr>
                                <tr style="padding-top: 50px;">
                                    <th style="vertical-align: middle; width: 150px;">
                                        Four Pane
                                    </th>
                                    <td valign="middle">
                                        <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="FourPane_1" AutoPostBack="true"
                                            OnCheckedChanged="LayoutStyle_CheckedChanged" />
                                    </td>
                                    <td valign="middle">
                                        <asp:Image ID="Image7" ImageUrl="~/Images/FourPane_1.jpg" runat="server" Width="72"
                                            Height="72" />
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
                        </td>
                        <td valign="top">
                            <asp:PlaceHolder ID="DashboardSettingPlaceHolder" runat="server"></asp:PlaceHolder>
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
                <asp:Panel runat="server" ID="ctrl_DashboardFilters">
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAddDashboardFilter" />
            </Triggers>
        </asp:UpdatePanel>
        <dx:ASPxButton ID="btnAddDashboardFilter" runat="server" Text="Add Filter" OnClick="btnAddDashboardFilter_Click">
        </dx:ASPxButton>
        <asp:UpdateProgress ID="UpdateProcess" AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0"
            DynamicLayout="false" runat="server">
            <ProgressTemplate>
                <asp:Label runat="server" ID="lbUpdateProcess" Text="..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </fieldset>
    <div style="margin-left: 10px;">
        <dx:ASPxCheckBox runat="server" ID="chkDefault" Text="Is Default" />
    </div>
    <asp:UpdatePanel ID="toolbarAction" runat="server">
        <ContentTemplate>
            <table style="margin: 5px 10px">
                <tr>
                    <td>
                        <dx:ASPxButton runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" Text="Delete"
                            Visible="false">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
