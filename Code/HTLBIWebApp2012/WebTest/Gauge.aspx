<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gauge.aspx.cs" Inherits="WebTest.Gauge" %>

<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.State" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGaugeControl ID="ASPxGaugeControl1" runat="server" Height="260px" 
            Width="260px" BackColor="White" ClientIDMode="AutoID" Value="0">
            <Gauges>
                <dx:CircularGauge Bounds="0, 0, 260, 260" Name="cGauge1">
                    <scales>
                        <dx:ArcScaleComponent AppearanceTickmarkText-Font="Arial Narrow, 11pt, style=Bold" 
                            AppearanceTickmarkText-TextBrush="&lt;BrushObject Type=&quot;Solid&quot; Data=&quot;Color:#C0C0FF&quot;/&gt;" 
                            Center="125, 125" EndAngle="30" MajorTickCount="10" 
                            MajorTickmark-AllowTickOverlap="True" MajorTickmark-FormatString="{0:F0}" 
                            MajorTickmark-ShapeOffset="-9" MajorTickmark-ShapeType="Circular_Style2_2" 
                            MajorTickmark-TextOffset="-22" MajorTickmark-TextOrientation="LeftToRight" 
                            MaxValue="280" MinorTickCount="4" MinorTickmark-ShapeType="Circular_Style2_1" 
                            Name="scale1" RadiusX="91" RadiusY="91" StartAngle="-240">
                        </dx:ArcScaleComponent>
                    </scales>
                    <backgroundlayers>
                        <dx:ArcScaleBackgroundLayerComponent Name="bg1" ScaleID="scale1" 
                            ShapeType="CircularFull_Style2" ZOrder="1000" />
                    </backgroundlayers>
                    <needles>
                        <dx:ArcScaleNeedleComponent EndOffset="-6" Name="needle1" ScaleID="scale1" 
                            ShapeType="CircularFull_Style2" StartOffset="9" ZOrder="-50" />
                    </needles>
                    <spindlecaps>
                        <dx:ArcScaleSpindleCapComponent Name="cap1" ScaleID="scale1" 
                            ShapeType="CircularFull_Style2" Size="32, 32" ZOrder="-100" />
                    </spindlecaps>
                </dx:CircularGauge>
            </Gauges>
        </dx:ASPxGaugeControl>
    </div>
    </form>
</body>
</html>
