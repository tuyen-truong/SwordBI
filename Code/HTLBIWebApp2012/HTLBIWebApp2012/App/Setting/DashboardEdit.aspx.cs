using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.Web.ASPxEditors;
using CECOM;
using System.Collections;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class DashboardEdit : PageBase
    {
        private string DashboardId { get; set; }
        private lsttbl_Dashboard Dashboard { get; set; }
        private string WHCode { get; set; }
        private List<string> CtrlDashboardFilterIDs
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

        protected void Page_Load(object sender, EventArgs e)
        {
            DashboardId = Get_Param(PageArgs.DashboardId);
            if (string.IsNullOrEmpty(DashboardId))
            {
                Page.Title = "New Dashboard";
                WHCode = Get_Param(PageArgs.WHCode);
                if (!IsPostBack)
                {
                    // clean up session data
                    MySession.DashboardDefine_CurEditing = null;
                    MySession.DashboardDefine_UsingPortlet.Clear();
                    // clean up control value
                    txtDashboardName.Text = "";
                    TwoPane_1.Checked = true;
                    // clean up filters
                    ctrl_DashboardFilters.Controls.Clear();
                    CtrlDashboardFilterIDs.Clear();
                    // Available portlets
                    IQueryable<lsttbl_Widget> availablePortlets = MyBI.Me.Get_Widget(WHCode);
                    Helpers.SetDataSource(lbxAvailablePortlet, availablePortlets, "Code", "Name");
                }
            }
            else
            {
                Page.Title = "Edit Dashboard";
                Dashboard = MyBI.Me.Get_Dashboard().FirstOrDefault(db => db.ID == Int32.Parse(DashboardId));
                WHCode = Dashboard.WHCode;
                if (!IsPostBack)
                {
                    txtDashboardName.Text = Dashboard.JsonObj.DisplayName;
                    // Layout style
                    RadioButton radio = Helpers.FindControlRecur(Page, Dashboard.JsonObj.Template) as RadioButton;
                    if (radio != null)
                    {
                        radio.Checked = true;
                    }
                    // Available portlets
                    IQueryable<lsttbl_Widget> availablePortlets = MyBI.Me.Get_Widget(WHCode);
                    Helpers.SetDataSource(lbxAvailablePortlet, availablePortlets, "Code", "Name");
                    // Using portlets
                    List<COMCodeNameObj> usingPortlets = Dashboard.JsonObj.Get_UsingPortlets();
                    MySession.DashboardDefine_UsingPortlet.Clear();
                    MySession.DashboardDefine_UsingPortlet.AddRange(usingPortlets);
                    Helpers.SetDataSource(lbxUsingPortlet, usingPortlets, "Code", "Name");
                    // Add Filter.
                    ctrl_DashboardFilters.Controls.Clear();
                    List<InteractionFilter> filters = Dashboard.JsonObj.Filters;
                    foreach (var item in filters)
                    {
                        var ctrl = this.Add_FilterControl(false);
                        ctrl.Set_FilterInfo(item);
                    }
                    MySession.DashboardDefine_CurEditing = Dashboard.Code;
                    // Default ?
                    chkDefault.Checked = Dashboard.IsDefault;
                }
            }
            if (IsPostBack)
            {
                ctrl_DashboardFilters.Controls.Clear();
                Add_FilterControl(true);
            }
            PortletPicker.WHCode = WHCode;
        }

        protected void btnAddDashboardFilter_Click(object sender, EventArgs e)
        {
            Add_FilterControl(false);
        }

        protected void FilterCtrl_Remove(object sender, EventArgs e)
        {
            string ctrlID = (sender.GetVal("Parent") as Control).ID;
            CtrlDashboardFilterIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
            ctrl_DashboardFilters.Controls.RemoveAll(p => p.ID == ctrlID);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ListEditItem item = PortletPicker.SelectedItem;
            DashboardDefine dbDefine = new DashboardDefine()
            {
                DisplayName = txtDashboardName.Text
            };
            // detect template
            if (FourPane_1.Checked)
            {
                dbDefine.Template = "FourPane_1";
            }
            else if (TwoPane_2.Checked)
            {
                dbDefine.Template = "TwoPane_2";
            }
            else if (ThreePane_1.Checked)
            {
                dbDefine.Template = "ThreePane_1";
            }
            else if (ThreePane_2.Checked)
            {
                dbDefine.Template = "ThreePane_2";
            }
            else if (ThreePane_3.Checked)
            {
                dbDefine.Template = "ThreePane_3";
            }
            else if (ThreePane_4.Checked)
            {
                dbDefine.Template = "ThreePane_4";
            }
            else
            {
                // default case
                dbDefine.Template = "TwoPane_1";
            }
            // filters
            dbDefine.Filters = this.ctrl_DashboardFilters.Controls.OfType<wcInteractionFilter>()
                    .Select(p => p.Get_FilterInfo()).ToList();
            // portlets which are used
            dbDefine.UsingPortlets = MySession.DashboardDefine_UsingPortlet
                    .OfType<COMCodeNameObj>().Select(p => p.Code).ToList();

            lsttbl_Dashboard db = new lsttbl_Dashboard()
            {
                Code = Lib.IfNOE(MySession.DashboardDefine_CurEditing, String.Format("dbrd_{0}_{1}", WHCode, DateTime.Now.ToString("yyyyMMddHHmmss"))),
                Name = txtDashboardName.Text,
                WHCode = this.WHCode,
                JsonStr = dbDefine.ToJsonStr(),
                IsDefault = chkDefault.Checked
            };
            MyBI.Me.Save_Dashboard(db);
            // clean session data
            MySession.DashboardDefine_CurEditing = null;
            MySession.DashboardDefine_UsingPortlet.Clear();
            Response.Redirect("DashboardSetting.aspx?whcode=" + WHCode);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardSetting.aspx?whcode=" + WHCode);
        }

        protected void BtnAddRemovePortlet_Click(object sender, EventArgs e)
        {
            ASPxButton btn = sender as ASPxButton;
            if (btn.ID == btnPortletAdd.ID)
            {
                ListEditItem selectedItem =lbxAvailablePortlet.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }
                string portletCode = Lib.NTE(selectedItem.Value);
                COMCodeNameObj portletInfo = new COMCodeNameObj()
                {
                    Code = portletCode,
                    Name = selectedItem.Text
                };
                ArrayList usingPortlets = MySession.DashboardDefine_UsingPortlet;
                if (!usingPortlets.ToArray().Exists(pl => pl.GetStr("Code") == portletInfo.Code))
                {
                    // if not exist
                    usingPortlets.Add(portletInfo);
                    Helpers.SetDataSource(lbxUsingPortlet, usingPortlets, "Code", "Name");
                }
            }
            else
            {
                // remove portlet
                ListEditItem removeItem = lbxUsingPortlet.SelectedItem;
                if (removeItem == null)
                {
                    return;
                }
                // remove item from listbox
                lbxUsingPortlet.Items.Remove(removeItem);
                // remove item from saved session
                ArrayList usingPortlets = MySession.DashboardDefine_UsingPortlet;
                COMCodeNameObj removeObj = usingPortlets.ToArray().FirstOrDefault(pl => pl.GetStr("Code") == Lib.NTE(removeItem.Value)) as COMCodeNameObj;
                usingPortlets.Remove(removeObj);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardSetting.aspx?whcode=" + WHCode);
        }

        private wcInteractionFilter Add_FilterControl(bool isReCreate)
        {
            var dsFields = MyBI.Me.Get_DWColumn(this.WHCode)
                .Where(p => p.Visible && p.DataType != "NUM" && !p.TblName_Virtual.Contains("DimTime"))
                .Select(p => new COMCodeNameObj(p.ColName, p.ColAliasVI)).ToList();
            wcInteractionFilter ctrl = null;
            if (isReCreate)
            {
                foreach (string ctrlID in CtrlDashboardFilterIDs)
                {
                    ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.FilterCtrl_Remove;
                    ctrl.Set_Source(dsFields, "Code", "Name");
                    ctrl_DashboardFilters.Controls.Add(ctrl);
                }
                return null;
            }
            var guiID = Guid.NewGuid().ToString().Replace("-", "_");
            ctrl = this.LoadControl("wcInteractionFilter.ascx") as wcInteractionFilter;
            ctrl.ID = string.Format("gen_{0}", guiID);
            ctrl.OnRemove += this.FilterCtrl_Remove;
            ctrl.Set_Source(dsFields, "Code", "Name");
            ctrl_DashboardFilters.Controls.Add(ctrl);
            CtrlDashboardFilterIDs.Add(ctrl.ID);
            return ctrl;
        }
    }
}