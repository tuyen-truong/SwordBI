using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcKPIContextMetric : KPIPartCtrlBase
    {
        #region Declares
        protected List<string> CtrlFilterIDs
        {
            get
            {
                if (ViewState["CtrlFilterIDs"] == null)
                    ViewState["CtrlFilterIDs"] = new List<string>();
                return ViewState["CtrlFilterIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlFilterIDs"] = value;
                if (value == null) GC.Collect();
            }
        }
        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        #endregion

        public override void Load_InitData()
        {
            if (Page.IsPostBack)
                this.Add_FilterControl(null, true);
            this.cboAggregator.Items.Clear();
            this.cboAggregator.Items.AddRange(InqMDX.GetSummatyFuncName());
        }
        public override void Set_Source(object ds)
        {
            try
            {
                this.Reset_Info();
                var myDs = ds as lsttbl_DashboardSource;
                var obj = myDs.JsonObjMDX;
                Helpers.SetDataSource(this.cboField, obj.Summaries, "Field.ColName", "FieldAlias", this.cboField.Value);
            }
            catch { }
        }
        public override void Set_Info(KPIField info)
        {
            try
            {
                var myInfo = info as KPICtxtMetric;
                this.cboField.Value = myInfo.FieldName;
                this.txtDisplayName.Text = myInfo.DisplayName;
                this.cboAggregator.Value = myInfo.Aggregator;
                // Add filter...
                foreach (var filter in myInfo.Filters)
                {
                    var myCtrl = this.Add_FilterControl(filter.GetTinyType(), false);
                    myCtrl.Set_Info(filter);
                }
                if (!string.IsNullOrEmpty(myInfo.TimeFilterPrev))
                {
                    var myCtrl = this.Add_FilterControl("PREVDATE", false);
                    myCtrl.Set_Info_General(new COMCodeNameObj(myInfo.TimeFilterPrev, "AND"));
                }
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            this.cboField.Items.Clear();
            this.cboField.Text = string.Empty;
            this.txtDisplayName.Text = "";
        }
        public override KPIField Get_KPIPartInfo()
        {
            var filters = new List<InqFilterInfoMDX>();
            var timeFilterPrev = "";
            foreach (FilterCtrlBase ctrl in this.ctrl_Filters.Controls)
            {
                if (ctrl == null) continue;
                var infoGeneral = ctrl.Get_FilterInfo_General();
                if (infoGeneral != null)
                    timeFilterPrev = (infoGeneral as COMCodeNameObj).Code;
                else
                    filters.Add(ctrl.Get_FilterInfo());
            }
            var ret = new KPICtxtMetric
            {
                FieldName = Lib.NTE(this.cboField.Value),
                DisplayName = this.txtDisplayName.Text,
                Aggregator = Lib.NTE(this.cboAggregator.Value),
                Filters = filters,
                TimeFilterPrev = timeFilterPrev
            };
            return ret;
        }
        private FilterCtrlBase Add_FilterControl(string type, bool isReCreate)
        {
            if (string.IsNullOrEmpty(type)) type = "NORMAL";
            var guiID = Guid.NewGuid().ToString();
            FilterCtrlBase ctrl = null;
            var bizCat = this.MyPage.WHCode;
            var tblFactNames = MyBI.Me.Get_DWTableName("FACT", bizCat);
            var ds = MyBI.Me.Get_DWColumn(bizCat);
            var dsField = new List<lsttbl_DWColumn>();

            // ReCreate...
            if (isReCreate)
            {
                foreach (var obj in this.CtrlFilterIDs)
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
                    else if (ctrlType == "PREVDATE")
                    {
                        ctrl = this.LoadControl("wcTimeFilterPrev.ascx") as wcTimeFilterPrev;
                    }
                    else
                    {
                        ctrl = this.LoadControl("wcNormalFilter.ascx") as wcNormalFilter;
                        dsField = ds.Where(p => p.Visible && p.DataType != "NUM" && p.DataType != "DATE").ToList();
                    }
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.RemoveFilter;
                    ctrl.Set_Source(dsField, "KeyField", "ColAliasVI");
                    ctrl.Set_VisibleMember(FilterCtrlBase.Members.AndOr, false);
                    this.ctrl_Filters.Controls.Add(ctrl);
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
            else if (type == "PREVDATE")
            {
                ctrl = this.LoadControl("wcTimeFilterPrev.ascx") as wcTimeFilterPrev;
            }
            else // normal
            {
                ctrl = this.LoadControl("wcNormalFilter.ascx") as wcNormalFilter;
                dsField = ds.Where(p =>
                        p.Visible && p.DataType != "NUM" && p.DataType != "DATE"
                ).ToList();
            }
            ctrl.ID = string.Format("gen_{1}_{0}_{1}", guiID, type);
            ctrl.OnRemove += this.RemoveFilter;
            ctrl.Set_Source(dsField, "KeyField", "ColAliasVI");
            ctrl.Set_VisibleMember(FilterCtrlBase.Members.AndOr, false);
            this.ctrl_Filters.Controls.Add(ctrl);
            this.CtrlFilterIDs.Add(string.Format("{0},{1}", ctrl.ID, type));
            return ctrl;
        }

        protected void ctrl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is ASPxComboBox)
                {
                    var ctrl = sender as ASPxComboBox;
                    if (ctrl.ID == this.cboField.ID)
                    {
                        this.txtDisplayName.Text = ctrl.Text;
                        this.txtDisplayName.Focus();
                        this.cboAggregator.Value = InqMDX.GetSummatyFuncName().FirstOrDefault(p => !string.IsNullOrEmpty(p));
                    }
                }
            }
            catch { }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var ctrlID = string.Format("CtrlFilter_{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            //    var ctrl = this.LoadControl("wcNumFilter.ascx") as wcNumFilter;
            //    ctrl.ID = ctrlID;
            //    ctrl.OnRemove += this.RemoveFilter;
            //    this.ctrl_Filters.Controls.Add(ctrl);
            //    this.CtrlFilterIDs.Add(ctrlID);
            //}
            //catch { }
        }
        protected void popMen_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
        {
            this.Add_FilterControl(e.Item.Name, false);
        }
        private void RemoveFilter(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CtrlFilterIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrl_Filters.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }
    }
}