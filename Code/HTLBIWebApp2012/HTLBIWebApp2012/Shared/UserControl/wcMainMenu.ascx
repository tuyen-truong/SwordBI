<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcMainMenu.ascx.cs" Inherits="HTLBIWebApp2012.Shared.UserControl.wcMainMenu" %>

<%=this.WriteMenuToHTML()%>

<%--Đánh dấu trên menu được chọn và Disable chức năng Link của nó--%>
<script type="text/javascript" language="javascript">
    $(document).ready
    (
        function () {
            $("#<%=CurTabID%> a").css({
                'background-image': 'url(<%=ImagesFolder%>/left-sub_on.gif)',
                'background-repeat': 'no-repeat',
                'background-position': 'left top',
                'color': '#000000'
            });

            $("#<%=CurTabID%> a span").css({
                'background-image': 'url(<%=ImagesFolder%>/right-sub_on.gif)',
                'background-repeat': 'no-repeat',
                'background-position': 'right top',
                'color': '#000000'
            });

            $("#<%=CurTabID%> a").removeAttr("href");
        }
    );
</script>