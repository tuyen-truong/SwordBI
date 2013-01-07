<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:RadioButton runat="server" ID="radio1" GroupName="RadioGroup" AutoPostBack="true"
                    OnCheckedChanged="Radio_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <label for="MainContent_radio1">
                    <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/TwoPane_1.jpg" /></label></asp:TableCell>
            <asp:TableCell>
                <asp:RadioButton runat="server" ID="radio2" GroupName="RadioGroup" AutoPostBack="true"
                    OnCheckedChanged="Radio_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <label for="MainContent_radio2">
                    <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/TwoPane_2.jpg" /></label></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="radio1" />
            <asp:AsyncPostBackTrigger ControlID="radio2" />
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <span style="vertical-align: top">Display Name:</span>
                    <input size="50" />
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <span style="vertical-align: top">test</span>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="container" style="width:100%">
        <div id="header" style="background-color: #FFA500;">
            <h1 style="margin-bottom: 0;">
                Main Title of Web Page</h1>
        </div>
        <div id="menu" style="background-color: #FFD700; width: 100px; float: left;">
            <b>Menu</b><br>
            HTML<br>
            CSS<br>
            JavaScript</div>
        <div id="content" style="background-color: #EEEEEE; height: 200px; width: 400px;
            float: left;">
            Content goes here</div>
        <div style="background-color: #EEDDFF;height: 200px;width:250px;">right</div>
        <div id="footer" style="background-color: #FFA500; clear: both; text-align: center;">
            Copyright � W3Schools.com</div>
    </div>
</asp:Content>
