<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Shared/GeneralMaster.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="HTLBIWebApp2012.Index" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

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
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>
    <div id="pagecontent">
        <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID" 
            OLAPConnectionString="provider=MSOLAP.4;data source=.;initial catalog=HTLBI2012_SSAS;cube name=ARCube;user id=;password=">
            <Fields>
                <dx:PivotGridField ID="fieldQuantity1" Area="DataArea" AreaIndex="0" 
                    Caption="Quantity" FieldName="[Measures].[Quantity]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldDocTotal1" Area="DataArea" AreaIndex="3" 
                    Caption="Doc Total" FieldName="[Measures].[Doc Total]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldGrosProfitFC1" Area="DataArea" AreaIndex="2" 
                    Caption="Gros Profit FC" FieldName="[Measures].[Gros Profit FC]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldGrosProfit1" Area="DataArea" AreaIndex="1" 
                    Caption="Gros Profit" FieldName="[Measures].[Gros Profit]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldLineTotal1" Area="DataArea" AreaIndex="4" 
                    Caption="Line Total" FieldName="[Measures].[Line Total]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldItemGroupName" Area="RowArea" AreaIndex="0" 
                    Caption="Item Group" FieldName="[ARDimItem].[ItemGroupName].[ItemGroupName]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldItemName" Area="RowArea" AreaIndex="1" 
                    Caption="Item Name" FieldName="[ARDimItem].[ItemName].[ItemName]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldYear" Area="ColumnArea" AreaIndex="0" 
                    Caption="Year" FieldName="[ARDimTime].[Year].[Year]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldQuarter1" Area="ColumnArea" AreaIndex="1" 
                    Caption="Quarter" FieldName="[ARDimTime].[Quarter].[Quarter]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldPeriod" Area="ColumnArea" AreaIndex="2" 
                    Caption="Period" FieldName="[ARDimTime].[Period].[Period]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldVendorName" AreaIndex="0" Caption="VendorName" 
                    FieldName="[ARDimVendor].[VendorName].[VendorName]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldProjectName" AreaIndex="1" Caption="ProjectName" 
                    FieldName="[ARDimProject].[ProjectName].[ProjectName]">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldSalePersonName" AreaIndex="2" 
                    Caption="SalePersonName" 
                    FieldName="[ARDimSalePerson].[SalePersonName].[SalePersonName]">
                </dx:PivotGridField>
            </Fields>
        </dx:ASPxPivotGrid>
    </div>
</asp:Content>
