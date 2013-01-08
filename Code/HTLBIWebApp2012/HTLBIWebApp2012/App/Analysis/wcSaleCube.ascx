<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcSaleCube.ascx.cs" Inherits="HTLBIWebApp2012.App.Analysis.wcSaleCube" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraCharts.v10.2.Web, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<%--JavaScript tự động thay đổi độ rộng của Webchart khi độ rộng trình duyệt thay đổi--%>
<script type="text/javascript">
    var isNeedShowDialog = 0;

    var chartWidth = 986;
    var chartWidthOld = 986;
    var callbackState = 0;

    //window.onresize = function() { Resized(); }

    function Resized() {
        if (callbackState == 0)
            DoCallback();
    }
    function DoCallback() {
        chartWidth = document.body.offsetWidth - 21; // -24
        if (chartWidth != chartWidthOld) {
            WebChart.PerformCallback(chartWidth);
            chartWidthOld = chartWidth;
        }
    }
    function ResizeChart(s, e) {
        callbackState = 0;
        s.GetMainElement().style.width = chartWidth + "px";
    }
    function ResetCallbackState() {
        callbackState = 1;
    }
</script>
<!--GUI for Tooltip dashboard-->
<script type="text/javascript"><!--
    window.onload = function () {
        /*Đăng ký các sự kiện cho việc hiển thị tooltip*/
        _aspxAttachEventToDocument("mousemove", OnMouseMove);
        _aspxAttachEventToDocument("mousedown", OnMouseDown);
        /*Gửi sự kiện trên từng chart về server để thay đổi kích thước theo trình duyệt hiện hành*/
        //DoCallback();
    }
    function GetValueString(value) {
        if (!(value instanceof Date))
            return value.toString();
        var minutes = value.getMinutes();
        return (value.getUTCMonth() + 1) + "/" + value.getUTCDate() + " " + value.getUTCHours() + ":" + Math.round(minutes / 10).toString() + (minutes % 10).toString();
    }

    var srcClick;
    function OnMouseDown(evt) {
        srcClick = _aspxGetEventSource(evt);
    }
    function OnMouseMove(evt) {
        var srcElement = _aspxGetEventSource(evt);
        ToolTip.Hide();
    }
    function SetTooltipForChart(s, e) {
        if (e.hitInfo.inSeriesPoint) {
            var dimName = e.hitInfo.seriesPoint.argument;
            var measName = GetValueString(e.additionalHitObject.series.name);
            var measValue = "";
            if (measName.indexOf("<%=Resources.BI.Milion_CurrencySymbol %>") != -1)
                measValue = e.additionalHitObject.values[0] + " <%=Resources.BI.Milion_CurrencySymbol %>";
            else
                measValue = e.additionalHitObject.values[0];

            var text = "<b>" + dimName + "</b>";
            text += "<br><b>" + measName + " :</b> " + measValue;
            ToolTip.SetContentHTML("<span style=\"white-space:nowrap\">" + text + "</span>");
            ToolTip.ShowAtPos(e.absoluteX + 10, e.absoluteY + 20);
        }
    }
//--></script>

<dx:ASPxPivotGrid ID="pivotGrid" runat="server" CustomizationFieldsLeft="600" CustomizationFieldsTop="400" 
    ClientInstanceName="pivotGrid" Width="100%" 
        oncustomcallback="pivotGrid_CustomCallback" >
    <ClientSideEvents Init="function(s, e) { WebChart.PerformCallback(&quot;pivotGridChanged&quot;);}" />
    <Fields>
        <dx:PivotGridField ID="fieldQuantity" Area="DataArea" AreaIndex="0" 
            Caption="Quantity" FieldName="[Measures].[Quantity]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldGrossProfit" Area="DataArea" AreaIndex="1" 
            Caption="Gross Profit" FieldName="[Measures].[Gross Profit]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">                
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldVatSum" Area="DataArea" AreaIndex="2" 
            Caption="Vat Sum" FieldName="[Measures].[Vat Sum]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">                
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldDocTotal" Area="DataArea" AreaIndex="3" 
            Caption="Doc Total" FieldName="[Measures].[Doc Total]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">                
        </dx:PivotGridField>        
        <dx:PivotGridField ID="fieldTotalExpns" Area="DataArea" AreaIndex="4" 
            Caption="Total Expns" FieldName="[Measures].[Total Expns]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">                
        </dx:PivotGridField> 
        <dx:PivotGridField ID="fieldGrossProfit_Perc" Area="DataArea" AreaIndex="5" 
            Caption="Gross Profit %" FieldName="[Measures].[Gross Profit %]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">                
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldSales" Area="DataArea" AreaIndex="6" 
            Caption="Sales" FieldName="[Measures].[Sales]"
            CellFormat-FormatString="#,##0"
            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#,##0"
            GrandTotalCellFormat-FormatType="Numeric">                
        </dx:PivotGridField> 
        
        <dx:PivotGridField ID="fieldItemGroupName" Area="RowArea" AreaIndex="0" 
            Caption="Item Group" FieldName="[SO_Item].[Itms Grp Nam].[Itms Grp Nam]">
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldItemName" Area="RowArea" AreaIndex="1" 
            Caption="Item Name" FieldName="[SO_Item].[Item].[Item]">
        </dx:PivotGridField>               
        
        <dx:PivotGridField ID="fieldYear" Area="ColumnArea" AreaIndex="0" 
            Caption="Year" FieldName="[SO_Date].[Year].[Year]">
        </dx:PivotGridField>                       
        <dx:PivotGridField ID="fieldMonth" Area="ColumnArea" AreaIndex="1" 
            Caption="Month" FieldName="[SO_Date].[Month].[Month]">
        </dx:PivotGridField>                       
        <dx:PivotGridField ID="fieldDate" Area="ColumnArea" AreaIndex="2" 
            Caption="Date" FieldName="[SO_Date].[Date].[Date]">
        </dx:PivotGridField>                       
        
        <dx:PivotGridField ID="fieldPartnerGroupName" AreaIndex="0" 
            Caption="Partner Group" FieldName="[SO_BizPartner].[Group Name].[Group Name]">
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldPartnerCardName" AreaIndex="1" 
            Caption="Partner Name" FieldName="[SO_BizPartner].[Card Name].[Card Name]">
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldInvType" AreaIndex="2" 
            Caption="Inv Type" 
            FieldName="[SO_InvType].[Inv Type].[Inv Type]">
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldInvoice" AreaIndex="3" 
            Caption="Invoice" 
            FieldName="[SO_Invoice].[Invoice].[Invoice]">
        </dx:PivotGridField>
        <dx:PivotGridField ID="fieldProject" AreaIndex="4" 
            Caption="Project" 
            FieldName="[SO_Project].[Project].[Project]">
        </dx:PivotGridField>                
        <dx:PivotGridField ID="fieldSaleperson" AreaIndex="5" 
            Caption="Saleperson" 
            FieldName="[SO_Saleperson].[Saleperson].[Saleperson]">
        </dx:PivotGridField>                    
    </Fields>
</dx:ASPxPivotGrid>
<table>
    <tr>  
        <td align="right"><dx:ASPxLabel ID="lblChartType" runat="server" Text="<%$ Resources:BI, lblChartType %>" Font-Bold="true" /></td>
        <td align="left">
            <dx:ASPxComboBox ID="cbbChartType" runat="server" >
                <ClientSideEvents ValueChanged="
                    function(s, e) { 
                        WebChart.PerformCallback(&quot;chartTypeValueChanged&quot;);
                    }" />
            </dx:ASPxComboBox>
        </td>
        <td align="right">&nbsp;<dx:ASPxLabel ID="lblAppearance" runat="server" Text="<%$ Resources:BI, Appearance %>" Font-Bold="true" /></td>
        <td align="left">
            <dx:ASPxComboBox ID="cbbAppearance" runat="server" Width="150px" SelectedIndex="0"
                ClientInstanceName="cbbAppearance">
                <ClientSideEvents ValueChanged="
                    function(){
                        WebChart.PerformCallback(&quot;Appearance&quot;);
                    }" />
            </dx:ASPxComboBox>
        </td>
        <td align="right">&nbsp;<dx:ASPxLabel ID="lblPalette" runat="server" Text="<%$ Resources:BI, Palette %>" Font-Bold="true" /></td>
        <td align="left">
            <dx:ASPxComboBox ID="cbbPalette" runat="server" Width="150px" SelectedIndex="0"
                ClientInstanceName="cbbPalette">
                <ClientSideEvents ValueChanged="
                    function(){
                        WebChart.PerformCallback(&quot;Palette&quot;);
                    }" />
            </dx:ASPxComboBox>
        </td>        
    </tr>
</table>
<dxchartsui:WebChartControl ID="WebChart" runat="server" DataSourceID="pivotGrid"
    Width="986px" Height="500px" LoadingPanelText="<%$ Resources:BI, loadingText %>" 
    OnObjectSelected ="WebChart_ObjectSelected" 
    oncustomcallback="WebChart_CustomCallback" ClientInstanceName="WebChart" 
    IndicatorsPaletteName="Default" SeriesDataMember="Series" 
    ClientIDMode="AutoID" >
    <ClientSideEvents
        ObjectHotTracked="function(s, e) {
            var hitInCategory = e.hitInfo.inSeriesPoint &amp;&amp; e.chart.series[0].visible;
            s.SetCursor(hitInCategory ? 'pointer' : 'default');	
            SetTooltipForChart(s, e);
        }" 
        ObjectSelected="function(s, e) {
            if (e.hitInfo.inSeriesPoint) {
                    e.processOnServer = true;
                    isNeedShowDialog = 1;
            }             
            else
            {
                e.processOnServer = false;
                isNeedShowDialog = 0
            }
        }"    
        BeginCallback="function(s, e) { ResetCallbackState(); }" 
        EndCallback="function(s, e) { 
            if(isNeedShowDialog == 1)
            {
                isNeedShowDialog = 0;
                gvDrillDown.PerformCallback('D|CHART');
            }
            ResizeChart(s,e);
        }" 
     />            
    <seriesserializable>
        <cc1:series Name="Series 1">
            <viewserializable>
                <cc1:sidebysidebarseriesview>
                </cc1:SideBySideBarSeriesView>
            </viewserializable>
            <labelserializable>
                <cc1:sidebysidebarserieslabel LineVisible="True">
                    <fillstyle>
                        <optionsserializable>
                            <cc1:solidfilloptions />
                        </optionsserializable>
                    </fillstyle>
                </cc1:SideBySideBarSeriesLabel>
            </labelserializable>
            <pointoptionsserializable>
                <cc1:pointoptions>
                </cc1:PointOptions>
            </pointoptionsserializable>
            <legendpointoptionsserializable>
                <cc1:pointoptions>
                </cc1:PointOptions>
            </legendpointoptionsserializable>
        </cc1:Series>
        <cc1:series Name="Series 2">
            <viewserializable>
                <cc1:sidebysidebarseriesview>
                </cc1:SideBySideBarSeriesView>
            </viewserializable>                        
            <labelserializable>
                <cc1:sidebysidebarserieslabel LineVisible="True">
                    <fillstyle>
                        <optionsserializable>
                            <cc1:solidfilloptions />
                        </optionsserializable>
                    </fillstyle>
                </cc1:SideBySideBarSeriesLabel>
            </labelserializable>
            <pointoptionsserializable>
                <cc1:pointoptions>
                </cc1:PointOptions>
            </pointoptionsserializable>
            <legendpointoptionsserializable>
                <cc1:pointoptions>
                </cc1:PointOptions>
            </legendpointoptionsserializable>
        </cc1:Series>
    </seriesserializable>
<SeriesTemplate argumentdatamember="Arguments" 
        valuedatamembersserializable="Values"><ViewSerializable>
<cc1:sidebysidebarseriesview></cc1:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<cc1:sidebysidebarserieslabel LineVisible="True">
<FillStyle><OptionsSerializable>
<cc1:solidfilloptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
</cc1:SideBySideBarSeriesLabel>
</LabelSerializable>
<PointOptionsSerializable>
<cc1:pointoptions></cc1:PointOptions>
</PointOptionsSerializable>
<LegendPointOptionsSerializable>
<cc1:pointoptions></cc1:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>

    <legend maxhorizontalpercentage="30"></legend>

<FillStyle><OptionsSerializable>
<cc1:solidfilloptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
    <diagramserializable>
        <cc1:xydiagram>
            <axisx visibleinpanesserializable="-1">
                <label angle="-30" antialiasing="True" />
                <range sidemarginsenabled="True" />
            </axisx>
            <axisy visibleinpanesserializable="-1">
                <range sidemarginsenabled="True" />
            </axisy>
            <defaultpane enableaxisxscrolling="False" enableaxisxzooming="False" 
                enableaxisyscrolling="False" enableaxisyzooming="False">
            </defaultpane>
        </cc1:XYDiagram>
    </diagramserializable>
</dxchartsui:WebChartControl>

<input runat="server" id="ColumnIndex" type="hidden" enableviewstate="true" />
<input runat="server" id="RowIndex" type="hidden" enableviewstate="true" />

<!--Popup Detail--> 
<dx:ASPxPopupControl ID="frmDrillDown" ClientInstanceName="frmDrillDown" runat="server" AppearAfter="100" 
    CloseAction="CloseButton" DisappearAfter="100" Font-Names="Arial" AllowResize="true"
    Font-Size="9pt" HeaderText="Thông tin chi tiết" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="true" 
    ShowFooter="false" Width="900px" Height="100%" ShowLoadingPanel="true" LoadingPanelText="<%$ Resources:BI, loadingText %>" >   
    <ContentStyle><Paddings Padding="5px" /></ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="popupDrillDown" runat="server">            
            <dx:ASPxGridView ID="gvDrillDown" ClientInstanceName="gvDrillDown" runat="server"
                AutoGenerateColumns="True" Font-Names="Arial" Font-Size="9pt" Width="100%"
                OnCustomCallback="gvDrillDown_CustomCallback"
                OnPageIndexChanged="gvDbrdDetail_PageIndexChanged">
                <ClientSideEvents EndCallback="function(){
                    frmDrillDown.Show();
                }" />
            </dx:ASPxGridView>            
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<!--Popup Tooltip -->
<dx:ASPxPopupControl ID="ToolTip" runat="server" ClientInstanceName="ToolTip" EnableAnimation="false" 
    Height="1px" ShowHeader="False" Width="1px" CloseAction="MouseOut" EnableHotTrack="False"
    PopupHorizontalAlign="Center" PopupVerticalAlign="TopSides">
    <ContentStyle><Paddings Padding="5px" /></ContentStyle>
    <ContentCollection><dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server"></dx:PopupControlContentControl></ContentCollection>
</dx:ASPxPopupControl>