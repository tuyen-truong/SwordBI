<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="HTLBIWebApp2012.Index" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <dx:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" KeyFieldName="(None)" Width="50%" AutoGenerateColumns="false">
    <Columns>
    <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" Width="40%" />
    <dx:GridViewDataTextColumn FieldName="Template" VisibleIndex="1" Width="40%" />
    <dx:GridViewDataCheckColumn FieldName="Default" VisibleIndex="2" Width="20%" />
    </Columns>
        <Templates>
            <EmptyDataRow>
            <tr>
                <td>1</td>
                <td></td>
                <td></td>
            </tr>
            </EmptyDataRow>
        </Templates>
    </dx:ASPxGridView>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>
</asp:Content>
