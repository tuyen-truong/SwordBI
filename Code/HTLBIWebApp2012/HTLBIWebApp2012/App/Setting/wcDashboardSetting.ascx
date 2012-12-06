<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcDashboardSetting.ascx.cs"
    Inherits="HTLBIWebApp2012.App.Setting.wcDashboardSetting" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx1" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<style type="text/css">
    *
    {
        margin: 0;
        padding: 0;
    }
    .memu
    {
    }
    .menu li
    {
        display: inline-block;
        list-style-type: none;
    }
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
    <ul class="menu">
        <li>
            <dx:ASPxHyperLink runat="server" ID="DataSource" ClientInstanceName="DataSource"
                Text="Data Source" />
        </li>
        <li>&nbsp;|&nbsp;</li>
        <li>
            <dx:ASPxHyperLink runat="server" ID="ASPxHyperLink1" ClientInstanceName="DataSource"
                Text="KPIs" />
        </li>
        <li>&nbsp;|&nbsp;</li>
        <li>
            <dx:ASPxHyperLink runat="server" ID="ASPxHyperLink2" ClientInstanceName="DataSource"
                Text="Layouts" />
        </li>
    </ul>
</div>
<div id="divWarehouseFilter" style="margin-top: 10px;">
    <dx:ASPxComboBox ID="cboDataDW" runat="server" AutoPostBack="true" OnValueChanged="cboDataDW_ValueChanged">
    </dx:ASPxComboBox>
</div>
<div id="divDashboardList" style="margin-top: 5px">
    <dx:ASPxLabel ID="lblLstTitle" runat="server" Text="Dashboard list" Font-Bold="True"
        Font-Size="10pt" />
    <dx:ASPxGridView ID="lstDashboard" runat="server" AutoGenerateColumns="false" Width="60%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" Width="50%">
                <Settings AllowDragDrop="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Template" VisibleIndex="1" Width="40%">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn Caption="Default" FieldName="IsDefault" VisibleIndex="2">
                <Settings AllowDragDrop="False" AllowSort="False" />
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn Caption="Default1">
                <DataItemTemplate>
                    <asp:CheckBox runat="server" name="IsDefault" />
                </DataItemTemplate>
            </dx:GridViewDataCheckColumn>
        </Columns>
        <SettingsPager Mode="ShowAllRecords" />
    </dx:ASPxGridView>
    <dx1:ASPxListBox runat="server" ID="lstBox" />
</div>
