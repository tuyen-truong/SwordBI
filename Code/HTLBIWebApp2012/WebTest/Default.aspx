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
            <label for="MainContent_radio1">
                <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/TwoPane_1.jpg" />
            </label>
        </asp:TableCell>
        <asp:TableCell>
            <asp:RadioButton runat="server" ID="radio2" GroupName="RadioGroup" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged"  />
            <label for="MainContent_radio2">
                <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/TwoPane_2.jpg" />
            </label>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <Triggers>
            <asp:PostBackTrigger ControlID="radio1" />
            <asp:PostBackTrigger ControlID="radio2" />
        </Triggers>
        <ContentTemplate>    
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <span style="vertical-align:top">Display Name:</span>
                    <input size="50" />
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
