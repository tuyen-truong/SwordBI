<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcFullCGauge.ascx.cs" Inherits="HTLBIWebApp2012.App.UserControls.wcFullCGauge" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>

<dx:ASPxGaugeControl ID="myGauge" runat="server" BackColor="Transparent" 
    Height="100%" Width="100%" ClientInstanceName="portlet_FullCGauge" oncustomcallback="myGauge_CustomCallback">
    <Gauges>
        <dx:CircularGauge Bounds="0, 0, 260, 260" Name="cGauge1">
            <needles>
                <dx:ArcScaleNeedleComponent EndOffset="-25" Name="needle1" ScaleID="scale1" 
                    ShapeType="CircularFull_Style7" StartOffset="-21" ZOrder="-50" />
            </needles>
            <backgroundlayers>
                <dx:ArcScaleBackgroundLayerComponent Name="bg1" ScaleID="scale1" 
                    ShapeType="CircularFull_Style7" ZOrder="1000" />
            </backgroundlayers>
            <indicators>
                <dx:ArcScaleStateIndicatorComponent Name="stateIndicator1" 
                    ScaleID="scale1" Center="125, 200" ZOrder="-100">
                </dx:ArcScaleStateIndicatorComponent>
            </indicators>
            <scales>
                <dx:ArcScaleComponent Center="125, 125" EndAngle="60" 
                    MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeType="Circular_Style7_2" 
                    MajorTickmark-TextOffset="22" MajorTickmark-TextOrientation="LeftToRight" 
                    MaxValue="100" MinorTickCount="4" MinorTickmark-ShapeType="Circular_Style7_1" 
                    Name="scale1" RadiusX="83" RadiusY="83" StartAngle="-240">
                </dx:ArcScaleComponent>
            </scales>
        </dx:CircularGauge>
    </Gauges>
</dx:ASPxGaugeControl>
