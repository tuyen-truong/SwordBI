using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxGauges.Gauges.Circular;
using DevExpress.Web.ASPxGauges.Base;
using DevExpress.Web.ASPxGauges.Gauges;
using DevExpress.Web.ASPxGauges.Gauges.Linear;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Core.Drawing;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGauges;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraCharts;
using System.Data;
using System.Data.Common;
using DevExpress.XtraGauges.Core.Base;
using CECOM;
namespace HTLBIWebApp2012
{
    public static class ASPxGaugeControlEx
    {
        #region Các hàm dùng chung
        public static ICircularGauge Get_CGauge(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                return ((ICircularGauge)gaugeControl.Gauges[gaugeIndex]);
            }
            catch { return null; }
        }
        public static ILinearGauge Get_LGauge(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                return ((ILinearGauge)gaugeControl.Gauges[gaugeIndex]);
            }
            catch { return null; }
        }
        public static IWebGauge Get_WGauge(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                return ((IWebGauge)gaugeControl.Gauges[gaugeIndex]);
            }
            catch { return null; }
        }
        public static ICircularGauge Get_CGauge(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                return ((ICircularGauge)gaugeControl.Gauges[gaugeName]);
            }
            catch { return null; }
        }
        public static ILinearGauge Get_LGauge(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                return ((ILinearGauge)gaugeControl.Gauges[gaugeName]);
            }
            catch { return null; }
        }
        public static IWebGauge Get_WGauge(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                return ((IWebGauge)gaugeControl.Gauges[gaugeName]);
            }
            catch { return null; }
        }
        public static GaugeType Get_Gauge<GaugeType>(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                return ((GaugeType)gaugeControl.Gauges[gaugeIndex]);
            }
            catch { return default(GaugeType); }
        }
        public static GaugeType Get_Gauge<GaugeType>(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                return ((GaugeType)gaugeControl.Gauges[gaugeName]);
            }
            catch { return default(GaugeType); }
        }
        public static LabelComponent Get_WGauge_Label(this ASPxGaugeControl gaugeControl, int gaugeIndex, int labelIndex)
        {
            try
            {
                return Get_WGauge(gaugeControl, gaugeIndex).Labels[labelIndex];
            }
            catch { return null; }
        }
        public static LabelComponent Get_WGauge_Label(this ASPxGaugeControl gaugeControl, string gaugeName, int labelIndex)
        {
            try
            {
                return Get_WGauge(gaugeControl, gaugeName).Labels[labelIndex];
            }
            catch { return null; }
        }
        public static LabelComponent Get_WGauge_Label(this ASPxGaugeControl gaugeControl, int gaugeIndex, string labelName)
        {
            try
            {
                return Get_WGauge(gaugeControl, gaugeIndex).Labels[labelName];
            }
            catch { return null; }
        }
        public static LabelComponent Get_WGauge_Label(this ASPxGaugeControl gaugeControl, string gaugeName, string labelName)
        {
            try
            {
                return Get_WGauge(gaugeControl, gaugeName).Labels[labelName];
            }
            catch { return null; }
        }

        public static void Clear_CGauge_BackgroundLayers(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).BackgroundLayers.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_BackgroundLayers(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).BackgroundLayers.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_EffectLayers(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).EffectLayers.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_EffectLayers(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).EffectLayers.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Indicators(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Indicators.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Indicators(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Indicators.Clear();
            }
            catch { }
        }

        public static void Clear_CGauge_Labels(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Labels.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Labels(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Labels.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Markers(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Markers.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Markers(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Markers.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Needles(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Needles.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Needles(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Needles.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_RangeBars(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).RangeBars.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_RangeBars(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).RangeBars.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Scales(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Scales.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_Scales(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Scales.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_SpindleCaps(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).SpindleCaps.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_SpindleCaps(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).SpindleCaps.Clear();
            }
            catch { }
        }

        public static void Clear_LGauge_BackgroundLayers(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).BackgroundLayers.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_BackgroundLayers(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).BackgroundLayers.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_EffectLayers(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).EffectLayers.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_EffectLayers(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).EffectLayers.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Indicators(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Indicators.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Indicators(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Indicators.Clear();
            }
            catch { }
        }

        public static void Clear_LGauge_Labels(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Labels.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Labels(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Labels.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Markers(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Markers.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Markers(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Markers.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_RangeBars(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).RangeBars.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_RangeBars(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).RangeBars.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Scales(this ASPxGaugeControl gaugeControl, int gaugeIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Scales.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_Scales(this ASPxGaugeControl gaugeControl, string gaugeName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Scales.Clear();
            }
            catch { }
        }

        public static void Set_StartEndAngle(this ASPxGaugeControl gaugeControl, BackgroundLayerShapeType bkgShape)
        {
            var myScale = gaugeControl.Get_CGauge_Scale(0, 0);
            var str = bkgShape.ToString();
            if (str.StartsWith("CircularFull"))
            {
                myScale.StartAngle = -240;
                myScale.EndAngle = 60;
            }
            else if (str.StartsWith("CircularHalf"))
            {
                myScale.StartAngle = -180;
                myScale.EndAngle = 0;
            }
            else if (str.StartsWith("CircularQuarter"))
            {
                myScale.StartAngle = -180;
                myScale.EndAngle = -90;
            }
            else if (str.StartsWith("CircularThreeFourth"))
            {
                myScale.StartAngle = -210;
                myScale.EndAngle = 30;
            }
        }
        #endregion

        #region Các hàm xóa tương tác trên CircularGauge
        public static void Clear_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorIndex].States.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorIndex].States.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorName].States.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorName].States.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int scaleIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Scales[scaleIndex].Ranges.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int scaleIndex)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Scales[scaleIndex].Ranges.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string scaleName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeIndex).Scales[scaleName].Ranges.Clear();
            }
            catch { }
        }
        public static void Clear_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string scaleName)
        {
            try
            {
                Get_CGauge(gaugeControl, gaugeName).Scales[scaleName].Ranges.Clear();
            }
            catch { }
        }
        #endregion

        #region Các hàm xóa tương tác trên LinearGauge
        public static void Clear_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Indicators[indicatorIndex].States.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Indicators[indicatorIndex].States.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Indicators[indicatorName].States.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Indicators[indicatorName].States.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int scaleIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Scales[scaleIndex].Ranges.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int scaleIndex)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Scales[scaleIndex].Ranges.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string scaleName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeIndex).Scales[scaleName].Ranges.Clear();
            }
            catch { }
        }
        public static void Clear_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string scaleName)
        {
            try
            {
                Get_LGauge(gaugeControl, gaugeName).Scales[scaleName].Ranges.Clear();
            }
            catch { }
        }
        #endregion

        #region Các hàm Lấy đối tượng của CircularGauge
        public static ArcScaleSpindleCapComponent Get_CGauge_SpindleCap(this ASPxGaugeControl gaugeControl, int gaugeIndex, int spindleCapIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).SpindleCaps[spindleCapIndex];
            }
            catch { return null; }
        }
        public static ArcScaleEffectLayerComponent Get_CGauge_EffectLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, int effectLayerIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).EffectLayers[effectLayerIndex];
            }
            catch { return null; }
        }
        public static ArcScaleBackgroundLayerComponent Get_CGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, int backgroundLayerIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).BackgroundLayers[backgroundLayerIndex];
            }
            catch { return null; }
        }
        public static ArcScaleStateIndicatorComponent Get_CGauge_StateIndicator(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorIndex];
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, int stateIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorIndex].States[stateIndex] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, string stateName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorIndex].States[stateName] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ArcScaleComponent Get_CGauge_Scale(this ASPxGaugeControl gaugeControl, int gaugeIndex, int scaleIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Scales[scaleIndex];
            }
            catch { return null; }
        }
        public static ArcScaleRangeBarComponent Get_CGauge_RangeBar(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeBarIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).RangeBars[rangeBarIndex];
            }
            catch { return null; }
        }
        public static ArcScaleMarkerComponent Get_CGauge_Marker(this ASPxGaugeControl gaugeControl, int gaugeIndex, int markerIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Markers[markerIndex];
            }
            catch { return null; }
        }
        public static ArcScaleNeedleComponent Get_CGauge_Needle(this ASPxGaugeControl gaugeControl, int gaugeIndex, int needleIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Needles[needleIndex];
            }
            catch { return null; }
        }
        //
        public static ArcScaleSpindleCapComponent Get_CGauge_SpindleCap(this ASPxGaugeControl gaugeControl, string gaugeName, int spindleCapIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).SpindleCaps[spindleCapIndex];
            }
            catch { return null; }
        }
        public static ArcScaleEffectLayerComponent Get_CGauge_EffectLayer(this ASPxGaugeControl gaugeControl, string gaugeName, int effectLayerIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).EffectLayers[effectLayerIndex];
            }
            catch { return null; }
        }
        public static ArcScaleBackgroundLayerComponent Get_CGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, string gaugeName, int backgroundLayerIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).BackgroundLayers[backgroundLayerIndex];
            }
            catch { return null; }
        }
        public static ArcScaleStateIndicatorComponent Get_CGauge_StateIndicator(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorIndex];
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, int stateIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorIndex].States[stateIndex] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, string stateName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorIndex].States[stateName] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ArcScaleComponent Get_CGauge_Scale(this ASPxGaugeControl gaugeControl, string gaugeName, int scaleIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Scales[scaleIndex];
            }
            catch { return null; }
        }
        public static ArcScaleRangeBarComponent Get_CGauge_RangeBar(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeBarIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).RangeBars[rangeBarIndex];
            }
            catch { return null; }
        }
        public static ArcScaleMarkerComponent Get_CGauge_Marker(this ASPxGaugeControl gaugeControl, string gaugeName, int markerIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Markers[markerIndex];
            }
            catch { return null; }
        }
        public static ArcScaleNeedleComponent Get_CGauge_Needle(this ASPxGaugeControl gaugeControl, string gaugeName, int needleIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Needles[needleIndex];
            }
            catch { return null; }
        }
        //
        public static ArcScaleSpindleCapComponent Get_CGauge_SpindleCap(this ASPxGaugeControl gaugeControl, int gaugeIndex, string spindleCapName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).SpindleCaps[spindleCapName];
            }
            catch { return null; }
        }
        public static ArcScaleEffectLayerComponent Get_CGauge_EffectLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, string effectLayerName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).EffectLayers[effectLayerName];
            }
            catch { return null; }
        }
        public static ArcScaleBackgroundLayerComponent Get_CGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, string backgroundLayerName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).BackgroundLayers[backgroundLayerName];
            }
            catch { return null; }
        }
        public static ArcScaleStateIndicatorComponent Get_CGauge_StateIndicator(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorName];
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, int stateIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorName].States[stateIndex] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, string stateName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Indicators[indicatorName].States[stateName] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ArcScaleComponent Get_CGauge_Scale(this ASPxGaugeControl gaugeControl, int gaugeIndex, string scaleName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Scales[scaleName];
            }
            catch { return null; }
        }
        public static ArcScaleRangeBarComponent Get_CGauge_RangeBar(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeBarName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).RangeBars[rangeBarName];
            }
            catch { return null; }
        }
        public static ArcScaleMarkerComponent Get_CGauge_Marker(this ASPxGaugeControl gaugeControl, int gaugeIndex, string markerName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Markers[markerName];
            }
            catch { return null; }
        }
        public static ArcScaleNeedleComponent Get_CGauge_Needle(this ASPxGaugeControl gaugeControl, int gaugeIndex, string needleName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeIndex).Needles[needleName];
            }
            catch { return null; }
        }
        //
        public static ArcScaleSpindleCapComponent Get_CGauge_SpindleCap(this ASPxGaugeControl gaugeControl, string gaugeName, string spindleCapName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).SpindleCaps[spindleCapName];
            }
            catch { return null; }
        }
        public static ArcScaleEffectLayerComponent Get_CGauge_EffectLayer(this ASPxGaugeControl gaugeControl, string gaugeName, string effectLayerName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).EffectLayers[effectLayerName];
            }
            catch { return null; }
        }
        public static ArcScaleBackgroundLayerComponent Get_CGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, string gaugeName, string backgroundLayerName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).BackgroundLayers[backgroundLayerName];
            }
            catch { return null; }
        }
        public static ArcScaleStateIndicatorComponent Get_CGauge_StateIndicator(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorName];
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, int stateIndex)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorName].States[stateIndex] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, string stateName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Indicators[indicatorName].States[stateName] as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }

        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int stateIndex)
        {
            try
            {
                return Get_CGauge_IndicatorStateWeb(gaugeControl, 0, 0, stateIndex) as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }
        public static ScaleIndicatorStateWeb Get_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string stateName)
        {
            try
            {
                return Get_CGauge_IndicatorStateWeb(gaugeControl, 0, 0, stateName) as ScaleIndicatorStateWeb;
            }
            catch { return null; }
        }

        public static ArcScaleComponent Get_CGauge_Scale(this ASPxGaugeControl gaugeControl, string gaugeName, string scaleName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Scales[scaleName];
            }
            catch { return null; }
        }
        public static ArcScaleRangeBarComponent Get_CGauge_RangeBar(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeBarName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).RangeBars[rangeBarName];
            }
            catch { return null; }
        }
        public static ArcScaleMarkerComponent Get_CGauge_Marker(this ASPxGaugeControl gaugeControl, string gaugeName, string markerName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Markers[markerName];
            }
            catch { return null; }
        }
        public static ArcScaleNeedleComponent Get_CGauge_Needle(this ASPxGaugeControl gaugeControl, string gaugeName, string needleName)
        {
            try
            {
                return Get_CGauge(gaugeControl, gaugeName).Needles[needleName];
            }
            catch { return null; }
        }
        #endregion

        #region Các hàm lấy đối tượng của LinearGauge
        //
        public static LinearScaleLevelComponent Get_LGauge_SpindleCap(this ASPxGaugeControl gaugeControl, int gaugeIndex, int levelIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Levels[levelIndex];
            }
            catch { return null; }
        }
        public static LinearScaleEffectLayerComponent Get_LGauge_EffectLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, int effectLayerIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).EffectLayers[effectLayerIndex];
            }
            catch { return null; }
        }
        public static LinearScaleBackgroundLayerComponent Get_LGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, int backgroundLayerIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).BackgroundLayers[backgroundLayerIndex];
            }
            catch { return null; }
        }
        public static LinearScaleStateIndicatorComponent Get_LGauge_StateIndicator(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Indicators[indicatorIndex];
            }
            catch { return null; }
        }
        public static LinearScaleComponent Get_LGauge_Scale(this ASPxGaugeControl gaugeControl, int gaugeIndex, int scaleIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Scales[scaleIndex];
            }
            catch { return null; }
        }
        public static LinearScaleRangeBarComponent Get_LGauge_RangeBar(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeBarIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).RangeBars[rangeBarIndex];
            }
            catch { return null; }
        }
        public static LinearScaleMarkerComponent Get_LGauge_Marker(this ASPxGaugeControl gaugeControl, int gaugeIndex, int markerIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Markers[markerIndex];
            }
            catch { return null; }
        }
        //
        public static LinearScaleLevelComponent Get_LGauge_SpindleCap(this ASPxGaugeControl gaugeControl, string gaugeName, int levelIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Levels[levelIndex];
            }
            catch { return null; }
        }
        public static LinearScaleEffectLayerComponent Get_LGauge_EffectLayer(this ASPxGaugeControl gaugeControl, string gaugeName, int effectLayerIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).EffectLayers[effectLayerIndex];
            }
            catch { return null; }
        }
        public static LinearScaleBackgroundLayerComponent Get_LGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, string gaugeName, int backgroundLayerIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).BackgroundLayers[backgroundLayerIndex];
            }
            catch { return null; }
        }
        public static LinearScaleStateIndicatorComponent Get_LGauge_StateIndicator(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Indicators[indicatorIndex];
            }
            catch { return null; }
        }
        public static LinearScaleComponent Get_LGauge_Scale(this ASPxGaugeControl gaugeControl, string gaugeName, int scaleIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Scales[scaleIndex];
            }
            catch { return null; }
        }
        public static LinearScaleRangeBarComponent Get_LGauge_RangeBar(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeBarIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).RangeBars[rangeBarIndex];
            }
            catch { return null; }
        }
        public static LinearScaleMarkerComponent Get_LGauge_Marker(this ASPxGaugeControl gaugeControl, string gaugeName, int markerIndex)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Markers[markerIndex];
            }
            catch { return null; }
        }
        //
        public static LinearScaleLevelComponent Get_LGauge_SpindleCap(this ASPxGaugeControl gaugeControl, int gaugeIndex, string levelName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Levels[levelName];
            }
            catch { return null; }
        }
        public static LinearScaleEffectLayerComponent Get_LGauge_EffectLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, string effectLayerName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).EffectLayers[effectLayerName];
            }
            catch { return null; }
        }
        public static LinearScaleBackgroundLayerComponent Get_LGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, int gaugeIndex, string backgroundLayerName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).BackgroundLayers[backgroundLayerName];
            }
            catch { return null; }
        }
        public static LinearScaleStateIndicatorComponent Get_LGauge_StateIndicator(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Indicators[indicatorName];
            }
            catch { return null; }
        }
        public static LinearScaleComponent Get_LGauge_Scale(this ASPxGaugeControl gaugeControl, int gaugeIndex, string scaleName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Scales[scaleName];
            }
            catch { return null; }
        }
        public static LinearScaleRangeBarComponent Get_LGauge_RangeBar(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeBarName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).RangeBars[rangeBarName];
            }
            catch { return null; }
        }
        public static LinearScaleMarkerComponent Get_LGauge_Marker(this ASPxGaugeControl gaugeControl, int gaugeIndex, string markerName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeIndex).Markers[markerName];
            }
            catch { return null; }
        }
        //
        public static LinearScaleLevelComponent Get_LGauge_SpindleCap(this ASPxGaugeControl gaugeControl, string gaugeName, string levelName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Levels[levelName];
            }
            catch { return null; }
        }
        public static LinearScaleEffectLayerComponent Get_LGauge_EffectLayer(this ASPxGaugeControl gaugeControl, string gaugeName, string effectLayerName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).EffectLayers[effectLayerName];
            }
            catch { return null; }
        }
        public static LinearScaleBackgroundLayerComponent Get_LGauge_BackgroundLayer(this ASPxGaugeControl gaugeControl, string gaugeName, string backgroundLayerName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).BackgroundLayers[backgroundLayerName];
            }
            catch { return null; }
        }
        public static LinearScaleStateIndicatorComponent Get_LGauge_StateIndicator(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Indicators[indicatorName];
            }
            catch { return null; }
        }
        public static LinearScaleComponent Get_LGauge_Scale(this ASPxGaugeControl gaugeControl, string gaugeName, string scaleName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Scales[scaleName];
            }
            catch { return null; }
        }
        public static LinearScaleRangeBarComponent Get_LGauge_RangeBar(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeBarName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).RangeBars[rangeBarName];
            }
            catch { return null; }
        }
        public static LinearScaleMarkerComponent Get_LGauge_Marker(this ASPxGaugeControl gaugeControl, string gaugeName, string markerName)
        {
            try
            {
                return Get_LGauge(gaugeControl, gaugeName).Markers[markerName];
            }
            catch { return null; }
        }
        #endregion

        #region Các hàm thêm đối tượng vào tập hợp tương ứng của CircularGauge

        #region IndicatorStateWeb
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                
                el.BeginUpdate();

                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                
                el.EndUpdate();

                rootShape.Add(el);

                var myStates = Get_CGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_CGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.AddRange(elem);
            }
            catch { }
        }
        #endregion

        #region RangeWeb
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_CGauge_RangeWeb(gaugeControl, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, 0, 0).Ranges;
                var range = new ArcScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, IRange elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, 0, 0).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, IRange[] elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, 0, 0).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_CGauge_RangeWeb(gaugeControl, gaugeName, rangeName, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeName, rangeName).Ranges;
                var range = new ArcScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, IRange elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeName, rangeName).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, IRange[] elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeName, rangeName).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_CGauge_RangeWeb(gaugeControl, gaugeIndex, rangeIndex, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeIndex, rangeIndex).Ranges;
                var range = new ArcScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, IRange elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeIndex, rangeIndex).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, IRange[] elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeIndex, rangeIndex).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_CGauge_RangeWeb(gaugeControl, gaugeName, rangeIndex, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeName, rangeIndex).Ranges;
                var range = new ArcScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, IRange elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeName, rangeIndex).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, IRange[] elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeName, rangeIndex).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_CGauge_RangeWeb(gaugeControl, gaugeIndex, rangeName, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeIndex, rangeName).Ranges;
                var range = new ArcScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, IRange elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeIndex, rangeName).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_CGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, IRange[] elem)
        {
            try
            {
                var myRanges = Get_CGauge_Scale(gaugeControl, gaugeIndex, rangeName).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }
        #endregion

        #endregion

        #region Các hàm thêm đối tượng vào tập hợp tương ứng của LinearGauge

        #region IndicatorStateWeb
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_LGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, 0, 0).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string indicatorName, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorName).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int indicatorIndex, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorIndex).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int indicatorIndex, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeName, indicatorIndex).States;
                myStates.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, string name, float startValue, float intervalLength, Color shapeColor)
        {
            try
            {
                var stateWeb = new ScaleIndicatorStateWeb()
                {
                    Name = name,
                    StartValue = startValue,
                    IntervalLength = intervalLength
                };
                var rootShape = stateWeb.Shape as ComplexShape;
                rootShape.Collection.Clear(); //remove old shapes
                var el = new EllipseShape(new RectangleF2D(0, 0, 70, 70)); //ellipse
                el.BeginUpdate();
                el.Name = "el";
                el.Appearance.ContentBrush = new SolidBrushObject(shapeColor);
                el.Appearance.BorderBrush = new SolidBrushObject(Color.Gray);
                el.Appearance.BorderWidth = 5F;
                el.EndUpdate();
                rootShape.Add(el);

                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.Add(stateWeb);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, string name, float startValue, float intervalLength, StateIndicatorShapeType shapeType)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.Add(new ScaleIndicatorStateWeb() { Name = name, StartValue = startValue, IntervalLength = intervalLength, ShapeType = shapeType });
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, IIndicatorState elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_IndicatorStateWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string indicatorName, IIndicatorState[] elem)
        {
            try
            {
                var myStates = Get_LGauge_StateIndicator(gaugeControl, gaugeIndex, indicatorName).States;
                myStates.AddRange(elem);
            }
            catch { }
        }
        #endregion

        #region RangeWeb
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_LGauge_RangeWeb(gaugeControl, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, 0, 0).Ranges;
                var range = new LinearScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, IRange elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, 0, 0).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, IRange[] elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, 0, 0).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_LGauge_RangeWeb(gaugeControl, gaugeName, rangeName, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeName, rangeName).Ranges;
                var range = new LinearScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, IRange elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeName, rangeName).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, string rangeName, IRange[] elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeName, rangeName).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_LGauge_RangeWeb(gaugeControl, gaugeIndex, rangeIndex, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeIndex, rangeIndex).Ranges;
                var range = new LinearScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, IRange elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeIndex, rangeIndex).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, int rangeIndex, IRange[] elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeIndex, rangeIndex).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_LGauge_RangeWeb(gaugeControl, gaugeName, rangeIndex, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeName, rangeIndex).Ranges;
                var range = new LinearScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, IRange elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeName, rangeIndex).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, string gaugeName, int rangeIndex, IRange[] elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeName, rangeIndex).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }

        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, Color color)
        {
            var border = new SolidBrushObject(Color.Gray);
            var content = new SolidBrushObject(color);
            var appShape = new BaseShapeAppearance(border, content);
            appShape.BorderWidth = 1;
            Add_LGauge_RangeWeb(gaugeControl, gaugeIndex, rangeName, name, startValue, endValue, forPercent, startThickness, endThickness, shapeOffset, appShape);
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, string name, float startValue, float endValue, bool forPercent, float startThickness, float endThickness, float shapeOffset, BaseShapeAppearance appBrush)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeIndex, rangeName).Ranges;
                var range = new LinearScaleRangeWeb();
                range.Name = name;
                if (forPercent)
                {
                    range.StartPercent = startValue;
                    range.EndPercent = endValue;
                }
                else
                {
                    range.StartValue = startValue;
                    range.EndValue = endValue;
                }
                range.StartThickness = startThickness;
                range.EndThickness = endThickness;
                range.ShapeOffset = shapeOffset;
                range.AppearanceRange.ContentBrush = appBrush.ContentBrush;
                range.AppearanceRange.BorderBrush = appBrush.BorderBrush;
                range.AppearanceRange.BorderWidth = appBrush.BorderWidth;
                myRanges.Add(range);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, IRange elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeIndex, rangeName).Ranges;
                myRanges.Add(elem);
            }
            catch { }
        }
        public static void Add_LGauge_RangeWeb(this ASPxGaugeControl gaugeControl, int gaugeIndex, string rangeName, IRange[] elem)
        {
            try
            {
                var myRanges = Get_LGauge_Scale(gaugeControl, gaugeIndex, rangeName).Ranges;
                myRanges.AddRange(elem);
            }
            catch { }
        }
        #endregion

        #endregion
    }

    public static class WebControlEx
    {
        public static string RenderToHTMLStr(this WebControl ctrl)
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                var sw = new System.IO.StringWriter(sb);
                var htw = new HtmlTextWriter(sw);
                //ctrl.RenderBeginTag(htw);
                ctrl.RenderControl(htw);
                //ctrl.RenderEndTag(htw);
                return sb.ToString();
            }
            catch { return ""; }
        }
    }

    public static class ControlCollectionEx
    { 
        public static Control At(this ControlCollection ctrls, string id_or_uniqueID_or_clientID)
        {
            var found = ctrls.OfType<Control>()
                .FirstOrDefault(p => p.ID == id_or_uniqueID_or_clientID || 
                                     p.UniqueID == id_or_uniqueID_or_clientID || 
                                     p.ClientID == id_or_uniqueID_or_clientID);
            return found;
        }
    }

    public static class WebChartControlEx
    {
        public static XYDiagram Get_XYDiagram(this WebChartControl wc)
        {
            try
            {
                return wc.Diagram as XYDiagram;
            }
            catch { return null; }
        }
        public static AxisXBase Get_AxisX(this WebChartControl wc)
        {
            try
            {
                return wc.Get_XYDiagram().AxisX as AxisXBase;
            }
            catch { return null; }
        }
        public static AxisYBase Get_AxisY(this WebChartControl wc)
        {
            try
            {
                return wc.Get_XYDiagram().AxisY as AxisYBase;
            }
            catch { return null; }
        }

        public static void Set_TitleX(this WebChartControl wc, string title)
        {
            var axisX = wc.Get_AxisX();
            axisX.Title.Text = title;
        }
        public static void Set_TitleY(this WebChartControl wc, string title)
        {
            var axisY = wc.Get_AxisY();
            axisY.Title.Text = title;
        }
        /// <summary>
        /// Add các series riêng lẽ vào webchart hiện hành.
        /// </summary>
        public static void Set_Series(this WebChartControl chart, params HTLBIChartSeriesInfo[] serInf)
        {
            if (serInf == null || serInf.Length == 0) return;
            foreach (var obj in serInf)
            {
                try
                {
                    var ser = new Series(obj.Name, obj.ViewTypeDefault);
                    ser.ArgumentDataMember = obj.ArgDataMember;
                    ser.ValueDataMembers.Clear();
                    ser.ValueDataMembers.AddRange(obj.ValDataMember);
                    ser.Label.Visible = obj.VisibleLabel;
                    ser.DataSource = obj.DataSource;
                    chart.Series.Add(ser);
                }
                catch {}
                continue;
            }
        }

        /// <summary>
        /// Lấy đối tượng series của Chart Control (hoặc SeriesTemplate nếu index không được cung cấp)
        /// </summary>
        /// <param name="index">
        /// <para>Nếu được cung cấp giá trị hợp lệ: sẽ trả về đối tượng Series đầu tiên của index; 
        /// ngược lại sẽ trả về đối tượng SeriesTemplate</para>
        /// </param>
        public static SeriesBase Get_Series(this WebChartControl wc, params int[] index)
        {
            if (index != null && index.Length > 0)
                return wc.Series[index.First()];
            return wc.SeriesTemplate;
        }
        public static void Set_VisibleSeries(this WebChartControl wc, bool visible)
        {
            if (wc.Series == null || wc.Series.Count == 0) return;
            foreach (SeriesBase series in wc.Series)
                series.Visible = visible;
        }

        public static PieSeriesLabel Get_Label_Pie(this WebChartControl wc, params int[] series_Index)
        {
            return wc.Get_Series(series_Index).Label as PieSeriesLabel;
        }
        public static PieSeriesViewBase Get_View_Pie(this WebChartControl wc, params int[] series_Index)
        {
            return wc.Get_Series(series_Index).View as PieSeriesViewBase;
        }

        public static DoughnutSeriesLabel Get_Label_Doughnut(this WebChartControl wc, params int[] series_Index)
        {
            return wc.Get_Series(series_Index).Label as DoughnutSeriesLabel;
        }
        public static DoughnutSeriesView Get_View_Doughnut(this WebChartControl wc, params int[] series_Index)
        {
            return wc.Get_Series(series_Index).View as DoughnutSeriesView;
        }
        /// <summary>
        /// Chuyển ViewType cho một series cụ thể được chỉ ra bỡi tên của series.
        /// </summary>
        /// <param name="serName">Tên của series.</param>
        public static void SetChartType(this WebChartControl wc, string chartTypeName, string serName)
        {
            try
            {
                if (wc.Series == null || wc.Series.Count == 0) return;
                wc.Series[serName].ChangeView(Helpers.ToEnum<ViewType>(chartTypeName));
            }
            catch { }
        }
        /// <summary>
        /// Chuyển ViewType cho một series cụ thể được chỉ ra bỡi index của series.
        /// </summary>
        /// <param name="serName">Index của series.</param>
        public static void SetChartType(this WebChartControl wc, string chartTypeName, int serIndex)
        {
            try
            {
                if (wc.Series == null || wc.Series.Count == 0) return;
                wc.Series[serIndex].ChangeView(Helpers.ToEnum<ViewType>(chartTypeName));
            }
            catch { }
        }
        /// <summary>
        /// Chuyển ViewType của 'SeriesTemplate' trên chart
        /// </summary>
        public static void SetChartType(this WebChartControl wc, string chartTypeName)
        {
            if (wc.SeriesTemplate == null) return;
            wc.SeriesTemplate.ChangeView(Helpers.ToEnum<ViewType>(chartTypeName));
        }
        /// <summary>
        /// Chuyển ViewType cho SeriesTemplate hoặc tất cả các series trong tập hợp 'Series'.
        /// </summary>
        /// <param name="chartTypeName">Tên của ViewType ở dạng chuỗi.</param>
        /// <param name="noTemplate">Nếu được bật thì tất cả các series trong tập hợp series được áp theo cùng 1 ViewType.</param>
        public static void SetChartType(this WebChartControl wc, string chartTypeName, bool noTemplate)
        {
            if (noTemplate)
            {
                if (wc.Series == null || wc.Series.Count == 0) return;
                foreach (Series sr in wc.Series)
                    sr.ChangeView(Helpers.ToEnum<ViewType>(chartTypeName));
            }
            else
                wc.SetChartType(chartTypeName);
        }
        /// <summary>
        /// Chuyển ViewType của 'SeriesTemplate' trên chart
        /// </summary>
        public static void SetChartType(this WebChartControl wc, ViewType chartType)
        {
            if (wc.SeriesTemplate == null) return;
            wc.SeriesTemplate.ChangeView(chartType);
        }
        /// <summary>
        /// Chuyển ViewType cho SeriesTemplate hoặc tất cả các series trong tập hợp 'Series'.
        /// </summary>
        /// <param name="chartTypeName">Tên của ViewType ở dạng Enum.</param>
        /// <param name="noTemplate">Nếu được bật thì tất cả các series trong tập hợp series được áp theo cùng 1 ViewType.</param>
        public static void SetChartType(this WebChartControl wc, ViewType chartType, bool noTemplate)
        {
            if (noTemplate)
            {
                if (wc.Series == null || wc.Series.Count == 0) return;
                foreach (Series sr in wc.Series)
                    sr.ChangeView(chartType);
            }
            else
                wc.SetChartType(chartType);
        }
        public static void Format_SpecialView(this WebChartControl wc, ViewType currentViewType)
        {
            var series = wc.Series[0];
            switch (currentViewType)
            {
                case ViewType.Pie:                    
                    var lbl_Pie = series.Label as PieSeriesLabel;
                    //var pntOpn_Pie = lbl_Pie.PointOptions as PiePointOptions;
                    var view_Pie = series.View as PieSeriesViewBase;
                    break;
                case ViewType.Doughnut:
                    break;
            }
        }
    }

    public static class StringEx
    {
        public static string DummySet(this string value, int loop)
        {
            var ret = "";
            while (loop > 0)
            {
                ret = ret + value;
                loop--;
            }
            return ret;
        }
        public static string[] Split(this string value, StringSplitOptions options, params string[] separator)
        {
            return value.Split(separator, options);
        }
        public static string[] Split(this string value, string separator, StringSplitOptions options)
        {
            return value.Split(options, separator);
        }
        public static string[] Split(this string value, StringSplitOptions options, params char[] separator)
        {
            return value.Split(separator, options);
        }
        public static string[] Split(this string value, char separator, StringSplitOptions options)
        {
            return value.Split(options, separator);
        }
    }

    public static class ObjectEx
    {
        public static double GetDbl(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<double>(propertyName);
            }
            catch { return 0.0; }
        }
        public static float GetFloat(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<float>(propertyName);
            }
            catch { return 0F; }
        }
        public static decimal GetDec(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<decimal>(propertyName);
            }
            catch { return 0M; }
        }
        public static long GetInt64(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<long>(propertyName);
            }
            catch { return 0L; }
        }
        public static int GetInt32(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<int>(propertyName);
            }
            catch { return 0; }
        }
        public static short GetInt16(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<short>(propertyName);
            }
            catch { return 0; }
        }
        public static byte GetInt8(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<byte>(propertyName);
            }
            catch { return 0; }
        }
        public static string GetStr(this Object obj, string propertyName)
        {
            try
            {
                return obj.GetVal<string>(propertyName);
            }
            catch { return ""; }
        }
        public static T GetVal<T>(this Object obj, string propertyName)
        {
            try
            {
                return Lib.GetPropertyValue<T>(obj, propertyName);
            }
            catch { return default(T); }
        }
        public static object GetVal(this Object obj, string propertyName)
        {
            try
            {
                return Lib.GetPropertyValue(obj, propertyName);
            }
            catch { return null; }
        }
    }

    public static class EnumerableEx
    {       
        /// <summary>
        /// Checking for exists, if first element of the sequence that satisfies a condition or a 
        /// default value if no such element is found. whether Null or NotNull
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>true: if exists with condition specified by predicate; otherwise return false.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>        
        public static bool Exists<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.FirstOrDefault(predicate) != null;
        }

        public static void RemoveAll(this ControlCollection source)
        {
            source.RemoveAll(p => true);
        }
        public static void RemoveAll(this ControlCollection source, Func<Control, bool> predicate)
        {
            var ctrl = source.OfType<Control>().FirstOrDefault(predicate);
            if (ctrl == null) return;
            source.Remove(ctrl);
        }
    }
    
    public static class CollectionEx
    {
        //public static void Add<T>(this ICollection<T> source, Func<T, bool> predicate, bool isOnlyAddIfNotExists)
        //{
        //    var items = source.Where(predicate);
        //}
    }

    public static class DataContextEx
    {
        public static DbDataReader ExecuteReader(this System.Data.Linq.DataContext ctx, string sql)
        {
            return ctx.ExecuteReader(sql, CommandType.Text);
        }
        public static DbDataReader ExecuteReader(this System.Data.Linq.DataContext ctx, string sql, CommandType cmdType)
        {
            try
            {
                var cmd = ctx.Connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;
                cmd.Connection.Open();
                var ret = cmd.ExecuteReader();
                return ret;
            }
            catch { return null; }
        }
    }
}