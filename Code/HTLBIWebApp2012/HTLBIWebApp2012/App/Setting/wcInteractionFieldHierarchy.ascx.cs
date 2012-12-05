using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcInteractionFieldHierarchy : PartPlugCtrlBase
    {
        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        protected List<string> CtrlHierarchyFieldIDs
        {
            get
            {
                if (ViewState["CtrlHierarchyFieldIDs"] == null)
                    ViewState["CtrlHierarchyFieldIDs"] = new List<string>();
                return ViewState["CtrlHierarchyFieldIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlHierarchyFieldIDs"] = value;
                if (value == null) GC.Collect();
            }
        }

        public override void Load_InitData()
        {
            if (this.IsPostBack)
            {
                //Tạo lại control....
                this.Add_HierarchyFieldControl(true);
            }
            try
            {
                if (string.IsNullOrEmpty(MyPage.LayoutCode)) return;
                var fieldSelVal = this.cbbFieldHierarchy1.Value;
                this.cbbFieldHierarchy1.Items.Clear();
                this.cbbFieldHierarchy1.Text = "";
                var wgObj = MyBI.Me.Get_Widget_ByCode(MyPage.LayoutCode);
                if (wgObj.WidgetType == "chart")
                {
                    var wgChart = wgObj.JsonObj_Chart;
                    var field = wgChart.XFields.FirstOrDefault();
                    var ds = MyBI.Me.Get_DWColumn_Childrens(this.MyPage.WHCode, field)
                        .Select(p => new { Code = p.ColName, Name = p.ColAliasVI }).ToList();
                    Helpers.SetDataSource(this.cbbFieldHierarchy1, ds, "Code", "Name", fieldSelVal);
                }
            }
            catch { }
        }
        public List<string> Get_HierarchyInfo()
        {
            try
            {
                var ret = new List<string>();
                if (!Lib.IsNOE(this.cbbFieldHierarchy1.Value))
                    ret.Add(Lib.NTE(this.cbbFieldHierarchy1.Value));
                foreach (ASPxComboBox p in this.ctrl_HierarchyField.Controls)
                {
                    if (Lib.IsNOE(p.Value)) continue;
                    ret.Add(Lib.NTE(p.Value));
                }
                return ret;
            }
            catch { return null; }
        }
        public void Set_Enable(bool isEnable)
        {
            try
            {
                this.cbbFieldHierarchy1.ClientEnabled = isEnable;
                foreach (ASPxComboBox cbb in this.ctrl_HierarchyField.Controls)
                    cbb.ClientEnabled = isEnable;
            }
            catch { }
        }
        public void Reset_HierarchyInfo()
        {
            this.ctrl_HierarchyField.Controls.Clear();
            this.CtrlHierarchyFieldIDs.Clear();
            this.cbbFieldHierarchy1.Items.Clear();
            this.cbbFieldHierarchy1.Text = "";
            this.Set_Enable(false);
        }
        public void Set_HierarchyInfo(InteractionDefine info)
        {
            // Reset infor.
            this.Reset_HierarchyInfo();
            // set infor.
            this.Set_Enable(info.DrilldownCategory == "In" || info.DrilldownCategory == "Popup");
            if (!info.HasHierarchyFields()) return;

            var fieldStep = info.HierarchyFields.First();
            var wgObj = MyBI.Me.Get_Widget_ByCode(MyPage.LayoutCode);
            if (wgObj.WidgetType == "chart")
            {
                var wgChart = wgObj.JsonObj_Chart;
                var ds = MyBI.Me.Get_DWColumn_Childrens(this.MyPage.WHCode, wgChart.XFields.FirstOrDefault())
                    .Select(p => new { Code = p.ColName, Name = p.ColAliasVI }).ToList();
                Helpers.SetDataSource(this.cbbFieldHierarchy1, ds, "Code", "Name", fieldStep);
                info.HierarchyFields.Remove(fieldStep);
            }
            foreach (var field in info.HierarchyFields)
            {
                var ds = MyBI.Me.Get_DWColumn_Childrens(this.MyPage.WHCode, fieldStep)
                     .Select(p => new { Code = p.ColName, Name = p.ColAliasVI }).ToList();
                if (ds.Count > 0)
                {
                    var ctrl = this.Add_HierarchyFieldControl(false);
                    Helpers.SetDataSource(ctrl, ds, "Code", "Name", field);
                }
                fieldStep = field;
            }
            //this.cbbFieldHierarchy1.ClientEnabled = false;
        }
        private ASPxComboBox Add_HierarchyFieldControl(bool isReCreate)
        {
            var guiID = Guid.NewGuid().ToString();
            var toolTipText = "Field Hierarchy level {0}.";
            ASPxComboBox ctrl = null;
            var dsFields = MyBI.Me.Get_DWColumn(this.MyPage.WHCode)
                .Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime"))
                .Select(p => new COMCodeNameObj(p.ColName, p.ColAliasVI)).ToList();
            // ReCreate...
            if (isReCreate)
            {
                var count = 2;
                foreach (var ctrlID in this.CtrlHierarchyFieldIDs)
                {
                    ctrl = new ASPxComboBox();
                    ctrl.ID = ctrlID;
                    ctrl.ToolTip = string.Format(toolTipText, count);
                    ctrl.ValueChanged += cbb_ValueChanged;
                    ctrl.AutoPostBack = true;
                    ctrl.Width = new Unit(100, UnitType.Percentage);
                    this.ctrl_HierarchyField.Controls.Add(ctrl);
                    count++;
                }
                return null;
            }
            // Add new...
            ctrl = new ASPxComboBox();
            ctrl.ID = string.Format("gen_{0},{1}", guiID, this.CtrlHierarchyFieldIDs.Count + 1);
            ctrl.ToolTip = string.Format(toolTipText, this.CtrlHierarchyFieldIDs.Count + 2);
            ctrl.ValueChanged += cbb_ValueChanged;
            ctrl.AutoPostBack = true;
            ctrl.Width = new Unit(100, UnitType.Percentage);
            this.ctrl_HierarchyField.Controls.Add(ctrl);
            this.CtrlHierarchyFieldIDs.Add(ctrl.ID);
            return ctrl;
        }

        protected void cbb_ValueChanged(object sender, EventArgs e)
        {
            var ctrl = sender as ASPxComboBox;
            if (ctrl == null) return;            
            try
            {
                if (ctrl.ID == this.cbbFieldHierarchy1.ID && this.CtrlHierarchyFieldIDs.Count > 0) return;
                if (ctrl.ID.Contains(',') && int.Parse(ctrl.ID.Split(',').Last()) < this.CtrlHierarchyFieldIDs.Count) return;
                var field = Lib.NTE(ctrl.Value);
                var ds = MyBI.Me.Get_DWColumn_Childrens(this.MyPage.WHCode, field)
                    .Select(p => new { Code = p.ColName, Name = p.ColAliasVI }).ToList();
                if (ds.Count > 0)
                {
                    var cbb = this.Add_HierarchyFieldControl(false);
                    Helpers.SetDataSource(cbb, ds, "Code", "Name");
                }
            }
            catch { }
        }
    }
}