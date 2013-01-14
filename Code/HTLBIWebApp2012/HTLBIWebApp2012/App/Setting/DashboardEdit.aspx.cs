using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.App.Dashboard;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;

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

        String Layout
        {
            get
            {
                return ViewState["Dashboard_Layout"] as String;
            }
            set
            {
                ViewState["Dashboard_Layout"] = value;
            }
        }
        
        protected wcTwoPane TwoPane;
        protected wcThreePane ThreePane;
        protected wcFourPane FourPane;
        protected List<String> m_selectedPortlets = new List<string>();

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
                    Layout_Initialize(TwoPane_1.ID);
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
                    // Layout style
                    RadioButton radio = Helpers.FindControlRecur(Page, Dashboard.JsonObj.Template) as RadioButton;
                    if (radio != null)
                    {
                        radio.Checked = true;
                        Layout_Initialize(radio.ID);
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
                Add_FilterControl(true);

                if (!String.IsNullOrEmpty(Layout))
                {
                    Layout_Initialize(Layout);
                    int c = 0;
                    string[] postedKeys = Request.Form.AllKeys;
                    foreach (string key in postedKeys)
                    {
                        if (!string.IsNullOrEmpty(key)
                            && key.EndsWith("$m_portletCandidate")
                            && c <= 4)
                        {
                            m_selectedPortlets.Add(Request.Form[key]);
                            ++c;
                        }
                    }
                }
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
            dbDefine.UsingPortlets = m_selectedPortlets;

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
            Layout_Initialize(radio.ID);
        }

        /// <summary>
        /// Initialize a control to setting up dashboard layout
        /// </summary>
        /// <param name="strTemplateName">It should be on of the following names TwoPane_1, TwoPane_2, ThreePane_1, ..., ThreePane_4, FourPane_1</param>
        void Layout_Initialize(String strTemplateName)
        {
            DashboardSettingPlaceHolder.Controls.Clear();
            

            String[] IDs = strTemplateName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (IDs.Length < 2) { return; }
            switch (IDs[0])
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
                    
                    if (IsPostBack)
                    {
                        TwoPane.UsingPortlets = m_selectedPortlets;
                    }
                    else
                    {
                        TwoPane.UsingPortlets = Dashboard != null ? Dashboard.JsonObj.UsingPortlets : new List<String>();
                    }
                    
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
                    
                    if (IsPostBack)
                    {
                        ThreePane.UsingPortlets = m_selectedPortlets;
                    }
                    else
                    {
                        ThreePane.UsingPortlets = Dashboard != null ? Dashboard.JsonObj.UsingPortlets : new List<String>();
                    }

                    DashboardSettingPlaceHolder.Controls.Add(ThreePane);
                    break;
                case "FourPane":
                    FourPane = LoadControl(String.Format("~/App/Dashboard/wcFourPane.ascx")) as wcFourPane;
                    FourPane.CtrlMode = PartPlugCtrlBase.ControlMode.New;
                    FourPane.WHCode = this.WHCode;
                    
                    if (IsPostBack)
                    {
                        FourPane.UsingPortlets = m_selectedPortlets;
                    }
                    else
                    {
                        FourPane.UsingPortlets = Dashboard != null ? Dashboard.JsonObj.UsingPortlets : new List<String>();
                    }

                    DashboardSettingPlaceHolder.Controls.Add(FourPane);
                    break;
                default:
                    DashboardSettingPlaceHolder.Controls.Add(new LiteralControl("Under Constructor!!!"));
                    break;
            }
            Layout = strTemplateName;
        }
    }
}