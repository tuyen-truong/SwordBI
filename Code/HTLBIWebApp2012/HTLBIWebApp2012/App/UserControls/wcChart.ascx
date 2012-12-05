<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcChart.ascx.cs" Inherits="HTLBIWebApp2012.App.UserControls.wcChart" %>
<%@ Register Assembly="DevExpress.XtraCharts.v10.2.Web, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<dxchartsui:WebChartControl ID="chart" runat="server" ClientInstanceName="chart"
    SideBySideBarDistanceVariable="0.1" SideBySideEqualBarWidth="True" EnableClientSideAPI="true" 
    Width="318px" Height="205px" oncustomdrawseriespoint="chart_CustomDrawSeriesPoint" 
    OnObjectSelected ="chart_ObjectSelected" OnCustomCallback="chart_CustomCallback">
    <SeriesSerializable>
        <cc1:Series Name="Object_Zone1">
            <ViewSerializable>
                <cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
            </ViewSerializable>
            <LabelSerializable>
                <cc1:SideBySideBarSeriesLabel LineVisible="True" BackColor="255, 255, 192"
                    TextColor="0, 0, 255" >
                    <FillStyle><OptionsSerializable>
                        <cc1:SolidFillOptions></cc1:SolidFillOptions>
                        </OptionsSerializable>
                    </FillStyle>
                </cc1:SideBySideBarSeriesLabel>
            </LabelSerializable>
            <PointOptionsSerializable>
                <cc1:PointOptions></cc1:PointOptions>
            </PointOptionsSerializable>
            <LegendPointOptionsSerializable>
                <cc1:PointOptions></cc1:PointOptions>
            </LegendPointOptionsSerializable>
        </cc1:Series>
    </SeriesSerializable>
    <SeriesTemplate>
        <ViewSerializable>
            <cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
        </ViewSerializable>
        <LabelSerializable>
            <cc1:SideBySideBarSeriesLabel LineVisible="True">
                <FillStyle><OptionsSerializable>
                    <cc1:SolidFillOptions></cc1:SolidFillOptions>
                    </OptionsSerializable>
                </FillStyle>
            </cc1:SideBySideBarSeriesLabel>
        </LabelSerializable>
        <PointOptionsSerializable>
            <cc1:PointOptions></cc1:PointOptions>
        </PointOptionsSerializable>
        <LegendPointOptionsSerializable>
            <cc1:PointOptions></cc1:PointOptions>
        </LegendPointOptionsSerializable>
    </SeriesTemplate>
    <ClientSideEvents    
        ObjectHotTracked="function(s, e) {
	        var hitInCategory = e.hitInfo.inSeriesPoint &amp;&amp; e.chart.series[0].visible;
	        s.SetCursor(hitInCategory ? 'pointer' : 'default');	
	        //SetTooltipForChart(s, e, 1);
        }" 
        ObjectSelected ="function(s, e) {
            //Set_ProcessState('0');
            if (e.hitInfo.inSeriesPoint) {
                    //e.processOnServer = true;
                    //isNeedShowDialog = 1;
            }             
            else
            {
                //e.processOnServer = false;
                //isNeedShowDialog = 0
            }
        }"
        BeginCallback="function(s, e) { /*ResetCallbackState(1);*/ }" 
        endcallback="function(s, e) {
            //ResizeChart(s,e,1);
            /*if(isNeedShowDialog == 1)
            {
                isNeedShowDialog = 0;
                gvDbrdDetail.PerformCallback();
            }*/
        }" />
    <FillStyle >
        <OptionsSerializable>
            <cc1:SolidFillOptions></cc1:SolidFillOptions>
        </OptionsSerializable>
    </FillStyle>
    <Legend AlignmentVertical="Center" visible="False"></Legend>
    <BorderOptions Visible="False" />
    <DiagramSerializable>
        <cc1:XYDiagram >
            <axisx visibleinpanesserializable="-1">
                <label angle="-50" antialiasing="True" />
                <range sidemarginsenabled="True" />
            </axisx>                    
            <axisy visibleinpanesserializable="-1" title-visible="True">
                <label angle="-30" antialiasing="True"/>
                <range sidemarginsenabled="True" />
            </axisy>
            <defaultpane enableaxisxscrolling="False" enableaxisxzooming="False" 
                enableaxisyscrolling="False" enableaxisyzooming="False">
            </defaultpane>
        </cc1:XYDiagram>
    </DiagramSerializable>
</dxchartsui:WebChartControl>

<asp:SqlDataSource ID="sqlDS" runat="server"></asp:SqlDataSource>