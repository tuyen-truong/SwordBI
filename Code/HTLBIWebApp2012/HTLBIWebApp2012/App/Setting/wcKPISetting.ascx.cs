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
using DevExpress.XtraCharts;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.UserControls;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcKPISetting : PartPlugCtrlBase
    {
        #region Declares
        public Control My_upp_Dimension { get { return this.upp_Dimension; } }
        public Control My_upp_Measures { get { return this.upp_Measures; } }
        public Control My_upp_ContextMetrics { get { return this.upp_ContextMetrics; } }

        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        /// <summary>
        /// Nếu trường hợp có chọn KPI thì không lấy DatasourceID mới mà lấy DatasourceID từ KPI đã lưu.
        /// <para>Ngược lại thì lấy DatasourceID theo bên Tab DatasourceSettings.</para>
        /// </summary>
        public string DSCode_Target
        {
            get
            {
                var curKPI = this.Get_KPI();
                return (curKPI != null) ? curKPI.ParentCode : this.MyPage.DSCode;
            }
        }
        public string KPICode { get { return Lib.NTE(this.cboKPI.Value); } }
        public bool IsCurrentActive
        {
            get
            {
                if (MyPage == null) return false;
                var curTabPageName = MyPage.CurrentActiveTabPage.Name;
                curTabPageName = curTabPageName.Replace("tabPage_", "");
                return curTabPageName.ToLower() == "KPISetting".ToLower();
            }
        }
        protected List<string> CtrlKPIPartIDs
        {
            get
            {
                if (ViewState["CtrlKPIPartIDs"] == null)
                    ViewState["CtrlKPIPartIDs"] = new List<string>();
                return ViewState["CtrlKPIPartIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlKPIPartIDs"] = value;
                if (value == null) GC.Collect();
            }
        }
        protected List<string> CtrlKPIFilterIDs
        {
            get
            {
                if (ViewState["CtrlKPIFilterIDs"] == null)
                    ViewState["CtrlKPIFilterIDs"] = new List<string>();
                return ViewState["CtrlKPIFilterIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlKPIFilterIDs"] = value;
                if (value == null) GC.Collect();
            }
        }
        #endregion

        #region Members
        public override void Load_InitData()
        {
            this.OnChange += this_OnChange;
            this.Register_JavaScript();
            if (!this.IsPostBack)
            {
                // Control Type...
                var imgPathF = "~/Images/Control/{0}.png";
                this.cboCtrlType.Items.Clear();
                foreach (var item in Helpers.ArrDashboardCtrlType)
                {
                    var img = string.Format(imgPathF, item.Cat);
                    var litem = new ListEditItem(item.Name, item.Code, img);
                    this.cboCtrlType.Items.Add(litem);
                }
            }
            else
            {
                // Tải lại source cho cboKPI(để nó không bị thiếu khi vừa thêm mới 1 KPI trong sự kiện của CallbackPanel)
                // Vì cơ chế của CallbackPanel sẽ không để lại ViewState mỗi lần Render
                if (!string.IsNullOrEmpty(this.MyPage.WHCode))
                {
                    var kpis = MyBI.Me.Get_DashboardKPI_ByWH(this.MyPage.WHCode).ToList();
                    Helpers.SetDataSource(this.cboKPI, kpis, "Code", "NameVI", this.cboKPI.Value);
                }
                // Tạo lại control....
                this.Add_PartControl(null, true);
                this.Add_FilterControl(null, true);
            }
        }
        public void Register_JavaScript()
        {
            try
            {
                ///////////////// Register common...
                this.RegisterStartupScript(this.upp_Header,
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
                var code = this.DSCode_Target;
                if (string.IsNullOrEmpty(code)) return null;
                var ret = MyBI.Me.Get_DashboardSourceBy(code);
                return ret;
            }
            catch { return null; }
        }
        public lsttbl_DashboardSource Get_KPI()
        {
            try
            {
                var code = this.KPICode;
                if (string.IsNullOrEmpty(code)) return null;
                var ret = MyBI.Me.Get_DashboardSourceBy(code);
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
        private KPIPartCtrlBase Add_PartControl(string type, bool isReCreate)
        {
            if (string.IsNullOrEmpty(this.DSCode_Target))
            {
                this.tabCtrl_PortletSetting.ActiveTabIndex = 0;
                return null;
            }

            try
            {
                var guiID = Guid.NewGuid().ToString();
                var ctrlID = string.Format("gen_{1}_{0}", guiID, type);
                var ctrlID_Store = string.Format("{0},{1}", ctrlID, type);
                KPIPartCtrlBase ctrl = null;
                Control ctrlContainer = null;

                if (isReCreate)
                {
                    foreach (var iD_Type in this.CtrlKPIPartIDs)
                    {
                        var arr = iD_Type.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        ctrlID = arr.First();
                        var ctrlType = arr.Last();
                        if (ctrlType.StartsWith("dimension"))
                        {
                            ctrl = this.LoadControl("wcKPIDimension.ascx") as KPIPartCtrlBase;
                            ctrlContainer = this.ctrl_Dimensions;
                        }
                        else if (ctrlType.StartsWith("measure"))
                        {
                            ctrl = this.LoadControl("wcKPIMeasure.ascx") as KPIPartCtrlBase;
                            ctrlContainer = this.ctrl_Measures;
                        }
                        else if (ctrlType.StartsWith("context"))
                        {
                            var childCtrlType = ctrlType.Split('-', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                            if (childCtrlType == "Normal")
                                ctrl = this.LoadControl("wcKPIContextMetric.ascx") as KPIPartCtrlBase;
                            else if (childCtrlType == "Calc")
                                ctrl = this.LoadControl("wcKPIContextMetric_Calc.ascx") as KPIPartCtrlBase;
                            ctrlContainer = this.ctrl_ContextMetric;
                        }
                        ctrl.ID = ctrlID;
                        ctrl.OnRemove += this.PartCtrl_Remove;
                        // Set datasource.
                        var ds = this.Get_Datasource();
                        if (ds != null) ctrl.Set_Source(ds);
                        ctrlContainer.Controls.Add(ctrl);
                    }
                    return null;
                }

                if (string.IsNullOrEmpty(type)) return null;
                if (type.StartsWith("dimension"))
                {
                    ctrl = this.LoadControl("wcKPIDimension.ascx") as KPIPartCtrlBase;
                    ctrlContainer = this.ctrl_Dimensions;
                }
                else if (type.StartsWith("measure"))
                {
                    ctrl = this.LoadControl("wcKPIMeasure.ascx") as KPIPartCtrlBase;
                    ctrlContainer = this.ctrl_Measures;
                }
                else if (type.StartsWith("context"))
                {
                    var childType = type.Split('-', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                    if (childType == "Normal")
                        ctrl = this.LoadControl("wcKPIContextMetric.ascx") as KPIPartCtrlBase;
                    else if (childType == "Calc")
                        ctrl = this.LoadControl("wcKPIContextMetric_Calc.ascx") as KPIPartCtrlBase;
                    ctrlContainer = this.ctrl_ContextMetric;
                }
                ctrl.ID = ctrlID;
                this.CtrlKPIPartIDs.Add(ctrlID_Store);
                ctrl.OnRemove += this.PartCtrl_Remove;
                // Set datasource.
                var ds1 = this.Get_Datasource();
                if (ds1 != null) ctrl.Set_Source(ds1);
                ctrlContainer.Controls.Add(ctrl);
                return ctrl;
            }
            catch { }
            return null;
        }
        private KPIDefineSource Get_DefineInfo()
        {
            try
            {
                // Nếu trường hợp có chọn KPI thì không lấy DatasourceID mới mà lấy DatasourceID từ KPI đã lưu. 
                // Ngược lại thì lấy DatasourceID theo bên Tab DatasourceSettings.
                var ret = new KPIDefineSource()
                {
                    DisplayName = this.txtKPIDisplayName.Text,
                    DatasourceID = this.DSCode_Target,
                    CtrlTypeDefault = Lib.IsNOE(cboCtrlType.Value) ? "" : Lib.NTE(this.cboCtrlType.Value),
                    VisibleTypeDefault = Lib.IsNOE(cboCtrl.Value)? "" : Lib.NTE(cboCtrl.Value).Split('-', StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
                    MaxValue = double.Parse(this.txtMaxValue.Text),
                    MinValue = double.Parse(this.txtMinValue.Text)
                };
                // Lấy thông tin Dimension.
                foreach (KPIPartCtrlBase ctrl in this.ctrl_Dimensions.Controls)
                {
                    if (ctrl == null) continue;
                    ret.AddDimension(ctrl.Get_KPIPartInfo());
                }
                // Lấy thông tin Measure.
                foreach (KPIPartCtrlBase ctrl in this.ctrl_Measures.Controls)
                {
                    if (ctrl == null) continue;
                    ret.AddMeasure(ctrl.Get_KPIPartInfo());
                }
                // Lấy thông tin ContextMetric.
                foreach (KPIPartCtrlBase ctrl in this.ctrl_ContextMetric.Controls)
                {
                    if (ctrl == null) continue;
                    ret.AddContext(ctrl.Get_KPIPartInfo());
                }
                // Lấy thông tin Filter KPI.
                foreach (FilterCtrlBase ctrl in this.ctrl_KPIFilters.Controls)
                {
                    if (ctrl == null) continue;
                    ret.AddFilter(ctrl.Get_FilterInfo());
                }
                return ret;
            }
            catch { return null; }
        }
        private FilterCtrlBase Add_FilterControl(string type, bool isReCreate)
        {
            if (string.IsNullOrEmpty(type)) type = "NORMAL";
            var guiID = Guid.NewGuid().ToString();
            FilterCtrlBase ctrl = null;
            var whCode = this.MyPage.WHCode;
            var tblFactNames = MyBI.Me.Get_DWTableName("FACT", whCode);
            var ds = MyBI.Me.Get_DWColumn(whCode);
            var dsField = new List<lsttbl_DWColumn>();
            // ReCreate...
            if (isReCreate)
            {
                foreach (var obj in this.CtrlKPIFilterIDs)
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
                        dsField = ds.Where(p => p.Visible && p.DataType != "NUM" && p.DataType != "DATE").ToList();
                    }
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.FilterCtrl_Remove;
                    ctrl.Set_Source(dsField, "KeyField", "ColAliasVI");
                    this.ctrl_KPIFilters.Controls.Add(ctrl);
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
                        p.Visible && p.DataType != "NUM" && p.DataType != "DATE"
                    ).ToList();
            }
            ctrl.ID = string.Format("gen_{1}_{0}_{1}", guiID, type);
            ctrl.OnRemove += this.FilterCtrl_Remove;
            ctrl.Set_Source(dsField, "KeyField", "ColAliasVI");
            this.ctrl_KPIFilters.Controls.Add(ctrl);
            this.CtrlKPIFilterIDs.Add(string.Format("{0},{1}", ctrl.ID, type));
            return ctrl;
        }
        private void Clear_AllPartCtrl()
        {
            try
            {
                foreach (KPIPartCtrlBase ctrl in this.ctrl_Dimensions.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Reset_Info();
                }
                foreach (KPIPartCtrlBase ctrl in this.ctrl_Measures.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Reset_Info();
                }
                foreach (KPIPartCtrlBase ctrl in this.ctrl_ContextMetric.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Reset_Info();
                }
                foreach (FilterCtrlBase ctrl in this.ctrl_KPIFilters.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Reset_Info();
                }
            }
            catch { }
            try
            {
                this.ctrl_Dimensions.Controls.Clear();
                this.ctrl_Measures.Controls.Clear();
                this.ctrl_ContextMetric.Controls.Clear();
                this.ctrl_KPIFilters.Controls.Clear();
                this.CtrlKPIPartIDs.Clear();
                this.CtrlKPIFilterIDs.Clear();
            }
            catch { }
        }
        private void SetSource_AllPartCtrl()
        {
            try
            {
                var ds = this.Get_Datasource();
                if (ds == null) return;
                foreach (KPIPartCtrlBase ctrl in this.ctrl_Dimensions.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Set_Source(ds);
                }
                foreach (KPIPartCtrlBase ctrl in this.ctrl_Measures.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Set_Source(ds);
                }
                foreach (KPIPartCtrlBase ctrl in this.ctrl_ContextMetric.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Set_Source(ds);
                }
            }
            catch { }
        }
        private void Set_PreViewMDX(string mdx)
        {
            //this.txtPreViewSQL.Font.Bold = true;
            this.txtPreViewSQL.Font.Name = "Arial";
            this.txtPreViewSQL.Font.Size = new FontUnit(11, UnitType.Point);
            this.txtPreViewSQL.Text = mdx;
        }
        private void Set_SaveMsgText(string msg, bool isError)
        {
            this.lblSavingMsg.Font.Bold = true;
            this.lblSavingMsg.Font.Italic = true;
            this.lblSavingMsg.Font.Name = "Arial";
            this.lblSavingMsg.ForeColor = isError ? Color.Red : Color.Blue;
            this.lblSavingMsg.Text = msg;
        }
        private string Get_ValidMsg(KPIDefineSource obj)
        {
            var ret = "";
            var newLine = "\r\n";
            if (obj.Dimensions.Count == 0)
                ret += "Dimensions is Required!\r\n";
            if (obj.Measures.Count == 0)
                ret += "Measures is Required!\r\n";
            if (ret.EndsWith(newLine))
                ret = ret.Remove(ret.Length - newLine.Length);
            return ret;
        }
        #endregion

        #region Events
        /// <summary>
        /// Hàm được gọi khi có dữ liệu thay đổi và chuyển về từ nơi khác.
        /// </summary>
        protected void this_OnChange(object sender, EventArgs e)
        {
            if (!(sender is string)) return;
            var cat = sender.ToString();
            if (cat == "WH")
            {
                var whCode = this.MyPage.WHCode;
                if (!string.IsNullOrEmpty(whCode))
                {
                    MySession.KPIDefine_CurEditing = null;
                    this.Clear_AllPartCtrl();
                    this.cboKPI.Items.Clear();
                    this.cboKPI.Text = "";
                    // Load Available KPI By Warehouse.
                    var kpis = MyBI.Me.Get_DashboardKPI_ByWH(whCode).ToList();
                    Helpers.SetDataSource(this.cboKPI, kpis, "Code", "NameVI");
                }
            }
            else if (cat == "LAYOUT")
            {
                var eReceived = e as HTLBIEventArgs;
                if (eReceived == null) return;
                this.cboKPI.Value = eReceived.Values.ToString();
                this.cbo_ValueChanged(this.cboKPI, null);
            }
            else if (cat == "RESET")
            {
                this.btn_Click(this.btnNewKPI, null);
            }
        }
        protected void popMenAddCalcField_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
        {
            this.Add_PartControl(string.Format("context-{0}", e.Item.Name), false);
        }
        protected void PartCtrl_Remove(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CtrlKPIPartIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrl_Dimensions.Controls.RemoveAll(p => p.ID == ctrlID);
                this.ctrl_Measures.Controls.RemoveAll(p => p.ID == ctrlID);
                this.ctrl_ContextMetric.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }
        protected void popMenAddFilter_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
        {
            this.Add_FilterControl(e.Item.Name, false);
        }
        protected void FilterCtrl_Remove(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CtrlKPIFilterIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrl_KPIFilters.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }
        protected void cbo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var cbo = sender as ASPxComboBox;
                if (cbo == null) return;
                if (cbo.ID == this.cboKPI.ID)
                {
                    var item = this.Get_KPI();
                    if (item == null) return;

                    MySession.KPIDefine_CurEditing = item.Code;
                    this.txtKPIDisplayName.Text = item.NameVI;
                    var kpi = item.JsonObjKPI;

                    // Clear part control
                    this.Clear_AllPartCtrl();

                    // Set value to control
                    if (!string.IsNullOrEmpty(kpi.CtrlTypeDefault) && !string.IsNullOrEmpty(kpi.VisibleTypeDefault))
                    {
                        this.cboCtrlType.Value = kpi.CtrlTypeDefault;
                        cbo_ValueChanged(this.cboCtrlType, null);
                        this.cboCtrl.Value = string.Format("{0}-{1}", kpi.CtrlTypeDefault, kpi.VisibleTypeDefault);
                    }
                    // Add new Part controls of KPI is selected
                    foreach (var part in kpi.Dimensions)
                    {
                        var myCtrl = this.Add_PartControl("dimension", false);
                        myCtrl.Set_Info(part);
                    }
                    foreach (var part in kpi.Measures)
                    {
                        var myCtrl = this.Add_PartControl("measure", false);
                        myCtrl.Set_Info(part);
                    }
                    foreach (var part in kpi.Contexts)
                    {
                        var type = "";
                        if (part.HasCalcFields())
                            type = "context-Calc";
                        else
                            type = "context-Normal";
                        var myCtrl = this.Add_PartControl(type, false);
                        myCtrl.Set_Info(part);
                    }
                    foreach (var part in kpi.Filters)
                    {
                        var myCtrl = this.Add_FilterControl(part.GetTinyType(), false);
                        myCtrl.Set_Info(part);
                    }
                    // Raise Event OnChange.
                    this.MyPage.My_wcLayoutSetting.Raise_OnChange("KPI", null);
                    this.MyPage.My_wcDSSetting.Raise_OnChange("KPI", new HTLBIEventArgs(item.ParentCode));
                }
                else if (cbo.ID == this.cboCtrlType.ID)
                {
                    var arrCtrl = Helpers.ArrDashboardCtrl.Where(p => p.Cat == Lib.NTE(cbo.Value));
                    var imgPathF = "~/Images/Control/{0}.png";

                    this.cboCtrl.Items.Clear();
                    this.cboCtrl.Text = "";
                    foreach (var item in arrCtrl)
                    {
                        var value = string.Format("{0}-{1}", item.Cat, item.Code);
                        var img = string.Format(imgPathF, value);
                        var litem = new ListEditItem(item.Name, value, img);
                        this.cboCtrl.Items.Add(litem);
                    }
                    this.cboCtrl.SelectedIndex = 0;
                }
            }
            catch { }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ASPxButton;
                if (btn.ID == this.btnNewKPI.ID)
                {
                    MySession.KPIDefine_CurEditing = null;
                    // Clear General.
                    this.cboKPI.Text = "";
                    this.txtKPIDisplayName.Text = "";
                    this.cboCtrlType.Text = "";
                    this.cboCtrl.Text = "";
                    this.lblSavingMsg.Text = "";
                    // Clear Plug control.
                    this.Clear_AllPartCtrl();
                    if (string.IsNullOrEmpty(this.MyPage.DSCode))
                        this.tabCtrl_PortletSetting.ActiveTabIndex = 0;
                    // Raise Event OnChange.
                    this.Raise_OnChange(new { Cat = "KPI", Ctrl = this }, null);
                }
                else if (btn.ID == this.btnAddDimension.ID)
                    this.Add_PartControl("dimension", false);
                else if (btn.ID == this.btnAddMeasure.ID)
                    this.Add_PartControl("measure", false);
            }
            catch { }
        }
        protected void cbp_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            try
            {
                var cbp = sender as ASPxCallbackPanel;
                if (cbp == null) return;

                if (cbp.ID == this.cbpPreViewSQL.ID || cbp.ID == this.cbpSavingMsg.ID)
                {
                    var objSett = this.Get_DefineInfo();
                    if (objSett == null) return;
                    if (cbp.ID == this.cbpPreViewSQL.ID)
                    {                        
                        var inqObj = objSett.ReFilter_InqMDX();
                        this.Set_PreViewMDX(inqObj.ToMDX(true));
                    }
                    else if (cbp.ID == this.cbpSavingMsg.ID)
                    {
                        var actionName = Lib.IsNOE(MySession.KPIDefine_CurEditing) ? "Add new " : "Update ";
                        try
                        {
                            // Gọi hàm save
                            var obj = new lsttbl_DashboardSource()
                            {
                                Code = Lib.IfNOE(MySession.KPIDefine_CurEditing, "kpi_" + DateTime.Now.ToString("yyyyMMddHHmmss")),
                                ParentCode = objSett.DatasourceID,
                                JsonStr = objSett.ToJsonStr(),
                                NameVI = this.txtKPIDisplayName.Text,
                                NameEN = this.txtKPIDisplayName.Text,
                                WHCode = this.MyPage.WHCode,
                                SettingCat = GlobalVar.SettingCat_KPI
                            };
                            MyBI.Me.Save_DashboardSource(obj);
                            MySession.KPIDefine_CurEditing = obj.Code;
                        }
                        catch { this.Set_SaveMsgText(string.Format("{0} failed!", actionName), true); }
                        // Gửi trạng thái về client;            
                        this.Set_SaveMsgText(string.Format("{0} success!", actionName), false);
                    }
                }
                else if (cbp.ID == this.cbp_Header.ID)
                {
                    if (!string.IsNullOrEmpty(MySession.KPIDefine_CurEditing))
                    {
                        var kpis = MyBI.Me.Get_DashboardKPI_ByWH(this.MyPage.WHCode).ToList();
                        Helpers.SetDataSource(this.cboKPI, kpis, "Code", "NameVI", MySession.KPIDefine_CurEditing);
                    }
                }
            }
            catch { }
        }
        protected void gvPreViewData_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                var kpi = this.Get_DefineInfo();
                var inq = kpi.ReFilter_InqMDX();
                inq.Reset_SummariesID();
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
                foreach (var f in inq.CalcMembers)
                {
                    var col = new GridViewDataTextColumn();
                    col.Name = f.Code;
                    col.Caption = f.Cat;
                    col.FieldName = f.Code;
                    this.Format_GridColumn(col, ValueFormat.Numeric);
                    this.gvPreViewData.Columns.Add(col);
                }
                var ds = Lib.Db.ExecuteMDX(GlobalVar.DbOLAP_ConnectionStr_Tiny, inq.ToMDX());
                this.gvPreViewData.DataSource = ds;
                this.gvPreViewData.DataBind();
            }
            catch { }
        }
        protected void gvPreViewData_PageIndexChanged(object sender, EventArgs e)
        {
            this.gvPreViewData_CustomCallback(sender, null);
        }
        protected void gvPreViewData_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colLine")
                    e.Value = e.ListSourceRowIndex + 1;
            }
            catch { }
        }        
        #endregion
    }
}