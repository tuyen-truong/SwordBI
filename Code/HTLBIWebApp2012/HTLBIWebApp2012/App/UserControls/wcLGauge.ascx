<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wcLGauge.ascx.cs" Inherits="HTLBIWebApp2012.App.UserControls.wcLGauge" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>
<dx:ASPxGaugeControl ID="myGauge" runat="server" BackColor="White" 
    Height="260px" Value="20" Width="260px">
    <Gauges>
        <dx:LinearGauge Bounds="0, 0, 300, 350" Name="lGauge1" Orientation="Vertical">
            <backgroundlayers>
                <dx:LinearScaleBackgroundLayerComponent Name="bg1" ScaleEndPos="0.497, 0.135" 
                    ScaleID="scale1" ScaleStartPos="0.497, 0.865" ShapeType="Linear_Style7" 
                    ZOrder="1000" />
            </backgroundlayers>
            <indicators>
                <dx:LinearScaleStateIndicatorComponent Center="25, 205" 
                    Name="linearGauge9_Indicator1" ScaleID="scale1" Size="25, 25" ZOrder="-100">
                </dx:LinearScaleStateIndicatorComponent>
            </indicators>
            <labels>
                <dx:LabelComponent AppearanceText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:Black&quot;/&gt;" 
                    Name="linearGauge9_Label1" Position="25, 185" Text="" ZOrder="-1001" />
            </labels>            
            <scales>
                <dx:LinearScaleComponent AppearanceTickmarkText-Font="Tahoma, 10pt" 
                    EndPoint="62.5, 33" MajorTickCount="6" MajorTickmark-FormatString="{0:F0}" 
                    MajorTickmark-ShapeOffset="6" MajorTickmark-ShapeType="Linear_Style7_3" 
                    MajorTickmark-TextOffset="35" MajorTickmark-TextOrientation="BottomToTop" 
                    MaxValue="50" MinorTickCount="4" MinorTickmark-ShapeOffset="6" 
                    MinorTickmark-ShapeType="Linear_Style7_2" Name="scale1" StartPoint="62.5, 217" 
                    Value="20">
                </dx:LinearScaleComponent>             
            </scales>
            <levels>
                <dx:LinearScaleLevelComponent Name="level1" ScaleID="scale1" ShapeType="Style2" 
                    ZOrder="-50" />
            </levels>
        </dx:LinearGauge>
    </Gauges>
</dx:ASPxGaugeControl>