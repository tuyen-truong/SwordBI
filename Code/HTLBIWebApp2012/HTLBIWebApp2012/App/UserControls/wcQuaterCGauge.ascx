<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcQuaterCGauge.ascx.cs" Inherits="HTLBIWebApp2012.App.UserControls.wcQuaterCGauge" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>
<dx:ASPxGaugeControl ID="myGauge" runat="server" BackColor="White" 
    Height="260px" Value="30" Width="260px">
    <Gauges>
        <dx:CircularGauge Bounds="0, 0, 260, 260" Name="circularGauge3">
            <needles>
                <dx:ArcScaleNeedleComponent EndOffset="-25" Name="arcScaleNeedleComponent3" ScaleID="scale1" 
                    ShapeType="CircularFull_Style7" StartOffset="-29" ZOrder="-50" />
            </needles>

            <backgroundlayers>
                <dx:ArcScaleBackgroundLayerComponent Name="arcScaleBackgroundLayerComponent3" ScaleID="scale1" 
                    ShapeType="CircularQuarter_Style3Left" ZOrder="1000" ScaleCenterPos="0.77, 0.77" />
            </backgroundlayers>
            <indicators>
                <dx:ArcScaleStateIndicatorComponent Name="stateIndicator1" 
                    ScaleID="scale1" Center="125, 210" ZOrder="-100">
                </dx:ArcScaleStateIndicatorComponent>
            </indicators>               
            <scales>
                <dx:ArcScaleComponent Center="195, 195" EndAngle="-90" 
                    MajorTickmark-FormatString="{0:F0}" MajorTickmark-ShapeType="Circular_Style7_2" 
                    MajorTickmark-TextOffset="28" MajorTickmark-TextOrientation="LeftToRight" 
                    MaxValue="60" MinorTickCount="4" MinorTickmark-ShapeType="Circular_Style7_1" 
                    Name="scale1" RadiusX="127" RadiusY="127" StartAngle="-180" 
                    MajorTickCount="5" MinValue="20" Value="30" 
                    AppearanceTickmarkText-Font="Microsoft Sans Serif, 12pt">
                </dx:ArcScaleComponent>
            </scales>
        </dx:CircularGauge>
    </Gauges>
</dx:ASPxGaugeControl>
