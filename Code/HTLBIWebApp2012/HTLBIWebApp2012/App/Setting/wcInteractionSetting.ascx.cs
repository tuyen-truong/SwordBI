using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using HTLBIWebApp2012.Codes.BLL;
using CECOM;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using System.Drawing;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcInteractionSetting : PartPlugCtrlBase
    {
        #region Declares
        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        protected List<string> CtrlInteractionFilterIDs
        {
            get
            {
                if (ViewState["CtrlInteractionFilterIDs"] == null)
                    ViewState["CtrlInteractionFilterIDs"] = new List<string>();
                return ViewState["CtrlInteractionFilterIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlInteractionFilterIDs"] = value;
                if (value == null) GC.Collect();
            }
        }        
        #endregion

        #region Members
        public override void Load_InitData()
        {
            this.OnChange += this_OnChange;
            if (this.IsPostBack)
            {
                //Tạo lại control....
                this.Add_FilterControl(true);
            }
        }
        private InteractionDefine Get_DefineInfo()
        {
            try
            {
                var ret = new InteractionDefine()
                {
                    DrilldownCategory = Lib.NTE(this.radListDrilldownCat.Value),
                    DrilldownPortlet = Lib.NTE(this.cbbDrilldownPortlet.Value),
                    HierarchyFields = this.wcInteractionFieldHierarchy1.Get_HierarchyInfo()
                };
                ret.Filters = this.ctrl_InteractionFilters.Controls.OfType<wcInteractionFilter>()
                    .Select(p => p.Get_FilterInfo()).ToList();
                return ret;
            }
            catch { return null; }
        }
        private wcInteractionFilter Add_FilterControl(bool isReCreate)
        {
            var guiID = Guid.NewGuid().ToString().Replace('-', '_');
            wcInteractionFilter ctrl = null;
            var dsFields = MyBI.Me.Get_DWColumn(this.MyPage.WHCode)
                .Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime"))
                .Select(p => new COMCodeNameObj(p.ColName, p.ColAliasVI)).ToList();
            // ReCreate...
            if (isReCreate)
            {
                foreach (var ctrlID in this.CtrlInteractionFilterIDs)
                {
                    ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.FilterCtrl_Remove;
                    ctrl.Set_Source(dsFields, "Code", "Name");
                    this.ctrl_InteractionFilters.Controls.Add(ctrl);
                }
                return null;
            }
            // Add new...
            ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
            ctrl.ID = string.Format("genIntrF_{0}", guiID);
            ctrl.OnRemove += this.FilterCtrl_Remove;
            ctrl.Set_Source(dsFields, "Code", "Name");
            this.ctrl_InteractionFilters.Controls.Add(ctrl);
            this.CtrlInteractionFilterIDs.Add(ctrl.ID);
            return ctrl;
        }
        private void Set_SaveMsgText(string msg, bool isError)
        {
            this.lblSavingMsg.Font.Bold = true;
            this.lblSavingMsg.Font.Italic = true;
            this.lblSavingMsg.Font.Name = "Arial";
            this.lblSavingMsg.ForeColor = isError ? Color.Red : Color.Blue;
            this.lblSavingMsg.Text = msg;
        }
        /// <summary>
        /// Lấy Mã của WidgetInteraction trong database (nếu có).
        /// </summary>
        private string Get_CurrentSettingCode()
        {
            try
            {
                var obj = MyBI.Me.Get_WidgetInteraction_ByCode(this.MyPage.LayoutCode);
                return obj.WidgetCode;
            }
            catch { }
            return "";
        }
        #endregion

        #region Events
        /// <summary>
        /// Hàm được gọi khi có dữ liệu thay đổi và chuyển về từ một trong hai tab 'DatasourceSetting' hoặc 'KPISetting'.
        /// </summary>
        protected void this_OnChange(object sender, EventArgs e)
        {
            var cat = sender.ToString();
            var whCode = this.MyPage.WHCode;
            var layoutCode = this.MyPage.LayoutCode;
            if (cat == "LAYOUT" && !string.IsNullOrEmpty(whCode) && !string.IsNullOrEmpty(layoutCode))
            {
                this.ctrl_InteractionFilters.Controls.Clear();
                this.CtrlInteractionFilterIDs.Clear();
                this.cbbDrilldownPortlet.Items.Clear();
                this.radListDrilldownCat.Value = "None";
                this.cbbDrilldownPortlet.ClientEnabled = false;
                this.wcInteractionFieldHierarchy1.Reset_HierarchyInfo();
                
                // Load Available Layout By Warehouse.
                var portlets = MyBI.Me.Get_Widget(whCode)
                    .Where(p => p.Code != layoutCode).ToList();
                Helpers.SetDataSource(this.cbbDrilldownPortlet, portlets, "Code", "Name");

                // Set infor.
                var wgIntr = MyBI.Me.Get_WidgetInteraction_ByCode(layoutCode);
                if (wgIntr != null && wgIntr.JsonObj != null)
                {
                    var info = wgIntr.JsonObj;
                    this.radListDrilldownCat.Value = info.DrilldownCategory;
                    this.cbbDrilldownPortlet.ClientEnabled = info.DrilldownCategory == "Other";
                    this.cbbDrilldownPortlet.Value = info.DrilldownPortlet;
                    // Set infor hierarchy field.
                    this.wcInteractionFieldHierarchy1.Set_HierarchyInfo(info);
                    // Add Filter.
                    foreach (var item in info.Filters)
                    {
                        var ctrl = this.Add_FilterControl(false);
                        ctrl.Set_FilterInfo(item);
                    }
                }
            }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ASPxButton;
                if (btn.ID == this.btnAddInteractionFilter.ID)
                    this.Add_FilterControl(false);
            }
            catch { }
        }
        protected void FilterCtrl_Remove(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CtrlInteractionFilterIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrl_InteractionFilters.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }
        protected void cbp_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            var cbp = sender as ASPxCallbackPanel;
            if (cbp == null) return;

            var objSett = this.Get_DefineInfo();
            if (cbp.ID == this.cbpSavingMsg.ID)
            {
                var actionName = Lib.IsNOE(this.Get_CurrentSettingCode()) ? "Add new " : "Update ";
                try
                {
                    // Gọi hàm save
                    var objWgIntr = new lsttbl_WidgetInteraction()
                    {
                        WidgetCode = this.MyPage.LayoutCode,
                        JsonStr = objSett.ToJsonStr()
                    };
                    MyBI.Me.Save_WidgetInteraction(objWgIntr);
                }
                catch { this.Set_SaveMsgText(string.Format("{0} failed!", actionName), true); }
                // Gửi trạng thái về client;            
                this.Set_SaveMsgText(string.Format("{0} success!", actionName), false);
            }
            else if (cbp.ID == this.cbpPreView.ID)
            {
                //this.SetPreView(objSett);
            }
        }
        #endregion
    }
}