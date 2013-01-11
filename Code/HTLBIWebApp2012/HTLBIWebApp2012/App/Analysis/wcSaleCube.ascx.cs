﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using DevExpress.Web.ASPxGridView;
using CECOM;
using DevExpress.Utils;

namespace HTLBIWebApp2012.App.Analysis
{
    public partial class wcSaleCube : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = Resources.BI.Analysis_SalePageTitle;
            // Được thực thi khi lần đầu tiên được load hoặc khi Page ReInit
            if (!Page.IsPostBack)
            {
                //this.field.Caption = string.Format("{0}{1}", this.fieldDoanhSo.Caption, Resources.BI.Milion_CurrencySymbol);
                //////////////////////////////////////
                // Init OlapConnectionString
                string errReturn = OLAPConnector.TryConnect(this.pivotGrid, OLAPConnector.OLAPConnectionString, "ARCube");
                if (!string.IsNullOrEmpty(errReturn))
                {
                    Control errorPanel = OLAPConnector.CreateErrorPanel(errReturn);
                    ErrorMsgPlaceHolder.Controls.Add(errorPanel);
                }
                //////////////////////////////////////
                // Init cbbChartType
                Helpers.InitChartTypeComboBox(cbbChartType, ViewType.Bar);
                // Init cbbChartType
                Helpers.InitAppearanceComboBox(this.cbbAppearance, Helpers.AppearanceNameDefault);
                // Init cbbChartType
                Helpers.InitPaletteComboBox(this.cbbPalette, Helpers.PaletteNameDefault);
                // Set Default ChartType
                Helpers.SetChartType(WebChart, cbbChartType.SelectedItem.Text);
                // Set Default AppearanceName
                this.WebChart.AppearanceName = this.cbbAppearance.SelectedItem.Text;
                // Set Default AppearanceName
                this.WebChart.PaletteName = this.cbbPalette.SelectedItem.Text;
                //////////////////////////////////////
                // Format for PivotGrid
                Helpers.FormatCommon_PivotGrid(this.pivotGrid, 0);
                //////////////////////////////////////
                // Format for WebChart Series template
                Helpers.FormatCommon_ChartControl(this.WebChart, 0);
                //var diagram = (XYDiagram)this.WebChart.Diagram;
                //diagram.AxisY.Label.EndText = string.Format(" {0}", Resources.BI.Milion_CurrencySymbol);
                //////////////////////////////////////
                // Format for GridView PopupDrillDown
                Helpers.InitFormat_GridView(this.gvDrillDown, 1, Lib.IfNOE(Helpers.GetAppSetting(Helpers.PopupGridPageSize), 10));
                this.pivotGrid.ClientSideEvents.CellDblClick = Helpers.GetJSCellClickHandler(this.ColumnIndex, this.RowIndex, this.gvDrillDown.ClientInstanceName, null);
                //////////////////////////////////////
            }
        }

        #region Member Methods
        private void Bind_gvDrillDown()
        {
            try
            {
                var columnIndexValue = ColumnIndex.Value;
                var rowIndexValue = RowIndex.Value;
                if (Session["Row_Col_Sel"] != null && Lib.NullToEmpty(Session["WhereSel"]).Equals("CHART"))
                {
                    var p = (System.Drawing.Point)Session["Row_Col_Sel"];
                    columnIndexValue = p.Y.ToString();
                    rowIndexValue = p.X.ToString();
                }
                if (!string.IsNullOrEmpty(columnIndexValue) && !string.IsNullOrEmpty(rowIndexValue))
                    this.Bind_gvDrillDown(columnIndexValue, rowIndexValue);
            }
            catch { }
        }
        private void Bind_gvDrillDown(string columnIndex, string rowIndex)
        {
            this.gvDrillDown.Columns.Clear();
            this.gvDrillDown.AutoGenerateColumns = true;
            this.gvDrillDown.DataSource = this.pivotGrid.CreateDrillDownDataSource(Int32.Parse(columnIndex), Int32.Parse(rowIndex));
            this.gvDrillDown.DataBind();
            // Thiết lập định dạng cho từng cột trong lưới popup theo lưới pivot
            for (int i = 0; i < this.gvDrillDown.Columns.Count; i++)
            {
                var col = this.gvDrillDown.Columns[i];
                var fInfo = Helpers.GetHeaderPivotInfo(this.pivotGrid, col.ToString());
                if (fInfo == null) return;
                this.ApplyColumnFormat(col, fInfo);
                Helpers.FormatCommon_GridColumn(col);
            }
        }
        public void ApplyColumnFormat(GridViewColumn gvCol, OlapColumnInfo fInfo)
        {
            var col = gvCol as GridViewDataColumn;
            if (col == null) return;
            col.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            col.Caption = fInfo.Caption;
            col.PropertiesEdit.DisplayFormatString = fInfo.Format.FormatString;
            switch (fInfo.Format.FormatType)
            {
                case FormatType.DateTime:
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    break;
                case FormatType.Numeric:
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    break;
                default:
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                    break;
            }
            if (fInfo.Caption.ToLower().Equals("year") ||
                fInfo.Caption.ToLower().Equals("month") ||
                fInfo.Caption.ToLower().Equals("date"))
            {
                col.Width = 50;
                col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                return;
            }
            col.Width = fInfo.ColWidth;
        }
        #endregion
        
        #region Events
        protected void pivotGrid_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
        {

        }
        protected void gvDbrdDetail_PageIndexChanged(object sender, EventArgs e)
        {
            this.Bind_gvDrillDown();
        }
        protected void gvDrillDown_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                var param = e.Parameters.Split('|');
                if (!param.First().Equals("D")) return;
                Session["WhereSel"] = param.Last();

                this.Bind_gvDrillDown();
                this.gvDrillDown.PageIndex = 0;
            }
            catch { }
        }
        /// <summary>
        /// Lấy thông tin tọa độ (dòng,cột) của cell olap tại seriesPoint đang chọn trên chart
        /// </summary>
        protected void WebChart_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            // Cột,dòng
            try
            {
                var sp = e.AdditionalObject as SeriesPoint;
                if (sp == null) return;
                var cr = sp.Tag as int[];
                if (cr == null || cr.Length < 1) return;
                Session["Row_Col_Sel"] = new System.Drawing.Point(cr.Last(), cr.First());
                GC.Collect();
            }
            catch { }
        }
        protected void WebChart_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Parameter))
                {
                    if (e.Parameter.Equals("chartTypeValueChanged"))
                        Helpers.SetChartType(WebChart, cbbChartType.SelectedItem.Text);
                    else if (e.Parameter.Equals("Appearance"))
                        this.WebChart.AppearanceName = this.cbbAppearance.SelectedItem.Text;
                    else if (e.Parameter.Equals("Palette"))
                        this.WebChart.PaletteName = this.cbbPalette.SelectedItem.Text;
                    else
                        this.WebChart.Width = new Unit(Convert.ToInt32(Lib.IfNOE(e.Parameter, 986)));
                }
                this.WebChart.DataBind();
            }
            catch { }
        }
        #endregion
    }
}