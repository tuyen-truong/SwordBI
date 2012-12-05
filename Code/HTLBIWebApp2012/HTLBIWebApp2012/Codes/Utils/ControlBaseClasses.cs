using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGauges.Gauges;
using DevExpress.Web.ASPxGauges.Gauges.Circular;
using DevExpress.Web.ASPxGauges.Base;
using DevExpress.Web.ASPxGauges;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
using System.Drawing;
using DevExpress.Web.ASPxEditors;
using System.IO;
using System.Text;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using CECOM;
using DevExpress.Web.ASPxGridView;
using System.Data;

namespace HTLBIWebApp2012
{
    #region Widget controls
    public abstract class WidgetCtrlBase : System.Web.UI.UserControl
    {
        /// <summary>
        /// Thông tin thiết lập cho control
        /// </summary>
        public WidgetBase Sett { get; set; }
        public bool IsDemo { get; set; }
        /// <summary>
        /// Tên thể hiện của control phía client theo template: widgetCode_type; VD: wdg_20120101_chart
        /// </summary>
        public string MyClientInstanceName { get; set; }
        public virtual Size GetSize() { return new Size(); }
        public virtual bool RegisterStartupScript(Control updatePanel, string javaScriptCode)
        {
            try
            {
                if (string.IsNullOrEmpty(javaScriptCode)) return true;
                if (updatePanel == null) return true;
                ScriptManager.RegisterStartupScript(updatePanel, typeof(string), string.Format("{0}_Script", this.GetType().Name), javaScriptCode, true);
                return true;
            }
            catch { }
            return false;
        }
        public virtual bool RegisterStartupScript(string updatePanelUniqueName, string javaScriptCode)
        {
            try
            {
                var udp = this.Page.Controls.At("ctl00")
                                   .Controls.At("aspnetForm")
                                   .Controls.At("ctl00$MainContent")
                                   .Controls.At(updatePanelUniqueName);
                return this.RegisterStartupScript(udp, javaScriptCode);
            }
            catch { return false; }
        }
        public virtual bool RegisterStartupScript(string javaScriptCode)
        {
            return this.RegisterStartupScript("ctl00$MainContent$upp_Setting", javaScriptCode);
        }
    }
    public class ChartCtrlBase : WidgetCtrlBase
    {
        private WebChartControl myChart;
        public WebChartControl MyChart
        {
            get
            {
                if (this.myChart == null)
                    this.myChart = this.FindControl("chart") as WebChartControl;
                return this.myChart;
            }
        }
        public WidgetChart MySett
        {
            get
            {
                if (this.Sett == null) this.Sett = new WidgetChart();
                return this.Sett as WidgetChart;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.InitCustom_Charts();
            //this.Load_Data();
        }
        public override Size GetSize()
        {
            return new Size((int)this.MyChart.Width.Value, (int)this.MyChart.Height.Value);
        }
        // Tùy chỉnh chart theo các thiết lập người dùng: trường hợp dùng SeriesTemplate
        public virtual void InitCustom_Charts()
        {
            try
            {
                if (this.MyChart == null) return;
                var chart = this.MyChart;
                // Thiết lập chung.
                chart.ClientInstanceName = this.MyClientInstanceName;

                // Lưu các thiết lập của người dùng vào session để sử dụng cho việc tải dữ liệu
                var wdg = this.MySett;
                if (wdg == null) return;
                //MySession.LayoutDefine_Preview_Widget = wdg;

                // Các khai báo dùng chung
                var titleFont = new Font("Arial", 7, FontStyle.Bold);
                var labelFont = new Font("Arial", 6, FontStyle.Bold);

                // Định dạng các thông số phổ biến
                Helpers.FormatCommon_ChartControl(chart, 2);

                // Định dạng các thuộc tính riêng của chart theo các thiết lập của người dùng 
                chart.BackColor = Color.Transparent;
                chart.Legend.Visible = true;
                chart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                chart.Legend.Direction = LegendDirection.LeftToRight;
                chart.Legend.Padding.All = 0;
                chart.Legend.BackColor = Color.Transparent;
                chart.Legend.Border.Visible = false;

                //chart.Legend.
                chart.Width = new Unit(wdg.Width);
                chart.Height = new Unit(wdg.Height);
                chart.Padding.All = 0;

                
                
                // Runtime format, add Dim & Measure
                // Add series...
                var xFields = wdg.Get_XFields();
                var argMember = xFields[0].Prop1;
                var serInfo = wdg.Get_YFields()
                    .Select(p => new HTLBIChartSeriesInfo(p.Prop2, this.MySett.Get_Data(), argMember, p.Prop1)).ToArray();
                chart.Series.Clear();
                chart.Set_Series(serInfo);
                //////////////////////////////////////


                // Định dạng XYDiagram của chart
                var xyDia = chart.Diagram as XYDiagram;
                xyDia.ScrollingOptions.UseKeyboard = true;
                xyDia.ScrollingOptions.UseMouse = true;
                xyDia.ScrollingOptions.UseScrollBars = true;
                xyDia.Margins.All = 0;
                var axisX = xyDia.AxisX;
                var axisY = xyDia.AxisY;
                if (wdg.RotatedXY)
                {
                    axisX.Reverse = true;
                    axisX.Label.Angle = -50;
                    axisY.Label.Angle = -30;
                    xyDia.Rotated = true;
                    xyDia.PaneLayoutDirection = PaneLayoutDirection.Horizontal;
                }
                else
                {
                    axisX.Label.Angle = -30;
                    axisY.Label.Angle = -50;
                    xyDia.Rotated = false;
                    xyDia.PaneLayoutDirection = PaneLayoutDirection.Vertical;
                }
                // Định dạng axisX của XYDiagram
                axisX.Title.Antialiasing = true;
                axisX.Title.Visible = true;
                axisX.Title.Font = titleFont;
                axisX.Title.Alignment = StringAlignment.Center;
                axisX.Label.Visible = true;
                axisX.Label.Antialiasing = true;
                axisX.Label.Font = labelFont;

                // Định dạng axisY của XYDiagram        
                axisY.Title.Antialiasing = true;
                axisY.Title.Visible = true;
                axisY.Title.Font = titleFont;
                axisY.Title.Alignment = StringAlignment.Center;
                axisY.Label.Visible = true;
                axisY.Label.Antialiasing = true;
                axisY.Label.Font = labelFont;
                axisY.Label.EndText = string.IsNullOrEmpty(wdg.YUnitName) ? "" : string.Format(" ({0})", wdg.YUnitName);

                // Định dạng series của chart            
                var series = chart.SeriesTemplate;
                chart.Series[0].SeriesPointsSortingKey = SeriesPointKey.Value_1;
                series.SeriesPointsSortingKey = SeriesPointKey.Value_1;
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                series.PointOptions.ValueNumericOptions.Precision = 2;
                series.Label.Visible = wdg.ShowSeriesLabel;

                //// Runtime format, add Dim & Measure
                //var xFields = wdg.Get_XFields();
                //var argMember = xFields[0].Prop1;
                //var serInfo = wdg.Get_YFields()
                //    .Select(p => new HTLBIChartSeriesInfo(p.Prop2, this.MySett.Get_Data(), argMember, p.Prop1)).ToArray();
                //chart.Series.Clear();
                //chart.Set_Series(serInfo);

                // Cái này không vẽ được nhiều series theo nhiều measure dc.
                //var xFields = wdg.Get_XFields();
                //var dimParent = xFields[0].Prop1;
                //var dimChild = xFields.Count > 1 ? xFields[1].Prop1 : dimParent;
                //var measure = wdg.Get_YFields().Select(p => p.Prop1).ToArray();
                //chart.SeriesDataMember = dimChild;
                //chart.SeriesTemplate.ArgumentDataMember = dimParent;
                //chart.SeriesTemplate.ValueDataMembers.Clear();
                //chart.SeriesTemplate.ValueDataMembers.AddRange(measure);

                chart.AppearanceName = wdg.AppearanceName;
                chart.PaletteName = wdg.PaletteName;
                chart.SetChartType(wdg.ChartType, true);
                chart.SetChartType(wdg.ChartType);
                chart.Set_TitleX(wdg.XTitle);
                chart.Set_TitleY(wdg.YTitle);
                //chart.Set_VisibleSeries(false);
            }
            catch { }
        }
        // Tùy chỉnh chart theo thiết lập người dùng: trường hợp không dùng SeriesTempalte (chưa hoàn chỉnh...)
        public virtual void InitCustom_Charts1()
        {
            try
            {
                var chart = this.FindControl("chart") as WebChartControl;
                if (chart == null) return;

                // Lưu các thiết lập của người dùng vào session để sử dụng cho việc tải dữ liệu
                var wdg = this.MySett;
                if (wdg == null) return;

                // Các khai báo dùng chung
                var titleFont = new Font("Arial", 7, FontStyle.Bold);
                var labelFont = new Font("Arial", 6, FontStyle.Bold);

                // Định dạng các thông số phổ biến
                //Helpers.FormatCommon_ChartControl(chart, 2);

                // Định dạng các thuộc tính riêng của chart theo các thiết lập của người dùng 
                chart.BackColor = Color.Transparent;
                chart.Legend.Visible = true;
                chart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                chart.Legend.Direction = LegendDirection.LeftToRight;
                chart.Legend.Padding.All = 0;
                chart.Legend.BackColor = Color.Transparent;
                chart.Legend.Border.Visible = false;
                chart.Width = new Unit(wdg.Width);
                chart.Height = new Unit(wdg.Height);
                chart.Padding.All = 0;

                // Định dạng XYDiagram của chart
                var xyDia = chart.Diagram as XYDiagram;
                xyDia.ScrollingOptions.UseKeyboard = true;
                xyDia.ScrollingOptions.UseMouse = true;
                xyDia.ScrollingOptions.UseScrollBars = true;
                xyDia.Margins.All = 0;
                var axisX = xyDia.AxisX;
                var axisY = xyDia.AxisY;
                if (wdg.RotatedXY)
                {
                    axisX.Reverse = true;
                    axisX.Label.Angle = -50;
                    axisY.Label.Angle = -30;
                    xyDia.Rotated = true;
                    xyDia.PaneLayoutDirection = PaneLayoutDirection.Horizontal;
                }
                else
                {
                    axisX.Label.Angle = -30;
                    axisY.Label.Angle = -50;
                    xyDia.Rotated = false;
                    xyDia.PaneLayoutDirection = PaneLayoutDirection.Vertical;
                }
                // Định dạng axisX của XYDiagram
                axisX.Title.Antialiasing = true;
                axisX.Title.Visible = true;
                axisX.Title.Font = titleFont;
                axisX.Title.Alignment = StringAlignment.Center;
                axisX.Label.Visible = true;
                axisX.Label.Antialiasing = true;
                axisX.Label.Font = labelFont;

                // Định dạng axisY của XYDiagram        
                axisY.Title.Antialiasing = true;
                axisY.Title.Visible = true;
                axisY.Title.Font = titleFont;
                axisY.Title.Alignment = StringAlignment.Center;
                axisY.Label.Visible = true;
                axisY.Label.Antialiasing = true;
                axisY.Label.Font = labelFont;
                axisY.Label.EndText = string.Format(" ({0})", wdg.YUnitName);

                // Định dạng series của chart
                // Runtime (after adding series)
                foreach (SeriesBase series in chart.Series)
                {
                    series.SeriesPointsSortingKey = SeriesPointKey.Value_1;
                    series.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                    series.PointOptions.ValueNumericOptions.Precision = 2;
                    series.Label.Visible = wdg.ShowSeriesLabel;
                }

                //// Runtime format
                //var dimParent = wdg.XFields[0];
                //var dimChild = wdg.XFields.Count > 1 ? wdg.XFields[1] : dimParent;
                //var measure = wdg.YFields.ToArray();
                //// Add Dim & Measure
                //chart.SeriesDataMember = dimChild;
                //chart.SeriesTemplate.ArgumentDataMember = dimParent;
                //chart.SeriesTemplate.ValueDataMembers.Clear();
                //chart.SeriesTemplate.ValueDataMembers.AddRange(measure);
                chart.AppearanceName = wdg.AppearanceName;
                chart.PaletteName = wdg.PaletteName;
                chart.SetChartType(wdg.ChartType, true);
                chart.SetChartType(wdg.ChartType);
                chart.Set_TitleX(wdg.XTitle);
                chart.Set_TitleY(wdg.YTitle);

                chart.Set_VisibleSeries(false);
            }
            catch { }
        }
        // Tải dữ liệu lên chart theo Datasource do người dùng thiết lập
        public virtual void Load_Data()
        {
            try
            {                
                if (this.MyChart == null) return;
                var chart = this.MyChart;
                chart.DataSource = this.MySett.Get_Data();
                chart.DataBind();
            }
            catch { }
        }
    }
    public class GaugeCtrlBase : WidgetCtrlBase
    {
        private ASPxGaugeControl _myGauge;
        public ASPxGaugeControl MyGauge
        {
            get
            {
                if (this._myGauge == null)
                    this._myGauge = this.FindControl("myGauge") as ASPxGaugeControl;
                return this._myGauge;
            }
        }
        public WidgetGauge MySett
        {
            get
            {
                if (this.Sett == null) this.Sett = new WidgetGauge();
                return this.Sett as WidgetGauge;
            }
        }
        public bool Lock_SetValue { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitCustomGauge();
            this.CustomGauge();
        }

        public override Size GetSize()
        {
            return new Size((int)this.MyGauge.Width.Value, (int)this.MyGauge.Height.Value);
        }

        public void InitCustomGauge()
        {
            this.InitCustom_CircleGauge();
            this.InitCustom_LinearGauge();
        }
        public void CustomGauge()
        {
            this.Custom_CircleGauge();
            this.Custom_LinearGauge();
            this.Custom_DigitalGauge();
        }

        public virtual void InitCustom_CircleGauge()
        {
            try
            {
                if (this.MyGauge == null) return;
                var myGauge = this.MyGauge;
                // Thiết lập chung...
                myGauge.ClientInstanceName = this.MyClientInstanceName;

                var gaugeTypeName = myGauge.Gauges[0].GetType().Name;
                if (!gaugeTypeName.StartsWith("CircularGauge")) return;

                // Switch by VisibleType...........
                var myBkg = myGauge.Get_CGauge_BackgroundLayer(0, 0);
                var myEfl = myGauge.Get_CGauge_EffectLayer(0, 0);
                var myScale = myGauge.Get_CGauge_Scale(0, 0);
                var myInctr = myGauge.Get_CGauge_StateIndicator(0, 0);
                if (myScale.Labels.Count == 0) myScale.Labels.Add(new ScaleLabelWeb());
                var myLbl = myScale.Labels[0];

                myGauge.Width = new Unit(this.Sett.Width);
                myGauge.Height = new Unit(this.Sett.Height);
                (myGauge.Get_CGauge(0) as CircularGauge).Bounds = new Rectangle(0, 0, this.Sett.Width, this.Sett.Height);
                switch (this.MySett.VisibleType)
                {
                    case GaugeType.CircleFull:
                        myBkg.ShapeType = BackgroundLayerShapeType.CircularFull_Style3;
                        myLbl.Position = new PointF2D(125, 224);                       
                        break;
                    case GaugeType.CircleThreeFour:
                        myBkg.ShapeType = BackgroundLayerShapeType.CircularThreeFourth_Style3;
                        myLbl.Position = new PointF2D(125, 224);
                        break;
                    case GaugeType.CircleHalf:
                        myBkg.ShapeType = BackgroundLayerShapeType.CircularHalf_Style3;
                        myLbl.Position = new PointF2D(125, 230);
                        break;
                    case GaugeType.CircleQuaterLeft:
                        myBkg.ShapeType = BackgroundLayerShapeType.CircularQuarter_Style3Left;
                        myBkg.ScaleCenterPos = new PointF2D(0.77F, 0.77F); // Left
                        myScale.StartAngle = -180; // Left
                        myScale.EndAngle = -90; // Left
                        myScale.Center = new PointF2D(195, 195); // Left
                        myInctr.Center = new PointF2D(65, 220);
                        myLbl.Position = new PointF2D(100, 220);
                        break;
                    case GaugeType.CircleQuaterRight:
                        myBkg.ShapeType = BackgroundLayerShapeType.CircularQuarter_Style3Right;
                        myBkg.ScaleCenterPos = new PointF2D(0.23F, 0.77F); // Right
                        myScale.StartAngle = -90; // Right
                        myScale.EndAngle = 0; // Right
                        myScale.Center = new PointF2D(55, 195); // Right
                        myInctr.Center = new PointF2D(185, 220);
                        myLbl.Position = new PointF2D(90, 220);
                        break;
                }
            }
            catch { }
        }
        public virtual void Custom_CircleGauge()
        {
            if (this.MyGauge == null) return;
            var myGauge = this.MyGauge;
            // Thiết lập chung...
            myGauge.ClientInstanceName = this.MyClientInstanceName;

            var gaugeTypeName = myGauge.Gauges[0].GetType().Name;
            if (!gaugeTypeName.StartsWith("CircularGauge")) return;
            
            this.Set_Value();

            //////////////// ADD STATE : KPIDefine            
            // Clear ScaleIndicatorStateWeb
            myGauge.Clear_CGauge_IndicatorStateWeb(0, 0);
            // Add ScaleIndicatorStateWeb  
            var index = 0;
            foreach (var kpi in this.MySett.StateRanges)
            {
                var stateName = string.Format("state_{0}", index++);
                var start = kpi.RangeVal.Start;
                var interval = kpi.RangeVal.End - kpi.RangeVal.Start;
                myGauge.Add_CGauge_IndicatorStateWeb(stateName, kpi.RangeVal.Start, interval, kpi.Color);
            }

            //////////////// ADD RANGE OF STATE : KPIDefine
            // Clear RangeWeb
            myGauge.Clear_CGauge_RangeWeb(0, 0);
            // Add RangeWeb
            index = 0;
            float startThickness = 1, endThickness = 3, shapeOffset = -30;
            foreach (var kpi in this.MySett.StateRanges)
            {
                var rangeName = string.Format("range_{0}", index++);
                var interval = kpi.RangeVal.End - kpi.RangeVal.Start;
                myGauge.Add_CGauge_RangeWeb(rangeName, kpi.RangeVal.Start, kpi.RangeVal.End, kpi.ForPerc, startThickness, endThickness, shapeOffset, kpi.Color);
                startThickness += 2; endThickness += 2;
            }

            ///////////////// COMMON
            // Needle
            var myNeedle = myGauge.Get_CGauge_Needle(0, 0);
            myNeedle.ShapeType = NeedleShapeType.CircularFull_Style7;
            // Scales : ArcScaleComponent
            var myScale = myGauge.Get_CGauge_Scale(0, 0);
            myScale.MaxValue = this.MySett.MaxValue;
            myScale.MinValue = this.MySett.MinValue;
            myScale.MajorTickCount = 10;
            myScale.MinorTickCount = 5;
            myScale.MajorTickmark.TextOrientation = LabelOrientation.LeftToRight;
            myScale.MajorTickmark.FormatString = "{0:" + this.MySett.FormatString + "}";
            myScale.AppearanceTickmarkText.Font = new Font("Arial", 8F, FontStyle.Bold);
            myScale.AppearanceTickmarkText.TextBrush = new SolidBrushObject(Color.WhiteSmoke);
            // Format Label color
            if (this.MySett.ShowCurValueText)
            {
                if (myScale.Labels.Count == 0) myScale.Labels.Add(new ScaleLabelWeb());
                var myLbl = myScale.Labels[0];
                //myLbl.Position = new PointF2D(125, 224); //                
                myLbl.Size = new SizeF(200, 20); //
                myLbl.FormatString = "{1:" + this.MySett.FormatString + "}";
                myLbl.AppearanceText.Font = new Font("Arial", 9F, FontStyle.Bold);
                myLbl.AppearanceText.TextBrush = this.MySett.ValueToBrush(myGauge.Value);
                myLbl.AppearanceBackground.BorderBrush = new SolidBrushObject(Color.Transparent);
                myLbl.AppearanceBackground.ContentBrush = new SolidBrushObject(Color.Transparent);
            }
            else
                myScale.Labels.Clear();            
        }
        public virtual void InitCustom_LinearGauge()
        {
            if (this.MyGauge == null) return;
            var myGauge = this.MyGauge;
            // Thiết lập chung...
            myGauge.ClientInstanceName = this.MyClientInstanceName;

            var gaugeTypeName = myGauge.Gauges[0].GetType().Name;
            if (!gaugeTypeName.StartsWith("LinearGauge")) return;

            // Switch by VisibleType.......
            var lGauge = myGauge.Get_LGauge(0);
            lGauge.Indicators[0].Center = new PointF2D(30, 215); //new PointF2D(25, 205)
            var myScale = myGauge.Get_LGauge_Scale(0, 0);
            if (myScale.Labels.Count == 0) myScale.Labels.Add(new ScaleLabelWeb());
            var myLbl = myScale.Labels[0];
            (myGauge.Get_LGauge(0) as DevExpress.Web.ASPxGauges.Gauges.Linear.LinearGauge).Bounds = new Rectangle(0, 0, this.Sett.Width, this.Sett.Height);
            myGauge.Width = new Unit(this.Sett.Width);
            myGauge.Height = new Unit(this.Sett.Height);

            switch (this.MySett.VisibleType)
            {
                case GaugeType.LinearHorizontal:
                    myLbl.Position = new PointF2D(30, 180);
                    lGauge.Orientation = ScaleOrientation.Horizontal;
                    foreach (var scale in lGauge.Scales)
                        scale.MajorTickmark.TextOrientation = LabelOrientation.BottomToTop;
                    break;
                case GaugeType.LinearVertical:
                    //myLbl.Position = new PointF2D(20, 190);
                    myLbl.Position = new PointF2D(30, 180);
                    lGauge.Orientation = ScaleOrientation.Vertical;
                    foreach (var scale in lGauge.Scales)
                        scale.MajorTickmark.TextOrientation = LabelOrientation.LeftToRight;
                    break;
            }
        }
        public virtual void Custom_LinearGauge()
        {
            if (this.MyGauge == null) return;
            var myGauge = this.MyGauge;
            // Thiết lập chung...
            myGauge.ClientInstanceName = this.MyClientInstanceName;

            var gaugeTypeName = myGauge.Gauges[0].GetType().Name;
            if (!gaugeTypeName.StartsWith("LinearGauge")) return;

            this.Set_Value();

            //////////////// ADD STATE : KPIDefine
            // Clear ScaleIndicatorStateWeb
            myGauge.Clear_LGauge_IndicatorStateWeb(0, 0);
            // Add ScaleIndicatorStateWeb
            var index = 0;
            foreach (var kpi in this.MySett.StateRanges)
            {
                var stateName = string.Format("state_{0}", index++);
                var start = kpi.RangeVal.Start;
                var interval = kpi.RangeVal.End - kpi.RangeVal.Start;
                myGauge.Add_LGauge_IndicatorStateWeb(stateName, kpi.RangeVal.Start, interval, kpi.Color);
            }

            //////////////// ADD RANGE OF STATE : KPIDefine
            // Clear RangeWeb
            myGauge.Clear_LGauge_RangeWeb(0, 0);
            // Add RangeWeb
            index = 0;
            float startThickness = 9, endThickness = 9, shapeOffset = -14;
            foreach (var kpi in this.MySett.StateRanges)
            {
                var rangeName = string.Format("range_{0}", index++);
                var interval = kpi.RangeVal.End - kpi.RangeVal.Start;
                myGauge.Add_LGauge_RangeWeb(rangeName, kpi.RangeVal.Start, kpi.RangeVal.End, kpi.ForPerc, startThickness, endThickness, shapeOffset, kpi.Color);
            }

            ///////////////// COMMON
            // BackgroundLayer {Ở nơi override sẽ thiết lập lại BackgroundLayer}
            var myBkg = myGauge.Get_LGauge_BackgroundLayer(0, 0);
            myBkg.ShapeType = BackgroundLayerShapeType.Linear_Style2;
            // Scales : ArcScaleComponent
            var myScale = myGauge.Get_LGauge_Scale(0, 0);
            myScale.MaxValue = this.MySett.MaxValue;
            myScale.MinValue = this.MySett.MinValue;
            myScale.MajorTickCount = 10;
            myScale.MinorTickCount = 5;
            myScale.MajorTickmark.FormatString = "{0:" + this.MySett.FormatString + "}";
            myScale.AppearanceTickmarkText.Font = new Font("Arial", 8F, FontStyle.Bold);
            myScale.AppearanceTickmarkText.TextBrush = new SolidBrushObject(Color.WhiteSmoke);
            // Format Label color
            if (this.MySett.ShowCurValueText)
            {
                if (myScale.Labels.Count == 0) myScale.Labels.Add(new ScaleLabelWeb());
                var myLbl = myScale.Labels[0];
                myLbl.Size = new SizeF(200, 20);
                myLbl.TextOrientation = myScale.MajorTickmark.TextOrientation;
                myLbl.FormatString = "{1:" + this.MySett.FormatString + "}";
                myLbl.AppearanceText.Font = new Font("Arial", 9F, FontStyle.Bold);
                myLbl.AppearanceText.TextBrush = this.MySett.ValueToBrush(myGauge.Value);
                myLbl.AppearanceBackground.BorderBrush = new SolidBrushObject(Color.Transparent);
                myLbl.AppearanceBackground.ContentBrush = new SolidBrushObject(Color.Transparent);
            }
            else
                myScale.Labels.Clear();
        }
        public virtual void Custom_DigitalGauge()
        {            
            if (this.MyGauge == null) return;
            var myGauge = this.MyGauge;

            var gaugeTypeName = myGauge.Gauges[0].GetType().Name;
            if (!gaugeTypeName.StartsWith("DigitalGauge")) return;
        }
        public virtual void Set_Value()
        {
            try
            {
                if (Lock_SetValue) return;
                var valStr = "0";
                if (this.IsDemo) // với mục đích hiển thị trong trang setting để xem trước dữ liệu.
                    valStr = ((int)this.MySett.MaxValue / 3).ToString();
                else
                {
                    // Truy vấn giá trị từ datasource cho gauge (nếu có)
                    valStr = this.MySett.Get_ActualValue();
                }
                var myGauge = this.MyGauge;
                if (myGauge == null) return;                
                myGauge.Value = valStr;
            }
            catch { }
        }
    }
    public class GridCtrlBase : WidgetCtrlBase
    {
        private ASPxGridView myGV;
        public ASPxGridView MyGV
        {
            get
            {
                if (this.myGV == null)
                    this.myGV = this.FindControl("gvData") as ASPxGridView;
                return this.myGV;
            }
        }
        public WidgetGrid MySett
        {
            get
            {
                if (this.Sett == null) this.Sett = new WidgetGrid();
                return this.Sett as WidgetGrid;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Custom_GridView();
            this.Load_Data();
        }
        public override Size GetSize()
        {
            return new Size((int)this.MyGV.Width.Value, 205);
        }
        public virtual void Custom_GridView()
        {
            if (this.MyGV == null) return;
            var gvData = this.MyGV;
            // Thiết lập chung...
            gvData.ClientInstanceName = this.MyClientInstanceName;

            try
            {
                var arrColumn = this.MySett.Columns.Where(p => !string.IsNullOrEmpty(p.FieldName))
                    .OrderBy(p => p.VisibleIndex);
                gvData.Width = new Unit(arrColumn.Sum(p => p.Width + 50), UnitType.Pixel);
                // Xóa các Column dữ liệu
                gvData.Columns.Clear();
                // Thêm cột số thứ tự
                var colLine = new GridViewDataTextColumn
                {
                    Name = "colLine",
                    Caption = "No",
                    FieldName = "#",
                    VisibleIndex = 0,
                    Width = new Unit(50, UnitType.Pixel),
                    UnboundType = DevExpress.Data.UnboundColumnType.Integer,
                };
                gvData.KeyFieldName = "#";
                gvData.SettingsBehavior.AllowFocusedRow = true;
                gvData.SettingsBehavior.EnableRowHotTrack = true;
                if (!this.IsDemo)
                {
                    gvData.SettingsBehavior.AllowSort = true;
                    gvData.SettingsBehavior.AllowDragDrop = false;
                }
                gvData.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Disabled;
                colLine.PropertiesTextEdit.DisplayFormatString = "#,##0";
                colLine.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                colLine.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                gvData.Columns.Add(colLine);

                var lstFields = this.MySett.Get_ColumnFields();
                foreach (var field in arrColumn)
                {
                    var col = new GridViewDataTextColumn();
                    col.Name = field.Name;
                    col.Caption = field.Caption;
                    var objField = lstFields.FirstOrDefault(p => p.Prop2 == field.FieldName);
                    col.FieldName = objField.Prop3;
                    col.VisibleIndex = field.VisibleIndex;
                    col.Width = new Unit(field.Width, UnitType.Pixel);
                    col.CellStyle.HorizontalAlign = field.Align;
                    col.PropertiesEdit.DisplayFormatString = field.DisplayF;
                    this.FormatGridColumn(col, objField.Prop1);
                    gvData.Columns.Add(col);
                }                
            }
            catch { }
        }
        public virtual void FormatGridColumn(GridViewDataTextColumn col, string tinyDataTypeStr)
        {
            col.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            col.HeaderStyle.Font.Bold = true;
            col.HeaderStyle.Font.Name = "Arial";
            col.HeaderStyle.Font.Size = new FontUnit(9, UnitType.Point);
            col.CellStyle.Font.Name = "Arial";
            col.CellStyle.Font.Size = new FontUnit(9, UnitType.Point);
            switch (tinyDataTypeStr.ToUpper())
            {
                case "NORMAL":
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                    break;
                case "TIME":
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    break;
                case "NUM":
                    col.PropertiesEdit.DisplayFormatString = "#,##0";
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    break;
            }
        }
        public virtual void Load_Data()
        {
            try
            {
                if (this.IsDemo) return;                
                if (this.MyGV == null) return;
                var gvData = this.MyGV;
                gvData.DataSource = this.MySett.Get_Data();
                gvData.DataBind();
            }
            catch { }
        }
    }
    #endregion

    #region Control part base for defination: Filter, KPI, Property Controls
    public abstract class PartPlugCtrlBase : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Load_InitData();
        }
        public virtual void Load_InitData() { }
        public virtual void Reset_Info(params object[] param) { }
        public event EventHandler OnRemove;
        public event EventHandler OnChange;
        protected void Remove_Click(object sender, EventArgs e)
        {
            if (this.OnRemove != null)
                this.OnRemove(new { Parent = this, Sender = sender }, e);
        }
        public void Raise_OnChange(object sender, EventArgs e)
        {
            if (this.OnChange != null)
                this.OnChange(sender, e);
        }
        public virtual bool RegisterStartupScript(Control updatePanel, string javaScriptCode)
        {
            try
            {
                if (string.IsNullOrEmpty(javaScriptCode)) return true;
                if (updatePanel == null) return true;
                ScriptManager.RegisterStartupScript(updatePanel, typeof(string), string.Format("{0}_Script", this.GetType().Name), javaScriptCode, true);
                return true;
            }
            catch { }
            return false;
        }
        public virtual bool RegisterStartupScript(string updatePanelUniqueName, string javaScriptCode)
        {
            try
            {
                var udp = this.Page.Controls.At("ctl00")
                                   .Controls.At("aspnetForm")
                                   .Controls.At("ctl00$MainContent")
                                   .Controls.At(updatePanelUniqueName);
                return this.RegisterStartupScript(udp, javaScriptCode);
            }
            catch { return false; }
        }

        protected bool Has_Param_QueryString()
        {
            return Request.QueryString != null && Request.QueryString.Count > 0;
        }
        protected bool Has_Param(string paramName)
        {
            return this.Has_Param_QueryString() && Request.QueryString.AllKeys.Contains(paramName);
        }
        protected string Get_Param(string paramName)
        {
            return this.Has_Param(paramName) ? Request.QueryString[paramName] : "";
        }
    }
    public abstract class FilterCtrlBase : PartPlugCtrlBase
    {
        public enum Members { KeyField, Operator, Value, AndOr }
        public virtual void Set_VisibleMember(Members part, bool isVisible)
        {
            try
            {
                Control ctrl = null;
                switch (part)
                {
                    case Members.KeyField:
                        ctrl = this.FindControl("cbbKeyField");
                        break;
                    case Members.Operator:
                        ctrl = this.FindControl("cbbOperator");
                        break;
                    case Members.Value:
                        ctrl = this.FindControl("txtValue");
                        break;
                    case Members.AndOr:
                        ctrl = this.FindControl("cbbAndOr");
                        break;
                }
                if (ctrl != null) ctrl.Visible = isVisible;
            }
            catch { }
        }
        public virtual InqFilterInfoMDX Get_FilterInfo()
        {
            return null;
        }
        public virtual object Get_FilterInfo_General()
        {
            return null;
        }
        public virtual void Set_Info(InqFilterInfoMDX info)
        {
        }
        public virtual void Set_Info_General(object info)
        {
        }
        public virtual void Set_Source(object src, string valField, string txtField)
        {
            try
            {
                var cbbKeyField = this.FindControl("cbbKeyField") as ASPxComboBox;
                Helpers.SetDataSource(cbbKeyField, src, valField, txtField);
            }
            catch { }
        }
        public virtual bool RegisterStartupScript(string javaScriptCode)
        {
            return this.RegisterStartupScript("ctl00$MainContent$upp_Filter", javaScriptCode);
        }
    }
    public abstract class KPIPartCtrlBase : PartPlugCtrlBase
    {
        public enum Parts{ Dimension, Measure, ContextMetric}
        public virtual void Set_Info(KPIField info)
        {
        }
        public virtual void Set_Source(object ds)
        {
        }
        public virtual KPIField Get_KPIPartInfo()
        {
            return default(KPIField);
        }
    }
    public abstract class CalcFieldCtrlBase : PartPlugCtrlBase
    {
        public enum Types { NumAndField, NumAndNum, FieldAndNum, FieldAndField }
        public virtual void Set_Type(string typeStr)
        {            
            try
            {
                var type = Lib.ToEnum<CalcFieldCtrlBase.Types>(typeStr);
                switch (type)
                {
                    case Types.FieldAndField:
                        this.FindControl("txtMember1").Visible = false;
                        this.FindControl("txtMember2").Visible = false;
                        break;
                    case Types.FieldAndNum:
                        this.FindControl("txtMember1").Visible = false;
                        this.FindControl("cboMember2").Visible = false;
                        break;
                    case Types.NumAndField:
                        this.FindControl("cboMember1").Visible = false;
                        this.FindControl("txtMember2").Visible = false;
                        break;
                    case Types.NumAndNum:
                        this.FindControl("cboMember1").Visible = false;
                        this.FindControl("cboMember2").Visible = false;
                        break;
                }
            }
            catch { }
        }
        public virtual void Set_Info(CalcField info) { }
        public virtual void Set_Source(object ds) { }
        public virtual CalcField Get_CalcFieldInfo()
        {
            return default(CalcField);
        }
    }
    public abstract class PropCtrlBase : PartPlugCtrlBase
    {
        public virtual void Set_ViewType(string viewType)
        {
        }
        public virtual void Set_Info(object info) { }
        public virtual void Set_Source(object ds)
        {
        }        
        public virtual WidgetBase GetInputSett()
        {
            return default(WidgetBase);
        }
        public virtual bool RegisterStartupScript(string javaScriptCode)
        {
            return this.RegisterStartupScript("ctl00$MainContent$upp_Setting", javaScriptCode);
        }
    }
    #endregion
}