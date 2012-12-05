using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.Web.ASPxCallbackPanel;
using CECOM;
using System.Drawing;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcDashboardSetting : PartPlugCtrlBase
    {
        #region Declares
        public string MenuCode { get; set; }
        public string WHCode { get; set; }
        protected List<string> CtrlDashboardFilterIDs
        {
            get
            {
                if (ViewState["CtrlDashboardFilterIDs"] == null)
                    ViewState["CtrlDashboardFilterIDs"] = new List<string>();
                return ViewState["CtrlDashboardFilterIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlDashboardFilterIDs"] = value;
                if (value == null) GC.Collect();
            }
        }        
        #endregion

        #region Members
        private DashboardDefine Get_DefineInfo()
        {
            try
            {
                var ret = new DashboardDefine()
                {
                    DisplayName = this.txtDisplayName.Text,
                    Template = Lib.NTE(this.cboTemplate.Value)
                };
                ret.Filters = this.ctrl_DashboardFilters.Controls.OfType<wcInteractionFilter>()
                    .Select(p => p.Get_FilterInfo()).ToList();
                ret.UsingPortlets = MySession.DashboardDefine_UsingPortlet
                    .OfType<COMCodeNameObj>().Select(p => p.Code).ToList();
                return ret;
            }
            catch { return null; }
        }
        private wcInteractionFilter Add_FilterControl(bool isReCreate)
        {
            var guiID = Guid.NewGuid().ToString().Replace('-', '_');
            wcInteractionFilter ctrl = null;
            var dsFields = MyBI.Me.Get_DWColumn(this.WHCode)
                .Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime"))
                .Select(p => new COMCodeNameObj(p.ColName, p.ColAliasVI)).ToList();
            // ReCreate...
            if (isReCreate)
            {
                foreach (var ctrlID in this.CtrlDashboardFilterIDs)
                {
                    ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.FilterCtrl_Remove;
                    ctrl.Set_Source(dsFields, "Code", "Name");
                    this.ctrl_DashboardFilters.Controls.Add(ctrl);
                }
                return null;
            }
            // Add new...
            ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
            ctrl.ID = string.Format("gen_{0}", guiID);
            ctrl.OnRemove += this.FilterCtrl_Remove;
            ctrl.Set_Source(dsFields, "Code", "Name");
            this.ctrl_DashboardFilters.Controls.Add(ctrl);
            this.CtrlDashboardFilterIDs.Add(ctrl.ID);
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
        public override void Reset_Info(params object[] param)
        {
            this.lblSavingMsg.Text = "";
            this.txtDisplayName.Text = "";
            this.cboTemplate.Value = "";
            MySession.DashboardDefine_CurEditing = null;

            this.lbxUsingPortlet.Items.Clear();
            MySession.DashboardDefine_UsingPortlet.Clear();

            this.ctrl_DashboardFilters.Controls.Clear();
            this.CtrlDashboardFilterIDs.Clear();
        }
        #endregion        

        #region Events
        public override void Load_InitData()
        {
            if (!IsPostBack)
            {
                // Load Data WareHouse
                Helpers.SetDataSource(this.cboDataDW, MyBI.Me.GetDW(), "Value", "Text");
            }

            // Load template..
            Helpers.SetDataSource(this.cboTemplate, DashboardDefine.Get_Template(), "Code", "Name", this.cboTemplate.Value);
            
            // Load Portlet by WHCode.
            var ds = MyBI.Me.Get_Widget(this.WHCode).ToList();
            Helpers.SetDataSource(this.lbxAvailablePortlet, ds, "Code", "Name");

            //Tạo lại control....
            if (this.IsPostBack)
                this.Add_FilterControl(true);
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ASPxButton;
                if (btn.ID == this.btnIn.ID)
                {
                    var item = this.lbxAvailablePortlet.SelectedItem;
                    if (item == null) return;
                    var portletCode = Lib.NTE(item.Value);
                    var info = new COMCodeNameObj(portletCode, item.Text);
                    var sel_UsingPortlet = MySession.DashboardDefine_UsingPortlet;
                    if (sel_UsingPortlet.ToArray().Exists(p => p.GetStr("Code") == info.Code)) return;
                    sel_UsingPortlet.Add(info);
                    Helpers.SetDataSource(this.lbxUsingPortlet, sel_UsingPortlet, "Code", "Name");
                }
                else if (btn.ID == this.btnOut.ID)
                {
                    var itemRemove = lbxUsingPortlet.SelectedItem;
                    this.lbxUsingPortlet.Items.Remove(itemRemove);
                    var objRemove = MySession.DashboardDefine_UsingPortlet.ToArray()
                        .FirstOrDefault(p => p.GetStr("Code") == Lib.NTE(itemRemove.Value));
                    MySession.DashboardDefine_UsingPortlet.Remove(objRemove);
                }
                else if (btn.ID == this.btnView.ID)
                { 
                    
                }
                else if (btn.ID == this.btnNew.ID)
                {
                    this.Reset_Info();
                }
                else if (btn.ID == this.btnEdit.ID)
                {
                    this.Reset_Info();
                    if (this.lbxDashboard.SelectedItem == null) return;
                    var dbrdCode = Lib.NTE(this.lbxDashboard.SelectedItem.Value);
                    MySession.DashboardDefine_CurEditing = dbrdCode;
                    var obj = MyBI.Me.Get_DashboardBy(dbrdCode);
                    var objDbrd = obj.JsonObj;                    
                    this.txtDisplayName.Text = objDbrd.DisplayName;
                    this.cboTemplate.Value = objDbrd.Template;
                    this.chkIsDefault.Checked = obj.IsDefault;
                    var usingPortlets = objDbrd.Get_UsingPortlets();
                    MySession.DashboardDefine_UsingPortlet.AddRange(usingPortlets);
                    Helpers.SetDataSource(this.lbxUsingPortlet, usingPortlets, "Code", "Name");
                    // Add Filter.
                    foreach (var item in objDbrd.Filters)
                    {
                        var ctrl = this.Add_FilterControl(false);
                        ctrl.Set_FilterInfo(item);
                    }
                }
                else if (btn.ID == this.btnSave.ID)
                {
                    var objSett = this.Get_DefineInfo();
                    var actionName = Lib.IsNOE(MySession.DashboardDefine_CurEditing) ? "Add new " : "Update ";
                    try
                    {
                        // Gọi hàm save
                        var objDbrd = new lsttbl_Dashboard()
                        {
                            Code = Lib.IfNOE(MySession.DashboardDefine_CurEditing, string.Format("dbrd_{0}_{1}", this.WHCode, DateTime.Now.ToString("yyyyMMddHHmmss"))),
                            Name = this.txtDisplayName.Text,
                            WHCode = this.WHCode,
                            JsonStr = objSett.ToJsonStr(),
                            IsDefault = this.chkIsDefault.Checked
                        };
                        MyBI.Me.Save_Dashboard(objDbrd);
                        Helpers.SetDataSource(this.lbxDashboard, MyBI.Me.Get_Dashboard(this.WHCode).ToList(), "Code", "Name");
                        MySession.DashboardDefine_CurEditing = objDbrd.Code;
                    }
                    catch { this.Set_SaveMsgText(string.Format("{0} failed!", actionName), true); }
                    this.Set_SaveMsgText(string.Format("{0} success!", actionName), false);
                }
                else if (btn.ID == this.btnAddDashboardFilter.ID)
                    this.Add_FilterControl(false);                
            }
            catch { }
        }
        protected void FilterCtrl_Remove(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CtrlDashboardFilterIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrl_DashboardFilters.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }
        protected void cbp_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            var cbp = sender as ASPxCallbackPanel;
            if (cbp == null) return;
        }
        #endregion

        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            this.WHCode = Lib.NTE(this.cboDataDW.Value);
            // Load Available dashboard...
            Helpers.SetDataSource(this.lbxDashboard, MyBI.Me.Get_Dashboard(this.WHCode).ToList(), "Code", "Name");
        }
    }
}