<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
<asp:Table ID="Table1" runat="server">
    <asp:TableRow>
        <asp:TableCell>
            <asp:RadioButton runat="server" ID="radio1" GroupName="RadioGroup" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged" />
        </asp:TableCell>
        <asp:TableCell><label for="MainContent_radio1"><asp:Image runat="server" ID="Image1" ImageUrl="~/Images/TwoPane_1.jpg" /></label></asp:TableCell>
        <asp:TableCell>
            <asp:RadioButton runat="server" ID="radio2" GroupName="RadioGroup" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged"  />
        </asp:TableCell>
        <asp:TableCell><label for="MainContent_radio2"><asp:Image runat="server" ID="Image2" ImageUrl="~/Images/TwoPane_2.jpg" /></label></asp:TableCell>
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
                    <span style="vertical-align:top">Display Name:</span>
                    <input size="50" />
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <span style="vertical-align:top">test</span>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="max-width:1024px; position:relative">
    <div style="position:absolute;top:0px;padding-right: 50%;float: left;">
        12345678901234567890123456789012345678901 234567890123456789012345678901 2345678901234567890123456789012345678901234567890
        1234567890123456789 0123456789012345678 901234567890123456789012345678901 2345678901234567890123456789012345678901234567890 1234567890123456789012345678901234567 890123456789012345678 90123456789012345678901234567890
    </div>    
    <div style="position:absolute;top:0px; border:1px solid red; padding-left: 50%; float:right;">
        1234567890123456789012345678901234567890 12345678901234 567890123456789012345678901234567890123 456789012345678901234567890
        1234567890123456789012345678901234567890123456789 01234567890123456789012345678901 234567890123456789012345678901234567890 1234567890123456789012345678901234567890123 45678901234567890123456789012345678901234567890
    </div>
    </div>
</asp:Content>
