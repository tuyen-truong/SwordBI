<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="DashboardEdit.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.DashboardEdit" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
<style type="text/css">
table.layout-style
{
    border-spacing: 5px 20px;
}
</style>
    <table style="padding: 10px;" cellspacing="10">
        <tr>
            <td>Display Name</td>
            <td><dx:ASPxTextBox ID="txtDashboardName" runat="server" ClientInstanceName="txtDashboardName" Width="250px"></dx:ASPxTextBox></td>
        </tr>
    </table>
    <fieldset style="margin: 5px">
        <legend>Layout Style</legend>
        <table class="layout-style">
            <tr>
                <td style="vertical-align: middle; width: 150px">
                    Two Pane
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="TwoPane_1" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image6" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="TwoPane_2" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image1" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
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
                <td style="vertical-align: middle; width: 150px">Three Pane</td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_1" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image2" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_2" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image3" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_3" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image4" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
                        Width="96" Height="96" />
                </td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="ThreePane_4" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image5" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
                        Width="96" Height="96" />
                </td>
            </tr>
            <tr style="padding-top: 50px;">
                <td style="vertical-align: middle; width: 150px;">Four Pane</td>
                <td valign="middle">
                    <asp:RadioButton GroupName="LayoutStyle" runat="server" ID="FourPane_1" />
                </td>
                <td valign="middle">
                    <asp:Image ID="Image7" ImageUrl="~/Images/Appearance/Dark Flat.png" runat="server"
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
    <fieldset style="margin: 5px">
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
</asp:Content>
