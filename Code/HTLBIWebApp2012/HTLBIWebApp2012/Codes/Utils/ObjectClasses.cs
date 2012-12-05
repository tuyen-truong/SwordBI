using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using DevExpress.XtraCharts;
using CECOM;
using DevExpress.Utils;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;
using System.Drawing;
using DevExpress.XtraGauges.Core.Drawing;
using System.Web.UI.WebControls;
using System.Data;

namespace HTLBIWebApp2012
{
    #region Inherit And.NET classes
    public class HTLBIEventArgs : EventArgs
    {
        public string Name { get; set; }
        public object Values { get; set; }
        public HTLBIEventArgs() { }
        public HTLBIEventArgs(object values) : this(string.Empty, values) { }
        public HTLBIEventArgs(string name, object values)
        {
            this.Name = name;
            this.Values = values;
        }
    }
    public class HTLBIChartSeriesInfo
    {
        public string Name { get; set; }
        public object DataSource { get; set; }
        public string ArgDataMember { get; set; }
        public string ValDataMember { get; set; }
        public bool VisibleLabel { get; set; }
        public ViewType ViewTypeDefault { get; set; }
        public HTLBIChartSeriesInfo(string name, object dataSource, string argDataMember, string valDataMember, ViewType vType, bool visibleLabel)
        {
            this.ArgDataMember = argDataMember;
            this.DataSource = dataSource;
            this.Name = name;
            this.ValDataMember = valDataMember;
            this.VisibleLabel = visibleLabel;
            this.ViewTypeDefault = vType;
        }
        public HTLBIChartSeriesInfo(string name, object dataSource, string argDataMember, string valDataMember, ViewType vType) : this(name, dataSource, argDataMember, valDataMember, vType, false) { }
        public HTLBIChartSeriesInfo(string name, object dataSource, string argDataMember, string valDataMember) : this(name, dataSource, argDataMember, valDataMember, ViewType.Bar) { }
        public HTLBIChartSeriesInfo(object dataSource, string argDataMember, string valDataMember, ViewType vType) : this(valDataMember, dataSource, argDataMember, valDataMember, vType) { }
        public HTLBIChartSeriesInfo(object dataSource, string argDataMember, string valDataMember) : this(dataSource, argDataMember, valDataMember, ViewType.Bar) { }
    }
    public enum MeasureFunc
    {
        Sum,
        Max,
        Min,
        Avg
    }
    /// <summary>
    /// Lớp định nghĩa các thông tin của một cột trong lưới Olap.
    /// </summary>
    public class OlapColumnInfo
    {
        public string Caption { get; set; }
        public int ColWidth { get; set; }
        public FormatInfo Format { get; set; }
        public OlapColumnInfo(string caption, FormatInfo fInfo, int colWidth)
        {
            this.Caption = caption;
            this.Format = fInfo;
            this.ColWidth = colWidth;
        }
    }
    /// <summary>
    /// Lớp định nghĩa các thông tin của một Widget lấy được khi chọn trên series point của chart.
    /// </summary>
    public class ChartSeriesPointSelection
    {
        /// <summary>
        /// Đối tượng Series bên trong chart, chỉ những thông tin riêng của series
        /// </summary>
        public Series Ser { get; set; }
        /// <summary>
        /// Đối tượng Series bên trong chart, và các đối tượng liên quan tại điểm của series
        /// </summary>
        public SeriesPoint SerPoint { get; set; }
        public int DbdZone { get; set; }
        public ChartSeriesPointSelection() { }
        public ChartSeriesPointSelection(int zone, object serPoint, object ser)
        {
            this.DbdZone = zone;
            this.SerPoint = serPoint as SeriesPoint;
            this.Ser = ser as Series;
        }
    }
    #endregion

    #region Dashboard - Definations Models

    #region Widget setting.
    /// <summary>
    /// CircleFull, CircleHalf, CircleQuaterLeft, CircleQuaterRight, CircleThreeFour, Digital, LinearHorizontal, LinearVertical
    /// </summary>
    public enum GaugeType
    {
        CircleFull,
        CircleHalf,
        CircleQuaterLeft,
        CircleQuaterRight,
        CircleThreeFour,
        LinearHorizontal,
        LinearVertical
    }
    /// <summary>
    /// Định nghĩa một đối tượng khoảng giá trị với kiểu dữ liệu(từ tới) được quyết định lúc sử dụng.
    /// </summary>
    /// <typeparam name="TStart">Kiểu dữ liệu của giá trị bắt đầu</typeparam>
    /// <typeparam name="TEnd">Kiểu dữ liệu của giá trị kết thúc</typeparam>
    public class ValueRange<TStart, TEnd>
    {
        /// <summary>
        /// Giá trị bắt đầu của khoản.
        /// </summary>
        public TStart Start { get; set; }
        /// <summary>
        /// Giá trị kết thúc của khoản.
        /// </summary>
        public TEnd End { get; set; }
        public ValueRange() { }
        public ValueRange(TStart start, TEnd end)
        {
            this.Start = start;
            this.End = end;
        }
        public ValueRange<TStart, TEnd> Clone()
        {
            try
            {
                var newObj = new ValueRange<TStart, TEnd>(this.Start, this.End);
                return newObj;
            }
            catch { return null; }
        }
        public ValueRange<TStart, TEnd> Copy()
        {
            var ret = new ValueRange<TStart, TEnd>(this.Start, this.End);
            return ret;
        }
        public override bool Equals(object obj)
        {
            try
            {
                var info = obj as ValueRange<TStart, TEnd>;
                return info.Start.Equals(this.Start) && info.End.Equals(this.End);
            }
            catch { return false; }
        }
        public override string ToString()
        {
            try
            {
                return string.Format("{0},{1}", this.Start, this.End);
            }
            catch { return ""; }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    /// <summary>
    /// Định nghĩa một đối tượng chỉ ra trạng thái của một RangeVale bỡi màu sắc.
    /// </summary>
    /// <typeparam name="TStart">Kiểu dữ liệu của giá trị bắt đầu</typeparam>
    /// <typeparam name="TEnd">Kiểu dữ liệu của giá trị kết thúc</typeparam>
    public class StatusRange<TStart, TEnd>
    {
        public string Name { get; set; }
        public ValueRange<TStart, TEnd> RangeVal { get; set; }
        public bool ForPerc { get; set; }
        public Color Color { get; set; }
        public string Des { get; set; }

        public StatusRange() { }
        public StatusRange(string name) { this.Name = name; }
        public StatusRange(string name, TStart start, TEnd end, Color color, string des)
            : this(name, new ValueRange<TStart, TEnd>(start, end), false, color, des) { }
        public StatusRange(string name, TStart start, TEnd end, bool forPerc, Color color, string des)
            : this(name, new ValueRange<TStart, TEnd>(start, end), forPerc, color, des) { }
        public StatusRange(string name, ValueRange<TStart, TEnd> rangeVal, bool forPerc, Color color, string des)
        {
            this.Name = name;
            this.RangeVal = rangeVal;
            this.ForPerc = forPerc;
            this.Color = color;
            this.Des = des;
        }

        public StatusRange<TStart, TEnd> Copy()
        {
            var ret = new StatusRange<TStart, TEnd>(this.Name);
            ret.RangeVal = this.RangeVal.Copy();
            ret.Color = this.Color;
            ret.Des = this.Des;
            ret.ForPerc = this.ForPerc;
            return ret;
        }
    }
    /// <summary>
    /// Định nghĩa một đối tượng cột dữ liệu cho lưới Asp.Net
    /// </summary>
    public class WebGirdColumn
    {
        public string Name { get; set; }
        public string FieldName { get; set; }
        public string Caption { get; set; }
        public string DisplayF { get; set; }
        public System.Web.UI.WebControls.HorizontalAlign Align { get; set; }
        public int VisibleIndex { get; set; }
        public int Width { get; set; }
        public WebGirdColumn Copy()
        {
            var ret = new WebGirdColumn();
            ret.Align = this.Align;
            ret.Caption = this.Caption;
            ret.DisplayF = this.DisplayF;
            ret.FieldName = this.FieldName;
            ret.Name = this.Name;
            ret.VisibleIndex = this.VisibleIndex;
            ret.Width = this.Width;
            return ret;
        }
    }
    /// <summary>
    /// Định nghĩa một đối tượng chứa các thông tin về một field được dùng làm điều kiện lọc cho Widget lúc chạy (do người dùng chọn trên UI).
    /// </summary>
    public class WidgetFilter
    {
        /// <summary>
        /// Lưu tên field sẽ filter trên Widget.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Kiểu của filter (Scope/Range/Single).
        /// </summary>
        public string LimitType { get; set; }
        /// <summary>
        /// Tập phạm vi giá trị sẽ được sử dụng cho filter kiểu filter 'Scope'.
        /// </summary>
        public List<string> ScopeValue { get; set; }
        /// <summary>
        /// Giá trị bắt đầu cho filter kiểu filter 'Range'
        /// </summary>
        public string ValueFrom { get; set; }
        /// <summary>
        /// Giá trị kết thúc cho filter kiểu filter 'Range'
        /// </summary>
        public string ValueTo { get; set; }
        /// <summary>
        /// Giá trị cho filter kiểu filter 'Single'
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Kiểm tra đối tượng WidgetFilter hiện hành có ScopeValue hay không.
        /// </summary>
        public bool Has_ScopeValue()
        {
            return this.LimitType == "Scope" && this.ScopeValue != null && this.ScopeValue.Count > 0;
        }
        /// <summary>
        /// Đưa danh sách các ScopeValue (nếu tồn tại) về dạng chuỗi cách nhau bỡi dấu phẩy.
        /// </summary>
        public string To_ScopeValueStrMDX(string whCode)
        {
            var ret = "";
            if (!this.Has_ScopeValue()) return ret;
            try
            {
                var vTblObj = MyBI.Me.Get_DWColumn_ByColName(this.Name);
                var count = this.ScopeValue.Count;
                for (int i = 0; i < count; i++)
                {
                    var val = this.ScopeValue[i];
                    ret += string.Format("[{0}{1}].[{2}].&[{3}]", whCode, vTblObj.TblName_Virtual, this.Name, val);
                    if (i < count - 1) ret += ",\r\n";
                }
            }
            catch { }
            return string.IsNullOrEmpty(ret) ? ret : "{\r\n" + ret + "}\r\n";
        }
        /// <summary>
        /// Chuyển các thông tin filter từ người dùng cung cấp lúc chạy dashboard thành các thông tin mà nó có thể chạy ở môi trường MDX.
        /// <para>
        /// Chỉ áp dụng cho LimitType:(Range, Single).
        /// </para>
        /// </summary>
        /// <param name="wgFilter">Thông tin filter từ người dùng cung cấp lúc chạy dashboard.</param>
        public ICollection<InqFilterInfoMDX> To_InqFilter()
        {
            if (this.LimitType != "Range" && this.LimitType != "Single") return null;                
            var ret = new List<InqFilterInfoMDX>();
            var whereKey = MyBI.Me.Get_DWColumn_ByColName(this.Name).ToInqMDX();            
            switch (this.LimitType)
            {
                case "Range":
                    ret.Add(new InqFilterInfoMDX(whereKey, ">=", this.ValueFrom));
                    ret.Add(new InqFilterInfoMDX(whereKey, "<=", this.ValueTo));
                    break;
                case "Single":
                    ret.Add(new InqFilterInfoMDX(whereKey, "=", this.Value));
                    break;
            }
            return ret;
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin cơ bản của một đối tượng Widget.
    /// </summary>
    public abstract class WidgetBase
    {
        private int width, height;

        /// <summary>
        /// Tên của Widget
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ID của datasource trong bảng thiết lập datasource
        /// </summary>
        public string DatasourceID { get; set; }
        /// <summary>
        /// Lệnh truy vấn trực tiếp Sql
        /// </summary>
        public string SelectCommand { get; set; }
        /// <summary>
        /// Loại control biểu diễn cho Widget
        /// </summary>
        public string CtrlType { get; set; }
        /// <summary>
        /// Độ rộng của Widget
        /// </summary>
        public int Width 
        {
            get { if (this.width == 0) this.width = 170; return this.width; }
            set { this.width = value; }
        }
        /// <summary>
        /// Độ cao của Widget
        /// </summary>
        public int Height
        {
            get { if (this.height == 0) this.height = 170; return this.height; }
            set { this.height = value; }
        }
        /// <summary>
        /// Danh sách các field và giá trị cần được filter lại cho Datasource của Widget do người dùng cung cấp lúc chạy.
        /// </summary>
        public List<WidgetFilter> WidgetFiltersRuntime { get; set; }
        /// <summary>
        /// Kiểm tra xem widget có filter do người dùng cung cấp lúc chạy hay không.
        /// </summary>
        public bool Has_WidgetFiltersRuntime()
        {
            return this.WidgetFiltersRuntime != null && this.WidgetFiltersRuntime.Count > 0;
        }

        /// <summary>
        /// Chuyển về chuỗi json từ đối tượng hiện hành
        /// </summary>
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        /// <summary>
        /// Lấy câu SQL sau khi lọc lại với các field mà người dùng chọn ở trục X và Y
        /// </summary>
        public virtual string Get_SqlByAxisXY(bool isWrapText)
        {
            try
            {
                var ds = Get_BIDatasource();
                return ds.JsonObj.ToSql(isWrapText);
            }
            catch { return ""; }
        }
        /// <summary>
        /// Lấy câu MDX sau khi lọc lại với các field mà người dùng chọn ở trục X và Y
        /// </summary>
        public virtual string Get_MDXByAxisXY(bool isWrapText)
        {
            var inq = this.ReFilter_InqMDX();
            return inq == null ? "" : inq.ToMDX(isWrapText);
        }
        /// <summary>
        /// Trả về một đối tượng 'InqDefineSourceMDX' theo DatasourceID tương ứng với loại datasource mà Widget chọn(user thiết lập).
        /// <para>
        /// Nếu loại datasource là KPI thì sẽ được filter lại theo giới hạn trong KPI.
        /// </para>
        /// <para>Nếu tập WidgetFiltersRuntime tồn tại thì cũng sẽ được đưa vào filter của truy vấn.</para>
        /// </summary>
        public virtual InqDefineSourceMDX ReFilter_InqMDX()
        {
            InqDefineSourceMDX inq = null;
            try
            {
                var ds = Get_BIDatasource();
                if (ds.SettingCat == GlobalVar.SettingCat_DS)
                    inq = ds.JsonObjMDX;
                else if (ds.SettingCat == GlobalVar.SettingCat_KPI)
                    inq = ds.JsonObjKPI.ReFilter_InqMDX();

                // Nếu trên dashboard hay widget có cung cấp filter thì sẽ xử lý chỗ này.
                if (this.Has_WidgetFiltersRuntime())
                {
                    var explicitSetFilterStr = "";
                    var whCode = this.Get_BIDatasource().WHCode;
                    foreach (var obj in this.WidgetFiltersRuntime)
                    {
                        if (obj.Has_ScopeValue())
                            explicitSetFilterStr += obj.To_ScopeValueStrMDX(whCode) + ",\r\n";
                        else
                            inq.Filters.AddRange(obj.To_InqFilter());
                    }
                    if (explicitSetFilterStr.EndsWith(",\r\n"))
                        explicitSetFilterStr = explicitSetFilterStr.Remove(explicitSetFilterStr.Length - 3);
                    inq.ExplicitSetFilterStr = explicitSetFilterStr;
                }
            }
            catch { }
            return inq;
        }
        /// <summary>
        /// Lấy đối tượng Datasource từ hệ thống
        /// </summary>
        public virtual lsttbl_DashboardSource Get_BIDatasource()
        {
            try
            {
                return MyBI.Me.Get_DashboardSourceBy(this.DatasourceID);
            }
            catch { return new lsttbl_DashboardSource(); }
        }
        /// <summary>
        /// Lấy dữ liệu sau khi đã filter theo các điều kiện do người dùng thiết lập.
        /// </summary>
        public virtual DataTable Get_Data()
        {
            try
            {
                var mdxCmd = this.Get_MDXByAxisXY(false);
                return Lib.Db.ExecuteMDX(GlobalVar.DbOLAP_ConnectionStr_Tiny, mdxCmd);
            }
            catch { }
            return null;
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin trên một tế bào trong dashboard
    /// </summary>
    public abstract class Widget<T> : WidgetBase where T : class
    {
        /// <summary>
        /// Chuyển trở lại đối tượng từ chuỗi lưu trữ dạng json
        /// </summary>
        public static T FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<T>(jsonStr);
        }
        public virtual T Copy() { return default(T); }
    }
    public class WidgetChart : Widget<WidgetChart>
    {
        public bool RotatedXY { get; set; }
        private string _XTitle;
        public string XTitle 
        {
            get
            { 
                if(string.IsNullOrWhiteSpace(_XTitle))
                    if (this.XFields != null)
                    {
                        var tmp = "";
                        if (this.XFields.Count < 2)
                            tmp = this.XFields.First();
                        else
                        {
                            tmp = this.XFields.Last();
                            for (int i = this.XFields.Count-2; i >= 0; i--)
                                tmp = tmp + string.Format(" By {0}", this.XFields[i]);
                        }
                        _XTitle = tmp;
                    }
                return _XTitle;
            }
            set
            {
                _XTitle = value;
            }
        }
        private string _YTitle;
        public string YTitle
        {
            get
            {
                //if (string.IsNullOrWhiteSpace(_YTitle))
                //    if (this.YFields != null)
                //    {
                //        var tmp = "";
                //        if (this.YFields.Count < 2)
                //            tmp = this.YFields.First();
                //        else
                //        {
                //            tmp = this.YFields.Last();
                //            for (int i = this.YFields.Count - 2; i >= 0; i--)
                //                tmp = tmp + string.Format(" By {1}", this.YFields[i]);
                //        }
                //        _YTitle = tmp;
                //    }
                return _YTitle;
            }
            set
            {
                _YTitle = value;
            }
        }
        public string YUnitName { get; set; }
        public bool ShowSeriesLabel { get; set; }
        public List<string> XFields { get; set; }
        public List<string> YFields { get; set; }
        public string AppearanceName { get; set; }
        public string PaletteName { get; set; }
        public ViewType ChartType { get; set; }

        public WidgetChart() : this(null, null) { }
        public WidgetChart(List<string> xFields, List<string> yFields)
            : this(false, "My Chart", "Axis X", "Axis Y", xFields, yFields, Helpers.AppearanceNameDefault, Helpers.PaletteNameDefault, ViewType.Bar) { }
        public WidgetChart(bool rotatedXY, string displayName, string xTitle, string yTitle, List<string> xFields, List<string> yFields, string appearanceName, string paletteName, ViewType chartType) 
        {
            this.RotatedXY = rotatedXY;
            this.DisplayName = displayName;
            this.XTitle = xTitle;
            this.YTitle = yTitle;
            this.XFields = xFields;
            this.YFields = yFields;
            this.AppearanceName = appearanceName;
            this.PaletteName = paletteName;
            this.ChartType = chartType;
            this.YUnitName = "";
            this.Width = 485;
            this.Height = 400;
        }

        public void AddXField(params string[] fields)
        {
            if (this.XFields==null) this.XFields = new List<string>();
            this.XFields.AddRange(fields);
        }
        public void AddYField(params string[] fields)
        {
            if (this.YFields == null) this.YFields = new List<string>();
            this.YFields.AddRange(fields);
        }
        public string GetYUnitName(params string[] coverLeftRight)
        {
            if (string.IsNullOrEmpty(this.YUnitName)) return "";
            if (coverLeftRight == null || coverLeftRight.Length == 0)
                return this.YUnitName;

            var left = coverLeftRight.First();
            var right = coverLeftRight.Last();
            return string.Format(left + "{0}" + right, this.YUnitName);
        }
        /// <summary>
        /// Filter lại Datasource hoặc KPI theo giới hạn của 'XFields' và 'YFields'
        /// </summary>
        public override InqDefineSourceMDX ReFilter_InqMDX()
        {
            var inq = base.ReFilter_InqMDX();
            inq.ReFilter_Fields(p => this.XFields.Contains(p.ColName));
            inq.ReFilter_Summaries(p => this.YFields.Contains(p.Field.ColName));
            inq.ReFilter_CalcMembers(p => this.YFields.Contains(p.Code));
            inq.Reset_SummariesID();
            return inq;
        }
        public List<COM2ProperiesObj> Get_XFields()
        {
            var inq = base.ReFilter_InqMDX();
            inq.ReFilter_Fields(p => this.XFields.Contains(p.ColName));
            return inq.GetFields_KeyFieldAndDisplayName()
                .Select(p => new COM2ProperiesObj(p.Prop2, p.Prop3)).ToList();
        }
        public List<COM2ProperiesObj> Get_YFields()
        {
            var inq = base.ReFilter_InqMDX();
            inq.ReFilter_Summaries(p => this.YFields.Contains(p.Field.ColName));
            inq.ReFilter_CalcMembers(p => this.YFields.Contains(p.Code));
            var ret = inq.GetSummaries_KeyFieldAndDisplayName()
                .Union(inq.GetCalcMembers_KeyFieldAndDisplayName())
                .Select(p => new COM2ProperiesObj(p.Prop2, p.Prop3))
                .ToList();
            return ret;
        }
    }
    public class WidgetGauge : Widget<WidgetGauge>
    {
        private float maxValue;
        private List<StatusRange<float, float>> stateRanges;
        private string formatString;

        public GaugeType VisibleType { get; set; }
        public List<StatusRange<float, float>> StateRanges
        {
            get
            {
                if (this.stateRanges == null)
                    this.stateRanges = new List<StatusRange<float, float>>();
                return this.stateRanges;
            }
            set
            {
                this.stateRanges = value;
            }
        }
        public string Dimension { get; set; }
        public string Measure { get; set; }
        public float Value { get; set; }
        public float MinValue { get; set; }
        public float MaxValue
        {
            get
            {
                if (this.maxValue != 0) return this.maxValue;
                if (this.StateRanges == null || this.StateRanges.Count <= 0) return 0;
                return this.StateRanges.Max(p => p.RangeVal.End);
            }
            set { this.maxValue = value; }
        }        
        public string FormatString
        {
            get
            {
                if (string.IsNullOrEmpty(this.formatString)) this.formatString = "N0";
                return this.formatString;
            }
            set
            {
                this.formatString = value;
            }
        }
        public bool ShowCurValueText { get; set; }

        public override WidgetGauge Copy()
        {
            var ret = new WidgetGauge();
            ret.StateRanges = this.StateRanges.Select(p => p.Copy()).ToList();
            ret.CtrlType = this.CtrlType;
            ret.DatasourceID = this.DatasourceID;
            ret.DisplayName = this.DisplayName;
            ret.FormatString = this.FormatString;
            ret.Height = ret.Height;
            ret.MaxValue = this.MaxValue;
            ret.Measure = this.Measure;
            ret.MinValue = this.MinValue;
            ret.SelectCommand = this.SelectCommand;
            ret.ShowCurValueText = this.ShowCurValueText;
            ret.Value = this.Value;
            ret.VisibleType = this.VisibleType;
            ret.Width = this.Width;
            return ret;
        }
        public WidgetGauge()
        { 
            this.StateRanges = new List<StatusRange<float, float>>();
            this.VisibleType = GaugeType.CircleFull;
        }
        public WidgetGauge(ICollection<StatusRange<float, float>> stateRanges)
        {
            this.StateRanges = new List<StatusRange<float, float>>(stateRanges);
            this.VisibleType = GaugeType.CircleFull;
        }
        public BrushObject ValueToBrush(string value)
        {
            return new SolidBrushObject(this.ValueToColor(value));
        }
        public BrushObject ValueToBrush(float value)
        {
            return new SolidBrushObject(this.ValueToColor(value));
        }
        public Color ValueToColor(string value)
        {
            return this.ValueToColor(float.Parse(value));
        }
        public Color ValueToColor(float value)
        {
            var obj = this.StateRanges
                .FirstOrDefault(p => value > p.RangeVal.Start && value <= p.RangeVal.End);
            if (obj == null) return Color.Gray;
            return obj.Color;
        }
        /// <summary>
        /// Filter lại 'Datasource hoặc KPI' theo giới hạn của 'Dimension' và 'Measure'
        /// </summary>
        public override InqDefineSourceMDX ReFilter_InqMDX()
        {
            var inq = base.ReFilter_InqMDX();            
            inq.ReFilter_Fields(p => p.ColName == this.Dimension);
            inq.ReFilter_Summaries(p => p.Field.ColName == this.Measure);
            inq.ReFilter_CalcMembers(p => p.Code == this.Measure);
            inq.Reset_SummariesID();
            return inq;
        }
        /// <summary>
        /// Prop1:KeyField, Prop2:DisplayName
        /// </summary>
        public COM2ProperiesObj Get_Dimension()
        {
            var inq = base.ReFilter_InqMDX();
            inq.ReFilter_Fields(p => p.ColName == this.Dimension);
            var obj = inq.GetFields_KeyFieldAndDisplayName().FirstOrDefault();
            return new COM2ProperiesObj(obj.Prop2, obj.Prop3);
        }
        /// <summary>
        /// Prop1:KeyField, Prop2:DisplayName, Prop3:Func
        /// </summary>
        public COM3ProperiesObj Get_Measure()
        {
            var inq = this.ReFilter_InqMDX();            
            var ret = inq.GetSummaries_KeyFieldAndDisplayName()
                .Union(inq.GetCalcMembers_KeyFieldAndDisplayName())
                .Select(p => new COM3ProperiesObj(p.Prop2, p.Prop3, p.Prop4))
                .FirstOrDefault();
            return ret;
        }
        /// <summary>
        /// Lấy giá trị trả về thật sự từ Database
        /// </summary>
        public string Get_ActualValue()
        {
            var ds = this.Get_Data();
            if (ds != null)
            {
                var measure = this.Get_Measure();
                var valObj = ds.Compute(string.Format("{0}({1})", measure.Prop3, measure.Prop1), null);
                return Lib.IfNOE(valObj, "0");
            }
            return "0";
        }
    }
    public class WidgetGrid : Widget<WidgetGrid>
    {
        private List<WebGirdColumn> columns;
        public List<WebGirdColumn> Columns {
            get
            {
                if (this.columns == null)
                    this.columns = new List<WebGirdColumn>();
                return this.columns;
            }
            set
            {
                this.columns = value;
            }
        }
        public override WidgetGrid Copy()
        {
            var ret = new WidgetGrid();
            ret.Columns = this.Columns.Select(p => p.Copy()).ToList();
            ret.CtrlType = this.CtrlType;
            ret.DatasourceID = this.DatasourceID;
            ret.DisplayName = this.DisplayName;
            ret.Height = this.Height;
            ret.SelectCommand = this.SelectCommand;
            ret.Width = this.Width;
            return ret;
        }
        /// <summary>
        /// Filter lại 'Datasource hoặc KPI' theo giới hạn của 'Columns'.
        /// </summary>
        public override InqDefineSourceMDX ReFilter_InqMDX()
        {
            var inq = base.ReFilter_InqMDX();
            var arrField = this.Columns.Select(p => p.FieldName).ToArray();
            inq.ReFilter_Fields(p => arrField.Contains(p.ColName));
            inq.ReFilter_Summaries(p => arrField.Contains(p.Field.ColName));
            inq.ReFilter_CalcMembers(p => arrField.Contains(p.Code));
            return inq;
        }
        /// <summary>
        /// Trả về danh sách các đối tượng 'COM3ProperiesObj' tương ứng với danh sách 'Columns'.
        /// <para>
        /// Prop1:(TIME/NORMAL/NUM), Prop2:FieldName, Prop3:KeyField, Prop4:DisplayName
        /// </para>
        /// </summary>
        public List<COM4ProperiesObj> Get_ColumnFields()
        {
            var inq = this.ReFilter_InqMDX();
            var fields_Time = inq.GetFields_KeyFieldAndDisplayName()
                .Where(p => new[] { "DateKey", "Period", "Quarter", "Year" }.Contains(p.Prop1))
                .Select(p => new COM4ProperiesObj("TIME", p.Prop1, p.Prop2, p.Prop3));
            var fields_Normal = inq.GetFields_KeyFieldAndDisplayName()
                .Where(p => !new[] { "DateKey", "Period", "Quarter", "Year" }.Contains(p.Prop1))
                .Select(p => new COM4ProperiesObj("NORMAL", p.Prop1, p.Prop2, p.Prop3));
            var summaries = inq.GetSummaries_KeyFieldAndDisplayName()
                .Union(inq.GetCalcMembers_KeyFieldAndDisplayName())
                .Select(p => new COM4ProperiesObj("NUM", p.Prop1, p.Prop2, p.Prop3));
            var ret = fields_Time.Union(fields_Normal).Union(summaries).ToList();
            return ret;
        }
    }
    #endregion

    #region Interaction.
    public class InteractionFilter
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        /// <summary>
        /// Loại control sẽ được sử dụng để hiển thị hộp filter trên portlet.
        /// <para>
        /// VD: CheckedComboBox, ComboBox, TreeListBox, Calendar.
        /// </para>
        /// </summary>
        public string Control { get; set; }
        /// <summary>
        /// Tên field sẽ được sử dụng làm nơi lấy dữ liệu đưa vào hộp filter.
        /// <para>
        /// VD: ItemCode, ItemName, LocationCode, Period ...
        /// </para>
        /// </summary>
        public string SourceField { get; set; }
        /// <summary>
        /// Chỉ ra có hiện filter dạng [từ...tới] hay không.
        /// </summary>
        public bool EnableRange { get; set; }
        public InteractionFilter Copy()
        {
            return new InteractionFilter
            {
                Control = this.Control,
                Name = this.Name,
                SourceField = this.SourceField,
                EnableRange = EnableRange
            };
        }
        public static List<COMCodeNameObj> Get_Control()
        {
            return new List<COMCodeNameObj>()
            {
                new COMCodeNameObj( "ComboBox", "Combo Box" ),
                new COMCodeNameObj( "CheckedComboBox", "Checked Combo Box" ),                
                new COMCodeNameObj( "TreeListBox", "Tree List Box" ),                
                new COMCodeNameObj( "Calendar_Year", "Calendar - Year" ),
                new COMCodeNameObj( "Calendar_Quarter", "Calendar - Quarter" ),
                new COMCodeNameObj( "Calendar_Period", "Calendar - Period" ),
                new COMCodeNameObj( "Calendar_Day", "Calendar - Day" ),
                new COMCodeNameObj( "Calendar_Prev", "Calendar - Previous Time" )
            };
        }
    }
    public class InteractionDefine
    {
        /// <summary>
        /// Danh sách đối tượng dùng để định nghĩa cho việc filter trên từng portlet.
        /// </summary>
        public List<InteractionFilter> Filters { get; set; }
        /// <summary>
        /// Danh sách các field theo thứ tự DrillDown khi tương tác với người dùng.
        /// <para>Thông tin này sẽ được sử dụng khi DrilldownCategory là (In hoặc Popup).</para>
        /// </summary>
        public List<string> HierarchyFields { get; set; }
        /// <summary>
        /// Loại DrillDown: In, Popup, Other.
        /// </summary>
        public string DrilldownCategory { get; set; }
        /// <summary>
        /// Mã của Widget mà nó sẽ nhận được tham số trong quan hệ DrillDown.
        /// </summary>
        public string DrilldownPortlet { get; set; }

        public bool HasFilters()
        {
            return this.Filters != null && this.Filters.Count > 0;
        }
        public bool HasHierarchyFields()
        {
            return this.HierarchyFields != null && this.HierarchyFields.Count > 0;
        }
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        public static InteractionDefine FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<InteractionDefine>(jsonStr);
        }
    }
    #endregion

    #region Templates
    public class DashboardDefine
    {
        /// <summary>
        /// Tên hiển thị của dashboard.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Template sẽ được dụng để trình bài các portlet cho dashboard.
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// Danh sách các mã portlet sẽ được hiện lên template của dashboard
        /// </summary>
        public List<string> UsingPortlets { get; set; }
        /// <summary>
        /// Danh sách đối tượng dùng để định nghĩa cho việc filter trên toàn dashboard.
        /// </summary>
        public List<InteractionFilter> Filters { get; set; }

        public List<COMCodeNameObj> Get_UsingPortlets()
        {
            try
            {
                var ret= MyBI.Me.Get_Widget().Where(p => this.UsingPortlets.Contains(p.Code))
                    .Select(p => new COMCodeNameObj(p.Code, p.Name)).ToList();
                return ret;
            }
            catch { }
            return new List<COMCodeNameObj>();
        }
        public bool HasUsingPortlets()
        {
            return this.UsingPortlets != null && this.UsingPortlets.Count > 0;
        }
        public bool HasFilters()
        {
            return this.Filters != null && this.Filters.Count > 0;
        }
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        public static DashboardDefine FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<DashboardDefine>(jsonStr);
        }
        public static List<COMCodeNameObj> Get_Template()
        {
            return new List<COMCodeNameObj>()
            {
                new COMCodeNameObj( "TwoPortlet_Flow", "Two Portlet Flow" ),
                new COMCodeNameObj( "TwoPortlet_Grid", "Two Portlet Grid" ),
                new COMCodeNameObj( "ThreePortlet_Flow", "Three Portlet Flow" ),
                new COMCodeNameObj( "ThreePortlet_Grid", "Three Portlet Grid" ),
                new COMCodeNameObj( "FourPortlet_Flow", "Four Portlet Flow" ),
                new COMCodeNameObj( "FourPortlet_Grid", "Four Portlet Grid" )
            };
        }
    }
    #endregion

    #endregion

    #region Datasource - Definations Models (Apply For: Optional Database Relationship)
    /// <summary>
    /// Định nghĩa các hàm cơ bản của đối tượng Inquiry
    /// </summary>
    public abstract class Inq
    {
        /// <summary>
        /// Các kiểu dữ liệu của Inq (kết quả ánh xạ từ kiểu dữ liệu của SQL)
        /// </summary>
        public enum InqDataType
        { 
            NUM, DATE, TEXT, NTEXT, UNKNOWN
        }

        public abstract string ToSql();
        public abstract bool IsValid();
        public virtual void DoValid() { }
        /// <summary>
        /// Danh sách các tên hàm thống kê
        /// </summary>
        public virtual string[] GetSummatyFuncName()
        {
            return new string[] { "SUM", "COUNT", "AVG" };
        }
        /// <summary>
        /// Danh sách các toán tử của filter
        /// </summary>
        public virtual string[] GetFilterOperator()
        {
            return new string[] { "=", ">", ">=", "<", "<=", "<>", "NULL" };
        }
        /// <summary>
        /// Danh sách các tên thứ tự dữ liệu
        /// </summary>
        public virtual string[] GetOrderByName()
        {
            return new string[] { "ASC", "DESC" };
        }
        /// <summary>
        /// Danh sách các tên kiểu dữ liệu của Inq (kết quả ánh xạ từ kiểu dữ liệu của SQL)
        /// </summary>
        public virtual string[] GetInqDataTypeName()
        {
            return new string[] { "DATE", "NTEXT", "TEXT", "NUM" };
        }
    }
    public class InqTableInfo : Inq
    {
        public bool Visible { get; set; }
        public string TblCat { get; set; }
        public string BizCat { get; set; }
        public string TblName { get; set; }
        public string TblAliasVI { get; set; }
        public string TblAliasEN { get; set; }
        public string RefInfo { get; set; }

        public InqTableInfo() { }
        public InqTableInfo(string tblName)
        {
            this.TblName = tblName;
        }
        public InqTableInfo(string tblName, string tblCat, string bizCat)
        {
            this.TblName = tblName;
            this.TblCat = tblCat;
            this.BizCat = bizCat;
        }

        public override string ToSql()
        {
            if (!this.IsValid()) return "";
            return string.Format("[{0}]", this.TblName);
        }
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.TblName);
        }
        public override void DoValid()
        {
            this.TblName = "[MASTER].[sys].[tables]";
        }
    }
    /// <summary>
    /// Thông tin của 1 field dữ liệu
    /// </summary>
    public class InqFieldInfo : Inq
    {
        public string KeyField { get { return string.Format("{0}_{1}", this.TblName, this.ColName); } }
        public bool Visible { get; set; }
        public string TblName { get; set; }
        public string ColName { get; set; }
        public string ColAliasVI { get; set; }
        public string ColAliasEN { get; set; }
        public string DataType { get; set; }

        public string OrderName { get; set; }

        public InqFieldInfo() { }
        public InqFieldInfo(string tblName, string colName)
        {
            this.TblName = tblName;
            this.ColName = colName;
        }
        public InqFieldInfo(string tblName, string colName, string dataType)
        {
            this.TblName = tblName;
            this.ColName = colName;
            this.DataType = dataType;
        }
        public InqFieldInfo(string tblName, string colName, string colAlias, string dataType)
        {
            this.TblName = tblName;
            this.ColName = colName;
            this.DataType = dataType;
            this.ColAliasVI = colAlias;
        }

        /// <summary>
        /// Đưa về chuỗi có dạng: "[tblAlias].[Tên cột]" (VD: [tblTmp].[ItemCode])
        /// </summary>
        public string ToSql(string tblAlias)
        {
            if (!this.IsValid()) return "";
            if (string.IsNullOrWhiteSpace(tblAlias))
                return string.Format("[{0}].[{1}]", this.TblName, this.ColName);
            return string.Format("[{0}].[{1}]", tblAlias, this.ColName);
        }
        /// <summary>
        /// Đưa về chuỗi có dạng: "[Tên table].[Tên cột]" (VD: [DimItem].[ItemCode])
        /// </summary>
        public override string ToSql()
        {
            return this.ToSql(null);
        }

        public string ToSql_OrderBy(string tblAlias)
        {
            if (!this.IsValid()) return "";
            if (string.IsNullOrWhiteSpace(this.OrderName)) return this.ToSql(tblAlias);
            if (string.IsNullOrWhiteSpace(tblAlias))
                return string.Format("[{0}].[{1}] {2}", this.TblName, this.ColName, this.OrderName);
            return string.Format("[{0}].[{1}] {2}", tblAlias, this.ColName, this.OrderName);
        }
        /// <summary>
        /// Đưa về chuỗi có dạng: "[Tên table].[Tên cột] OrderName" (VD: [DimItem].[ItemCode] ASC)
        /// </summary>
        public string ToSql_OrderBy()
        {
            return this.ToSql_OrderBy(null);
        }

        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// <para>strong=true</para>
        /// </summary>
        public override bool IsValid()
        {
            return this.IsValid(true);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// <para><param name="strong">Kiểm tra thêm các thông tin cần và đủ.</param></para>
        /// </summary>
        public virtual bool IsValid(bool strong)
        {
            if (strong)
                return
                    !string.IsNullOrWhiteSpace(this.TblName) &&
                    !string.IsNullOrWhiteSpace(this.ColName) &&
                    !string.IsNullOrWhiteSpace(this.DataType) &&
                     this.GetInqDataTypeName().Contains(this.DataType) &&
                    (string.IsNullOrWhiteSpace(this.OrderName) ||
                     this.GetOrderByName().Contains(this.OrderName));
            return
                !string.IsNullOrWhiteSpace(this.TblName) &&
                !string.IsNullOrWhiteSpace(this.ColName) &&
                (string.IsNullOrWhiteSpace(this.DataType) ||
                 this.GetInqDataTypeName().Contains(this.DataType)) &&
                (string.IsNullOrWhiteSpace(this.OrderName) ||
                 this.GetOrderByName().Contains(this.OrderName));
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// <para>type = InqDataType.TEXT</para>
        /// </summary>
        public override void DoValid()
        {
            this.DoValid(InqDataType.TEXT);
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public void DoValid(InqDataType type)
        {
            switch (type)
            { 
                case InqDataType.DATE:
                    this.ColName = "GETDATE() AS DATE";
                    break;
                case InqDataType.TEXT:
                case InqDataType.NTEXT:
                    this.ColName = "name AS TEXT";
                    break;
                case InqDataType.NUM:
                    this.ColName = "0 AS NUM";
                    break;
            }
            this.TblName = "[MASTER].[sys].[tables]";
            this.DataType = type.ToString();
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin của một field ở dạng thống kê (Tổng, Trung bình....)
    /// </summary>
    public class InqSummaryInfo : Inq
    {
        /// <summary>
        /// Tên 1 trong các hàm thống kê (SUM, COUNT, AVG ...)
        /// </summary>
        public string FuncName { get; set; }
        /// <summary>
        /// Thông tin trường dữ liệu
        /// </summary>
        public InqFieldInfo Field { get; set; }
        public string FieldName { get { return this.Field.ColName; } }
        public string KeyField { get { return this.Field.KeyField; } }
        private string _FieldAlias;
        /// <summary>
        /// Tên giả của trường dữ liệu
        /// </summary>
        public string FieldAlias 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_FieldAlias))
                    if (this.Field != null)
                        _FieldAlias = this.Field.ColName;
                return _FieldAlias;
            }
            set
            {
                _FieldAlias = value;
            }
        }

        public InqSummaryInfo() { }
        public InqSummaryInfo(InqFieldInfo field, string funcName)
        {
            this.FuncName = funcName;
            this.Field = field;
        }
        public InqSummaryInfo(InqFieldInfo field, string funcName, string alias)
        {
            this.FuncName = funcName;
            this.Field = field;
            this.FieldAlias = alias;
        }

        /// <summary>
        /// Đưa về chuỗi có dạng: "FuncName(ISNULL(FieldName,0)) AS FieldName" (VD: SUM(ISNULL(Quantity,0)) AS Quantity)
        /// </summary>
        public virtual string ToSql(bool withAlias)
        {
            if (!this.IsValid()) return "";
            if (withAlias)
                return string.Format("{0}(ISNULL([{1}].[{2}],0)) AS {3}", FuncName, Field.TblName, Field.ColName, Field.ColName);
            return string.Format("{0}(ISNULL([{1}].[{2}],0))", FuncName, Field.TblName, Field.ColName);
        }
        /// <summary>
        /// Đưa về chuỗi có dạng: "FuncName(ISNULL(FieldName,0)) AS FieldName" (VD: SUM(ISNULL(Quantity,0)) AS Quantity)
        /// <param name="tblAlias">
        /// Ten alias của bảng dữ liệu do truy vấn bên trong trả về
        /// </param>
        /// </summary>
        public virtual string ToSql(bool withAlias, string tblAlias)
        {
            if (!this.IsValid()) return "";
            if (string.IsNullOrEmpty(tblAlias)) return ToSql(withAlias);

            if (withAlias)
                return string.Format("{0}(ISNULL([{1}].[{2}],0)) AS {3}", FuncName, tblAlias, Field.ColName, Field.ColName);
            return string.Format("{0}(ISNULL([{1}].[{2}],0))", FuncName, tblAlias, Field.ColName);
        }
        /// <summary>
        /// Đưa về chuỗi có dạng: "FuncName(ISNULL(FieldName,0)) AS FieldName" (VD: SUM(ISNULL(Quantity,0)) AS Quantity)
        /// <para>withAlias = true</para>
        /// </summary>
        public override string ToSql()
        {
            return this.ToSql(true);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.FuncName) &&
                    this.GetSummatyFuncName().Contains(this.FuncName) &&
                    this.Field != null && this.Field.IsValid(false);
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public override void DoValid()
        {
            this.FuncName = this.GetSummatyFuncName().First();
            if (this.Field == null) this.Field = new InqFieldInfo();
            this.Field = new InqFieldInfo();
            this.Field.DoValid(InqDataType.NUM);
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin của một đối tượng filter trong mệnh dề WHERE hoặc HAVING.
    /// </summary>
    public class InqFilterInfo : Inq
    {
        /// <summary>
        /// Khóa để filter ở mệnh đề 'WHERE' của câu lệnh SQL
        /// </summary>
        public InqFieldInfo WhereKey { get; set; }
        public string WhereField
        {
            get
            {
                try { return WhereKey.ColName; }
                catch { return ""; }
            }
        }
        public string WhereKeyField
        {
            get
            {
                try { return WhereKey.KeyField; }
                catch { return ""; }
            }
        }
        /// <summary>
        /// Khóa để filter ở mệnh đề 'HAVING' của câu lệnh SQL
        /// </summary>
        public InqSummaryInfo HavingKey { get; set; }
        public string HavingField { get { return HavingKey.FieldName; } }
        public string HavingKeyField { get { return HavingKey.KeyField; } }
        /// <summary>
        /// Toán tử so sánh
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// Trị giá so sánh
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Toán tử liên kết logic
        /// </summary>
        public string Logic { get; set; }

        public InqFilterInfo() { }
        public InqFilterInfo(InqFieldInfo whereKey, string opt, object value) : this(whereKey, opt, value, "AND") { }
        public InqFilterInfo(InqFieldInfo whereKey, string opt, object value, string logic)
        {
            this.WhereKey = whereKey;
            this.Operator = opt;
            this.Value = value;
            this.Logic = logic;
        }
        public InqFilterInfo(InqSummaryInfo havingKey, string opt, object value) : this(havingKey, opt, value, "AND") { }
        public InqFilterInfo(InqSummaryInfo havingKey, string opt, object value, string logic)
        {
            this.HavingKey = havingKey;
            this.Operator = opt;
            this.Value = value;
            this.Logic = logic;
        }

        public string GetFilterType()
        {
            if (this.WhereKey != null)
            {
                switch (this.WhereKey.DataType)
                {
                    case "TEXT":
                    case "NTEXT": return "NORMAL";
                }
                return this.WhereKey.DataType;
            }
            else if (this.HavingKey != null)
            {
                switch (this.HavingKey.Field.DataType)
                {
                    case "TEXT":
                    case "NTEXT": return "NORMAL";
                }
                return this.HavingKey.Field.DataType;
            }
            return "";
        }
        /// <summary>
        /// Đưa về chuỗi có dạng: "[TableName].[ColName] Operator Value"/ "SummaryField Operator Value" (VD: [DimItem].[ItemCode] = 'abc', [DimItem].[ItemCode] = N'abc' / Quantity_Sum > 100)
        /// </summary>
        public override string ToSql()
        {
            try
            {
                if (!this.IsValid()) return "";

                var opt = Lib.NTE(this.Operator);
                if (this.WhereKey != null)
                {
                    var tblName = Lib.NTE(this.WhereKey.TblName);
                    var colName = Lib.NTE(this.WhereKey.ColName);
                    if (this.Operator == "NULL")
                        return string.Format("[{0}].[{1}] IS NULL", tblName, colName);
                    switch (this.WhereKey.DataType)
                    {
                        case "TEXT":
                            // ProductCode = 'abc'
                            return string.Format("[{0}].[{1}] {2} '{3}'", tblName, colName, opt, Lib.NTE(this.Value));
                        case "NTEXT":
                            // ProductCode = N'abc'
                            return string.Format("[{0}].[{1}] {2} N'{3}'", tblName, colName, opt, Lib.NTE(this.Value));
                        case "NUM":
                            // ProductPrice = 150
                            return string.Format("[{0}].[{1}] {2} {3}", tblName, colName, opt, this.Value);
                        case "DATE":
                            // dbo.fDateOnly(ProductDate) = dbo.fDateOnly('2012/04/23')
                            return string.Format("dbo.fDateOnly([{0}].[{1}]) {2} dbo.fDateOnly('{3}')", tblName, colName, opt, Lib.NTE(this.Value));
                    }
                }
                else if (this.HavingKey != null)
                {
                    // SUM/COUNT/AVG(ProductPrice) = 150
                    if (this.Operator == "NULL")
                        return string.Format("{0} IS NULL", this.HavingKey.ToSql(false));
                    return string.Format("{0} {1} {2}", this.HavingKey.ToSql(false), opt, this.Value);
                }
            }
            catch { }
            return "";
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return (
                    (this.WhereKey != null && this.WhereKey.IsValid()) ||
                    (this.HavingKey != null && this.HavingKey.IsValid())
                   ) &&
                   !string.IsNullOrWhiteSpace(this.Operator) &&
                   this.GetFilterOperator().Contains(this.Operator);
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public override void DoValid()
        {
            if (this.WhereKey == null) this.WhereKey = new InqFieldInfo();
            this.WhereKey.DoValid();
            if (this.HavingKey == null) this.HavingKey = new InqSummaryInfo();
            this.HavingKey.DoValid();
            this.Operator = this.GetFilterOperator().First();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class InqOrderByInfo : Inq
    {
        /// <summary>
        /// Thông tin trường dữ liệu
        /// </summary>
        public InqFieldInfo Field { get; set; }
        public string FieldName { get { return Field.ColName; } }
        public string KeyField { get { return Field.KeyField; } }
        /// <summary>
        /// Tên của thứ tự của SQL (ASC/DESC)
        /// </summary>
        public string OrderName { get; set; }

        public InqOrderByInfo() { }
        public InqOrderByInfo(InqFieldInfo field) : this(field, "ASC") { }
        public InqOrderByInfo(InqFieldInfo field, string orderName)
        {
            this.Field = field;
            this.OrderName = orderName;
        }

        public string ToSql(string tblAlias)
        {
            if (!this.IsValid()) return "";
            if (string.IsNullOrWhiteSpace(this.OrderName)) return this.Field.ToSql();
            if (string.IsNullOrWhiteSpace(tblAlias))
                return string.Format("[{0}].[{1}] {2}", this.Field.TblName, this.Field.ColName, this.OrderName);
            return string.Format("[{0}].[{1}] {2}", tblAlias, this.Field.ColName, this.OrderName);
        }
        /// <summary>
        /// Đưa về chuỗi có dạng: "[Tên table].[Tên cột] OrderName" (VD: [DimItem].[ItemCode] ASC)
        /// </summary>
        public override string ToSql()
        {
            return this.ToSql(null);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return
                this.Field != null && this.Field.IsValid(false) &&
               (string.IsNullOrWhiteSpace(this.OrderName) ||
                this.GetOrderByName().Contains(this.OrderName));
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public override void DoValid()
        {
            if (this.Field == null) this.Field = new InqFieldInfo();
            this.Field.DoValid();
        }
    }    
    public class InqDefineSource_Back : Inq
    {
        /// <summary>
        /// Số lượng dòng tối đa mà tập dữ liệu sẽ trả về từ truy vấn
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Danh sách các thông tin cần lấy của mệnh đề 'SELECT'
        /// </summary>
        public List<InqFieldInfo> Fields { get; set; }
        /// <summary>
        /// Danh sách các thông tin thống kê của mệnh đề 'SELECT'
        /// </summary>
        public List<InqSummaryInfo> Summaries { get; set; }
        /// <summary>
        /// Danh sách các thông tin của mệnh đề 'WHERE' hoặc 'HAVING'
        /// </summary>
        public List<InqFilterInfo> Filters { get; set; }
        /// <summary>
        /// Danh sách các các thông tin của mệnh đề 'ORDER BY'
        /// </summary>
        public List<InqOrderByInfo> Orders { get; set; }

        public InqDefineSource_Back()
        {
            this.Fields = new List<InqFieldInfo>();
            this.Filters = new List<InqFilterInfo>();
            this.Summaries = new List<InqSummaryInfo>();
        }
        public InqDefineSource_Back(int top)
            : this()
        {
            this.Top = top;
        }
        public InqDefineSource_Back(int top, List<InqFieldInfo> fields, List<InqSummaryInfo> summaries, List<InqFilterInfo> filters, List<InqOrderByInfo> orders)
        {
            this.Top = top;
            this.Fields = fields;
            this.Summaries = summaries;
            this.Filters = filters;
            this.Orders = orders;
        }

        /// <summary>
        /// Chuyển về chuỗi json từ đối tượng hiện hành
        /// </summary>
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        /// <summary>
        /// Chuyển trở lại đối tượng từ chuỗi lưu trữ dạng json
        /// </summary>
        public static InqDefineSource_Back FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<InqDefineSource_Back>(jsonStr);
        }

        public void AddField(string tblName, string colName)
        {
            this.AddField(tblName, colName, "NTEXT");
        }
        public void AddField(string tblName, string colName, string dataType)
        {
            this.AddField(new InqFieldInfo(tblName, colName, dataType));
        }
        public void AddField(InqFieldInfo info)
        {
            if (this.Fields == null) this.Fields = new List<InqFieldInfo>();
            this.Fields.Add(info);
        }

        public void AddSummary(string tblName, string colName, string funcName, string alias)
        {
            this.AddSummary(new InqSummaryInfo(new InqFieldInfo(tblName, colName), funcName, alias));
        }
        public void AddSummary(InqFieldInfo field, string funcName, string alias)
        {
            this.AddSummary(new InqSummaryInfo(field, funcName, alias));
        }
        public void AddSummary(InqSummaryInfo info)
        {
            if (this.Summaries == null) this.Summaries = new List<InqSummaryInfo>();
            this.Summaries.Add(info);
        }

        public void AddFilter(InqFieldInfo whereKey, string opt, object value)
        {
            this.AddFilter(new InqFilterInfo(whereKey, opt, value));
        }
        public void AddFilter(InqSummaryInfo havingKey, string opt, object value)
        {
            this.AddFilter(new InqFilterInfo(havingKey, opt, value));
        }
        public void AddFilter(InqFilterInfo info)
        {
            if (this.Filters == null) this.Filters = new List<InqFilterInfo>();
            this.Filters.Add(info);
        }

        public void AddOrder(string tblName, string colName)
        {
            this.AddOrder(new InqFieldInfo(tblName, colName));
        }
        public void AddOrder(InqFieldInfo field)
        {
            this.AddOrder(new InqOrderByInfo(field));
        }
        public void AddOrder(string tblName, string colName, string orderName)
        {
            this.AddOrder(new InqOrderByInfo(new InqFieldInfo(tblName, colName), orderName));
        }
        public void AddOrder(InqFieldInfo field, string orderName)
        {
            this.AddOrder(new InqOrderByInfo(field, orderName));
        }
        public void AddOrder(InqOrderByInfo info)
        {
            if (this.Orders == null) this.Orders = new List<InqOrderByInfo>();
            this.Orders.Add(info);
        }

        /// <summary>
        /// Đưa đối tượng về thành câu lệnh Sql để thực thi
        /// </summary>
        public virtual string ToSql(bool isWrapText)
        {
            var wrapLine = isWrapText ? Environment.NewLine : "";
            var wrapTab = isWrapText ? "\t" : "";
            var lineAndTab = wrapLine + wrapTab;
            var sqlStr = "";
            try
            {
                //////////////////////////////////////////////SELECT... (Bắt buộc)
                var selectStr = (this.Summaries.Count == 0 && this.Top > 0)
                    ? string.Format("SELECT TOP({0}) ", this.Top) : "SELECT ";
                var normalFieldStr = "";
                var summaryFieldStr = "";
                foreach (var f in this.Fields)
                {
                    normalFieldStr = normalFieldStr + lineAndTab + string.Format("{0}, ", f.ToSql());
                }
                foreach (var f in this.Summaries)
                {
                    summaryFieldStr = summaryFieldStr + lineAndTab + string.Format("{0}, ", f.ToSql());
                }
                selectStr = selectStr + normalFieldStr + summaryFieldStr;
                if (selectStr.EndsWith(", "))
                    selectStr = selectStr.Remove(selectStr.Length - (", ").Length);
                //////////////////////////////////////////////SELECT...]

                //////////////////////////////////////////////GROUP BY... (Nếu có)
                var groupBy = "";
                if (this.Summaries.Count > 0)
                {
                    groupBy = wrapLine + "GROUP BY " + normalFieldStr;
                }
                if (groupBy.EndsWith(", "))
                    groupBy = groupBy.Remove(groupBy.Length - (", ").Length);
                //////////////////////////////////////////////GROUP BY...]
                
                //////////////////////////////////////////////FROM... (Bắt buộc)
                // Lấy ra danh sách các bảng dữ liệu tham gia                
                var tbls = new List<lsttbl_DWTable>();
                foreach (var tbl in this.Fields.Select(p => p.TblName).Distinct())
                {
                    if (tbls.FirstOrDefault(p => p.TblName == tbl) == null)
                    {
                        var mytbl = MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == tbl);
                        tbls.Add(mytbl);
                    }
                }

                // Xuất phát từ bảng dữ liệu (FACT).
                var mainTbl = tbls.FirstOrDefault(p => p.TblCat == "FACT");
                var fromStr = wrapLine + string.Format("FROM [{0}] ", mainTbl.TblName);
                // Chứa danh sách các bảng bị phụ thuộc
                var refTbl = new List<lsttbl_DWTable>();
                // Lấy ra các thông tin tham chiếu giữa các bảng, đi từ bảng chính
                var joinStr = "";
                //[tbl.RefInfo:]  ItemCode=DimItem:ItemCode,SlpCode=DimSalePerson:SlpCode
                if (string.IsNullOrWhiteSpace(mainTbl.RefInfo)) return "không có tham chiếu";
                var arrRef = mainTbl.RefInfo.Split(',');
                foreach (var r in arrRef)
                {
                    //[r:]  ItemCode=DimItem:ItemCode
                    var arr1 = r.Split('=');
                    var arr2 = arr1.First().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var arr3 = arr1.Last().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var refTblName = arr3.First();
                    arr3.Remove(refTblName);
                    if (tbls.FirstOrDefault(p => p.TblName == refTblName) != null)
                    {
                        if (refTbl.FirstOrDefault(p => p.TblName == refTblName) == null)
                            refTbl.Add(MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == refTblName));
                    }
                    else continue;
                    if (arr2.Count != arr3.Count) continue; // bắt buộc phải bằng nhau mới đi tiếp

                    var onJoin = "";
                    for (int i = 0; i < arr2.Count; i++)
                    {
                        var mainField = arr2[i];
                        var refField = arr3[i];
                        onJoin = onJoin + string.Format("[{0}].[{1}] = [{2}].[{3}] ", mainTbl.TblName, mainField, refTblName, refField);
                        if (i < arr2.Count - 1)
                            onJoin = onJoin + "AND ";
                    }
                    joinStr = joinStr + lineAndTab +
                              string.Format("LEFT OUTER JOIN [{0}] ON {1}", refTblName, onJoin);
                }

                var level = 1;
                // Trên từng bảng [DIM] Làm tương tự như trên bảng chính..
                while (refTbl.Count > 0)
                {
                    // Nhảy vào một cấp ứng với cấp độ của các bảng tham chiếu hiện hành.
                    lineAndTab = wrapLine + wrapTab.DummySet(level++);
                    //joinStr = joinStr + lineAndTab;
                    var refTbl1 = new List<lsttbl_DWTable>();
                    foreach (var rf in refTbl)
                    {
                        mainTbl = rf;
                        // Lấy ra các thông tin tham chiếu giữa các bảng, đi từ bảng chính
                        //[tbl.RefInfo:]  ItemCode=DimItem:ItemCode,SlpCode=DimSalePerson:SlpCode
                        if (string.IsNullOrWhiteSpace(mainTbl.RefInfo)) continue;
                        arrRef = mainTbl.RefInfo.Split(',');
                        foreach (var r in arrRef)
                        {
                            //[r:]  ItemCode=DimItem:ItemCode
                            var arr1 = r.Split('=');
                            var arr2 = arr1.First().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var arr3 = arr1.Last().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var refTblName = arr3.First();
                            arr3.Remove(refTblName);
                            if (tbls.FirstOrDefault(p => p.TblName == refTblName) != null)
                            {
                                if (refTbl1.FirstOrDefault(p => p.TblName == refTblName) == null)
                                    refTbl1.Add(MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == refTblName));
                            }
                            else continue;
                            if (arr2.Count != arr3.Count) continue; // bắt buộc phải bằng nhau mới đi tiếp

                            var onJoin = "";
                            for (int i = 0; i < arr2.Count; i++)
                            {
                                var mainField = arr2[i];
                                var refField = arr3[i];
                                onJoin = onJoin + string.Format("[{0}].[{1}] = [{2}].[{3}] ", mainTbl.TblName, mainField, refTblName, refField);
                                if (i < arr2.Count - 1)
                                    onJoin = onJoin + "AND ";
                            }
                            joinStr = joinStr + lineAndTab +
                                      string.Format("LEFT OUTER JOIN [{0}] ON {1}", refTblName, onJoin);
                        }
                    }
                    // Gán lại những bảng [DIM] nào còn có quan hệ qua các bảng khác để lập lại xử lý trên.
                    refTbl = new List<lsttbl_DWTable>(refTbl1);
                }
                fromStr = fromStr + joinStr;
                //////////////////////////////////////////////FROM...]

                //////////////////////////////////////////////WHERE... (Nếu có)
                lineAndTab = wrapLine + wrapTab;                
                var whereStr = "";
                var whereSet = this.Filters.Where(p => p.WhereKey != null).ToList();
                if (whereSet.Count > 0)
                    whereStr = wrapLine + "WHERE ";
                for (int i = 0; i < whereSet.Count; i++)
                {
                    whereStr = whereStr + lineAndTab + whereSet[i].ToSql();
                    if (i < whereSet.Count - 1)
                        whereStr = whereStr + " AND ";
                }
                //////////////////////////////////////////////WHERE...]

                //////////////////////////////////////////////HAVING... (Nếu có)
                var havingStr = "";
                var havingSet = this.Filters.Where(p => p.HavingKey != null).ToList();
                if (havingSet.Count > 0)
                    havingStr = wrapLine + "HAVING ";
                for (int i = 0; i < havingSet.Count; i++)
                {
                    havingStr = havingStr + lineAndTab + havingSet[i].ToSql();
                    if (i < havingSet.Count - 1)
                        havingStr = havingStr + " AND ";
                }
                //////////////////////////////////////////////HAVING...]

                //////////////////////////////////////////////ORDER BY... (Nếu có)
                var orderByStr = "";
                var tblAlias = this.Summaries.Count == 0 ? "" : "TBL_L1";
                if (this.Orders.Count > 0)
                    orderByStr = wrapLine + "ORDER BY ";
                for (int i = 0; i < this.Orders.Count; i++)
                {
                    orderByStr = orderByStr + lineAndTab + this.Orders[i].ToSql(tblAlias);
                    if (i < this.Orders.Count - 1)
                        orderByStr = orderByStr + ", ";
                }
                //////////////////////////////////////////////ORDER BY...]                

                //sqlStr = string.Format("{0} {1} {2} {3} {4} {5}", selectStr, fromStr, whereStr, groupBy, havingStr, orderByStr);

                // Nếu có thống kê thì 'Top' và 'OrderBy' nằm ở cấp ngoài.
                if (this.Summaries.Count > 0)
                {
                    sqlStr = string.Format("{0} {1} {2} {3} {4}", selectStr, fromStr, whereStr, groupBy, havingStr);
                    var topStr = this.Top > 0 ? string.Format("TOP({0})", this.Top) : "";
                    sqlStr = string.Format("SELECT {0} * FROM (" + wrapLine + "{1}" + wrapLine + ") AS [{2}] {3}", topStr, sqlStr, tblAlias, orderByStr);
                }
                else
                    sqlStr = string.Format("{0} {1} {2} {3} {4} {5}", selectStr, fromStr, whereStr, groupBy, havingStr, orderByStr);
            }
            catch { }
            return sqlStr;
        }
        /// <summary>
        /// Đưa đối tượng về thành câu lệnh Sql để thực thi
        /// <para>isWrapText=false</para>
        /// </summary>
        public override string ToSql()
        {
            return this.ToSql(false);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return
                this.Fields != null && this.Fields.Count > 0 &&
                this.Fields.Count(p => !p.IsValid()) == 0 &&
               (this.Filters == null || !this.Filters.Exists(p => !p.IsValid())) &&
               (this.Summaries == null || !this.Summaries.Exists(p => !p.IsValid())) &&
               (this.Orders == null || !this.Orders.Exists(p => !p.IsValid()));
        }
    }
    /// <summary>
    /// Định nghĩa cấu trúc truy vấn có thể build thành câu lệnh SQL, và có thể lưu lại ở dạng chuỗi Json
    /// </summary>
    public class InqDefineSource : Inq
    {
        /// <summary>
        /// Số lượng dòng tối đa mà tập dữ liệu sẽ trả về từ truy vấn
        /// </summary>
        public int Top { get; set; }
        public List<string> TblNames { get; set; }
        /// <summary>
        /// Danh sách các thông tin cần lấy của mệnh đề 'SELECT'
        /// </summary>
        public List<InqFieldInfo> Fields { get; set; }
        /// <summary>
        /// Danh sách các thông tin thống kê của mệnh đề 'SELECT'
        /// </summary>
        public List<InqSummaryInfo> Summaries { get; set; }
        /// <summary>
        /// Danh sách các thông tin của mệnh đề 'WHERE' hoặc 'HAVING'
        /// </summary>
        public List<InqFilterInfo> Filters { get; set; }

        public InqDefineSource()
        {
            this.Top = 0;
            this.TblNames = new List<string>();
            this.Fields = new List<InqFieldInfo>();
            this.Filters = new List<InqFilterInfo>();
            this.Summaries = new List<InqSummaryInfo>();
        }
        public InqDefineSource(string name)
            : this()
        {
            this.Top = 0;
        }
        public InqDefineSource(List<InqFieldInfo> fields, List<InqSummaryInfo> summaries, List<InqFilterInfo> filters)
        {
            this.Top = 0;
            this.TblNames = new List<string>();
            this.Fields = fields;
            this.Summaries = summaries;
            this.Filters = filters;
        }

        /// <summary>
        /// Chuyển về chuỗi json từ đối tượng hiện hành
        /// </summary>
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        /// <summary>
        /// Chuyển trở lại đối tượng từ chuỗi lưu trữ dạng json
        /// </summary>
        public static InqDefineSource FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<InqDefineSource>(jsonStr);
        }

        public void AddTblName(List<string> tblNames)
        {
            if (tblNames != null && tblNames.Count > 0)
            {
                if (this.TblNames == null) this.TblNames = new List<string>();
                this.TblNames.AddRange(tblNames);
            }
        }
        public void AddTblName(params string[] tblNames)
        {
            if (tblNames != null && tblNames.Length > 0)
            {
                if (this.TblNames == null) this.TblNames = new List<string>();
                this.TblNames.AddRange(tblNames);
            }
        }

        public void AddField(string tblName, string colName)
        {
            this.AddField(tblName, colName, "NTEXT");
        }
        public void AddField(string tblName, string colName, string dataType)
        {
            this.AddField(new InqFieldInfo(tblName, colName, dataType));
        }
        public void AddField(InqFieldInfo info)
        {
            if (this.Fields == null) this.Fields = new List<InqFieldInfo>();
            this.Fields.Add(info);
        }

        public void AddSummary(string tblName, string colName, string funcName, string alias)
        {
            this.AddSummary(new InqSummaryInfo(new InqFieldInfo(tblName, colName), funcName, alias));
        }
        public void AddSummary(string tblName, string colName, string funcName)
        {
            this.AddSummary(new InqSummaryInfo(new InqFieldInfo(tblName, colName), funcName, colName));
        }
        public void AddSummary(InqFieldInfo field, string funcName, string alias)
        {
            this.AddSummary(new InqSummaryInfo(field, funcName, alias));
        }
        public void AddSummary(InqFieldInfo field, string funcName)
        {
            this.AddSummary(new InqSummaryInfo(field, funcName));
        }
        public void AddSummary(InqSummaryInfo info)
        {
            if (this.Summaries == null) this.Summaries = new List<InqSummaryInfo>();
            this.Summaries.Add(info);
        }

        public void AddFilter(InqFieldInfo whereKey, string opt, object value)
        {
            this.AddFilter(new InqFilterInfo(whereKey, opt, value));
        }
        public void AddFilter(InqSummaryInfo havingKey, string opt, object value)
        {
            this.AddFilter(new InqFilterInfo(havingKey, opt, value));
        }
        public void AddFilter(InqFilterInfo info)
        {
            if (this.Filters == null) this.Filters = new List<InqFilterInfo>();
            this.Filters.Add(info);
        }

        public virtual List<lsttbl_DWTable> GetTblOnJoin()
        {
            // Lấy ra danh sách các bảng dữ liệu tham gia
            var tbls = new List<lsttbl_DWTable>();
            foreach (var tbl in this.Fields.Select(p => p.TblName).Distinct())
            {
                if (!tbls.Exists(p => p.TblName == tbl))
                {
                    var mytbl = MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == tbl);
                    tbls.Add(mytbl);
                }
            }
            foreach (var tbl in this.Summaries.Select(p => p.Field.TblName).Distinct())
            {
                if (!tbls.Exists(p => p.TblName == tbl))
                {
                    var mytbl = MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == tbl);
                    tbls.Add(mytbl);
                }
            }
            return tbls;
        }
        /// <summary>
        /// Đưa đối tượng về thành câu lệnh Sql để thực thi
        /// </summary>
        public virtual string ToSql(bool isWrapText)
        {
            var wrapLine = isWrapText ? Environment.NewLine : "";
            var wrapTab = isWrapText ? "\t" : "";
            var lineAndTab = wrapLine + wrapTab;
            var sqlStr = "";
            try
            {
                ////////////////////////////////////////////// Auto correct...
                foreach (var obj in this.Summaries)
                {
                    if (obj.Field == null) continue;
                    obj.Field.DataType = "NUM";
                }
                //////////////////////////////////////////////

                //////////////////////////////////////////////SELECT... (Bắt buộc)
                var selectStr = (this.Summaries.Count == 0 && this.Top > 0)
                    ? string.Format("SELECT DISTINCT TOP({0}) ", this.Top) : "SELECT ";

                var normalFieldStr = "";
                var summaryFieldStr = "";
                foreach (var f in this.Fields)
                {
                    normalFieldStr = normalFieldStr + lineAndTab + string.Format("{0}, ", f.ToSql());
                }
                foreach (var f in this.Summaries)
                {
                    summaryFieldStr = summaryFieldStr + lineAndTab + string.Format("{0}, ", f.ToSql());
                }
                selectStr = selectStr + normalFieldStr + summaryFieldStr;
                if (selectStr.EndsWith(", "))
                    selectStr = selectStr.Remove(selectStr.Length - (", ").Length);
                //////////////////////////////////////////////SELECT...]

                //////////////////////////////////////////////GROUP BY... (Nếu có)
                var groupBy = "";
                if (this.Summaries.Count > 0)
                {
                    groupBy = wrapLine + "GROUP BY " + normalFieldStr;
                }
                if (groupBy.EndsWith(", "))
                    groupBy = groupBy.Remove(groupBy.Length - (", ").Length);
                //////////////////////////////////////////////GROUP BY...]

                //////////////////////////////////////////////FROM... (Bắt buộc)
                // Lấy ra danh sách các bảng dữ liệu tham gia                
                var tbls = MyBI.Me.Get_DWTable().Where(p => this.TblNames.Contains(p.TblName)).ToList();
                // Xuất phát từ bảng dữ liệu (FACT).
                var factTbl = tbls.FirstOrDefault(p => p.TblCat == "FACT");
                var fromStr = wrapLine + string.Format("FROM [{0}] ", factTbl.TblName);
                // Chứa danh sách các bảng bị phụ thuộc
                var dimTbl = new List<lsttbl_DWTable>();
                // Lấy ra các thông tin tham chiếu giữa các bảng, được bảng chính tham chiếu trực tiếp.
                var joinStr = "";
                //[tbl.RefInfo:]  ItemCode=DimItem:ItemCode,SlpCode=DimSalePerson:SlpCode
                if (string.IsNullOrWhiteSpace(factTbl.RefInfo)) return "không có tham chiếu";
                var arrFactRef = factTbl.RefInfo.Split(',');
                foreach (var dim in arrFactRef)
                {
                    //[r:]  ItemCat:ItemCode=DimItem:ItemCat:ItemCode (Bảng chính tham khảo ItemCat và ItemCode trên bảng DimItem)
                    var arr1 = dim.Split('=');
                    var arr2 = arr1.First().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var arr3 = arr1.Last().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var dimTblName = arr3.First();
                    arr3.Remove(dimTblName);
                    if (tbls.Exists(p => p.TblName == dimTblName))
                    {
                        if (!dimTbl.Exists(p => p.TblName == dimTblName))
                            dimTbl.Add(MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == dimTblName));
                    }
                    else continue;
                    if (arr2.Count != arr3.Count) continue; // bắt buộc phải bằng nhau mới đi tiếp

                    var onJoin = "";
                    for (int i = 0; i < arr2.Count; i++)
                    {
                        var factField = arr2[i];
                        var dimField = arr3[i];
                        onJoin = onJoin + string.Format("[{0}].[{1}] = [{2}].[{3}] ", factTbl.TblName, factField, dimTblName, dimField);
                        if (i < arr2.Count - 1) onJoin = onJoin + "AND ";
                    }
                    joinStr = joinStr + lineAndTab +
                              string.Format("LEFT OUTER JOIN [{0}] ON {1}", dimTblName, onJoin);
                }

                var level = 1;
                // Trên từng bảng [DIM] Làm tương tự như trên bảng chính..
                while (dimTbl.Count > 0)
                {
                    // Nhảy vào một cấp ứng với cấp độ của các bảng tham chiếu hiện hành.
                    lineAndTab = wrapLine + wrapTab.DummySet(level++);
                    //joinStr = joinStr + lineAndTab;
                    var dimTbl1 = new List<lsttbl_DWTable>();
                    foreach (var parentDim in dimTbl)
                    {
                        //var parentDim = dim;
                        // Lấy ra các thông tin tham chiếu giữa các bảng, đi từ bảng chính
                        //[tbl.RefInfo:]  ItemCode=DimItem:ItemCode,SlpCode=DimSalePerson:SlpCode
                        if (string.IsNullOrEmpty(parentDim.RefInfo)) continue;
                        var arrParentDimRef = parentDim.RefInfo.Split(',');
                        foreach (var parentDimRef in arrParentDimRef)
                        {
                            //[r:]  ItemCode=DimItem:ItemCode
                            var arr1 = parentDimRef.Split('=');
                            var arr2 = arr1.First().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var arr3 = arr1.Last().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var childDimTblName = arr3.First();
                            arr3.Remove(childDimTblName);
                            if (tbls.FirstOrDefault(p => p.TblName == childDimTblName) != null)
                            {
                                if (dimTbl1.FirstOrDefault(p => p.TblName == childDimTblName) == null)
                                    dimTbl1.Add(MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == childDimTblName));
                            }
                            else continue;
                            if (arr2.Count != arr3.Count) continue; // bắt buộc phải bằng nhau mới đi tiếp

                            var onJoin = "";
                            for (int i = 0; i < arr2.Count; i++)
                            {
                                var parentDimField = arr2[i];
                                var childDimField = arr3[i];
                                onJoin = onJoin + string.Format("[{0}].[{1}] = [{2}].[{3}] ", parentDim.TblName, parentDimField, childDimTblName, childDimField);
                                if (i < arr2.Count - 1)
                                    onJoin = onJoin + "AND ";
                            }
                            joinStr = joinStr + lineAndTab +
                                      string.Format("LEFT OUTER JOIN [{0}] ON {1}", childDimTblName, onJoin);
                        }
                    }
                    // Gán lại những bảng [DIM] nào còn có quan hệ qua các bảng khác để lập lại xử lý trên.
                    dimTbl = new List<lsttbl_DWTable>(dimTbl1);
                }
                fromStr = fromStr + joinStr;
                //////////////////////////////////////////////FROM...]

                //////////////////////////////////////////////WHERE... (Nếu có)
                lineAndTab = wrapLine + wrapTab;
                var whereStr = "";
                var whereSet = this.Filters.Where(p => p.WhereKey != null).ToList();
                if (whereSet.Count > 0)
                    whereStr = wrapLine + "WHERE ";
                for (int i = 0; i < whereSet.Count; i++)
                {
                    whereStr = whereStr + lineAndTab + whereSet[i].ToSql();
                    if (i < whereSet.Count - 1)
                        whereStr = whereStr + string.Format(" {0} ", whereSet[i].Logic);
                }
                //////////////////////////////////////////////WHERE...]

                //////////////////////////////////////////////HAVING... (Nếu có)
                var havingStr = "";
                var havingSet = this.Filters.Where(p => p.HavingKey != null).ToList();
                if (havingSet.Count > 0)
                    havingStr = wrapLine + "HAVING ";
                for (int i = 0; i < havingSet.Count; i++)
                {
                    havingStr = havingStr + lineAndTab + havingSet[i].ToSql();
                    if (i < havingSet.Count - 1)
                        havingStr = havingStr + string.Format(" {0} ", havingSet[i].Logic);
                }
                //////////////////////////////////////////////HAVING...]

                //////////////////////////////////////////////ORDER BY... (Nếu có)
                var orderByStr = "";
                var tblAlias = this.Summaries.Count == 0 ? "" : "TBL_L1";
                var fieldOrders = this.Fields.Where(p => !string.IsNullOrWhiteSpace(p.OrderName)).ToList();
                var fieldOrders1 = this.Summaries.Where(p => !string.IsNullOrWhiteSpace(p.Field.OrderName)).ToList();

                if (fieldOrders.Count > 0 || fieldOrders1.Count > 0)
                    orderByStr = wrapLine + "ORDER BY ";
                for (int i = 0; i < fieldOrders.Count; i++)
                {
                    orderByStr = orderByStr + lineAndTab + fieldOrders[i].ToSql_OrderBy(tblAlias);
                    if (i < fieldOrders.Count - 1)
                        orderByStr = orderByStr + ", ";
                }
                //                
                if (fieldOrders1.Count > 0 && orderByStr != wrapLine + "ORDER BY ") 
                    orderByStr = orderByStr + ", ";
                for (int i = 0; i < fieldOrders1.Count; i++)
                {
                    orderByStr = orderByStr + lineAndTab + fieldOrders1[i].Field.ToSql_OrderBy(tblAlias);
                    if (i < fieldOrders1.Count - 1)
                        orderByStr = orderByStr + ", ";
                }
                //////////////////////////////////////////////ORDER BY...]                

                //sqlStr = string.Format("{0} {1} {2} {3} {4} {5}", selectStr, fromStr, whereStr, groupBy, havingStr, orderByStr);

                // Nếu có thống kê thì 'Top' và 'OrderBy' nằm ở cấp ngoài.
                if (this.Summaries.Count > 0)
                {
                    sqlStr = string.Format("{0} {1} {2} {3} {4}", selectStr, fromStr, whereStr, groupBy, havingStr);
                    var topStr = this.Top > 0 ? string.Format("TOP({0})", this.Top) : "";
                    sqlStr = string.Format("SELECT {0} * FROM (" + wrapLine + "{1}" + wrapLine + ") AS [{2}] {3}", topStr, sqlStr, tblAlias, orderByStr);
                }
                else
                    sqlStr = string.Format("{0} {1} {2} {3} {4} {5}", selectStr, fromStr, whereStr, groupBy, havingStr, orderByStr);
            }
            catch { }
            return sqlStr;
        }
        /// <summary>
        /// Đưa đối tượng về thành câu lệnh Sql để thực thi
        /// <para>isWrapText=false</para>
        /// </summary>
        public override string ToSql()
        {
            return this.ToSql(false);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return
                this.Fields != null && this.Fields.Count > 0 &&
                this.Fields.Count(p => !p.IsValid()) == 0 &&
               (this.Filters == null || !this.Filters.Exists(p => !p.IsValid())) &&
               (this.Summaries == null || !this.Summaries.Exists(p => !p.IsValid()));
        }        
    }
    #endregion

    #region Datasource - Definations Models (Apply For: OLAP Database Relationship)

    #region Datasource...
    /// <summary>
    /// Định nghĩa các hàm cơ bản của đối tượng Inquiry
    /// </summary>
    public abstract class InqMDX
    {
        public int ID { get; set; }       

        /// <summary>
        /// Các kiểu dữ liệu của Inq (kết quả ánh xạ từ kiểu dữ liệu của SQL MDX)
        /// </summary>
        public enum InqDataType
        {
            NUM, DATE, TEXT, NTEXT, UNKNOWN
        }
        public virtual string ToMDX() { return ""; }
        public abstract bool IsValid();
        public virtual void DoValid() { }
        /// <summary>
        /// Danh sách các tên hàm thống kê
        /// </summary>
        public static string[] GetSummatyFuncName()
        {
            return new string[] { "", "SUM", "COUNT", "AVG", "MIN", "MAX" };
        }
        /// <summary>
        /// Danh sách các toán tử của filter
        /// </summary>
        public static string[] GetFilterOperator()
        {
            return new string[] { "=", ">", ">=", "<", "<=", "<>" };
        }
        /// <summary>
        /// Danh sách các tên thứ tự dữ liệu
        /// </summary>
        public static string[] GetOrderByName()
        {
            return new string[] { "", "ASC", "DESC" };
        }
        /// <summary>
        /// Danh sách các tên kiểu dữ liệu của Inq (kết quả ánh xạ từ kiểu dữ liệu của SQL)
        /// </summary>
        public static string[] GetInqDataTypeName()
        {
            return new string[] { "DATE", "NTEXT", "TEXT", "NUM" };
        }
        /// <summary>
        /// Danh sách các toán tử Logic kết hợp 2 mệnh đề với nhau.
        /// </summary>
        public static string[] GetLogicCombine()
        {
            return new string[] { "AND", "OR" };
        }
    }
    public class InqTableInfoMDX : InqMDX
    {
        public bool Visible { get; set; }
        public string TblCat { get; set; }
        public string BizCat { get; set; }
        public string TblName { get; set; }
        public string TblAliasVI { get; set; }
        public string TblAliasEN { get; set; }
        public string RefInfo { get; set; }

        public InqTableInfoMDX() { }
        public InqTableInfoMDX(string tblName)
        {
            this.TblName = tblName;
        }
        public InqTableInfoMDX(string tblName, string tblCat, string bizCat)
        {
            this.TblName = tblName;
            this.TblCat = tblCat;
            this.BizCat = bizCat;
        }
        public virtual string ToMDX(string preffix)
        {
            if (!this.IsValid()) return "";
            return string.Format("[{0}{1}]", preffix, this.TblName);
        }
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.TblName);
        }
        public override void DoValid()
        {
            this.TblName = "[MASTER].[sys].[tables]";
        }
    }
    /// <summary>
    /// Thông tin của 1 field dữ liệu
    /// </summary>
    public class InqFieldInfoMDX : InqMDX
    {
        public string KeyField { get { return string.Format("{0}_{1}", this.TblName, this.ColName); } }
        public bool Visible { get; set; }
        public string TblName { get; set; }
        public string ColName { get; set; }
        public string ColAliasVI { get; set; }
        public string ColAliasEN { get; set; }
        public string DataType { get; set; }
        public string OrderName { get; set; }
        public int Level { get; set; }

        public InqFieldInfoMDX() { }
        public InqFieldInfoMDX(string tblName, string colName)
        {
            this.TblName = tblName;
            this.ColName = colName;
        }
        public InqFieldInfoMDX(string tblName, string colName, int level)
            : this(tblName, colName) 
        {
            this.Level = level;
        }
        public InqFieldInfoMDX(string tblName, string colName, string dataType)
        {
            this.TblName = tblName;
            this.ColName = colName;
            this.DataType = dataType;
        }
        public InqFieldInfoMDX(string tblName, string colName, string dataType, int level)
            : this(tblName, colName, dataType)
        {
            this.Level = level;
        }
        public InqFieldInfoMDX(string tblName, string colName, string colAlias, string dataType)
        {
            this.TblName = tblName;
            this.ColName = colName;
            this.DataType = dataType;
            this.ColAliasVI = colAlias;
        }
        public InqFieldInfoMDX(string tblName, string colName, string colAlias, string dataType, int level)
            : this(tblName, colName, colAlias, dataType)
        {
            this.Level = level;
        }

        public InqFieldInfoMDX Copy()
        {
            var ret = new InqFieldInfoMDX()
            {
                Visible = this.Visible,
                TblName = this.TblName,
                ColName = this.ColName,
                ColAliasVI = this.ColAliasVI,
                ColAliasEN = this.ColAliasEN,
                DataType = this.DataType,
                OrderName = this.OrderName,
                Level = this.Level,
            };
            return ret;
        }

        /// <summary>
        /// Kiểm tra Field hiện hành có được thiết lập OrderName hay không.
        /// </summary>
        public virtual bool HasOrder()
        {
            return !string.IsNullOrEmpty(this.OrderName);
        }
        /// <summary>
        /// Trả về tên field theo cú pháp của MDX ([TableName].[ColumnName].CHILDREN). 
        /// </summary>
        public override string ToMDX()
        {
            if (!this.IsValid()) return "";
            if (string.IsNullOrWhiteSpace(this.OrderName))
                //return string.Format("[{0}].[{1}].CHILDREN", this.TblName, this.ColName);
                return string.Format("[{0}].[{1}].MEMBERS", this.TblName, this.ColName);
            //return string.Format("ORDER([{0}].[{1}].CHILDREN,[{0}].[{1}].CURRENTMEMBER.NAME,{2})", this.TblName, this.ColName, this.OrderName);
            return string.Format("ORDER([{0}].[{1}].MEMBERS,[{0}].[{1}].CURRENTMEMBER.NAME,{2})", this.TblName, this.ColName, this.OrderName);
        }
        /// <summary>
        /// Trả về tên field theo cú pháp của MDX ([TableName].[ColumnName].CHILDREN). 
        /// Trường hợp Field xuất hiện ở 2 nơi(do user định nghĩa): trong mệnh đề SELECT và ở WHERE
        /// </summary>
        /// <param name="filterSetName">Tên tập giá trị của Dimension sau khi được filter</param>
        public virtual string ToMDX(string filterSetName)
        {
            if (string.IsNullOrEmpty(filterSetName)) return this.ToMDX();
            if (!this.IsValid()) return "";
            if (string.IsNullOrWhiteSpace(this.OrderName))
                return string.Format("{0}", filterSetName);
            return string.Format("ORDER({0},[{1}].[{2}].CURRENTMEMBER.NAME,{3})", filterSetName, this.TblName, this.ColName, this.OrderName);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// <para>strong=true</para>
        /// </summary>
        public override bool IsValid()
        {
            return this.IsValid(true);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// <para><param name="strong">Kiểm tra thêm các thông tin cần và đủ.</param></para>
        /// </summary>
        public virtual bool IsValid(bool strong)
        {
            if (strong)
                return
                    !string.IsNullOrWhiteSpace(this.TblName) &&
                    !string.IsNullOrWhiteSpace(this.ColName) &&
                    !string.IsNullOrWhiteSpace(this.DataType) &&
                     GetInqDataTypeName().Contains(this.DataType) &&
                    (string.IsNullOrWhiteSpace(this.OrderName) ||
                     GetOrderByName().Contains(this.OrderName));
            return
                !string.IsNullOrWhiteSpace(this.TblName) &&
                !string.IsNullOrWhiteSpace(this.ColName) &&
                (string.IsNullOrWhiteSpace(this.DataType) ||
                 GetInqDataTypeName().Contains(this.DataType)) &&
                (string.IsNullOrWhiteSpace(this.OrderName) ||
                 GetOrderByName().Contains(this.OrderName));
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// <para>type = InqDataType.TEXT</para>
        /// </summary>
        public override void DoValid()
        {
            this.DoValid(InqDataType.TEXT);
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public void DoValid(InqDataType type)
        {
            switch (type)
            {
                case InqDataType.DATE:
                    this.ColName = "GETDATE() AS DATE";
                    break;
                case InqDataType.TEXT:
                case InqDataType.NTEXT:
                    this.ColName = "name AS TEXT";
                    break;
                case InqDataType.NUM:
                    this.ColName = "0 AS NUM";
                    break;
            }
            this.TblName = "[MASTER].[sys].[tables]";
            this.DataType = type.ToString();
        }
        public override bool Equals(object obj)
        {
            var myObj = obj as InqFieldInfoMDX;
            if (myObj == null) return false;
            return
                (string.IsNullOrEmpty(this.TblName) || string.IsNullOrEmpty(myObj.TblName) || this.TblName.Equals(myObj.TblName)) &&
                (string.IsNullOrEmpty(this.ColName) || string.IsNullOrEmpty(myObj.ColName) || this.ColName.Equals(myObj.ColName));
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin của một field ở dạng thống kê (Tổng, Trung bình....)
    /// </summary>
    public class InqSummaryInfoMDX : InqMDX
    {
        /// <summary>
        /// Tên 1 trong các hàm thống kê (SUM, COUNT, AVG ...)
        /// </summary>
        public string FuncName { get; set; }
        /// <summary>
        /// Thông tin trường dữ liệu
        /// </summary>
        public InqFieldInfoMDX Field { get; set; }
        //public string FieldName { get { return this.Field.ColName; } }
        //public string KeyField { get { return this.Field.KeyField; } }
        private string _FieldAlias;
        /// <summary>
        /// Tên giả của trường dữ liệu
        /// </summary>
        public string FieldAlias
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_FieldAlias))
                    if (this.Field != null)
                        _FieldAlias = this.Field.ColName;
                return _FieldAlias;
            }
            set
            {
                _FieldAlias = value;
            }
        }
        /// <summary>
        /// Từ khóa chỉ ra một khoản thời gian trước đó: ([DimTime].[Year], [DimTime].[Quarter], [DimTime].[Period], [DimTime].[Day]).
        /// </summary>
        public string TimeFilterPrev { get; set; }
        /// <summary>
        /// Danh sách các thông tin filter trên Context
        /// </summary>
        public List<InqFilterInfoMDX> InnerFilters { get; set; }

        public InqSummaryInfoMDX() { }
        public InqSummaryInfoMDX(InqFieldInfoMDX field, string funcName)
        {
            this.FuncName = funcName;
            this.Field = field;
        }
        public InqSummaryInfoMDX(InqFieldInfoMDX field, string funcName, string alias)
        {
            this.FuncName = funcName;
            this.Field = field;
            this.FieldAlias = alias;
        }
        public InqSummaryInfoMDX Copy()
        {
            var ret = new InqSummaryInfoMDX()
            {
                Field = Field == null ? null : this.Field.Copy(),
                InnerFilters = this.InnerFilters == null ? null : this.InnerFilters.Select(p => p.Copy()).ToList(),
                FieldAlias = this.FieldAlias,
                FuncName = this.FuncName,
                TimeFilterPrev = this.TimeFilterPrev
            };
            return ret;
        }
        /// <summary>
        /// Lấy KeyField của đối tượng hiện hành (KeyField là một tên field được định danh lại dựa trên định danh gốc).
        /// <para>
        /// Cấu trúc của KeyField: [ColName][FuncName][ID], VD: QuantitySUM1
        /// </para>
        /// </summary>
        public virtual string Get_SummaryKeyField()
        {
            return string.Format("{0}{1}{2}", this.Field.ColName, this.FuncName, this.ID);
        }
        public bool HasInnerFilters()
        {
            return (this.InnerFilters != null && this.InnerFilters.Count > 0) ||
                    !string.IsNullOrEmpty(this.TimeFilterPrev);
        }
        /// <summary>
        /// Chuyển các InnerFilters về thành cú pháp With...Member của MDX
        /// </summary>
        public string GetSyntaxMDX_MemberStr(string lineAndTab)
        {
            var ret = "";
            var subffixMember = string.Format("For_{0}", this.Get_SummaryKeyField());
            var lstFilterParse = InqFilterInfoMDX.GetSyntaxMDX(this.InnerFilters, subffixMember);
            foreach (var filterParse in lstFilterParse)
                ret = ret + string.Format("SET {0} AS {1}", filterParse.Prop1, filterParse.Prop4) + lineAndTab;
            if (!string.IsNullOrEmpty(lineAndTab) && ret.EndsWith(lineAndTab))
                ret = ret.Remove(ret.Length - lineAndTab.Length);
            return ret;
        }
        /// <summary>
        /// Trả về một biểu thức tính toán trên field theo cú pháp MDX, dependSet_Name được lấy theo ngữ cảnh của thời gian (PrevYear, PrevQuarter, PrevPeriod, PrevDay).
        /// <para>Hoặc được lấy từ danh bộ lọc 'InnerFilters' (nếu có)</para>
        /// <para>Exp: COALESCEEMPTY(SUM([ARDimTime].[Period].PREVMEMBER,[Measures].[Quantity]),0)</para>
        /// </summary>
        public virtual string ToMDX()
        {
            if (!this.IsValid()) return "";
            //var linkChar = " * ";
            //var dependSet = string.IsNullOrEmpty(this.TimeFilterPrev) ? "" : string.Format("{0}.PREVMEMBER{1}", this.TimeFilterPrev, linkChar);
            //var subffixMember = string.Format("For_{0}", this.Get_SummaryKeyField());
            //var lstInnerFilterParse = InqFilterInfoMDX.GetSyntaxMDX(this.InnerFilters, subffixMember);
            //foreach (var item in lstInnerFilterParse)
            //    dependSet = dependSet + item.Prop1 + linkChar;
            //if (dependSet.EndsWith(linkChar))
            //    dependSet = dependSet.Remove(dependSet.Length - linkChar.Length);
            //return string.Format("COALESCEEMPTY({0}({1},[Measures].[{2}]),0)", FuncName, dependSet, Field.ColName);

            return string.Format("COALESCEEMPTY([Measures].[{0}],0)", Field.ColName);
        }
        /// <summary>
        /// Trả về một biểu thức tính toán trên field theo cú pháp MDX.
        /// <para>Exp: COALESCEEMPTY(SUM([ARDimItem].[ItemGroupName].CURRENTMEMBER,[Measures].[Quantity]),0) , trong đó dependSet là: [ARDimItem].[ItemGroupName].CURRENTMEMBER</para>
        /// <para>Exp: COALESCEEMPTY(SUM([ItemGroupFilter],[Measures].[Quantity]),0) , trong đó dependSet là alias: [ItemGroupFilter]</para>
        /// </summary>
        /// <param name="dependSet">Alias hoặc biểu thức của tập hợp mà việc Summary phải phụ thuộc lên nó.
        /// </param>
        public virtual string ToMDX(string dependSet)
        {
            if (!this.IsValid()) return "";
            //return string.Format("COALESCEEMPTY({0}({1}.CURRENTMEMBER,[Measures].[{2}]),0)", FuncName, dependSet, Field.ColName);
            return string.Format("COALESCEEMPTY([Measures].[{0}],0)",Field.ColName);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.FuncName) &&
                    GetSummatyFuncName().Contains(this.FuncName) &&
                    this.Field != null && this.Field.IsValid(false);
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public override void DoValid()
        {
            this.FuncName = GetSummatyFuncName().First();
            if (this.Field == null) this.Field = new InqFieldInfoMDX();
            this.Field = new InqFieldInfoMDX();
            this.Field.DoValid(InqDataType.NUM);
        }
        public override bool Equals(object obj)
        {
            var myObj = obj as InqSummaryInfoMDX;
            if (myObj == null) return false;
            return
                (this.Field == null || myObj.Field == null || this.Field.Equals(myObj.Field)) &&
                (string.IsNullOrEmpty(this.FuncName) || string.IsNullOrEmpty(myObj.FuncName) || this.FuncName.Equals(myObj.FuncName)) &&
                (string.IsNullOrEmpty(this.TimeFilterPrev) || string.IsNullOrEmpty(myObj.TimeFilterPrev) || this.TimeFilterPrev.Equals(myObj.TimeFilterPrev));
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin của một đối tượng filter trong mệnh dề WHERE hoặc HAVING.
    /// </summary>
    public class InqFilterInfoMDX : InqMDX
    {
        /// <summary>
        /// Khóa để filter ở mệnh đề 'WHERE' của câu lệnh SQL
        /// </summary>
        public InqFieldInfoMDX WhereKey { get; set; }
        //public string WhereField
        //{
        //    get
        //    {
        //        try { return WhereKey.ColName; }
        //        catch { return ""; }
        //    }
        //}
        //public string WhereKeyField
        //{
        //    get
        //    {
        //        try { return WhereKey.KeyField; }
        //        catch { return ""; }
        //    }
        //}
        /// <summary>
        /// Khóa để filter ở mệnh đề 'HAVING' của câu lệnh SQL
        /// </summary>
        public InqSummaryInfoMDX HavingKey { get; set; }
        //public string HavingField { get { return HavingKey.FieldName; } }
        //public string HavingKeyField { get { return HavingKey.Field.KeyField; } }
        /// <summary>
        /// Toán tử so sánh
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// Trị giá so sánh
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Toán tử liên kết logic
        /// </summary>
        public string Logic { get; set; }

        public InqFilterInfoMDX() { }
        public InqFilterInfoMDX(InqFieldInfoMDX whereKey, string opt, object value) : this(whereKey, opt, value, "AND") { }
        public InqFilterInfoMDX(InqFieldInfoMDX whereKey, string opt, object value, string logic)
        {
            this.WhereKey = whereKey;
            this.Operator = opt;
            this.Value = value;
            this.Logic = logic;
        }
        public InqFilterInfoMDX(InqSummaryInfoMDX havingKey, string opt, object value) : this(havingKey, opt, value, "AND") { }
        public InqFilterInfoMDX(InqSummaryInfoMDX havingKey, string opt, object value, string logic)
        {
            this.HavingKey = havingKey;
            this.Operator = opt;
            this.Value = value;
            this.Logic = logic;
        }

        public InqFilterInfoMDX Copy()
        {
            var ret = new InqFilterInfoMDX()
            {
                WhereKey = this.WhereKey == null ? null : this.WhereKey.Copy(),
                HavingKey = this.HavingKey == null ? null : this.HavingKey.Copy(),
                Operator = this.Operator,
                Value = this.Value,
                Logic = this.Logic
            };
            return ret;
        }
        /// <summary>
        /// Chuyển một tập hợp các đối tượng 'InqFilterInfoMDX' về thành một tập hợp các chuỗi lệnh FILTER theo cú pháp MDX.
        /// <para>
        /// Gom những đối tượng 'InqFilterInfoMDX' có điều kiện cùng kẹp trên một tên field lại với nhau
        /// </para>
        /// <para>
        /// Prop1:[AliasMember], Prop2:TableName, Prop3:FieldName, Prop4:ExpressionStr
        /// </para>
        /// </summary>
        /// <param name="src">Tập hợp các đối tượng 'InqFilterInfoMDX' do người dùng thiết lập</param>
        public static List<COM4ProperiesObj> GetSyntaxMDX(ICollection<InqFilterInfoMDX> src, string subffixMember)
        {
            var ret = new List<COM4ProperiesObj>();
            try
            {
                if (src.Count == 0) throw new Exception();
                var logicCombine = "";
                var lstDistinctName = src.Select(p => new { p.WhereKey.TblName, p.WhereKey.ColName }).Distinct();
                foreach (var field in lstDistinctName)
                {
                    var strFilter = "";
                    foreach (var item in src.Where(p => p.WhereKey.ColName == field.ColName))
                    {
                        logicCombine = string.Format(" {0} ", item.Logic);
                        strFilter = strFilter + string.Format("{0}{1}", item.ToMDX(), logicCombine);
                    }
                    strFilter = strFilter.Remove(strFilter.Length - logicCombine.Length);
                    strFilter = string.Format("FILTER([{0}].[{1}].MEMBERS, {2})", field.TblName, field.ColName, strFilter);
                    //strFilter = string.Format("FILTER([{0}].[{1}].CHILDREN, {2})", field.TblName, field.ColName, strFilter);
                    var cat = "";
                    if (string.IsNullOrEmpty(subffixMember))
                        cat = string.Format("[{0}FilterSet]", field.ColName);
                    else
                        cat = string.Format("[{0}FilterSet_{1}]", field.ColName, subffixMember);
                    ret.Add(new COM4ProperiesObj(cat, field.TblName, field.ColName, strFilter));
                }
            }
            catch { }
            return ret;
        }
        public bool HasWhereKey()
        {
            return this.WhereKey != null;
        }
        public bool HasHavingKey()
        {
            return this.HavingKey != null;
        }
        public string GetTinyType()
        {
            if (this.WhereKey != null)
            {
                switch (this.WhereKey.ColName)
                {
                    case "Year":
                    case "Quarter":
                    case "Period":
                    case "DateKey":
                        return "DATE";
                }
                switch (this.WhereKey.DataType)
                {
                    case "TEXT":
                    case "NTEXT": return "NORMAL";
                }
                return this.WhereKey.DataType;
            }
            else if (this.HavingKey != null)
            {
                switch (this.HavingKey.Field.DataType)
                {
                    case "TEXT":
                    case "NTEXT": return "NORMAL";
                }
                return this.HavingKey.Field.DataType;
            }
            return "";
        }
        public DateTime ToTimeValue()
        {
            var ret = DateTime.Now;
            try
            {
                var valStr = Lib.NTE(this.Value);
                if (Lib.IsNOE(valStr)) return ret;

                if (valStr.Length == 4) // giá trị hiện tại của năm
                    ret = new DateTime(int.Parse(valStr), ret.Month, ret.Day);
                else if (valStr.Length < 4) // giá trị hiện tại của Quí
                    ret = new DateTime(ret.Year, ret.Month, int.Parse(valStr));
                else if (valStr.Length == 6) // giá trị hiện tại của kỳ (VD: 201201)
                    ret = new DateTime(int.Parse(valStr.Substring(0, 4)), int.Parse(valStr.Substring(4, 2)), ret.Day);
                else if (valStr.Length == 10) // giá trị hiện tại của ngày tháng năm đầy đủ (VD: 2012/01/01)
                {
                    var arrTime = valStr.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    ret = new DateTime(int.Parse(arrTime[0]), int.Parse(arrTime[1]), int.Parse(arrTime[2]));
                }
            }
            catch { }
            return ret;
        }
        /// <summary>
        /// Trả về cấu trúc filter theo MDX.
        /// <para>VD: [ARDimTime].[Period].CURRENTMEMBER.NAME = "Value"</para>
        /// </summary>
        public override string ToMDX()
        {
            //FILTER([ARDimTime].[Period].MEMBERS,[ARDimTime].[Period].CURRENTMEMBER.NAME = Value)
            try
            {
                if (!this.IsValid()) throw new Exception();

                var opt = Lib.NTE(this.Operator);
                if (this.HasWhereKey())
                {
                    var tblName = Lib.NTE(this.WhereKey.TblName);
                    var colName = Lib.NTE(this.WhereKey.ColName);
                    if (opt == "NULL")
                        return string.Format("[{0}].[{1}].CURRENTMEMBER.NAME = NULL", tblName, colName);
                    switch (this.WhereKey.DataType)
                    {
                        case "TEXT":
                        case "NTEXT":
                        case "DATE":
                        case "NUM":
                            return string.Format("[{0}].[{1}].CURRENTMEMBER.NAME {2} \"{3}\"", tblName, colName, opt, Lib.NTE(this.Value));                       
                    }
                }
            }
            catch { }
            return "";
        }
        /// <summary>
        /// Trả về cấu trúc filter theo MDX, mà nó sẽ đặt trong mệnh đề HAVING.
        /// <para>VD: calculateMember_Name = "Value"</para>
        /// </summary>
        /// <param name="calculateMember_Name">Tên alias được đặt trong quá trình xây dựng cột tính toán</param>
        public virtual string ToMDX(string calculateMember_Name)
        {
            try
            {
                if (!this.IsValid()) throw new Exception();
                var opt = Lib.NTE(this.Operator);
                if (opt == "NULL")
                    return string.Format("{0} = 0", calculateMember_Name);
                if (this.HasHavingKey())
                    return string.Format("{0} {1} {2}", calculateMember_Name, opt, this.Value);
            }
            catch { }
            return "";
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return (
                    (this.HasWhereKey() && this.WhereKey.IsValid()) ||
                    (this.HasHavingKey() && this.HavingKey.IsValid())
                   ) &&
                   !string.IsNullOrWhiteSpace(this.Operator) &&
                    GetFilterOperator().Contains(this.Operator);
        }
        /// <summary>
        /// Làm cho các thông tin của đối tượng trở nên hợp thức hóa theo kiểu No-Bug (không đảm bảo nghiệp vụ).
        /// </summary>
        public override void DoValid()
        {
            if (!this.HasWhereKey()) this.WhereKey = new InqFieldInfoMDX();
            this.WhereKey.DoValid();
            if (!this.HasHavingKey()) this.HavingKey = new InqSummaryInfoMDX();
            this.HavingKey.DoValid();
            this.Operator = GetFilterOperator().First();
        }
        public override bool Equals(object obj)
        {
            var myObj = obj as InqFilterInfoMDX;
            if (myObj == null) return false;

            return
                (Lib.IsNOE(this.Value) || Lib.IsNOE(myObj.Value) || this.Value.Equals(myObj.Value)) &&
                (string.IsNullOrEmpty(this.Operator) || string.IsNullOrEmpty(myObj.Operator) || this.Operator.Equals(myObj.Operator)) &&
                (string.IsNullOrEmpty(this.Logic) || string.IsNullOrEmpty(myObj.Logic) || this.Logic.Equals(myObj.Logic)) &&
                (!this.HasHavingKey() || !myObj.HasHavingKey() || this.HavingKey.Equals(myObj.HavingKey)) &&
                (!this.HasWhereKey() || !myObj.HasWhereKey() || this.WhereKey.Equals(myObj.WhereKey));
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin của đối tượng Top trong truy vấn MDX.
    /// </summary>
    public class InqTopMDX : InqMDX
    {
        public int TopCount { get; set; }
        public string TopMeasureField { get; set; }
        public bool IsUp { get; set; }                
        public InqTopMDX(int topCount, string topMeasureField, bool isUp)
        {
            this.TopCount = topCount;
            this.TopMeasureField = topMeasureField;
            this.IsUp = isUp;
        }
        public InqTopMDX(int topCount, string topMeasureField) : this(topCount, topMeasureField, true) { }
        public InqTopMDX() : this(0, null, true) { }
        public override bool IsValid()
        {
            return this.TopCount > 0 && !string.IsNullOrEmpty(this.TopMeasureField);
        }
    }
    /// <summary>
    /// Định nghĩa cấu trúc truy vấn có thể build thành câu lệnh SQL, và có thể lưu lại ở dạng chuỗi Json
    /// </summary>
    public class InqDefineSourceMDX : InqMDX
    {
        private List<InqFieldInfoMDX> _Fields;
        private List<InqSummaryInfoMDX> _Summaries;
        private List<InqFilterInfoMDX> _Filters;
        private List<COMCatCodeNameObj> _CalcMembers;

        /// <summary>
        /// Thiết lập cho tên của các Dimension sẽ bắt đầu với một Preffix
        /// </summary>
        public string PreffixDimTable { get; set; }
        /// <summary>
        /// Tên của CUBE (Khối dữ liệu lập phương đa chiều được build từ SSAS)
        /// </summary>
        public string OlapCubeName { get; set; }
        /// <summary>
        /// Thông tin giới hạn về số dòng dữ liệu trả về từ truy vấn MDX
        /// </summary>
        public InqTopMDX Top { get; set; }
        /// <summary>
        /// Danh sách các thông tin cần lấy của mệnh đề 'SELECT'
        /// </summary>
        public List<InqFieldInfoMDX> Fields
        {
            get
            {
                if (this._Fields == null) this._Fields = new List<InqFieldInfoMDX>();
                return this._Fields;
            }
            set { this._Fields = value; }
        }
        /// <summary>
        /// Danh sách các thông tin thống kê của mệnh đề 'SELECT'
        /// </summary>
        public List<InqSummaryInfoMDX> Summaries
        {
            get
            {
                if (this._Summaries == null) this._Summaries = new List<InqSummaryInfoMDX>();
                return this._Summaries;
            }
            set { this._Summaries = value; }
        }
        /// <summary>
        /// Danh sách các thông tin của mệnh đề 'WHERE' hoặc 'HAVING'
        /// </summary>
        public List<InqFilterInfoMDX> Filters
        {
            get
            {
                if (this._Filters == null) this._Filters = new List<InqFilterInfoMDX>();
                return this._Filters;
            }
            set { this._Filters = value; }
        }
        /// <summary>
        /// Danh sách thông tin của các field tính toán sẽ được định nghĩa từ KPI.
        /// <para>
        /// Cat:DisplayName, Code:MemberAlias of MDX(is KeyField), Name:Expression after MemberAlias MDX: is a string format include a argument is a 'dependSet' summary for  if it include 'Aggregator' (VD: abc{0}def...).
        /// </para>
        /// </summary>
        public List<COMCatCodeNameObj> CalcMembers
        {
            get
            {
                if (this._CalcMembers == null) this._CalcMembers = new List<COMCatCodeNameObj>();
                return this._CalcMembers;
            }
            set { this._CalcMembers = value; }
        }
        /// <summary>
        /// Chuỗi chứa một tập các giá trị tường minh được bọc trong cặp { ... } cho mục đích filter ờ mệnh đề WHERE của MDX.
        /// <para>
        /// Nếu các giá trị nằm trên nhiều Hierarchy khác nhau thì sẽ được cách nhau bỡi dấu phẩy như VD bên dưới.
        /// </para>
        /// <para>
        /// VD: {[ARDimItem].[Period].&[201001], [ARDimItem].[Period].&[201011]},{[ARDimItem].[ItemCode].&[Computer], [ARDimItem].[ItemCode].&[Laptop]}
        /// </para>
        /// </summary>
        public string ExplicitSetFilterStr { get; set; }

        public InqDefineSourceMDX()
        {            
            this.Fields = new List<InqFieldInfoMDX>();
            this.Filters = new List<InqFilterInfoMDX>();
            this.Summaries = new List<InqSummaryInfoMDX>();
            this.CalcMembers = new List<COMCatCodeNameObj>();
        }
        public InqDefineSourceMDX(string name) : this() { }
        public InqDefineSourceMDX(List<InqFieldInfoMDX> fields, List<InqSummaryInfoMDX> summaries, List<InqFilterInfoMDX> filters)
        {
            this.Fields = fields;
            this.Summaries = summaries;
            this.Filters = filters;
        }

        /// <summary>
        /// Đặt lại ID của các phần tử trong tập Summaries một cách tăng dần để đảm bảo rằng các summary field không bị trùng khi Render ra truy vấn MDX.
        /// </summary>
        public virtual void Reset_SummariesID()
        {
            var id = 1;
            foreach (var f in this.Summaries)
                f.ID = id++;
        }
        /// <summary>
        /// Copy cả đối tượng (List of InqSummaryInfoMDX) ra một đối tượng mới độc lập với đối tượng cữ, nhằm sử dụng cho các mục đích khác nếu cần.
        /// </summary>
        public List<InqSummaryInfoMDX> Copy_Summaries()
        {
            var ret = this.Summaries.Select(p => p.Copy()).ToList();
            return ret;
        }

        /// <summary>
        /// Lọc lại danh sách field từ tập Fields có sẵn của đối tượng hiện hành.
        /// </summary>
        /// <param name="predicate">Điều kiện: hợp lệ trả về true thì việc lọc mới có hiệu lực.</param>
        public void ReFilter_Fields(Func<InqFieldInfoMDX, bool> predicate)
        {
            if (this.HasFields())
                this.ReFilter_Fields(this.Fields.Where(predicate));
        }
        /// <summary>
        /// Lọc lại danh sách field từ tập Fields có sẵn của đối tượng hiện hành.
        /// </summary>
        /// <param name="src">Tập các fields sẽ dùng làm giới hạn cho tập Fields có sẵn của đối tượng.</param>
        public void ReFilter_Fields(IEnumerable<InqFieldInfoMDX> src)
        {
            this.Fields = src.ToList();
            GC.Collect();
        }

        /// <summary>
        /// Lọc lại danh sách summaries từ tập Summaries có sẵn của đối tượng hiện hành.
        /// </summary>
        /// <param name="predicate">Điều kiện: hợp lệ trả về true thì việc lọc mới có hiệu lực.</param>
        public void ReFilter_Summaries(Func<InqSummaryInfoMDX, bool> predicate)
        {
            if (this.HasSummaries())
                this.ReFilter_Summaries(this.Summaries.Where(predicate));
        }
        /// <summary>
        /// Lọc lại danh sách summaries từ tập Summaries có sẵn của đối tượng hiện hành.
        /// </summary>
        /// <param name="src">Tập các summaries sẽ dùng làm giới hạn cho tập Summaries có sẵn của đối tượng.</param>
        public void ReFilter_Summaries(IEnumerable<InqSummaryInfoMDX> src)
        {
            this.Summaries = src.ToList();
            GC.Collect();
        }

        /// <summary>
        /// Lọc lại danh sách calcMembers từ tập CalcMembers có sẵn của đối tượng hiện hành.
        /// </summary>
        /// <param name="predicate">Điều kiện: hợp lệ trả về true thì việc lọc mới có hiệu lực.</param>
        public void ReFilter_CalcMembers(Func<COMCatCodeNameObj, bool> predicate)
        {
            if (this.HasCalcMembers())
                this.ReFilter_CalcMembers(this.CalcMembers.Where(predicate));
        }
        /// <summary>
        /// Lọc lại danh sách calcMembers từ tập CalcMembers có sẵn của đối tượng hiện hành.
        /// </summary>
        /// <param name="src">Tập các calcMembers sẽ dùng làm giới hạn cho tập CalcMembers có sẵn của đối tượng.</param>
        public void ReFilter_CalcMembers(IEnumerable<COMCatCodeNameObj> src)
        {
            this.CalcMembers = src.ToList();
            GC.Collect();
        }

        /// <summary>
        /// Chuyển về chuỗi json từ đối tượng hiện hành
        /// </summary>
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        /// <summary>
        /// Chuyển trở lại đối tượng từ chuỗi lưu trữ dạng json
        /// </summary>
        public static InqDefineSourceMDX FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<InqDefineSourceMDX>(jsonStr);
        }
        /// <summary>
        /// Kiểm tra có thiết lập giới hạn Top do người dùng định nghĩa hay không.
        /// </summary>
        public bool HasTop()
        {
            return this.Top != null && this.Top.IsValid();
        }
        public bool HasFields()
        {
            return this.Fields != null && this.Fields.Count > 0;
        }
        public bool HasSummaries()
        {
            return this.Summaries != null && this.Summaries.Count > 0;
        }
        public bool HasFilters()
        {
            return this.Filters != null && this.Filters.Count > 0;
        }
        public bool HasCalcMembers()
        {
            return this.CalcMembers != null && this.CalcMembers.Count > 0;
        }
        public void SetTop(int topCount, string topMeasureField, bool isUp)
        {
            this.Top = new InqTopMDX(topCount, topMeasureField, isUp);
        }
        public void SetTop(int topCount, string topMeasureField)
        {
            this.Top = new InqTopMDX(topCount, topMeasureField);
        }

        /// <summary>
        /// Trả về danh sách các 'Fields' theo cấu trúc đối tượng: 'COM3ProperiesObj'
        /// <para>
        /// Prop1:FieldName, Prop2:KeyField, Prop3:DisplayName
        /// </para>
        /// </summary>
        public List<COM3ProperiesObj> GetFields_KeyFieldAndDisplayName()
        {
            if (this.HasFields())
                return this.Fields
                    .Select(p => new COM3ProperiesObj(p.ColName, p.ColName, p.ColAliasVI)).ToList();
            return new List<COM3ProperiesObj>();
        }
        /// <summary>
        /// Trả về danh sách các 'Summaries' theo cấu trúc đối tượng: 'COM3ProperiesObj'
        /// <para>
        /// Prop1:FieldName, Prop2:KeyField, Prop3:DisplayName, Prop4:Func
        /// </para>
        /// </summary>
        public List<COM4ProperiesObj> GetSummaries_KeyFieldAndDisplayName()
        {
            if (this.HasSummaries())
                return this.Summaries
                    .Select(p => new COM4ProperiesObj(p.Field.ColName, p.Get_SummaryKeyField(), p.FieldAlias, p.FuncName)).ToList();
            return new List<COM4ProperiesObj>();
        }
        /// <summary>
        /// Trả về danh sách các Calculate member theo cấu trúc đối tượng: 'COM3ProperiesObj'
        /// <para>
        /// Prop1:FieldName, Prop2:KeyField, Prop3:DisplayName, Prop4:Func
        /// </para>
        /// </summary>
        public List<COM4ProperiesObj> GetCalcMembers_KeyFieldAndDisplayName()
        {
            if (this.HasCalcMembers())
                return this.CalcMembers
                    .Select(p => new COM4ProperiesObj(p.Code, p.Code, p.Cat, "SUM")).ToList();
            return new List<COM4ProperiesObj>();
        }

        public void AddField(string tblName, string colName, int level)
        {
            //if (!string.IsNullOrEmpty(this.PreffixDimTable) && !tblName.StartsWith(this.PreffixDimTable))
            //    this.AddField(string.Format("{0}{1}", this.PreffixDimTable, tblName), colName, "NTEXT", hierarchyLevel);
            //else
                this.AddField(tblName, colName, "NTEXT", level);
        }
        public void AddField(string tblName, string colName)
        {
            this.AddField(tblName, colName, 0);
        }
        public void AddField(string tblName, string colName, string dataType, int level)
        {
            //if (!string.IsNullOrEmpty(this.PreffixDimTable) && !tblName.StartsWith(this.PreffixDimTable))
            //    this.AddField(new InqFieldInfoMDX(string.Format("{0}{1}", this.PreffixDimTable, tblName), colName, dataType, hierarchyLevel));
            //else
                this.AddField(new InqFieldInfoMDX(tblName, colName, dataType, level));
        }
        public void AddField(string tblName, string colName, string dataType)
        {
            this.AddField(tblName, colName, dataType, 0);
        }
        public void AddField(InqFieldInfoMDX info)
        {
            if (this.Fields == null) this.Fields = new List<InqFieldInfoMDX>();
            //if (!string.IsNullOrEmpty(this.PreffixDimTable) && !info.TblName.StartsWith(this.PreffixDimTable))
            //    info.TblName = string.Format("{0}{1}", this.PreffixDimTable, info.TblName);
            this.Fields.Add(info);
        }

        public void AddSummary(string tblName, string colName, string funcName, string alias)
        {
            this.AddSummary(new InqSummaryInfoMDX(new InqFieldInfoMDX(tblName, colName), funcName, alias));
        }
        public void AddSummary(string tblName, string colName, string funcName)
        {
            this.AddSummary(new InqSummaryInfoMDX(new InqFieldInfoMDX(tblName, colName), funcName, colName));
        }
        public void AddSummary(InqFieldInfoMDX field, string funcName, string alias)
        {
            this.AddSummary(new InqSummaryInfoMDX(field, funcName, alias));
        }
        public void AddSummary(InqFieldInfoMDX field, string funcName)
        {
            this.AddSummary(new InqSummaryInfoMDX(field, funcName));
        }
        public void AddSummary(InqSummaryInfoMDX info)
        {
            if (this.Summaries == null) this.Summaries = new List<InqSummaryInfoMDX>();
            this.Summaries.Add(info);
        }

        public void AddFilter(InqFieldInfoMDX whereKey, string opt, object value)
        {
            //if (!string.IsNullOrEmpty(this.PreffixDimTable) && !whereKey.TblName.StartsWith(this.PreffixDimTable))
            //    whereKey.TblName = string.Format("{0}{1}", this.PreffixDimTable, whereKey.TblName);
            this.AddFilter(new InqFilterInfoMDX(whereKey, opt, value));
        }
        public void AddFilter(InqSummaryInfoMDX havingKey, string opt, object value)
        {
            //if (!string.IsNullOrEmpty(this.PreffixDimTable) && !havingKey.Field.TblName.StartsWith(this.PreffixDimTable))
            //    havingKey.Field.TblName = string.Format("{0}{1}", this.PreffixDimTable, havingKey.Field.TblName);
            this.AddFilter(new InqFilterInfoMDX(havingKey, opt, value));
        }
        public void AddFilter(InqFilterInfoMDX info)
        {
            if (this.Filters == null) this.Filters = new List<InqFilterInfoMDX>();
            //if (!string.IsNullOrEmpty(this.PreffixDimTable))
            //{
            //    if (info.HasWhereKey() && !info.WhereKey.TblName.StartsWith(this.PreffixDimTable))
            //        info.WhereKey.TblName = string.Format("{0}{1}", this.PreffixDimTable, info.WhereKey.TblName);
            //    else if (info.HasHavingKey() && !info.HavingKey.Field.TblName.StartsWith(this.PreffixDimTable))
            //        info.HavingKey.Field.TblName = string.Format("{0}{1}", this.PreffixDimTable, info.HavingKey.Field.TblName);
            //}
            this.Filters.Add(info);
        }

        public virtual void AutoValidAllBeforeBuildToMDX()
        {
            if (!string.IsNullOrEmpty(this.PreffixDimTable))
            {
                foreach (var f in this.Fields)
                {
                    if (f.TblName.StartsWith(this.PreffixDimTable)) continue;
                    f.TblName = this.PreffixDimTable + f.TblName;
                }
                foreach (var f in this.Filters)
                {
                    if (f.HasWhereKey() && !f.WhereKey.TblName.StartsWith(this.PreffixDimTable))
                        f.WhereKey.TblName = this.PreffixDimTable + f.WhereKey.TblName;
                    else if (f.HasHavingKey() && !f.HavingKey.Field.TblName.StartsWith(this.PreffixDimTable))
                        f.HavingKey.Field.TblName = this.PreffixDimTable + f.HavingKey.Field.TblName;
                }
                foreach (var f in this.Summaries.Where(p=>p.HasInnerFilters()))
                {
                    foreach (var f1 in f.InnerFilters)
                    {
                        if (f1.HasWhereKey() && !f1.WhereKey.TblName.StartsWith(this.PreffixDimTable))
                            f1.WhereKey.TblName = this.PreffixDimTable + f1.WhereKey.TblName;
                        else if (f1.HasHavingKey() && !f1.HavingKey.Field.TblName.StartsWith(this.PreffixDimTable))
                            f1.HavingKey.Field.TblName = this.PreffixDimTable + f1.HavingKey.Field.TblName;
                    }
                }
            }
            //
            foreach (var obj in this.Summaries)
            {
                if (obj.Field == null) continue;
                obj.Field.DataType = "NUM";
            }
        }
        //public virtual List<lsttbl_DWTable> GetTblOnJoin()
        //{
        //    // Lấy ra danh sách các bảng dữ liệu tham gia
        //    var tbls = new List<lsttbl_DWTable>();
        //    foreach (var tbl in this.Fields.Select(p => p.TblName).Distinct())
        //    {
        //        if (!tbls.Exists(p => p.TblName == tbl))
        //        {
        //            var mytbl = MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == tbl);
        //            tbls.Add(mytbl);
        //        }
        //    }
        //    foreach (var tbl in this.Summaries.Select(p => p.Field.TblName).Distinct())
        //    {
        //        if (!tbls.Exists(p => p.TblName == tbl))
        //        {
        //            var mytbl = MyBI.Me.Get_DWTable().FirstOrDefault(p => p.TblName == tbl);
        //            tbls.Add(mytbl);
        //        }
        //    }
        //    return tbls;
        //}

        /// <summary>
        /// Chuyển tập hợp đối tượng 'InqSummaryInfoMDX' thành tập hợp các biểu thức tính toán trên MDX.
        /// </summary>
        /// <param name="dependFieldSELECT">[Tên bảng].[Tên cột] hoặc Alias của Member, mà việc Summary phải phụ thuộc lên nó.</param>
        public virtual List<COMCatCodeNameObj> GetSummariesMDX(string dependFieldSELECT)
        {            
            var ret = new List<COMCatCodeNameObj>();
            try
            {
                var dependSetStr = dependFieldSELECT;
                foreach (var f in this.Summaries)
                {
                    var calcExpressionStr = "";
                    if (f.HasInnerFilters())
                        calcExpressionStr = f.ToMDX();
                    else
                        calcExpressionStr = f.ToMDX(dependSetStr);
                    var cat = string.Format("[Measures].[{0}]", f.Get_SummaryKeyField());
                    ret.Add(new COMCatCodeNameObj(cat, f.Field.ColName, calcExpressionStr));
                }
            }
            catch { }
            return ret;
        }
        /// <summary>
        /// Chuyển tập hợp các biểu thức tính toán trên MDX mà có chứa trong tập hợp 'Filters' do người dùng thiết lập về biểu thức HAVING theo MDX.
        /// </summary>
        /// <param name="lstSummariesMDX">tập hợp các biểu thức tính toán trên MDX sau khi được chuyển từ những đối tượng 'InqSummaryInfoMDX'.</param>
        public virtual string GetHAVINGMDXStr(List<COMCatCodeNameObj> lstSummariesMDX)
        {
            var ret = "HAVING ";
            var logicCombine = "";
            try
            {
                foreach (var obj in lstSummariesMDX)
                {
                    // Thêm (bổ sung) ngày: 01/08/2012.
                    // Lấy tất cả các đối tượng filter có fieldName trùng với fieldName trong 'obj'
                    var filterObjs = this.Filters
                        .Where(p => p.HasHavingKey() && p.HavingKey.Field.ColName == obj.Code);
                    foreach (var filterObj in filterObjs)
                    {
                        if (filterObj == null) continue;
                        logicCombine = string.Format(" {0} ", filterObj.Logic);
                        ret = ret + filterObj.ToMDX(obj.Cat) + logicCombine;
                    }
                }
                if (ret.EndsWith(logicCombine))
                    ret = ret.Remove(ret.Length - logicCombine.Length);
            }
            catch { }
            return ret == "HAVING " ? "" : ret;
        }

        /// <summary>
        /// Chuyển các đối tượng do người dùng thiết lập về câu lệnh MDX để thực thi
        /// </summary>
        public virtual string ToMDX(bool isWrapText)
        {
            var wrapLine = isWrapText ? Environment.NewLine : "";
            var wrapTab = isWrapText ? "\t" : "";
            var lineAndTab = wrapLine + wrapTab;
            var separa = ", ";
            var sqlStr = "";
            var withMemberStr = "WITH ";
            var selectStr = "SELECT ";
            var fromStr = " FROM " + this.OlapCubeName;
            string whereStr = "", onROWSStr = "", onCOLUMNSStr = "";
            try
            {
                ////////////////////////////////////////////// Auto correct other informations...
                this.AutoValidAllBeforeBuildToMDX();
                this.Reset_SummariesID();

                //////////////////////////////////////////////SELECT... (Bắt buộc)
                //// Trục: ON ROWS
                // Lấy ra những field từ tập Filters mà nó tồn tại trong tập Fields SELECT để đưa lên cho SELECT.
                var lstFilterInnerSELECT = this.Filters
                    .Where(p => p.HasWhereKey() &&
                                this.Fields.FirstOrDefault(q => q.ColName == p.WhereKey.ColName
                                                          ) != null
                          ).ToList();
                // Lấy ra những field từ tập Filters mà nó không tồn tại trong tập Fields SELECT để đưa xuống WHERE.
                var lstFilterOuterSELECT = this.Filters
                    .Where(p => p.HasWhereKey() &&
                                this.Fields.FirstOrDefault(q => q.ColName == p.WhereKey.ColName
                                                          ) == null
                          ).ToList();

                // Ghi các member là InnerFilter bên trong tập 'Summaries' vào chuỗi withMemberStr
                var lstWithMemberStr_InnerSummaries = this.Summaries
                    .Where(p => p.HasInnerFilters())
                    .Select(p => p.GetSyntaxMDX_MemberStr(lineAndTab))
                    .ToList();
                foreach (var memberStr in lstWithMemberStr_InnerSummaries)
                    withMemberStr = withMemberStr + lineAndTab + memberStr;

                // Tiếp tục ghi thêm các member là InnerSELECT bên trong từng phần tử của tập 'lstFilterInnerSELECT'
                // vào chuỗi withMemberStr và danh sách CrossJoin.
                var lstFilterParse = InqFilterInfoMDX.GetSyntaxMDX(lstFilterInnerSELECT, "");
                var lstCROSSJOIN = new List<COMCodeNameObj>();
                foreach (var filterParse in lstFilterParse)
                {
                    withMemberStr = withMemberStr + lineAndTab +
                        string.Format("SET {0} AS {1}", filterParse.Prop1, filterParse.Prop4);
                    var f = this.Fields.First(p => p.ColName == filterParse.Prop3);
                    lstCROSSJOIN.Add(new COMCodeNameObj(f.Level.ToString(), f.ToMDX(filterParse.Prop1)));
                }
                // Lấy ra những field từ tập Fields SELECT mà nó không tồn tại trong tập lstFilterInnerSELECT
                // để đưa vào danh sách CrossJoin.
                var lstNormalSELECT = this.Fields
                    .Where(p => lstFilterInnerSELECT.FirstOrDefault(q => q.WhereKey.ColName == p.ColName
                                                          ) == null
                          ).ToList();
                foreach (var f in lstNormalSELECT)
                    lstCROSSJOIN.Add(new COMCodeNameObj(f.Level.ToString(), f.ToMDX()));
                // CROSSJOIN các dimension trên ROW theo thứ tự 'HierarchyLevel' do người dùng thiết lập.
                // Kết các phần tử trong danh sách CrossJoin lại thành 1 chuỗi mà nó sẽ đặt trên trục ROWS của truy vấn MDX
                foreach (var onRow in lstCROSSJOIN.OrderBy(p => p.Code))
                    onROWSStr = onROWSStr + lineAndTab + onRow.Name + "*";
                if (onROWSStr.EndsWith("*"))
                    onROWSStr = onROWSStr.Remove(onROWSStr.Length - 1);

                //////////////////// ....????????? đang xem xét vì hiện tại vẫn chưa ok lắm.
                ////Trục: ON COLUMNS
                // Lấy ra phần tử cuối cùng trong tập lstNormalSELECT (các field thông thường, ko phải chế biến lại khi Render MDX)
                // để làm dependSet cho biểu thức tính toán trên MDX ở trục COLUMNS.
                var lastElemOnRow = "";
                var tmp = lstNormalSELECT.LastOrDefault();
                if (tmp != null)
                    lastElemOnRow = string.Format("[{0}].[{1}]", tmp.TblName, tmp.ColName);
                else
                {
                    var tmp1 = lstFilterParse.LastOrDefault();
                    if (tmp1 != null)
                        lastElemOnRow = string.Format("[{0}].[{1}]", tmp1.Prop2, tmp1.Prop3);
                }

                // Đưa các field tính toán vào WITH MEMBER và trục COLUMNS của truy vấn MDX (nếu có)
                if (this.HasCalcMembers())
                {
                    foreach (var f in this.CalcMembers)
                    {
                        // Đưa dependSet 'lastElemOnRow' vào từng Expresstion của biểu thức tính toán trên trục COLUMNS
                        var expressionStr = string.Format(f.Name, lastElemOnRow);
                        withMemberStr = withMemberStr + lineAndTab +
                            string.Format("MEMBER {0} AS {1}", f.Code, expressionStr);
                        onCOLUMNSStr = onCOLUMNSStr + lineAndTab + f.Code + separa;
                    }
                }
                var hAVINGMDXStr = "";
                if (this.HasSummaries())
                {
                    // Lấy danh sách các Expresstion từ tập Summaries với dependSet để sử dụng trên trục COLUMNS.
                    var lstSummariesMDX = this.GetSummariesMDX(lastElemOnRow);
                    hAVINGMDXStr = this.GetHAVINGMDXStr(lstSummariesMDX);
                    foreach (var sumParse in lstSummariesMDX)
                    {
                        withMemberStr = withMemberStr + lineAndTab + 
                            string.Format("MEMBER {0} AS {1}", sumParse.Cat, sumParse.Name);
                        onCOLUMNSStr = onCOLUMNSStr + lineAndTab + sumParse.Cat + separa;
                    }
                    // Nếu 'Measure' có thiết lập thuộc tính 'OrderName' mà được tìm thấy đầu tiên
                    // thì gói 'onROWSStr' vào hàm 'ORDER' như sao: ORDER({onROWSStr},Measure,BDESC)...
                    var firstMeasureOrder = this.Summaries.FirstOrDefault(p => p.Field.HasOrder());
                    if (firstMeasureOrder != null)
                    {
                        var aliasMeasureOrder = lstSummariesMDX.FirstOrDefault(p => p.Code == firstMeasureOrder.Field.ColName);
                        onROWSStr = lineAndTab + "ORDER" + lineAndTab +
                            "({" + onROWSStr + lineAndTab + "}" +
                            string.Format(", {0}, B{1})", aliasMeasureOrder.Cat, firstMeasureOrder.Field.OrderName);
                    }
                    // Nếu thuộc tính 'Top' được thiết lập
                    // thì gói 'onROWSStr' vào hàm 'TOPCOUNT' như sao: TOPCOUNT({onROWSStr},10,Measure)...
                    if (this.HasTop())
                    {
                        var aliasMeasureTop = lstSummariesMDX.FirstOrDefault(p => p.Code == this.Top.TopMeasureField);
                        onROWSStr = lineAndTab + "TOPCOUNT" + lineAndTab +
                            "(" + onROWSStr + lineAndTab +
                            string.Format(", {0}, {1})", this.Top.TopCount, aliasMeasureTop.Cat);
                    }
                }
                if (onCOLUMNSStr.EndsWith(separa))
                    onCOLUMNSStr = onCOLUMNSStr.Remove(onCOLUMNSStr.Length - separa.Length);

                onROWSStr = "{" + onROWSStr + wrapLine + "} " + hAVINGMDXStr + wrapLine + "ON ROWS";
                onCOLUMNSStr = "{" + onCOLUMNSStr + wrapLine + "} ON COLUMNS";
                selectStr = selectStr + wrapLine + onROWSStr + "," + wrapLine + wrapLine + onCOLUMNSStr;

                ////////////////////////////////////////////////WHERE... (Nếu có)
                // Filter MDX cho mệnh đề WHERE sẽ có 2 kiểu:
                // 1. Kiểu: Kẹp các điều kiện để giới hạn tập giá trị trả về.
                // 2. Kiểu: Tập các giá trị tường minh (chỉ việc liệt kê các giá trị muốn filter và giữa chúng cách nhau bỡi dấu phẩy).
                //   Nếu các giá trị nằm trong cùng một HIERARCHIZE của [Cube] thì phải được gói trong cặp dấu: { ... }.
                //   Nếu các giá trị nằm ở nhiều HIERARCHIZE khác nhau của [Cube] thì phải có nhiều { ... }, { ... } mà nó dc cách nhau bỡi dấu phẩy.
                var lstFilterOuter = InqFilterInfoMDX.GetSyntaxMDX(lstFilterOuterSELECT, "");
                if (lstFilterOuter.Count > 0 || !string.IsNullOrEmpty(this.ExplicitSetFilterStr))
                {                    
                    whereStr = "WHERE " + wrapLine + "(";
                    if (lstFilterOuter.Count > 0)
                    {
                        foreach (var filterOuter in lstFilterOuter)
                            whereStr = whereStr + lineAndTab + "{" + filterOuter.Prop4 + "}" + separa;
                    }
                    if (!string.IsNullOrEmpty(this.ExplicitSetFilterStr))
                        whereStr = whereStr + lineAndTab + this.ExplicitSetFilterStr;

                    if (whereStr.EndsWith(separa))
                        whereStr = whereStr.Remove(whereStr.Length - separa.Length);
                    whereStr = whereStr + wrapLine + ")";
                }
                sqlStr = withMemberStr + wrapLine + selectStr + wrapLine + fromStr + wrapLine + whereStr;
            }
            catch { }
            return sqlStr;
        }
        /// <summary>
        /// Đưa đối tượng về thành câu lệnh Sql để thực thi
        /// <para>isWrapText=false</para>
        /// </summary>
        public override string ToMDX()
        {
            return this.ToMDX(false);
        }
        /// <summary>
        /// Kiểm tra đối tượng chứa các thông tin có hợp lệ để đảm bảo tính dúng đắng logic
        /// </summary>
        public override bool IsValid()
        {
            return
                this.Fields != null && this.Fields.Count > 0 &&
                this.Fields.Count(p => !p.IsValid()) == 0 &&
               (this.Filters == null || !this.Filters.Exists(p => !p.IsValid())) &&
               (this.Summaries == null || !this.Summaries.Exists(p => !p.IsValid()));
        }
    }
    #endregion

    #region KPIs sẽ sử dụng Datasource làm model...
    /// <summary>
    /// Define object model for the CalcField.
    /// </summary>
    public class CalcField
    {
        public string Types { get; set; }
        /// <summary>
        /// The name for identity calc field (Unique name not duplicate).
        /// <para>
        /// Note: The name can be used by Member1 or Member2 if it is defined on the previous KPICalcField.
        /// </para>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Member number 1 (Field or Number value).
        /// </summary>
        public string Member1 { get; set; }
        /// <summary>
        /// Operator for calculation between Member1 and Member2.
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// Member number 2 (Field or Number value).
        /// </summary>
        public string Member2 { get; set; }
        public int Order { get; set; }
        /// <summary>
        /// Putting objects into calculation expression string : (Member1 Operator  Member2).
        /// <para>
        /// Exp : (Amount + VAT) (or) Amount + VAT
        /// </para>
        /// </summary>
        public string To_Expression(bool hasConver_bracket)
        {
            var myMember1 = this.Member1;
            var myMember2 = this.Member2;
            if (this.Types == CalcFieldCtrlBase.Types.FieldAndField.ToString())
            {
                if (!this.Member1.Contains("[Measures]."))
                    myMember1 = string.Format("[Measures].[{0}]", this.Member1);
                if (!this.Member2.Contains("[Measures]."))
                    myMember2 = string.Format("[Measures].[{0}]", this.Member2);
            }
            else if (this.Types == CalcFieldCtrlBase.Types.FieldAndNum.ToString())
            {
                if (!this.Member1.Contains("[Measures]."))
                    myMember1 = string.Format("[Measures].[{0}]", this.Member1);
            }
            else if (this.Types == CalcFieldCtrlBase.Types.NumAndField.ToString())
            {
                if (!this.Member2.Contains("[Measures]."))
                    myMember2 = string.Format("[Measures].[{0}]", this.Member2);            
            }

            if (this.Operator == "%")
            {
                if (hasConver_bracket)
                    return string.Format("(({0}/{1}) * 100)", myMember1, myMember2);
                return string.Format("({0}/{1}) * 100", myMember1, myMember2);
            }
            if (hasConver_bracket)
                return string.Format("({0} {1} {2})", myMember1, this.Operator, myMember2);
            return string.Format("{0} {1} {2}", myMember1, this.Operator, myMember2);            
        }
        /// <summary>
        /// Putting objects into calculation expression string : (Member1 Operator  Member2). By default not Conver_bracket
        /// <para>
        /// Exp : Amount + VAT
        /// </para>
        /// </summary>
        public string To_Expression()
        {
            return this.To_Expression(false);
        }
        /// <summary>
        /// Copy current object into new object.
        /// </summary>
        public CalcField Copy()
        {
            var ret = new CalcField
            {
                Member1 = this.Member1,
                Member2 = this.Member2,
                Name = this.Name,
                Operator = this.Operator,
                Order = this.Order
            };
            return ret;
        }
        /// <summary>
        /// Danh sách các toán tử tính toán của KPICalcField
        /// </summary>
        public static string[] GetCalcOperator()
        {
            return new string[] { "+", "-", "*", "/", "%" };
        }
    }
    /// <summary>
    /// Define object model for the Collection of CalcField.
    /// </summary>
    public class CalcFieldCollection : List<CalcField>
    {
        public CalcFieldCollection() { }
        public CalcFieldCollection(IEnumerable<CalcField> collection)
        {
            this.AddRange(collection);
        }
        public List<CalcField> Copy()
        {
            var ret = this.Select(p => p.Copy()).ToList();
            GC.Collect();
            return ret;
        }
        /// <summary>
        /// Build this Collection become to calculation expression string.
        /// </summary>
        public string To_Expression()
        {
            var ret = "";
            var lst = this.Copy().OrderBy(p => p.Order).ToList();
            var count = lst.Count;
            if (count > 1)
            {
                for (int i = 1; i < count; i++)
                {
                    var f = lst[i];
                    // Find object to replace Member1.
                    for (int j = i - 1; j >= 0; j--)
                    {
                        var fj = lst[j];
                        if (f.Member1 == fj.Name)
                        {
                            f.Member1 = fj.To_Expression(true);
                            break;
                        }
                    }
                    // Find object to replace Member2.
                    for (int j = i - 1; j >= 0; j--)
                    {
                        var fj = lst[j];
                        if (f.Member2 == fj.Name)
                        {
                            f.Member2 = fj.To_Expression(true);
                            break;
                        }
                    }
                }
                // Lấy chuỗi biểu thức của phần tử cuối cùng.
                ret = lst.Last().To_Expression();
            }
            else if (count == 1) ret = lst.First().To_Expression();
            return ret;
        }
    }

    public abstract class KPIField
    {
        /// <summary>
        /// Tên trường dữ liệu.
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Tên hiển thị thân thiện cho trường dữ liệu khi nó được biểu diễn lên Control.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Hàm kiểm tra các thuộc tính trong Dimension có hợp lệ hay không.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid() { return !string.IsNullOrEmpty(this.FieldName); }   
    }
    /// <summary>
    /// Định nghĩa các thông tin của đối tượng KPIDimension
    /// </summary>
    public class KPIDimension : KPIField { }
    /// <summary>
    /// Định nghĩa thông tin của đối tượng Measure.
    /// </summary>
    public class KPIMeasure : KPIDimension
    {
        /// <summary>
        /// Tên hàm tính toán dạng (thống kê) theo cú pháp của MDX.
        /// </summary>
        public string Aggregator { get; set; }
    }
    /// <summary>
    /// Định nghĩa các thông tin cùa một ContextMetric được dùng để so sánh với Measure.
    /// </summary>
    public class KPICtxtMetric : KPIMeasure
    {
        /// <summary>
        /// Từ khóa chỉ ra một khoản thời gian trước đó: ([DimTime].[Year], [DimTime].[Quarter], [DimTime].[Period], [DimTime].[Day]).
        /// </summary>
        public string TimeFilterPrev { get; set; }
        /// <summary>
        /// Danh sách các Filter bên trong từng Metric, và nó sẽ được sử dụng để giới hạn một tập hợp mà hàm tính toán trong Measure phải phụ thuộc lên nó.
        /// </summary>
        public List<InqFilterInfoMDX> Filters { get; set; }
        /// <summary>
        /// Danh sách các field tính toán, nếu thuộc tính này sử dụng thì không sử dụng đến thuộc tính:
        /// <para>
        ///     'Filters' và 'TimeFilterPrev'
        /// </para>
        /// </summary>
        public CalcFieldCollection CalcFields { get; set; }
        public bool HasFilters()
        {
            return this.Filters != null && this.Filters.Count > 0;
        }
        public bool HasCalcFields()
        {
            return this.CalcFields != null && this.CalcFields.Count > 0;
        }
        /// <summary>
        /// Return the object consist of 3 elements:
        /// <para>
        /// Cat:DisplayName, Code:MemberAlias of MDX(is KeyField), Name:Expression after MemberAlias MDX: is a string format include a argument is a 'dependSet' summary for  if it include 'Aggregator' (VD: abc{0}def...).
        /// </para>
        /// </summary>
        public COMCatCodeNameObj Get_ExpressionMDX_CalcField()
        {
            if (!this.HasCalcFields()) return null;
            var expressionStr = this.CalcFields.To_Expression();
            if (!string.IsNullOrEmpty(this.Aggregator))
                expressionStr = string.Format("COALESCEEMPTY({0}(", this.Aggregator) +
                                "{0}" +
                                string.Format(",{0}),0)", this.CalcFields.To_Expression());
            return new COMCatCodeNameObj(this.DisplayName, string.Format("{0}{1}", this.FieldName, this.Aggregator), expressionStr);
        }
        public override bool IsValid()
        {
            var ret = base.IsValid();
            foreach (var item in this.Filters)
            {
                ret &= item.IsValid();
                if (!ret) break;
            }
            return ret;
        }
    }
    /// <summary>
    /// Định nghĩa các thông tin của một KPI.
    /// </summary>
    public class KPIDefineSource
    {
        /// <summary>
        /// Tên hiển thị của KPI.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Mã datasource được sử dụng bỡi KPI.
        /// </summary>
        public string DatasourceID { get; set; }
        /// <summary>
        /// Loại control sẽ mặc định cho KPI.
        /// </summary>
        public string CtrlTypeDefault { get; set; }
        /// <summary>
        /// Kiểu hiển thị của control sẽ mặc định cho KPI.
        /// </summary>
        public string VisibleTypeDefault { get; set; }
        /// <summary>
        /// Giá trị nhỏ nhất trong một range của KPI
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// Giá trị lớn nhất trong một range của KPI
        /// </summary>
        public double MaxValue { get; set; }
        public List<KPIDimension> Dimensions { get; set; }
        public List<KPIMeasure> Measures { get; set; }
        public List<KPICtxtMetric> Contexts { get; set; }
        public List<InqFilterInfoMDX> Filters { get; set; }

        public KPIDefineSource()
        {
            this.Dimensions = new List<KPIDimension>();
            this.Measures = new List<KPIMeasure>();
            this.Contexts = new List<KPICtxtMetric>();
            this.Filters = new List<InqFilterInfoMDX>();
        }

        public void AddDimension(KPIField info)
        {
            if (this.Dimensions == null) this.Dimensions = new List<KPIDimension>();
            this.Dimensions.Add(info as KPIDimension);
        }
        public void AddMeasure(KPIField info)
        {
            if (this.Measures == null) this.Measures = new List<KPIMeasure>();
            this.Measures.Add(info as KPIMeasure);
        }
        public void AddContext(KPIField info)
        {
            if (this.Contexts == null) this.Contexts = new List<KPICtxtMetric>();
            this.Contexts.Add(info as KPICtxtMetric);
        }
        public void AddFilter(InqFilterInfoMDX info)
        {
            if (this.Filters == null) this.Filters = new List<InqFilterInfoMDX>();
            this.Filters.Add(info);
        }

        /// <summary>
        /// Lấy danh sách các field mà được dùng để thống kê số liệu.
        /// <para>
        /// Được tổng hợp từ các 'Measures' và 'Contexts'.
        /// </para>
        /// <para>
        /// Danh sách trả về được đóng gói trong đối tượng 'COMCodeNameObj { Code = Tên field, Name = Tên hiển thị}'
        /// </para>
        /// </summary>
        public List<COMCodeNameObj> Get_SummaryFields()
        {
            var ret = this.Measures.Select(p => new COMCodeNameObj(p.FieldName, p.DisplayName)).ToList();
            var ctx_Normals = this.Contexts.Where(p => !p.HasCalcFields())
                .Select(p => new COMCodeNameObj(p.FieldName, p.DisplayName)).ToList();
            if (ctx_Normals != null && ctx_Normals.Count > 0)
                ret.AddRange(ctx_Normals);
            var ctx_Calcs = this.Contexts.Where(p => p.HasCalcFields())
                .Select(p => p.Get_ExpressionMDX_CalcField()).ToList();
            if (ctx_Calcs != null && ctx_Calcs.Count > 0)
            {
                foreach (var item in ctx_Calcs)
                    ret.Add(new COMCodeNameObj(item.Code, item.Cat));
            }
            return ret;
        }
        /// <summary>
        /// Chuyển về chuỗi json từ đối tượng hiện hành
        /// </summary>
        public string ToJsonStr()
        {
            return Lib.ToJSonStr(this);
        }
        /// <summary>
        /// Chuyển trở lại đối tượng từ chuỗi lưu trữ dạng json
        /// </summary>
        public static KPIDefineSource FromJsonStr(string jsonStr)
        {
            return Lib.FromJsonStr<KPIDefineSource>(jsonStr);
        }
        /// <summary>
        /// Chỉ ra thuộc tính MinValue được tick vào NoMinValue, và tương đương với giá trị = 0
        /// </summary>
        public bool NoMinValue()
        {
            return this.MinValue == 0.0;
        }
        /// <summary>
        /// Chỉ ra thuộc tính MaxValue được tick vào NoMaxValue, và tương đương với giá trị = 0
        /// </summary>
        public bool NoMaxValue()
        {
            return this.MaxValue == 0.0;
        }
        public bool IsValid()
        {
            return true;
        }
        /// <summary>
        /// Lọc lại Datasource(có ID = DatasourceID đã được tạo trước) bởi các thông tin 'Dimensions', 'Measures', 'Contexts'
        /// </summary>
        public InqDefineSourceMDX ReFilter_InqMDX()
        {
            try
            {
                var ret = MyBI.Me.Get_DashboardSourceBy(this.DatasourceID).JsonObjMDX;
                var orgCopySummaries = ret.Copy_Summaries();
                if (this.Dimensions.Count > 0)
                {
                    ret.ReFilter_Fields(p => this.Dimensions.Exists(q => q.FieldName == p.ColName));
                    foreach (var f in ret.Fields)
                    {
                        var f1 = this.Dimensions.FirstOrDefault(p => p.FieldName == f.ColName);
                        f.ColAliasVI = f1.DisplayName;
                        f.ColAliasEN = f1.DisplayName;
                    }
                }
                if (this.Measures.Count > 0)
                {
                    ret.ReFilter_Summaries(p => this.Measures.Exists(q => q.FieldName == p.Field.ColName));
                    foreach (var f in ret.Summaries)
                    {
                        // Measure
                        var f1 = this.Measures.FirstOrDefault(p => p.FieldName == f.Field.ColName);
                        if (f1 != null)
                        {
                            f.FieldAlias = f1.DisplayName;
                            f.FuncName = f1.Aggregator;
                        }
                    }
                }
                foreach (var f in this.Contexts)
                {
                    if (f.HasCalcFields())
                    {
                        ret.CalcMembers.Add(f.Get_ExpressionMDX_CalcField());
                    }
                    else
                    {
                        var newSummaries = orgCopySummaries
                            .FirstOrDefault(p => p.Field.ColName == f.FieldName);
                        if (newSummaries != null)
                        {
                            newSummaries = newSummaries.Copy();
                            newSummaries.FieldAlias = f.DisplayName;
                            newSummaries.FuncName = f.Aggregator;
                            newSummaries.InnerFilters = f.Filters;
                            newSummaries.TimeFilterPrev = f.TimeFilterPrev;
                            ret.AddSummary(newSummaries);
                        }
                    }
                }
                if (this.Filters.Count > 0)
                    ret.Filters.AddRange(this.Filters);
                return ret;
            }
            catch { }
            return null;
        }
    }
    #endregion

    #endregion

    // Partialclass model
    namespace Codes.Models
    {
        public partial class lsttbl_DashboardSource
        {
            public InqDefineSource JsonObj
            {
                get
                {
                    return InqDefineSource.FromJsonStr(this.JsonStr);
                }
            }
            public InqDefineSourceMDX JsonObjMDX
            {
                get
                {
                    return InqDefineSourceMDX.FromJsonStr(this.JsonStr);
                }
            }
            public KPIDefineSource JsonObjKPI
            {
                get
                {
                    return KPIDefineSource.FromJsonStr(this.JsonStr);
                }
            }
            public lsttbl_DashboardSource(string code, string jsonStr, string nameVI, string nameEN, string nameFormatVI, string nameFormatEN)
            {
                this.Code = code;
                this.JsonStr = jsonStr;
                this.NameVI = nameVI;
                this.NameEN = nameEN;
                this.NameFormatVI = nameFormatVI;
                this.NameFormatEN = nameFormatEN;
            }
            public lsttbl_DashboardSource(string code, InqDefineSource jsonObj, string nameVI, string nameEN, string nameFormatVI, string nameFormatEN)
            {
                this.Code = code;
                this.JsonStr = jsonObj.ToJsonStr();
                this.NameVI = nameVI;
                this.NameEN = nameEN;
                this.NameFormatVI = nameFormatVI;
                this.NameFormatEN = nameFormatEN;
            }
            public lsttbl_DashboardSource(string code, InqDefineSourceMDX jsonObj, string nameVI, string nameEN, string nameFormatVI, string nameFormatEN)
            {
                this.Code = code;
                this.JsonStr = jsonObj.ToJsonStr();
                this.NameVI = nameVI;
                this.NameEN = nameEN;
                this.NameFormatVI = nameFormatVI;
                this.NameFormatEN = nameFormatEN;
            }
            public void UpdateOnSubmit(string code, string parentCode, string settingCat, string whCode, string jsonStr, string nameVI, string nameEN, string nameFormatVI, string nameFormatEN)
            {
                this.Code = code;
                this.JsonStr = jsonStr;
                this.NameVI = nameVI;
                this.NameEN = nameEN;
                this.NameFormatVI = nameFormatVI;
                this.NameFormatEN = nameFormatEN;
            }
            public void UpdateOnSubmit(lsttbl_DashboardSource info)
            {
                this.UpdateOnSubmit(info.Code, info.ParentCode, info.SettingCat, info.WHCode, info.JsonStr, info.NameVI, info.NameEN, info.NameFormatVI, info.NameFormatEN);
            }
        }
        public partial class lsttbl_Widget
        {
            public WidgetChart JsonObj_Chart
            {
                get
                {
                    return WidgetChart.FromJsonStr(this.JsonStr);
                }
            }
            public WidgetGauge JsonObj_Gauge
            {
                get
                {
                    return WidgetGauge.FromJsonStr(this.JsonStr);
                }
            }
            public WidgetGrid JsonObj_Grid
            {
                get
                {
                    return WidgetGrid.FromJsonStr(this.JsonStr);
                }
            }

            public lsttbl_Widget(string code, string jsonStr)
            {
                this.Code = code;
                this.JsonStr = jsonStr;
            }
            public lsttbl_Widget(string code, WidgetBase jsonObj)
                : this(code, jsonObj.ToJsonStr()) { }
            public void UpdateOnSubmit(string code, string name, string widgetType, string dsCode, string whCode, string jsonStr)
            {
                this.Code = code;
                this.Name = name;
                this.WidgetType = widgetType;
                this.DSCode = dsCode;
                this.WHCode = whCode;
                this.JsonStr = jsonStr;
            }
            public void UpdateOnSubmit(lsttbl_Widget info)
            {
                this.UpdateOnSubmit(info.Code, info.Name, info.WidgetType, info.DSCode, info.WHCode, info.JsonStr);
            }
        }
        public partial class lsttbl_WidgetInteraction
        {
            public InteractionDefine JsonObj
            {
                get
                {
                    return InteractionDefine.FromJsonStr(this.JsonStr);
                }
            }

            public void UpdateOnSubmit(string widgetCode, string jsonStr)
            {
                this.WidgetCode = widgetCode;
                this.JsonStr = jsonStr;
            }
            public void UpdateOnSubmit(lsttbl_WidgetInteraction info)
            {
                this.UpdateOnSubmit(info.WidgetCode, info.JsonStr);
            }
        }
        public partial class lsttbl_Dashboard
        {
            public DashboardDefine JsonObj
            {
                get
                {
                    return DashboardDefine.FromJsonStr(this.JsonStr);
                }
            }
            public void UpdateOnSubmit(string code, string name, string whCode, string jsonStr, bool isDefault)
            {
                this.Code = code;
                this.Name = name;
                this.WHCode = whCode;
                this.JsonStr = jsonStr;
                this.IsDefault = isDefault;
            }
            public void UpdateOnSubmit(lsttbl_Dashboard info)
            {
                this.UpdateOnSubmit(info.Code, info.Name, info.WHCode, info.JsonStr, info.IsDefault);
            }
        }
        public partial class lsttbl_DWColumn
        {
            public string KeyField { get { return string.Format("{0}_{1}", this.TblName_Virtual, this.ColName); } }
            public lsttbl_DWColumn(string tblName_Virtual, string colName)
            {
                this.TblName_Virtual = tblName_Virtual;
                this.ColName = colName;
            }
            public lsttbl_DWColumn(string tblName_Virtual, string colName, string dataType)
            {
                this.TblName_Virtual = tblName_Virtual;
                this.ColName = colName;
                this.DataType = dataType;
            }

            /// <summary>
            /// Đưa về chuỗi có dạng: "[Tên table].[Tên cột]" (VD: [DimItem].[ItemCode])
            /// </summary>
            public string ToSql()
            {
                return string.Format("[{0}].[{1}]", this.TblName_Virtual, this.ColName);
            }
            public InqFieldInfo ToInq()
            {
                var ret = new InqFieldInfo();
                ret.TblName = this.TblName_Virtual;
                ret.ColName = this.ColName;
                ret.DataType = this.DataType;
                ret.ColAliasEN = this.ColAliasEN;
                ret.ColAliasVI = this.ColAliasVI;
                ret.Visible = this.Visible;
                return ret;
            }
            public InqFieldInfoMDX ToInqMDX()
            {
                var ret = new InqFieldInfoMDX();
                ret.TblName = this.TblName_Virtual;
                ret.ColName = this.ColName;
                ret.DataType = this.DataType;
                ret.ColAliasEN = this.ColAliasEN;
                ret.ColAliasVI = this.ColAliasVI;
                ret.Visible = this.Visible;
                return ret;
            } 
        }
        public partial class lsttbl_DWTable
        {
            public InqTableInfo ToInq()
            {
                var ret = new InqTableInfo();
                ret.TblName = this.TblName;
                ret.TblCat = this.TblCat;
                ret.BizCat = this.BizCat;
                ret.RefInfo = this.RefInfo;
                ret.TblAliasEN = this.TblAliasEN;
                ret.TblAliasVI = this.TblAliasVI;                
                ret.Visible = this.Visible;
                return ret;
            }    
        }
    }
}