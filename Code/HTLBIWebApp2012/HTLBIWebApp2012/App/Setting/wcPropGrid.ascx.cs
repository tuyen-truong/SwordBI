using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.BLL;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.UserControls;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcPropGrid : PropCtrlBase
    {
        #region Declares
        protected List<string> CurCtrlColumn
        {
            get
            {
                if (ViewState["CurCtrlColumn"] == null)
                    ViewState["CurCtrlColumn"] = new List<string>();
                return ViewState["CurCtrlColumn"] as List<string>;
            }
            set
            {
                ViewState["CurCtrlColumn"] = value;
                if (value == null) GC.Collect();
            }
        }
        protected string CurCtrlPreView
        {
            get
            {
                if (ViewState["CurCtrlPreView"] == null)
                    ViewState["CurCtrlPreView"] = "";
                return ViewState["CurCtrlPreView"] as string;
            }
            set
            {
                ViewState["CurCtrlPreView"] = value;
                if (value == null) GC.Collect();
            }
        }
        #endregion

        #region Members
        public override void Load_InitData()
        {
            // Tạo lại control Column....
            this.AddNew_Column(false, true);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            // Tạo lại control preview....            
            try
            {
                if (!string.IsNullOrEmpty(this.CurCtrlPreView))
                {
                    var ctrl = this.LoadControl("../UserControls/wcGrid.ascx") as wcGrid;
                    ctrl.ID = this.CurCtrlPreView;
                    ctrl.IsDemo = true;
                    ctrl.Sett = GlobalSsn.WidgetGrid_SsnModel;
                    this.ctrlPreView.Controls.Clear();
                    this.ctrlPreView.Controls.Add(ctrl);
                }
            }
            catch { }
        }
        public wcPropGrid_Column AddNew_Column(bool isAddOnSetInfo, bool isReCreate)
        {
            wcPropGrid_Column ctrl = null;
            try
            {
                // Tạo lại...
                if (isReCreate)
                {
                    if (this.CurCtrlColumn == null || this.CurCtrlColumn.Count == 0) return null;
                    this.ctrlColumn.Controls.Clear();
                    foreach (var ctrlID in this.CurCtrlColumn)
                    {
                        ctrl = this.LoadControl("wcPropGrid_Column.ascx") as wcPropGrid_Column;
                        ctrl.ID = ctrlID;
                        ctrl.OnRemove += this.RemoveColumn;
                        this.ctrlColumn.Controls.Add(ctrl);
                    }
                    return null;
                }
                // Tạo mới...
                var newCtrlID = string.Format("Col_{0}", Guid.NewGuid().ToString());
                ctrl = this.LoadControl("wcPropGrid_Column.ascx") as wcPropGrid_Column;
                ctrl.ID = newCtrlID;
                ctrl.Set_DataSource(GlobalSsn.WidgetGrid_SsnModel.Get_BIDatasource());
                ctrl.OnRemove += this.RemoveColumn;
                this.ctrlColumn.Controls.Add(ctrl);
                this.CurCtrlColumn.Add(newCtrlID);
                if (!isAddOnSetInfo)
                    GlobalSsn.WidgetGrid_SsnModel.Columns.Add(ctrl.Get_GridColumn());
            }
            catch { }
            return ctrl;
        }
        public override void Set_ViewType(string viewType)
        {
            // Tạo mới một ID cho grid tương tứng với viewType.(Control sẽ được Add vào tập hợp ở hàm sự kiện 'OnPreRender')
            var ctrlID = string.Format("gridPreView_{0}", Guid.NewGuid().ToString());
            this.CurCtrlPreView = ctrlID;
        }
        public override void Set_Source(object ds)
        {
            // Clear List field
            var myDs = ds as lsttbl_DashboardSource;
            foreach (wcPropGrid_Column ctrl in this.ctrlColumn.Controls)
            {
                if (ctrl == null) continue;
                ctrl.Set_DataSource(myDs);
            }
        }
        public override void Set_Info(object info)
        {
            try
            {
                var myInfo = info as lsttbl_Widget;
                var wgObj = myInfo.JsonObj_Grid;
                GlobalSsn.WidgetGrid_SsnModel = wgObj.Copy();
                GlobalSsn.WidgetGrid_SsnModel.Columns.Clear();
                // Reset old infor
                this.Reset_Info();

                // Gọi hàm add Columns...
                foreach (var obj in wgObj.Columns)
                {
                    var ctrl = this.AddNew_Column(true, false);
                    ctrl.Set_GridColumn(obj);
                    GlobalSsn.WidgetGrid_SsnModel.Columns.Add(ctrl.Get_GridColumn());
                }
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            try
            {
                // Clear List field
                foreach (wcPropGrid_Column ctrl in this.ctrlColumn.Controls)
                {
                    if (ctrl == null) continue;
                    ctrl.Reset_Info(param);
                }
            }
            catch { }
        }
        public override WidgetBase GetInputSett()
        {
            try
            {
                var ret = new WidgetGrid();
                foreach (wcPropGrid_Column ctrl in this.ctrlColumn.Controls)
                {
                    if (ctrl == null) continue;
                    ret.Columns.Add(ctrl.Get_GridColumn());
                }
                return ret;
            }
            catch { return new WidgetGrid(); }
        }
        #endregion

        #region Events
        protected void btn_Click(object sender, EventArgs e)
        {
            this.AddNew_Column(false, false);
        }
        private void RemoveColumn(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CurCtrlColumn.RemoveAll(p => p == ctrlID);
                this.ctrlColumn.Controls.RemoveAll(p => p.ID == ctrlID);
                GlobalSsn.WidgetGrid_SsnModel.Columns.RemoveAll(p => p.Name == ctrlID);
            }
            catch { }
        }
        #endregion
    }
}