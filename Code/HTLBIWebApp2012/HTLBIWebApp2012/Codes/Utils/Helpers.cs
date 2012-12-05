using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxRoundPanel;
using System.Web.UI;
using DevExpress.Web.ASPxPivotGrid;
using System.Text;
using DevExpress.XtraPivotGrid.Data;
using System.Configuration;
using Newtonsoft.Json;
using CECOM;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraCharts.Web;
using DevExpress.XtraCharts;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using System.IO;
using System.Web.Security;

namespace HTLBIWebApp2012
{
    public class Helpers
    {
        #region Declares
        private const string sitemapFileName = "BIMenu.sitemap";
        public const string PaletteNameDefault = "Nature Colors";
        public const string AppearanceNameDefault = "Nature Colors";
        public static COMCatCodeNameObj[] ArrDashboardCtrlType = new COMCatCodeNameObj[] { 
                new COMCatCodeNameObj("chart-Bar","chart","Chart"),
                new COMCatCodeNameObj("gauge-CircleFull","gauge","Gauge"),
                new COMCatCodeNameObj("grid-GridView","grid","Grid View")
            };
        public static COMCatCodeNameObj[] ArrDashboardCtrl = new COMCatCodeNameObj[] { 
                new COMCatCodeNameObj("chart", "Area", "Area"),
                new COMCatCodeNameObj("chart", "Bar", "Bar"),
                new COMCatCodeNameObj("chart", "StackedBar", "Stacked Bar"),
                //new COMCatCodeNameObj("chart", "StackedLine", "Stacked Line"),                
                new COMCatCodeNameObj("chart", "StackedArea", "Stacked Area"),
                new COMCatCodeNameObj("chart", "StackedSplineArea", "Stacked Spline Area"),
                new COMCatCodeNameObj("chart", "SideBySideStackedBar", "SideBySide Stacked Bar"),
                new COMCatCodeNameObj("chart", "Point", "Point"),
                new COMCatCodeNameObj("chart", "Line", "Line"),
                //new COMCatCodeNameObj("chart", "StepArea", "Step Area"),
                new COMCatCodeNameObj("chart", "StepLine", "Step Line"),
                new COMCatCodeNameObj("chart", "Spline", "Spline"),
                new COMCatCodeNameObj("chart", "SplineArea", "Spline Area"),
                new COMCatCodeNameObj("chart", "ScatterLine", "Scatter Line"),
                new COMCatCodeNameObj("chart", "SwiftPlot", "Swift Plot"),                
                new COMCatCodeNameObj("chart", "RadarPoint", "Radar Point"),
                new COMCatCodeNameObj("chart", "RadarLine", "Radar Line"),
                new COMCatCodeNameObj("chart", "RadarArea", "Radar Area"),
                new COMCatCodeNameObj("chart", "Pie", "Pie"),
                new COMCatCodeNameObj("chart", "Doughnut", "Doughnut"),
                new COMCatCodeNameObj("gauge", "CircleFull", "Circle Full"),
                new COMCatCodeNameObj("gauge", "CircleHalf", "Circle Half"),
                new COMCatCodeNameObj("gauge", "CircleQuaterLeft", "Circle Quater Left"),
                new COMCatCodeNameObj("gauge", "CircleQuaterRight", "Circle Quater Right"),
                new COMCatCodeNameObj("gauge", "CircleThreeFour", "Circle ThreeFour"),
                new COMCatCodeNameObj("gauge", "LinearHorizontal", "Linear Horizontal"),
                new COMCatCodeNameObj("gauge", "LinearVertical", "Linear Vertical"),
                new COMCatCodeNameObj("grid", "GridView", "Grid View")
            };
        #endregion Declares

        public static DevExpress.Web.ASPxMenu.MenuItem CreateMenuObject(string caption, string name, string url)
        {
            var mnItem = new DevExpress.Web.ASPxMenu.MenuItem();
            mnItem.Text = caption;
            mnItem.ToolTip = caption;
            mnItem.Name = name;
            if (!string.IsNullOrEmpty(url)) // Là mục
                mnItem.NavigateUrl = url;
            return mnItem;
        }
        public static bool IsValidArray(string[] arr)
        {
            return (arr != null && arr.Length > 0);
        }
        public static string GetVirtualPath_FromCurrentToRoot(HttpRequest rq)
        {
            string virtualPath = string.Empty;
            string phAPath = rq.PhysicalApplicationPath;
            string phPath = rq.PhysicalPath;
            string[] arr = phPath.Replace(phAPath, string.Empty)
                            .Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder strb = new StringBuilder();
            if (arr.Length > 1)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    strb.Append("../");
                }
                virtualPath = strb.ToString();
            }

            //File.WriteAllText(@"D:\Duy_PhysicalApplicationPath.txt", rq.PhysicalApplicationPath);
            //File.WriteAllText(@"D:\Duy_PhysicalPath.txt", rq.PhysicalPath);
            //File.WriteAllText(@"D:\Duy_virtualPath.txt", virtualPath);

            return virtualPath;
        }
        public static string CombinePath(string fPath, string lPath)
        {
            if (string.IsNullOrEmpty(fPath) && string.IsNullOrEmpty(lPath))
                return string.Empty;
            if (!string.IsNullOrEmpty(fPath) && string.IsNullOrEmpty(lPath))
                return fPath;
            if (string.IsNullOrEmpty(fPath) && !string.IsNullOrEmpty(lPath))
                return lPath;

            // Cả 2 != null
            if ((!fPath.EndsWith("/") && !fPath.EndsWith("\\")) ||
                (!lPath.StartsWith("/") && !lPath.StartsWith("\\")))
            {
                if ((!fPath.EndsWith("/") && !fPath.EndsWith("\\")) &&
                    (!lPath.StartsWith("/") && !lPath.StartsWith("\\"))) // Cả hai không chứa '/'
                {
                    return string.Format("{0}/{1}", fPath, lPath);
                }
                else // Chỉ một trong 2 có chứa '/'
                {
                    return string.Format("{0}{1}", fPath, lPath);
                }

            }
            // Cả hai đều chứa '/'
            return string.Format("{0}{1}", fPath, lPath.Substring(1));
        }
        public static string GetConnectionString(string cnnName)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[cnnName].ConnectionString;
            }
            catch { return string.Empty; }
        }
        public static string GetAppSetting(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch { return string.Empty; }
        }
        public static string[] GetChartTypeName()
        {
            var ret = Enum.GetValues(typeof(ViewType))
                .OfType<ViewType>()
                .Select(p => p.ToString()).ToArray();
            return ret;
        }
        public static void InitChartTypeComboBox(ASPxComboBox cbbChartType, ViewType chartTypeDefault, params string[] restricted_EndWith)
        {
            ViewType[] restrictedTypes =  { 
                ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt,
				ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, 
                ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick,ViewType.Bubble };
            if (restricted_EndWith != null && restricted_EndWith.Length > 0)
                foreach (ViewType type in Enum.GetValues(typeof(ViewType)))
                {
                    if (Array.IndexOf<ViewType>(restrictedTypes, type) >= 0) continue;
                    var viewName = type.ToString();
                    if (restricted_EndWith.Exists(p => viewName.EndsWith(p))) continue;
                    cbbChartType.Items.Add(viewName);
                }
            else
                foreach (ViewType type in Enum.GetValues(typeof(ViewType)))
                {
                    if (Array.IndexOf<ViewType>(restrictedTypes, type) >= 0) continue;
                    cbbChartType.Items.Add(type.ToString());
                }

            cbbChartType.SelectedItem = cbbChartType.Items.FindByText(chartTypeDefault.ToString());
            cbbChartType.DropDownRows = GlobalVar.DropDownRow;
        }
        public static void InitMeasureFuncComboBox(ASPxComboBox cbbMeasFunc, MeasureFunc measFuncDefault)
        {
            var restrictedTypes = new MeasureFunc[] { MeasureFunc.Avg, MeasureFunc.Max, MeasureFunc.Min, MeasureFunc.Sum };
            foreach (var type in restrictedTypes)
                cbbMeasFunc.Items.Add(type.ToString());
            cbbMeasFunc.SelectedItem = cbbMeasFunc.Items.FindByText(measFuncDefault.ToString());
        }
        public static void InitAppearanceComboBox(ASPxComboBox cbbAppearance, string appearanceDefault)
        {
            var arrAppearance = new WebChartControl().GetAppearanceNames();
            foreach (var app in arrAppearance)
                cbbAppearance.Items.Add(app);
            cbbAppearance.SelectedItem = cbbAppearance.Items.FindByText(appearanceDefault);
        }
        public static void InitAppearanceComboBox(ASPxComboBox cbbAppearance, string appearanceDefault, bool showImage)
        {
            var arrAppearance = new WebChartControl().GetAppearanceNames();
            if (showImage)
            {
                var imgPathF = "~/Images/Appearance/{0}.png";
                foreach (var app in arrAppearance)
                    cbbAppearance.Items.Add(app, app, string.Format(imgPathF, app));
            }
            else
            {
                foreach (var app in arrAppearance)
                    cbbAppearance.Items.Add(app);
            }
            var selItem = cbbAppearance.Items.FindByValue(appearanceDefault);
            if(selItem==null)
                selItem = cbbAppearance.Items.FindByText(appearanceDefault);
            cbbAppearance.SelectedItem = selItem;
        }
        public static void InitPaletteComboBox(ASPxComboBox cbbPalette, string paletteDefault)
        {
            var arrPalette = new WebChartControl().GetPaletteNames();
            foreach (var pl in arrPalette)
                cbbPalette.Items.Add(pl);

            var selItem = cbbPalette.Items.FindByValue(paletteDefault);
            if (selItem == null)
                selItem = cbbPalette.Items.FindByText(paletteDefault);
            cbbPalette.SelectedItem = selItem;
        }
        public static void SetDataSource(ASPxComboBox cbb, object src, string valField, string txtField)
        {
            if (src == null) return;
            cbb.Items.Clear();
            cbb.Text = "";
            cbb.DataSource = src;
            cbb.ValueField = valField;
            cbb.TextField = txtField;
            cbb.DataBind();
        }
        public static void SetDataSource(ASPxComboBox cbb, object src, string valField, string txtField, object valueSelDefault)
        {
            SetDataSource(cbb, src, valField, txtField);
            cbb.Value = valueSelDefault;
        }
        public static void SetDataSource(ASPxListBox lst, object src, string valField, string txtField)
        {
            if (src == null) return;
            lst.DataSource = src;
            lst.ValueField = valField;
            lst.TextField = txtField;
            lst.DataBind();
            GC.Collect();
        }
        public static void SetDataSource(ASPxListBox lst, object src, string valField, string txtField, object valueSelDefault)
        {
            SetDataSource(lst, src, valField, txtField);
            lst.Value = valueSelDefault;
        }
        public static void SetDataSource(ASPxComboBox cbb, object src)
        {
            if (src == null) return;
            cbb.DataSource = src;
            cbb.DataBind();
            cbb.SelectedIndex = 0;
        }
        public static void SetDataSource(ASPxComboBox cbb, object src, object valueSelDefault)
        {
            SetDataSource(cbb, src);
            cbb.Value = valueSelDefault;
        }
        public static void SetDataSource<ObjectType>(CheckBoxList cblst, IEnumerable<ObjectType> src, bool isSelected, string valueField, string splitCharInText, params string[] textFields)
        {
            try
            {
                cblst.Items.Clear();
                if (string.IsNullOrEmpty(valueField)) return;
                if (textFields == null || textFields.Length <= 0) textFields = new string[] { valueField };
                if (string.IsNullOrEmpty(splitCharInText))
                    splitCharInText = "-";
                foreach (var item in src)
                {
                    var arrProp = item.GetType().GetProperties();
                    if (arrProp == null || arrProp.Length <= 0) continue;

                    var value = Lib.NullToEmpty(arrProp.SingleOrDefault(p => p.Name.Equals(valueField)).GetValue(item, null));
                    string text = string.Empty;
                    if (textFields.Length > 1)
                    {
                        foreach (var textField in textFields)
                        {
                            var newText = Lib.NullToEmpty(arrProp.SingleOrDefault(p => p.Name.Equals(textField)).GetValue(item, null));
                            text = string.Format("{0}{1}{2}", text, splitCharInText, newText);
                        }
                        text = text.Remove(0, splitCharInText.Length);
                    }
                    else // =1
                        text = Lib.NullToEmpty(arrProp.SingleOrDefault(p => p.Name.Equals(textFields.First())).GetValue(item, null));
                    var newListItem = new ListItem(text, value);
                    newListItem.Selected = isSelected;
                    cblst.Items.Add(newListItem);
                }
            }
            catch { }
        }
        public static void InitFormat_PopupControl(DevExpress.Web.ASPxPopupControl.ASPxPopupControl pop, int styleID, bool isModal)
        {
            switch (styleID)
            {
                case 0:
                    pop.CloseAction = DevExpress.Web.ASPxClasses.CloseAction.CloseButton;
                    pop.AllowResize = true;
                    pop.AllowDragging = true;
                    pop.AppearAfter = 100;
                    pop.DisappearAfter = 100;
                    pop.Font.Name = "Arial";
                    pop.Font.Size = new FontUnit(9, UnitType.Point);
                    pop.PopupHorizontalAlign = DevExpress.Web.ASPxClasses.PopupHorizontalAlign.WindowCenter;
                    pop.PopupVerticalAlign = DevExpress.Web.ASPxClasses.PopupVerticalAlign.WindowCenter;
                    pop.Modal = isModal;
                    pop.ShowFooter = false;
                    pop.ShowLoadingPanel = true;
                    //pop.LoadingPanelText = Resources.BI.loadingText;
                    break;
            }
        }
        public static void InitFormat_GridView(ASPxGridView gv, int styleID)
        {
            InitFormat_GridView(gv, styleID, 20);
        }
        public static void InitFormat_GridView(ASPxGridView gv, int styleID, int pageSize)
        {
            switch (styleID)
            {
                case 0:
                    gv.SettingsBehavior.AllowSort = false;
                    gv.SettingsBehavior.AllowDragDrop = false;
                    gv.SettingsBehavior.AllowFocusedRow = true;
                    gv.SettingsBehavior.EnableRowHotTrack = true;

                    gv.SettingsPager.Position = PagerPosition.TopAndBottom;
                    gv.SettingsPager.PageSize = pageSize;
                    gv.SettingsPager.FirstPageButton.Visible = true;
                    gv.SettingsPager.LastPageButton.Visible = true;
                    gv.Settings.ShowTitlePanel = true;
                    gv.SettingsText.EmptyDataRow = "Không có dữ liệu";
                    //gv.SettingsLoadingPanel.Text = Resources.BI.loadingText;
                    gv.Styles.TitlePanel.Font.Bold = true;
                    gv.Styles.TitlePanel.Font.Size = 12;
                    gv.Styles.Cell.Wrap = DefaultBoolean.True;
                    break;
                case 1:
                    gv.SettingsBehavior.AllowSort = false;
                    gv.SettingsBehavior.AllowDragDrop = false;
                    gv.SettingsBehavior.EnableRowHotTrack = true;

                    gv.Settings.ShowVerticalScrollBar = true;
                    //gv.Settings.ShowHorizontalScrollBar = true;
                    //gv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv.Settings.VerticalScrollBarStyle = GridViewVerticalScrollBarStyle.Virtual;
                    gv.Settings.VerticalScrollableHeight = 510;
                    gv.Settings.UseFixedTableLayout = true;

                    gv.SettingsPager.Position = PagerPosition.Top;
                    gv.SettingsPager.PageSize = pageSize;
                    gv.SettingsPager.FirstPageButton.Visible = true;
                    gv.SettingsPager.LastPageButton.Visible = true;
                    gv.Settings.ShowTitlePanel = true;
                    gv.SettingsText.EmptyDataRow = "Không có dữ liệu";
                    //gv.SettingsLoadingPanel.Text = Resources.BI.loadingText;
                    gv.Styles.TitlePanel.Font.Bold = true;
                    gv.Styles.TitlePanel.Font.Size = 12;
                    gv.Styles.Cell.Wrap = DefaultBoolean.True;
                    break;
                case 3:
                    gv.SettingsBehavior.AllowSort = false;
                    gv.SettingsBehavior.AllowDragDrop = false;
                    gv.SettingsBehavior.AllowFocusedRow = true;
                    gv.SettingsBehavior.EnableRowHotTrack = true;

                    gv.Settings.ShowVerticalScrollBar = true;
                    //gv.Settings.ShowHorizontalScrollBar = true;
                    gv.Settings.VerticalScrollableHeight = 335; //510
                    gv.Settings.UseFixedTableLayout = true;

                    gv.SettingsPager.PageSize = pageSize;
                    gv.SettingsPager.FirstPageButton.Visible = true;
                    gv.SettingsPager.LastPageButton.Visible = true;
                    gv.SettingsText.EmptyDataRow = "Không có dữ liệu";
                    //gv.SettingsLoadingPanel.Text = Resources.BI.loadingText;
                    gv.Styles.TitlePanel.Font.Bold = true;
                    gv.Styles.TitlePanel.Font.Size = 12;
                    gv.Styles.Cell.Wrap = DefaultBoolean.True;
                    break;
            }
        }
        public static void FormatCommon_GridColumn(GridViewColumn gc)
        {
            gc.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            gc.HeaderStyle.Font.Bold = true;
        }
        public static void RuntimeFormat_GridView(ASPxGridView gv)
        {
            if (gv.Columns.Count <= 0) return;
            foreach (var item in gv.Columns)
            {
                var col = item as DevExpress.Web.ASPxGridView.GridViewDataColumn;
                if (col == null) continue;
                FormatCommon_GridColumn(col);
            }
        }
        public static void FormatCommon_PivotGrid(ASPxPivotGrid pivotGrid, int styleID)
        {
            switch (styleID)
            {
                case 0:
                    pivotGrid.OptionsView.ShowFilterHeaders = true;
                    pivotGrid.OptionsView.ShowHorizontalScrollBar = true;
                    pivotGrid.OptionsChartDataSource.ShowColumnGrandTotals = false;
                    pivotGrid.OptionsChartDataSource.ShowRowGrandTotals = false;
                    pivotGrid.Styles.CellStyle.Cursor = "pointer";
                    //pivotGrid.OptionsLoadingPanel.Text = Resources.BI.loadingText;
                    //pivotGrid.ToolTip = Resources.BI.toolTip_PivotGrid;
                    pivotGrid.CssClass = "dxpgControl";
                    pivotGrid.BorderBottom.BorderStyle = BorderStyle.None;
                    pivotGrid.BackColor = Color.Transparent;
                    //pivotGrid.Border.BorderStyle = BorderStyle.None;
                    break;
            }
        }
        public static void FormatCommon_ChartControl(WebChartControl webChart, int styleID)
        {
            XYDiagram diagram = null;
            try
            {
                switch (styleID)
                {
                    case 0:
                        webChart.Padding.All = 0;
                        webChart.BorderOptions.Visible = false;
                        webChart.SeriesDataMember = "Series";
                        webChart.SeriesTemplate.ArgumentDataMember = "Arguments";
                        webChart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Values" });
                        webChart.SeriesTemplate.Label.Visible = false;// new 
                        webChart.SeriesTemplate.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                        webChart.SeriesTemplate.PointOptions.ValueNumericOptions.Precision = 2;
                        // Format for WebChart Series Axis
                        if (webChart.Series != null)
                            foreach (SeriesBase series in webChart.Series)
                                series.Visible = false;
                        diagram = (XYDiagram)webChart.Diagram;
                        diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Year;
                        diagram.AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Year;
                        diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
                        diagram.AxisX.DateTimeOptions.FormatString = "yyyy";
                        diagram.AxisY.NumericOptions.Format = NumericFormat.Number;
                        diagram.AxisY.NumericOptions.Precision = 0;
                        break;
                    case 1:
                        webChart.Padding.All = 0;
                        diagram = (XYDiagram)webChart.Diagram;
                        diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Year;
                        diagram.AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Year;
                        diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
                        diagram.AxisX.DateTimeOptions.FormatString = "yyyy";
                        diagram.AxisY.NumericOptions.Format = NumericFormat.Number;
                        diagram.AxisY.NumericOptions.Precision = 0;
                        diagram.DefaultPane.BorderVisible = false;
                        break;
                    case 2:
                        webChart.Padding.All = 0;
                        webChart.BorderOptions.Visible = false;
                        webChart.SeriesTemplate.Label.Visible = false;
                        webChart.SeriesTemplate.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
                        webChart.SeriesTemplate.PointOptions.ValueNumericOptions.Precision = 2;

                        diagram = (XYDiagram)webChart.Diagram;
                        diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Year;
                        diagram.AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Year;
                        diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
                        diagram.AxisX.DateTimeOptions.FormatString = "yyyy";
                        diagram.AxisY.NumericOptions.Format = NumericFormat.Number;
                        diagram.AxisY.NumericOptions.Precision = 0;
                        break;
                }
            }
            catch { }
        }
        public static List<string> GetSelectedItems(CheckBoxList cblst, bool getValue, string coverChar)
        {
            var ret = new List<string>();
            try
            {
                if (getValue)
                {
                    foreach (ListItem item in cblst.Items)
                        if (item.Selected)
                            ret.Add(string.Format("{1}{0}{1}", item.Value, coverChar));
                }
                else
                {
                    foreach (ListItem item in cblst.Items)
                        if (item.Selected)
                            ret.Add(string.Format("{1}{0}{1}", item.Text, coverChar));
                }
            }
            catch { }
            return ret;
        }
        public static List<string> GetSelectedItems(CheckBoxList cblst, bool getValue)
        {
            return GetSelectedItems(cblst, getValue, string.Empty);
        }
        public static List<COMCodeNameObj> GetSelectedItems(CheckBoxList cblst)
        {
            var ret = new List<COMCodeNameObj>();
            try
            {
                foreach (ListItem item in cblst.Items)
                    if (item.Selected)
                        ret.Add(new COMCodeNameObj(item.Value, item.Text));
            }
            catch { }
            return ret;
        }
        public static string ListToStringSQL(ICollection<string> ls)
        {
            return ListToString(ls, "N");
        }
        public static string ListToStringSQL(ICollection<string> ls, string coverChar)
        {
            return ListToString(ls, "N", coverChar);
        }
        public static string ListToString(ICollection<string> ls)
        {
            return ListToString(ls, null);
        }
        public static string ListToString(ICollection<string> ls, string preffix, string coverChar)
        {
            if (ls == null) return string.Empty;
            if (ls.Count <= 0) return string.Empty;
            coverChar = string.IsNullOrEmpty(coverChar) ? string.Empty : coverChar;

            string result = "(";
            if (string.IsNullOrEmpty(preffix))
                foreach (string str in ls)
                    result += string.Format("{0}{1}{0},", coverChar, str);
            else
                foreach (string str in ls)
                    result += string.Format("{0}{2}{1}{2},", preffix, str, coverChar);
            result = result.Remove(result.Length - 1) + ")";
            return result;
        }
        public static string ListToString(ICollection<string> ls, string preffix)
        {
            return ListToString(ls, preffix, "'");
        }
        public static OlapColumnInfo GetHeaderPivotInfo(ASPxPivotGrid pivot, string columnName)
        {
            try
            {
                for (int i = 0; i < pivot.Fields.Count; i++)
                {
                    var arrSource = pivot.Fields[i].OLAPDrillDownColumnName.ToLower().Split('.');
                    var arrDest = columnName.ToLower().Split('.');
                    if (arrDest != null && arrSource != null)
                    {
                        var olapcolName = arrSource.LastOrDefault();
                        var colName = arrDest.LastOrDefault();
                        if (olapcolName.Equals(colName))
                        {
                            return new OlapColumnInfo(pivot.Fields[i].Caption
                                                    , pivot.Fields[i].CellFormat
                                                    , pivot.Fields[i].Width);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        public static int GetTotalColumnsWidth(ASPxGridView gv)
        {
            int totalW = 0;
            foreach (GridViewColumn col in gv.Columns)
                totalW += (int)col.Width.Value;
            return totalW;
        }

        /// <summary>
        /// Lấy chuỗi javaScript cho xử lý sự kiện click hoặc doubclick của lưới.
        /// </summary>
        public static string GetJS_GridRowDblClick_Level1(int zone_Level1, string gvName_Level2, string chartName_Level2)
        {
            return string.Format(
                @"function (s, e) {{
                var paramStr = 'RowIndex='+s.GetFocusedRowIndex()+';Zone={0}';
                {1}.PerformCallback(paramStr);
                {2}.PerformCallback(paramStr);
              }}", zone_Level1, chartName_Level2, gvName_Level2);
        }

        /// <summary>
        /// Lấy chuỗi javaScript cho xử lý sự kiện click hoặc doubclick của lưới pivot.
        /// </summary>
        /// <param name="colIndexInput">Input control HTML: type=hidden, enableviewstate=true, runat=server</param>
        /// <param name="rowIndexInput">Input control HTML: type=hidden, enableviewstate=true, runat=server</param>
        /// <returns></returns>
        public static string GetJSCellClickHandler(HtmlInputHidden colIndexInput, HtmlInputHidden rowIndexInput)
        {
            return string.Format(@"function (s, e) {{
                var columnIndex = document.getElementById('{0}');
                var rowIndex = document.getElementById('{1}');
                columnIndex.value = e.ColumnIndex;
                rowIndex.value = e.RowIndex;
                gvDrillDown.PerformCallback('D');
                PopupDrillDown_ShowDialog();
                }}", colIndexInput.ClientID, rowIndexInput.ClientID);
        }
        public static string GetJSCellClickHandler(HtmlInputHidden colIndexInput, HtmlInputHidden rowIndexInput, string presentDataClientName, string popupClientName)
        {
            var preScript = string.Empty;
            if (!string.IsNullOrEmpty(presentDataClientName) && !string.IsNullOrEmpty(popupClientName))
                preScript = string.Format(" {0}.PerformCallback('D|GRID'); {1}.Show(); ", presentDataClientName, popupClientName);
            else if (!string.IsNullOrEmpty(presentDataClientName))
                preScript = string.Format(" {0}.PerformCallback('D|GRID'); ", presentDataClientName);
            else if (!string.IsNullOrEmpty(popupClientName))
                preScript = string.Format(" {0}.Show(); ", popupClientName);

            return string.Format(@"function (s, e) {{
                var columnIndex = document.getElementById('{0}');
                var rowIndex = document.getElementById('{1}');
                columnIndex.value = e.ColumnIndex;
                rowIndex.value = e.RowIndex;
                {2}
                }}", colIndexInput.ClientID, rowIndexInput.ClientID, preScript);
        }
        public static string GetJSShowAlertBox(string message, bool coverByFunction)
        {
            return coverByFunction ?
                  string.Format("function (s, e) {{ alert('{0}'); }}", message)
                : string.Format("alert('{0}');", message);
        }
        /// <summary>
        /// Trả về kiểu enum ứng với kiểu enum truyền vào
        /// </summary>
        /// <typeparam name="ReturnType"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumType ToEnum<EnumType>(object value)
        {
            if (string.IsNullOrEmpty(Lib.NullToEmpty(value)))
                return default(EnumType);
            return (EnumType)Enum.Parse(typeof(EnumType), value.ToString());
        }
        public static string ToJSonStr(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static ReturnType FromJsonStr<ReturnType>(string jsonString)
        {
            return JsonConvert.DeserializeObject<ReturnType>(jsonString);
        }
        public static string ToCurrentCulture(string pText_VI, string pText_EN)
        {
            switch (LanguageManager.CurrentCulture.Name)
            {
                case "vi-VN":
                    return pText_VI;
                case "en-US":
                    return pText_EN;
            }
            return pText_EN;
        }
    }
}