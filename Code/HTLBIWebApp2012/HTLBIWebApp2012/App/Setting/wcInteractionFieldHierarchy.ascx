<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcInteractionFieldHierarchy.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcInteractionFieldHierarchy" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>
<script type="text/javascript" language="javascript">
    function SetEnableHierarchy(isEnable) {        
        pal_InteractionHierarchy.SetEnabled(isEnable)
    }
</script>
<dx:ASPxPanel ID="pal_InteractionHierarchy" ClientInstanceName="pal_InteractionHierarchy" 
    runat="server" Width="100%">
    <Paddings Padding="0px" />
    <PanelCollection>
    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxComboBox ID="cbbFieldHierarchy1" runat="server" Width="100%"
            OnValueChanged="cbb_ValueChanged" ToolTip="Field Hierarchy level 1."
            AutoPostBack="true">
        </dx:ASPxComboBox>
        <div id="ctrl_HierarchyField" runat="server"></div>
    </dx:PanelContent>
</PanelCollection>
</dx:ASPxPanel>