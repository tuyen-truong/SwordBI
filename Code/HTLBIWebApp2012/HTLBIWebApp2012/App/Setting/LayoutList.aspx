<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="LayoutList.aspx.cs" Inherits="HTLBIWebApp2012.App.Setting.LayoutList" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="margin: 10px">
        <tr>
            <td>
                Data Warehouse
            </td>
            <td>
                <dx:ASPxComboBox ID="cboDataDW" runat="server" AutoPostBack="true" OnValueChanged="cboDataDW_ValueChanged">
                </dx:ASPxComboBox>
            </td>
        </tr>
    </table>
    <div style="margin-left: 10px" class="htl-grid-part">
        <span style="font-weight: bold;">Layouts</span>
        <dx:ASPxGridView ID="gridLayoutList" runat="server" AutoGenerateColumns="false" Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" Width="80%">
                    <DataItemTemplate>
                        <asp:HyperLink runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("Code", "~/App/Setting/LayoutSetting.aspx?lc={0}") + Eval("WHCode","&whcode={0}") %>'
                            ID="ctl"></asp:HyperLink>
                    </DataItemTemplate>
                    <Settings AllowDragDrop="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn Caption="Layout" FieldName="LayoutImageUrl" VisibleIndex="1" Width="20%">
                <DataItemTemplate>
                    <asp:Image ID="Image1" ImageUrl='<%# Eval("LayoutImageUrl") %>' runat="server" AlternateText="" />
                </DataItemTemplate>
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataColumn>
            </Columns>
            <SettingsPager Mode="ShowAllRecords" />
        </dx:ASPxGridView>
    </div>
</asp:Content>
