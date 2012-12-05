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
using CECOM;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.UserControls;


namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcLayoutSetting : PartPlugCtrlBase
    {
        #region Declares
        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        /// <summary>
        /// Nếu trường hợp có chọn 'Layout' thì không lấy DatasourceID mới mà lấy DatasourceID từ 'Layout' đã lưu.
        /// <para>Ngược lại thì lấy DatasourceID theo một trong 2 bên Tab DatasourceSetting hoặc KPISetting.</para>
        /// </summary>
        public string DSCode_Target
        {
            get
            {
                var curLayout = this.GetLayout();
                return (curLayout != null) ? curLayout.DSCode : this.DSCode_OtherPriority;
            }
        }
        /// <summary>
        /// Nếu trên tab 'KPISetting' có chọn KPICode thì ưu tiên lấy KPICode, nếu không thì lấy DSCode trên tab 'DatasourceSetting'.
        /// </summary>
        public string DSCode_OtherPriority
        {
            get
            {
                return !string.IsNullOrEmpty(this.MyPage.KPICode) ? this.MyPage.KPICode : this.MyPage.DSCode;
            }
        }
        public string LayoutCode { get { return Lib.NTE(this.cboLayout.Value); } }
        public string CtrlTypeStr { get { return Lib.NTE(this.cboCtrlType.Value); } }
        public string ViewTypeStr { get { return Lib.NTE(this.cboCtrl.Value).Split('-', StringSplitOptions.RemoveEmptyEntries).Last(); } }
        protected string CurPropCtrl
        {
            get
            {
                if (ViewState["CurPropCtrl"] == null)
                    ViewState["CurPropCtrl"] = "";
                return ViewState["CurPropCtrl"] as string;
            }
            set
            {
                ViewState["CurPropCtrl"] = value;
                if (value == null) GC.Collect();
            }
        }
        #endregion

        #region Members
        public override void Load_InitData()
        {
            this.OnChange += this_OnChange;
            //this.RegisterJavaScript();
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
                // Tải lại source cho cboLayout(để nó không bị thiếu khi vừa thêm mới 1 KPI trong sự kiện của CallbackPanel)
                // Vì cơ chế của CallbackPanel sẽ không để lại ViewState mỗi lần Render
                if (!string.IsNullOrEmpty(this.MyPage.WHCode))
                {
                    var layoputs = MyBI.Me.Get_Widget(this.MyPage.WHCode).ToList();
                    Helpers.SetDataSource(this.cboLayout, layoputs, "Code", "Name", this.cboLayout.Value);
                }
                // Tạo lại control....
                this.AddNew_PropControl(null, true);
            }
        }
        public lsttbl_Widget GetLayout()
        {
            try
            {
                var code = this.LayoutCode;
                if (string.IsNullOrEmpty(code)) return null;
                var ret = MyBI.Me.Get_Widget_ByCode(code);
                return ret;
            }
            catch { return null; }
        }
        public lsttbl_DashboardSource GetDatasource()
        {
            try
            {
                var dsCode = this.DSCode_Target;
                if (string.IsNullOrEmpty(dsCode)) return null;
                var ret = MyBI.Me.Get_DashboardSourceBy(dsCode);
                GlobalSsn.WidgetGrid_SsnModel.DatasourceID = dsCode;
                GlobalSsn.WidgetGauge_SsnModel.DatasourceID = dsCode;
                return ret;
            }
            catch { return null; }
        }
        private PropCtrlBase AddNew_PropControl(string type, bool isReCreate)
        {
            try
            {
                PropCtrlBase ctrl = null;
                // Re - create control.
                if (isReCreate)
                {
                    if (string.IsNullOrEmpty(this.CurPropCtrl)) return null;
                    var arr = this.CurPropCtrl.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    string ctrlID = arr.First(), ctrlType = arr.Last();
                    if (ctrlType.StartsWith("chart"))
                        ctrl = this.LoadControl("wcPropChart.ascx") as PropCtrlBase;
                    else if (ctrlType.StartsWith("gauge"))
                        ctrl = this.LoadControl("wcPropGauge.ascx") as PropCtrlBase;
                    else if (ctrlType.StartsWith("grid"))
                        ctrl = this.LoadControl("wcPropGrid.ascx") as PropCtrlBase;

                    ctrl.ID = ctrlID;
                    this.propCtrl.Controls.Clear();
                    this.propCtrl.Controls.Add(ctrl);
                    return ctrl;
                }

                // Add new control.
                if (string.IsNullOrEmpty(type)) return null;
                if (type.StartsWith("chart"))
                    ctrl = this.LoadControl("wcPropChart.ascx") as PropCtrlBase;
                else if (type.StartsWith("gauge"))
                    ctrl = this.LoadControl("wcPropGauge.ascx") as PropCtrlBase;
                else if (type.StartsWith("grid"))
                    ctrl = this.LoadControl("wcPropGrid.ascx") as PropCtrlBase;

                ctrl.ID = string.Format("gen_{1}_{0}", Guid.NewGuid().ToString(), type);
                this.propCtrl.Controls.Clear();
                this.propCtrl.Controls.Add(ctrl);
                this.CurPropCtrl = string.Format("{0},{1}", ctrl.ID, type);

                // Add datasource.
                var selObj = this.GetDatasource();
                if (selObj != null) ctrl.Set_Source(selObj);
                if (!Lib.IsNOE(this.cboCtrl.Value))
                    ctrl.Set_ViewType(this.ViewTypeStr);
                return ctrl;
            }
            catch { }
            return null;
        }
        private PropCtrlBase Find_PropControl()
        {
            try
            {
                var arr = this.CurPropCtrl.Split(',', StringSplitOptions.RemoveEmptyEntries);
                string ctrlID = arr.First();
                var ctrl = this.propCtrl.FindControl(ctrlID) as PropCtrlBase;
                return ctrl;
            }
            catch { return null; }
        }
        private void Clear_PropControl()
        {
            var ctrl = this.Find_PropControl();
            if (ctrl != null) ctrl.Reset_Info();
            this.propCtrl.Controls.Clear();
            this.CurPropCtrl = string.Empty;

            GlobalSsn.WidgetGauge_SsnModel = null;
            GlobalSsn.WidgetGrid_SsnModel = null;
        }
        public WidgetBase GetDefineInfo()
        {
            var curCtrl = this.Find_PropControl();
            var ret = curCtrl.GetInputSett();
            try
            {
                ret.DisplayName = this.txtDisplayName.Text;
                ret.DatasourceID = this.DSCode_Target;
                ret.CtrlType = this.CtrlTypeStr;
                var viewTypeStr = this.ViewTypeStr;
                if (ret.CtrlType == "chart")
                {
                    var obj = ret as WidgetChart;
                    obj.ChartType = Helpers.ToEnum<ViewType>(viewTypeStr);
                }
                else if (ret.CtrlType == "gauge")
                {
                    var obj = ret as WidgetGauge;
                    obj.VisibleType = Helpers.ToEnum<GaugeType>(viewTypeStr);
                }
            }
            catch { return default(WidgetBase); }
            return ret;
        }
        private void SetSaveMsgText(string msg, bool isError)
        {
            this.lblSavingMsg.Font.Bold = true;
            this.lblSavingMsg.Font.Italic = true;
            this.lblSavingMsg.Font.Name = "Arial";
            this.lblSavingMsg.ForeColor = isError ? Color.Red : Color.Blue;
            this.lblSavingMsg.Text = msg;
        }
        private void SetPreView(WidgetBase sett)
        {
            var type = sett.CtrlType;
            var viewType = this.ViewTypeStr;
            try
            {
                WidgetCtrlBase ctrl = null;
                var guiID = Guid.NewGuid().ToString();
                if (type == "chart")
                    ctrl = this.LoadControl("../UserControls/wcChart.ascx") as wcChart;
                else if (type == "gauge")
                {
                    var gaugeType = Helpers.ToEnum<GaugeType>(viewType);
                    switch (gaugeType)
                    {
                        case GaugeType.CircleFull:
                            ctrl = this.LoadControl("../UserControls/wcFullCGauge.ascx") as wcFullCGauge;
                            break;
                        case GaugeType.CircleThreeFour:
                            ctrl = this.LoadControl("../UserControls/wcThreeFourCGauge.ascx") as wcThreeFourCGauge;
                            break;
                        case GaugeType.CircleHalf:
                            ctrl = this.LoadControl("../UserControls/wcHalfCGauge.ascx") as wcHalfCGauge;
                            break;
                        case GaugeType.CircleQuaterLeft:
                        case GaugeType.CircleQuaterRight:
                            ctrl = this.LoadControl("../UserControls/wcQuaterCGauge.ascx") as wcQuaterCGauge;
                            break;
                        case GaugeType.LinearHorizontal:
                        case GaugeType.LinearVertical:
                            ctrl = this.LoadControl("../UserControls/wcLGauge.ascx") as wcLGauge;
                            break;
                    }
                }
                else if (type == "grid")
                    ctrl = this.LoadControl("../UserControls/wcGrid.ascx") as wcGrid;
                ctrl.Sett = sett;
                ctrl.ID = string.Format("preview_{1}_{0}", guiID, type);
                this.ctrlPreView.Controls.Clear();
                this.ctrlPreView.Controls.Add(ctrl);
            }
            catch { }
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
            var dsCode = this.DSCode_OtherPriority;
            if (cat == "WH" && !string.IsNullOrEmpty(whCode))
            {
                MySession.LayoutDefine_CurEditing = null;
                this.cboLayout.Items.Clear();
                this.cboLayout.Text = "";
                this.txtDisplayName.Text = "";
                this.cboCtrl.Value = "";
                this.cboCtrlType.Value = "";
                this.Clear_PropControl();
                // Load Available Layout By Warehouse.
                var layouts = MyBI.Me.Get_Widget(whCode).ToList();
                Helpers.SetDataSource(this.cboLayout, layouts, "Code", "Name");
            }
            else if ((cat == "DS" || cat == "KPI") && string.IsNullOrEmpty(this.LayoutCode) && !string.IsNullOrEmpty(dsCode))
            {
                // Add datasource.
                var selObj = this.GetDatasource();
                if (selObj == null) return;
                var ctrl = this.Find_PropControl();
                if (ctrl!= null) ctrl.Set_Source(selObj);
                if (cat == "KPI")
                {
                    var kpi = selObj.JsonObjKPI;
                    this.cboCtrlType.Value = kpi.CtrlTypeDefault;
                    this.cbo_ValueChanged(this.cboCtrlType, null);
                    this.cboCtrl.Value = string.Format("{0}-{1}", kpi.CtrlTypeDefault, kpi.VisibleTypeDefault);
                    this.cbo_ValueChanged(this.cboCtrl, null);
                }
            }
        }
        protected void cbo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var cbo = sender as ASPxComboBox;
                if (cbo == null) return;
                if (cbo.ID == this.cboLayout.ID)
                {
                    var layout = this.GetLayout();
                    if (layout == null) return;

                    MySession.LayoutDefine_CurEditing = layout.Code;
                    this.txtDisplayName.Text = layout.Name;
                    if (layout.WidgetType.ToLower() == "chart")
                    {
                        var obj = layout.JsonObj_Chart;
                        this.cboCtrlType.Value = obj.CtrlType;
                        this.cbo_ValueChanged(this.cboCtrlType, null);
                        this.cboCtrl.Value = string.Format("{0}-{1}", obj.CtrlType, obj.ChartType);
                        this.cbo_ValueChanged(this.cboCtrl, null);
                    }
                    else if (layout.WidgetType.ToLower() == "gauge")
                    {
                        var obj = layout.JsonObj_Gauge;
                        this.cboCtrlType.Value = obj.CtrlType;                        
                        this.cbo_ValueChanged(this.cboCtrlType, null);
                        this.cboCtrl.Value = string.Format("{0}-{1}", obj.CtrlType, obj.VisibleType);
                        this.cbo_ValueChanged(this.cboCtrl, null);
                    }
                    else if (layout.WidgetType.ToLower() == "grid")
                    {
                        var obj = layout.JsonObj_Grid;
                        this.cboCtrlType.Value = obj.CtrlType;
                        this.cbo_ValueChanged(this.cboCtrlType, null);
                    }
                    // Raise Event OnChange.
                    var curDS = MyBI.Me.Get_DashboardSourceBy(layout.DSCode);
                    if (curDS != null)
                    {
                        if (curDS.SettingCat == GlobalVar.SettingCat_DS)
                            this.MyPage.My_wcDSSetting.Raise_OnChange("LAYOUT", new HTLBIEventArgs(layout.DSCode));
                        else if (curDS.SettingCat == GlobalVar.SettingCat_KPI)
                            this.MyPage.My_wcKPISetting.Raise_OnChange("LAYOUT", new HTLBIEventArgs(layout.DSCode));
                    }
                    this.MyPage.My_wcInteractionSetting.Raise_OnChange("LAYOUT", null);
                }
                else if (cbo.ID == this.cboCtrlType.ID)
                {
                    this.Clear_PropControl();

                    var arrCtrl = Helpers.ArrDashboardCtrl.Where(p => p.Cat == this.CtrlTypeStr);
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
                    var ctrl = this.AddNew_PropControl(this.CtrlTypeStr, false);
                    var layout = this.GetLayout();
                    if (layout != null) ctrl.Set_Info(layout);
                }
                else if (cbo.ID == this.cboCtrl.ID)
                {
                    var viewType = this.ViewTypeStr;
                    var ctrl = this.Find_PropControl();
                    //if (ctrl is wcPropChart)
                    //{
                    //    var myCtrl = ctrl as wcPropChart;
                    //    if (viewType == "Pie" || viewType == "Doughnut")
                    //        myCtrl.IsPie = true;
                    //}
                    ctrl.Set_ViewType(viewType);
                }
            }
            catch { }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as ASPxButton;
                if (btn.ID == this.btnNewLayout.ID)
                {
                    MySession.LayoutDefine_CurEditing = null;
                    // Clear General.
                    this.cboLayout.Text = "";
                    this.txtDisplayName.Text = "";
                    this.cboCtrlType.Text = "";
                    this.cboCtrl.Text = "";
                    this.lblSavingMsg.Text = "";
                    this.Clear_PropControl();
                }
            }
            catch { }
        }
        protected void cbp_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            var cbp = sender as ASPxCallbackPanel;
            if (cbp == null) return;

            var objSett = this.GetDefineInfo();
            if (cbp.ID == this.cbpSavingMsg.ID)
            {
                var actionName = Lib.IsNOE(MySession.LayoutDefine_CurEditing) ? "Add new " : "Update ";
                try
                {
                    // Gọi hàm save
                    var objWg = new lsttbl_Widget()
                    {
                        Code = Lib.IfNOE(MySession.LayoutDefine_CurEditing, "wg_" + DateTime.Now.ToString("yyyyMMddHHmmss")),
                        Name = this.txtDisplayName.Text,
                        DSCode = objSett.DatasourceID,
                        WHCode = this.MyPage.WHCode,
                        WidgetType = this.CtrlTypeStr,
                        JsonStr = objSett.ToJsonStr()
                    };
                    MyBI.Me.Save_Widget(objWg);
                    MySession.LayoutDefine_CurEditing = objWg.Code;
                }
                catch { this.SetSaveMsgText(string.Format("{0} failed!", actionName), true); }
                // Gửi trạng thái về client;            
                this.SetSaveMsgText(string.Format("{0} success!", actionName), false);
            }
            else if (cbp.ID == this.cbp_Header.ID)
            {
                if (!string.IsNullOrEmpty(MySession.LayoutDefine_CurEditing))
                {
                    var layoputs = MyBI.Me.Get_Widget(this.MyPage.WHCode).ToList();
                    Helpers.SetDataSource(this.cboLayout, layoputs, "Code", "Name", MySession.LayoutDefine_CurEditing);
                }
            }
            else if (cbp.ID == this.cbpPreView.ID)
            {
                this.SetPreView(objSett);
            }
        }
        #endregion
    }
}