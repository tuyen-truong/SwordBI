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
using HTLBIWebApp2012.App.Dashboard;

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

        protected wcTwoPane TwoPane;
        protected wcThreePane ThreePane;
        protected wcFourPane FourPane;

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
                    // Using portlets
                    List<COMCodeNameObj> usingPortlets = Dashboard.JsonObj.Get_UsingPortlets();
                    MySession.DashboardDefine_UsingPortlet.Clear();
                    MySession.DashboardDefine_UsingPortlet.AddRange(usingPortlets);
                    // Layout style
                    RadioButton radio = Helpers.FindControlRecur(Page, Dashboard.JsonObj.Template) as RadioButton;
                    if (radio != null)
                    {
                        radio.Checked = true;
                        LayoutStyle_CheckedChanged(radio, new EventArgs());
                    }

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
                
                if (TwoPane_1.Checked)
                {
                    LayoutStyle_CheckedChanged(TwoPane_1, new EventArgs());
                }
                else if (TwoPane_2.Checked)
                {
                    LayoutStyle_CheckedChanged(TwoPane_2, new EventArgs());
                }
                else if (ThreePane_1.Checked)
                {
                    LayoutStyle_CheckedChanged(ThreePane_1, new EventArgs());
                }
                else if (ThreePane_2.Checked)
                {
                    LayoutStyle_CheckedChanged(ThreePane_2, new EventArgs());
                }
                else if (ThreePane_3.Checked)
                {
                    LayoutStyle_CheckedChanged(ThreePane_3, new EventArgs());
                }
                else if (ThreePane_4.Checked)
                {
                    LayoutStyle_CheckedChanged(ThreePane_4, new EventArgs());
                }
                else
                {
                    LayoutStyle_CheckedChanged(FourPane_1, new EventArgs());
                }
                
                Add_FilterControl(true);
            }
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
            List<String> _usingPortlets = new List<string>();

            DashboardDefine dbDefine = new DashboardDefine()
            {
                DisplayName = txtDashboardName.Text
            };
            // detect template
            if (FourPane_1.Checked)
            {
                dbDefine.Template = "FourPane_1";
                _usingPortlets.AddRange(FourPane.UsingPortlets);
            }
            else if (TwoPane_2.Checked)
            {
                dbDefine.Template = "TwoPane_2";
                _usingPortlets.AddRange(TwoPane.UsingPortlets);
            }
            else if (ThreePane_1.Checked)
            {
                dbDefine.Template = "ThreePane_1";
                _usingPortlets.AddRange(ThreePane.UsingPortlets);
            }
            else if (ThreePane_2.Checked)
            {
                dbDefine.Template = "ThreePane_2";
                _usingPortlets.AddRange(ThreePane.UsingPortlets);
            }
            else if (ThreePane_3.Checked)
            {
                dbDefine.Template = "ThreePane_3";
                _usingPortlets.AddRange(ThreePane.UsingPortlets);
            }
            else if (ThreePane_4.Checked)
            {
                dbDefine.Template = "ThreePane_4";
                _usingPortlets.AddRange(ThreePane.UsingPortlets);
            }
            else
            {
                // default case
                dbDefine.Template = "TwoPane_1";
                _usingPortlets.AddRange(FourPane.UsingPortlets);
            }
            // filters
            dbDefine.Filters = this.ctrl_DashboardFilters.Controls.OfType<wcInteractionFilter>()
                    .Select(p => p.Get_FilterInfo()).ToList();
            // portlets which are used
            //dbDefine.UsingPortlets = MySession.DashboardDefine_UsingPortlet
            //        .OfType<COMCodeNameObj>().Select(p => p.Code).ToList();
            dbDefine.UsingPortlets = _usingPortlets;

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

        protected void LayoutStyle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio == null) { return; }
            String ID = radio.ID;
            String[] IDs = ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (IDs.Length < 2) { return; }
            switch(IDs[0])
            {
                case "TwoPane":
                    TwoPane = LoadControl(String.Format("~/App/Dashboard/wcTwoPane.ascx")) as wcTwoPane;
                    TwoPane.WHCode = this.WHCode;
                    if (IDs[1] == "1")
                    {
                        TwoPane.WcType = wcTwoPane.PaneType.First;
                    }
                    else
                    {
                        TwoPane.WcType = wcTwoPane.PaneType.Second;
                    }
                    TwoPane.CtrlMode = wcTwoPane.ControlMode.New;
                    TwoPane.UsingPortlets = Dashboard != null ? Dashboard.JsonObj.Get_UsingPortlets().Select(p => p.Code).ToList() : new List<String>();
                    DashboardSettingPlaceHolder.Controls.Add(TwoPane);
                    break;
                case "ThreePane":
                    ThreePane = LoadControl(String.Format("~/App/Dashboard/wcThreePane.ascx")) as wcThreePane;
                    int nPaneType = 1;
                    int.TryParse(IDs[1], out nPaneType);
                    if (nPaneType == (int)wcThreePane.PaneType.Second)
                    {
                        ThreePane.WcType = wcThreePane.PaneType.Second;
                    }
                    else if (nPaneType == (int)wcThreePane.PaneType.Third)
                    {
                        ThreePane.WcType = wcThreePane.PaneType.Third;
                    }
                    else if (nPaneType == (int)wcThreePane.PaneType.Fourth)
                    {
                        ThreePane.WcType = wcThreePane.PaneType.Fourth;
                    }
                    else
                    {
                        ThreePane.WcType = wcThreePane.PaneType.First;
                    }
                    ThreePane.CtrlMode = wcThreePane.ControlMode.New;
                    ThreePane.WHCode = this.WHCode;
                    DashboardSettingPlaceHolder.Controls.Add(ThreePane);
                    break;
                case "FourPane":
                    FourPane = LoadControl(String.Format("~/App/Dashboard/wcFourPane.ascx")) as wcFourPane;
                    FourPane.CtrlMode = PartPlugCtrlBase.ControlMode.New;
                    FourPane.WHCode = this.WHCode;
                    DashboardSettingPlaceHolder.Controls.Add(FourPane);
                    break;
                default:
                    DashboardSettingPlaceHolder.Controls.Add(new LiteralControl("Under Constructor!!!"));
                    break;
            }
        }
    }
}