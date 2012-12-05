<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcNumFilter.ascx.cs" Inherits="HTLBIWebApp2012.App.Setting.wcNumFilter" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx1" %>

<script type="text/javascript" language="javascript">

    $(document).ready(
        function () {
            // Set Mask Numeric Input
            $(".numericInput").autoNumeric({ aSep: '.', aDec: ',', mDec: '2', vMax: '999999999999999999' });
            // Set Atttribute Input
            $(".numericInput").css("text-align", "right");
        }
    );
</script>

<table>
    <tr>
        <td>
            <dx:ASPxComboBox ID="cbbKeyField" runat="server" Width="120px">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbOperator" runat="server" Width="70px">
            </dx:ASPxComboBox>
        </td>
        <td>
            <asp:TextBox ID="txtValue" runat="server" CssClass="numericInput" Width="140px"></asp:TextBox>
        </td>
        <td>
            <dx:ASPxComboBox ID="cbbAndOr" runat="server" Width="70px">
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxButton ID="btnDelFilter" runat="server" Text="X" 
                ToolTip="Remove this filter" Width="20px" onclick="btnDelFilter_Click">
            </dx:ASPxButton>
        </td>
    </tr>
</table>