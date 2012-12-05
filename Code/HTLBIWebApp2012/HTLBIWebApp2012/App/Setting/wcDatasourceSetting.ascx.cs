using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcDatasourceSetting : PartPlugCtrlBase
    {
        #region Declares
        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        public string WHCode { get { return Lib.NTE(this.cboDataDW.Value); } }
        public string DSCode { get { return Lib.NTE(this.cboDatasource.Value); } }
        public bool IsCurrentActive
        {
            get
            {
                if (MyPage == null) return false;
                var curTabPageName = MyPage.CurrentActiveTabPage.Name;
                curTabPageName = curTabPageName.Replace("tabPage_", "");
                return curTabPageName.ToLower() == "DatasourceSetting".ToLower();
            }
        }
        protected List<string> FilterControlSet
        {
            get
            {
                if (ViewState["FilterControlSet"] == null)
                    ViewState["FilterControlSet"] = new List<string>();
                return ViewState["FilterControlSet"] as List<string>;
            }
            set
            {
                ViewState["FilterControlSet"] = value;
                if (value == null) GC.Collect();
            }
        }
        #endregion

        #region Members
        public override void Load_InitData()
        {
            this.OnChange += this_OnChange;
            //////////////////////////////////// Tab-DS
            this.Register_JavaScript();
            if (!this.IsPostBack)
            {                
                Helpers.SetDataSource(this.cboDataDW, MyBI.Me.GetDW(), "Value", "Text");
                this.cboFuncs.Items.AddRange(InqMDX.GetSummatyFuncName());
                this.cboOrderBy0.Items.AddRange(InqMDX.GetOrderByName());
                this.cboOrderBy1.Items.AddRange(InqMDX.GetOrderByName());
            }
            else
            {
                if (!string.IsNullOrEmpty(this.WHCode))
                {
                    // Tải lại source cho cboDatasource(để nó không bị thiếu khi vừa thêm mới 1 Datasource trong sự kiện của CallbackPanel)
                    // Vì cơ chế của CallbackPanel sẽ không để lại ViewState mỗi lần Render
                    var ds = MyBI.Me.Get_DashboardSource(this.WHCode, GlobalVar.SettingCat_DS).ToList();
                    Helpers.SetDataSource(this.cboDatasource, ds, "Code", "NameVI", this.cboDatasource.Value);

                    var tblFactNames = MyBI.Me.Get_DWTableName("FACT", this.WHCode);
                    var cols = MyBI.Me.Get_DWColumn(this.WHCode);
                    var dsField = cols.Where(p => p.Visible && p.DataType != "NUM").ToList();
                    var dsMetric = cols.Where(p => p.Visible && tblFactNames.Contains(p.TblName_Virtual) && p.DataType == "NUM").ToList();
                    Helpers.SetDataSource(this.lbxField, dsField, "KeyField", "ColAliasVI");
                    Helpers.SetDataSource(this.lbxMetricField, dsMetric, "KeyField", "ColAliasVI");
                    Helpers.SetDataSource(this.lbxFieldSelected, MySession.DSDefine_SelFieldInfo, "KeyField", "ColAliasVI");
                    Helpers.SetDataSource(this.lbxMetricFieldSelected, MySession.DSDefine_SelSumInfo, "Field.KeyField", "FieldAlias");
                }
                this.ReGen_CtrlOnPostPack();
            }
        }
        public void ReGen_CtrlOnPostPack()
        {
            this.Add_FilterControl(null, true);
        }
        public void Register_JavaScript()
        {
            try
            {
                //////////////////////////////////// Tab-DS
                this.RegisterStartupScript(this.upp_SelectClause,
                    "$(document).ready(function () {" +
                        "$(\".numericInput\").autoNumeric({ aSep: ',', aDec: '.', mDec: '0', vMax: '999999999999999999' });" +
                        "$(\".numericInput\").css(\"text-align\", \"right\");" +
                    "});"
                );
            }
            catch { }
        }
        public lsttbl_DashboardSource Get_Datasource()
        {
            try
            {
                if (string.IsNullOrEmpty(this.DSCode)) return null;
                var ret = MyBI.Me.Get_DashboardSourceBy(this.DSCode);
                return ret;
            }
            catch { return null; }
        }
        public void Format_GridColumn(GridViewDataTextColumn col, ValueFormat valF)
        {
            col.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            col.HeaderStyle.Font.Bold = true;
            col.HeaderStyle.Font.Name = "Arial";
            col.HeaderStyle.Font.Size = new FontUnit(9, UnitType.Point);
            col.CellStyle.Font.Name = "Arial";
            col.CellStyle.Font.Size = new FontUnit(9, UnitType.Point);
            switch (valF)
            {
                case ValueFormat.Date:
                case ValueFormat.DateTime:
                case ValueFormat.ShortTime:
                case ValueFormat.Time:
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    break;
                case ValueFormat.Numeric:
                    col.PropertiesEdit.DisplayFormatString = "#,##0";
                    col.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    break;
            }
        }
        private FilterCtrlBase Add_FilterControl(string type, bool isReCreate)
        {
            if (string.IsNullOrEmpty(type)) type = "NORMAL";
            var guiID = Guid.NewGuid().ToString();
            FilterCtrlBase ctrl = null;
            var whCode = this.WHCode;
            var tblFactNames = MyBI.Me.Get_DWTableName("FACT", whCode);
            var ds = MyBI.Me.Get_DWColumn(whCode);
            var dsField = new List<lsttbl_DWColumn>();

            // ReCreate...
            if (isReCreate)
            {
                foreach (var obj in this.FilterControlSet)
                {
                    var arr = obj.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var ctrlID = arr.First();
                    var ctrlType = arr.Last();
                    if (ctrlType == "NUM")
                    {
                        ctrl = this.LoadControl("wcNumFilter.ascx") as wcNumFilter;
                        dsField = ds.Where(p => p.Visible && tblFactNames.Contains(p.TblName_Virtual) && p.DataType == "NUM").ToList();
                    }
                    else if (ctrlType == "DATE")
                    {
                        ctrl = this.LoadControl("wcTimeFilter.ascx") as wcTimeFilter;
                        dsField = ds.Where(p => p.Visible && p.TblName_Virtual.Contains("DimTime")).ToList();
                    }
                    else
                    {
                        ctrl = this.LoadControl("wcNormalFilter.ascx") as wcNormalFilter;
                        dsField = ds.Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime")).ToList();
                    }
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.RemoveFilter;
                    ctrl.Set_Source(dsField, "KeyField", "ColAliasVI");
                    this.ctrlCollect.Controls.Add(ctrl);
                }
                return null;
            }
            // Add new...
            if (type == "NUM")
            {
                ctrl = this.LoadControl("wcNumFilter.ascx") as wcNumFilter;
                dsField = ds.Where(p =>
                       p.Visible && tblFactNames.Contains(p.TblName_Virtual) && p.DataType == "NUM"
                    ).ToList();
            }
            else if (type == "DATE")
            {
                ctrl = this.LoadControl("wcTimeFilter.ascx") as wcTimeFilter;
                dsField = ds.Where(p => p.Visible && p.TblName_Virtual.Contains("DimTime")).ToList();
            }
            else // normal
            {
                ctrl = this.LoadControl("wcNormalFilter.ascx") as wcNormalFilter;
                dsField = ds.Where(p =>
                        p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime")
                    ).ToList();
            }
            ctrl.ID = string.Format("gen_{1}_{0}_{1}", guiID, type);
            ctrl.OnRemove += this.RemoveFilter;
            ctrl.Set_Source(dsField, "KeyField", "ColAliasVI");
            this.ctrlCollect.Controls.Add(ctrl);
            this.FilterControlSet.Add(string.Format("{0},{1}", ctrl.ID, type));
            return ctrl;
        }
        public void Set_SaveMsgText(string msg, bool isError)
        {
            this.lblSavingMsg.Font.Bold = true;
            this.lblSavingMsg.Font.Italic = true;
            this.lblSavingMsg.Font.Name = "Arial";
            this.lblSavingMsg.ForeColor = isError ? Color.Red : Color.Blue;
            this.lblSavingMsg.Text = msg;
        }
        public void Set_PreViewMDX(string mdx)
        {
            //this.txtPreViewSQL.Font.Bold = true;
            this.txtPreViewSQL.Font.Name = "Arial";
            this.txtPreViewSQL.Font.Size = new FontUnit(11, UnitType.Point);
            this.txtPreViewSQL.Text = mdx;
        }
        public InqDefineSourceMDX Get_DefineSource()
        {
            try
            {
                //var top = string.IsNullOrEmpty(this.cboTop.Text) ? 0 : int.Parse(this.cboTop.Text);
                var fields = this.lbxFieldSelected.DataSource as List<InqFieldInfoMDX>;
                var summaries = this.lbxMetricFieldSelected.DataSource as List<InqSummaryInfoMDX>;
                var filters = new List<InqFilterInfoMDX>();
                foreach (FilterCtrlBase ctrl in this.ctrlCollect.Controls)
                {
                    var filter = ctrl.Get_FilterInfo();
                    if (filter == null) continue;
                    // Set lại hàm tính toán trên field filter giống với hàm của field đó trong Summaries
                    if (filter.HasHavingKey())
                    {
                        var objSummary = summaries.FirstOrDefault(p => p.Field.KeyField == filter.HavingKey.Field.KeyField);
                        if (objSummary != null)
                            filter.HavingKey.FuncName = objSummary.FuncName;
                    }
                    filters.Add(filter);
                }
                var ret = new InqDefineSourceMDX(fields, summaries, filters);
                ret.PreffixDimTable = this.WHCode;
                ret.OlapCubeName = string.Format("[{0}Cube]", ret.PreffixDimTable);
                return ret;
            }
            catch { return new InqDefineSourceMDX(); }
        }
        #endregion

        #region Events
        protected void this_OnChange(object sender, EventArgs e)
        {
            if (!(sender is string)) return;
            var cat = sender.ToString();
            if (cat == "LAYOUT" || cat == "KPI")
            {
                var eReceived = e as HTLBIEventArgs;
                if (eReceived == null) return;
                this.cboDatasource.Value = eReceived.Values.ToString();
                this.cbo_ValueChanged(this.cboDatasource, null);
                if (cat == "LAYOUT")
                {
                    this.MyPage.My_wcKPISetting.Raise_OnChange("RESET", null);
                }
            }
        }
        protected void cbo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var cbo = sender as ASPxComboBox;
                if (cbo == null) return;

                if (cbo.ID == this.cboDataDW.ID)
                {
                    // Load Respository
                    var whCode = this.WHCode;
                    var tblFactNames = MyBI.Me.Get_DWTableName("FACT", whCode);
                    var cols = MyBI.Me.Get_DWColumn(whCode);
                    var dsField = cols.Where(p => p.Visible && p.DataType != "NUM").ToList();
                    var dsMetric = cols.Where(p => p.Visible && tblFactNames.Contains(p.TblName_Virtual) && p.DataType == "NUM").ToList();
                    Helpers.SetDataSource(this.lbxField, dsField, "KeyField", "ColAliasVI");
                    Helpers.SetDataSource(this.lbxMetricField, dsMetric, "KeyField", "ColAliasVI");
                    // Load Available Datasource.
                    var datasource = MyBI.Me.Get_DashboardSource(whCode, GlobalVar.SettingCat_DS).ToList();
                    Helpers.SetDataSource(this.cboDatasource, datasource, "Code", "NameVI");
                    MySession.DSDefine_CurEditing = null;

                    // Clear Selected field
                    this.lbxFieldSelected.Items.Clear();
                    MySession.DSDefine_SelFieldInfo.Clear();
                    this.lbxMetricFieldSelected.Items.Clear();
                    MySession.DSDefine_SelSumInfo.Clear();

                    // Clear filtered field
                    this.FilterControlSet.Clear();
                    this.ctrlCollect.Controls.Clear();

                    // Clear Input
                    this.txtDisplayName0.Text = "";
                    this.txtDisplayName.Text = "";
                    this.cboFuncs.Text = "";
                    this.cboOrderBy1.Text = "";

                    // Raise Event OnChange.
                    this.MyPage.My_wcKPISetting.Raise_OnChange("WH", null);
                    this.MyPage.My_wcLayoutSetting.Raise_OnChange("WH", null);
                    //this.MyPage.My_wcInteractionSetting.Raise_OnChange("WH", null);
                }
                else if (cbo.ID == this.cboDatasource.ID)
                {
                    var item = this.Get_Datasource();
                    if (item == null) return;

                    MySession.DSDefine_CurEditing = item.Code;
                    this.txtDisplayNameDS.Text = item.NameVI;
                    var inq = item.JsonObjMDX;

                    // Clear Selected field
                    this.lbxFieldSelected.Items.Clear();
                    MySession.DSDefine_SelFieldInfo.Clear();
                    this.lbxMetricFieldSelected.Items.Clear();
                    MySession.DSDefine_SelSumInfo.Clear();
                    // Clear filtered field
                    this.FilterControlSet.Clear();
                    this.ctrlCollect.Controls.Clear();
                    // Clear Input
                    this.txtDisplayName0.Text = "";
                    this.txtDisplayName.Text = "";
                    this.cboFuncs.Text = "";
                    this.cboOrderBy1.Text = "";

                    // Set Available datasource info...
                    MySession.DSDefine_SelFieldInfo = inq.Fields;
                    MySession.DSDefine_SelSumInfo = inq.Summaries;
                    Helpers.SetDataSource(this.lbxFieldSelected, inq.Fields, "KeyField", "ColAliasVI");
                    Helpers.SetDataSource(this.lbxMetricFieldSelected, inq.Summaries, "Field.KeyField", "FieldAlias");
                    foreach (var filter in inq.Filters)
                    {
                        var myCtrl = this.Add_FilterControl(filter.GetTinyType(), false);
                        myCtrl.Set_Info(filter);
                    }

                    // Raise Event OnChange.
                    this.MyPage.My_wcKPISetting.Raise_OnChange("DS", null);
                    this.MyPage.My_wcLayoutSetting.Raise_OnChange("DS", null);
                }
                else if (cbo.ID == this.cboFuncs.ID)
                {
                    var item = this.lbxMetricFieldSelected.SelectedItem;
                    item.SetValue("FuncName", cbo.Value);
                }
                else if (cbo.ID == this.cboOrderBy0.ID)
                {
                    var item = this.lbxFieldSelected.SelectedItem;
                    item.SetValue("OrderName", cbo.Value);
                }
                else if (cbo.ID == this.cboOrderBy1.ID)
                {
                    var item = this.lbxMetricFieldSelected.SelectedItem;
                    item.SetValue("Field.OrderName", cbo.Value);
                }
            }
            catch { }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as DevExpress.Web.ASPxEditors.ASPxButton;
                if (btn == null) return;

                if (btn.ID == this.btnNewDS.ID)
                {
                    MySession.DSDefine_CurEditing = null;
                    // Clear Selected field
                    this.lbxFieldSelected.Items.Clear();
                    MySession.DSDefine_SelFieldInfo.Clear();
                    this.lbxMetricFieldSelected.Items.Clear();
                    MySession.DSDefine_SelSumInfo.Clear();

                    // Clear filtered field
                    this.FilterControlSet.Clear();
                    this.ctrlCollect.Controls.Clear();

                    // Clear Input
                    this.txtDisplayNameDS.Text = "";
                    this.cboDatasource.Text = "";
                    this.txtDisplayName0.Text = "";
                    this.txtDisplayName.Text = "";
                    this.cboFuncs.Text = "";
                    this.cboOrderBy1.Text = "";
                    this.lblSavingMsg.Text = "";

                    // Update Display to clientside...
                    this.upp_SelectClause.Update();
                    this.upp_Filter.Update();
                    this.upp_SavingMsg.Update();
                }
                else if (btn.ID == this.btnIn_1.ID)
                {
                    var item = this.lbxField.SelectedItem;
                    if (item == null) return;
                    var tblName = Lib.NTE(item.GetValue("TblName"));
                    var colName = Lib.NTE(item.GetValue("ColName"));
                    var colAlias = Lib.NTE(item.GetValue("ColAliasVI"));
                    var colDataType = Lib.NTE(item.GetValue("DataType"));
                    var info = new InqFieldInfoMDX(tblName, colName, colAlias, colDataType);
                    var sel_InqFieldInfo = MySession.DSDefine_SelFieldInfo;
                    if (sel_InqFieldInfo.Exists(p => p.KeyField == info.KeyField)) return;
                    sel_InqFieldInfo.Add(info);
                    Helpers.SetDataSource(this.lbxFieldSelected, sel_InqFieldInfo, "KeyField", "ColAliasVI");
                }
                else if (btn.ID == this.btnIn_2.ID)
                {
                    var item = this.lbxMetricField.SelectedItem;
                    if (item == null) return;
                    var tblName = Lib.NTE(item.GetValue("TblName"));
                    var colName = Lib.NTE(item.GetValue("ColName"));
                    var colAlias = Lib.NTE(item.GetValue("ColAliasVI"));
                    var colDataType = Lib.NTE(item.GetValue("DataType"));
                    var fieldInfo = new InqFieldInfoMDX(tblName, colName, colAlias, colDataType);
                    var info = new InqSummaryInfoMDX(fieldInfo, "SUM", colAlias);
                    var sel_InqSummaryInfo = MySession.DSDefine_SelSumInfo;
                    if (sel_InqSummaryInfo.Exists(p => p.Field.KeyField == info.Field.KeyField)) return;
                    sel_InqSummaryInfo.Add(info);
                    Helpers.SetDataSource(this.lbxMetricFieldSelected, sel_InqSummaryInfo, "Field.KeyField", "FieldAlias");
                }
                else if (btn.ID == this.btnOut_1.ID)
                {
                    var item = lbxFieldSelected.SelectedItem;
                    lbxFieldSelected.Items.Remove(item);
                    MySession.DSDefine_SelFieldInfo.RemoveAll(p => p.KeyField == Lib.NTE(item.GetValue("KeyField")));
                }
                else if (btn.ID == this.btnOut_2.ID)
                {
                    var item = lbxMetricFieldSelected.SelectedItem;
                    lbxMetricFieldSelected.Items.Remove(item);
                    MySession.DSDefine_SelSumInfo.RemoveAll(p => p.Field.KeyField == Lib.NTE(item.GetValue("Field.KeyField")));
                }
            }
            catch { }
        }
        protected void lbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var lbx = sender as ASPxListBox;
                if (lbx == null) return;

                var item = lbx.SelectedItem;
                if (lbx.ID == this.lbxFieldSelected.ID)
                {
                    this.txtDisplayName0.Text = Lib.NTE(item.GetValue("ColAliasVI"));
                    this.cboOrderBy0.Value = item.GetValue("OrderName");
                }
                else if (lbx.ID == this.lbxMetricFieldSelected.ID)
                {
                    this.txtDisplayName.Text = Lib.NTE(item.GetValue("FieldAlias"));
                    this.cboFuncs.Value = item.GetValue("FuncName");
                    this.cboOrderBy1.Value = item.GetValue("Field.OrderName");
                }
            }
            catch { }
        }
        protected void txt_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox)
                {
                    var txt = sender as TextBox;
                    if (txt == null) return;
                    //if (txt.ID == this.txtLevel.ID)
                    //{
                    //    var item = this.lbxFieldSelected.SelectedItem;
                    //    item.SetValue("Level", int.Parse(txt.Text));
                    //}
                }
                else if (sender is ASPxTextBox)
                {
                    var txt = sender as ASPxTextBox;
                    if (txt == null) return;

                    else if (txt.ID == this.txtDisplayName0.ID)
                    {
                        var item = this.lbxFieldSelected.SelectedItem;
                        item.SetValue("ColAliasVI", txt.Text);
                    }
                    else if (txt.ID == this.txtDisplayName.ID)
                    {
                        var item = this.lbxMetricFieldSelected.SelectedItem;
                        item.SetValue("FieldAlias", txt.Text);
                    }
                }
            }
            catch { }
        }

        protected void popMen_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
        {
            this.Add_FilterControl(e.Item.Name, false);
        }
        protected void RemoveFilter(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.FilterControlSet.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrlCollect.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }

        protected void gvPreViewData_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                var inq = this.Get_DefineSource();
                foreach (var f in inq.Fields.OrderBy(P => P.Level))
                {
                    var col = new GridViewDataTextColumn();
                    col.Name = f.KeyField;
                    col.Caption = f.ColAliasVI;
                    col.FieldName = f.ColName;
                    if (new[] { "DateKey", "Period", "Quarter", "Year" }.Contains(f.ColName))
                        this.Format_GridColumn(col, ValueFormat.DateTime);
                    else
                        this.Format_GridColumn(col, ValueFormat.Normal);
                    this.gvPreViewData.Columns.Add(col);
                }
                foreach (var f in inq.Summaries)
                {
                    var col = new GridViewDataTextColumn();
                    col.Name = f.Field.KeyField;
                    col.Caption = f.FieldAlias;
                    col.FieldName = f.Get_SummaryKeyField();
                    this.Format_GridColumn(col, ValueFormat.Numeric);
                    this.gvPreViewData.Columns.Add(col);
                }
                var ds = Lib.Db.ExecuteMDX(GlobalVar.DbOLAP_ConnectionStr_Tiny, inq.ToMDX());
                //var ds = Lib.Db.ExecuteMDX(GlobalVar.DbOLAP_ConnectionStr_Tiny, "WITH MEMBER [Measures].[QuantitySUM1] AS COALESCEEMPTY([Measures].[Quantity],0) SELECT {[ARDimItem].[ItemGroupName].MEMBERS * [ARDimItem].[ItemName].MEMBERS} ON ROWS,{[Measures].[QuantitySUM1]} ON COLUMNS FROM [ARCube]");

                this.gvPreViewData.DataSource = ds;
                this.gvPreViewData.DataBind();
            }
            catch { }
        }
        protected void gvPreViewData_PageIndexChanged(object sender, EventArgs e)
        {
            if (!this.IsCurrentActive) return;
            this.gvPreViewData_CustomCallback(sender, null);
        }
        protected void gvPreViewData_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colLine")
                {
                    e.Value = e.ListSourceRowIndex + 1;
                }
            }
            catch { }
        }
        protected void cbp_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            var cbp = sender as ASPxCallbackPanel;
            if (cbp == null) return;

            var inq = this.Get_DefineSource();
            if (cbp.ID == this.cbpPreViewSQL.ID)
            {
                this.Set_PreViewMDX(inq.ToMDX(true));
            }
            else if (cbp.ID == this.cbpSavingMsg.ID)
            {
                var actionName = string.IsNullOrEmpty(MySession.DSDefine_CurEditing) ? "Add new " : "Update ";
                try
                {
                    // Gọi hàm save
                    var objDs = new lsttbl_DashboardSource()
                    {
                        Code = Lib.IfNOE(MySession.DSDefine_CurEditing, "ds_" + DateTime.Now.ToString("yyyyMMddHHmmss")),
                        NameVI = this.txtDisplayNameDS.Text,
                        NameEN = this.txtDisplayNameDS.Text,
                        JsonStr = inq.ToJsonStr(),
                        WHCode = Lib.NTE(this.cboDataDW.Value),
                        SettingCat = GlobalVar.SettingCat_DS
                    };
                    MyBI.Me.Save_DashboardSource(objDs);
                    MySession.DSDefine_CurEditing = objDs.Code;
                }
                catch { this.Set_SaveMsgText(string.Format("{0} failed!", actionName), true); }
                // Gửi trạng thái về client;            
                this.Set_SaveMsgText(string.Format("{0} success!", actionName), false);
            }
            else if (cbp.ID == this.cbp_Header.ID)
            {
                if (!string.IsNullOrEmpty(MySession.DSDefine_CurEditing))
                {
                    var ds = MyBI.Me.Get_DashboardSource(this.WHCode, GlobalVar.SettingCat_DS).ToList();
                    Helpers.SetDataSource(this.cboDatasource, ds, "Code", "NameVI", MySession.DSDefine_CurEditing);
                }
            }
        }
        #endregion
    }
}