<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcSubMenu.ascx.cs" Inherits="HTLBIWebApp2012.Shared.UserControl.wcSubMenu" %>

<asp:Label ID="lblSubMenu" runat="server" />
<script type="text/javascript" language="javascript">
    $(document).ready
    (
        function () {
            $("#<%=CurTabID%>").css({ 'color': '#000000' });
            $("#<%=CurTabID%>").removeAttr("href");
        }
    );
</script>
