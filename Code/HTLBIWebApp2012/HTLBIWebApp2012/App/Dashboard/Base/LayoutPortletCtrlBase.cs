using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.App.UserControls;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using CECOM;
using DevExpress.Web.ASPxEditors;


namespace HTLBIWebApp2012.App.Dashboard.Base
{
    /// <summary>
    /// Lớp chứa các thuộc tính chung có thể tương tác trên tối đa 10 portlet trên một layout của dashboard.
    /// <para>Ngoài ra có thể định nghĩa thêm các hàm tiện ích, sự kiện dùng chung ở trong lớp này.</para>
    /// </summary>
    public class LayoutPortletCtrlBase : PartPlugCtrlBase
    {
        #region Declares
        public Control ContainerUpdatePanel { get; set; }
        protected string DashboardCode { get { return this.ID.Split('-')[1]; } }
        protected Control Container_Pl1 { get { return this.FindControl("container_Pl1"); } }
        protected Control Container_Pl2 { get { return this.FindControl("container_Pl2"); } }
        protected Control Container_Pl3 { get { return this.FindControl("container_Pl3"); } }
        protected Control Container_Pl4 { get { return this.FindControl("container_Pl4"); } }
        protected Control Container_Pl5 { get { return this.FindControl("container_Pl5"); } }
        protected Control Container_Pl6 { get { return this.FindControl("container_Pl6"); } }
        protected Control Container_Pl7 { get { return this.FindControl("container_Pl7"); } }
        protected Control Container_Pl8 { get { return this.FindControl("container_Pl8"); } }
        protected Control Container_Pl9 { get { return this.FindControl("container_Pl9"); } }
        protected Control Container_Pl10 { get { return this.FindControl("container_Pl10"); } }
        protected Control Container_Pl1_Param { get { return this.FindControl("container_Pl1_Param"); } }
        protected Control Container_Pl2_Param { get { return this.FindControl("container_Pl2_Param"); } }
        protected Control Container_Pl3_Param { get { return this.FindControl("container_Pl3_Param"); } }
        protected Control Container_Pl4_Param { get { return this.FindControl("container_Pl4_Param"); } }
        protected Control Container_Pl5_Param { get { return this.FindControl("container_Pl5_Param"); } }
        protected Control Container_Pl6_Param { get { return this.FindControl("container_Pl6_Param"); } }
        protected Control Container_Pl7_Param { get { return this.FindControl("container_Pl7_Param"); } }
        protected Control Container_Pl8_Param { get { return this.FindControl("container_Pl8_Param"); } }
        protected Control Container_Pl9_Param { get { return this.FindControl("container_Pl9_Param"); } }
        protected Control Container_Pl10_Param { get { return this.FindControl("container_Pl10_Param"); } }
        protected ASPxLabel Title_Pl1 { get { return this.FindControl("lblTitle_portlet1") as ASPxLabel; } }
        protected ASPxLabel Title_Pl2 { get { return this.FindControl("lblTitle_portlet2") as ASPxLabel; } }
        protected ASPxLabel Title_Pl3 { get { return this.FindControl("lblTitle_portlet3") as ASPxLabel; } }
        protected ASPxLabel Title_Pl4 { get { return this.FindControl("lblTitle_portlet4") as ASPxLabel; } }
        protected ASPxLabel Title_Pl5 { get { return this.FindControl("lblTitle_portlet5") as ASPxLabel; } }
        protected ASPxLabel Title_Pl6 { get { return this.FindControl("lblTitle_portlet6") as ASPxLabel; } }
        protected ASPxLabel Title_Pl7 { get { return this.FindControl("lblTitle_portlet7") as ASPxLabel; } }
        protected ASPxLabel Title_Pl8 { get { return this.FindControl("lblTitle_portlet8") as ASPxLabel; } }
        protected ASPxLabel Title_Pl9 { get { return this.FindControl("lblTitle_portlet9") as ASPxLabel; } }
        protected ASPxLabel Title_Pl10 { get { return this.FindControl("lblTitle_portlet10") as ASPxLabel; } }
        protected Control Container_Dashboard_Param { get { return this.FindControl("container_Dashboard_Param"); } }
        protected WidgetCtrlBase Ctrl_Pl1 { get { return this.Container_Pl1.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl2 { get { return this.Container_Pl2.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl3 { get { return this.Container_Pl3.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl4 { get { return this.Container_Pl4.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl5 { get { return this.Container_Pl5.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl6 { get { return this.Container_Pl6.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl7 { get { return this.Container_Pl7.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl8 { get { return this.Container_Pl8.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl9 { get { return this.Container_Pl9.Controls[0] as WidgetCtrlBase; } }
        protected WidgetCtrlBase Ctrl_Pl10 { get { return this.Container_Pl10.Controls[0] as WidgetCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl1_Param { get { return this.Container_Pl1_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl2_Param { get { return this.Container_Pl2_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl3_Param { get { return this.Container_Pl3_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl4_Param { get { return this.Container_Pl4_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl5_Param { get { return this.Container_Pl5_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl6_Param { get { return this.Container_Pl6_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl7_Param { get { return this.Container_Pl7_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl8_Param { get { return this.Container_Pl8_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl9_Param { get { return this.Container_Pl9_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Pl10_Param { get { return this.Container_Pl10_Param.Controls[0] as ParamCtrlBase; } }
        protected ParamCtrlBase Ctrl_Dashboard_Param { get { return this.Container_Dashboard_Param.Controls[0] as ParamCtrlBase; } }        
        #endregion

        #region Methods
        /// <summary>
        /// Tải một control Param (có thể cho dashboard hoặc cũng có thể cho portlet)
        /// </summary>
        /// <param name="widgetCode">Mã dashboard hoặc mã portlet.</param>
        protected ParamCtrlBase Load_CtrlParams(string widgetCode)
        {
            ParamCtrlBase ctrl = null;
            ctrl = this.LoadControl("wcPortletParams.ascx") as ParamCtrlBase;
            ctrl.ID = string.Format("genPortletParam_{0}", widgetCode);
            ctrl.DashboardCode = this.DashboardCode;
            ctrl.WidgetCode = widgetCode;
            return ctrl;
        }
        /// <summary>
        /// Tải một widget cho portlet với mã portlet.
        /// </summary>
        /// <param name="widgetCode">Mã portlet.</param>
        protected WidgetCtrlBase Load_CtrlWidget(string widgetCode)
        {
            WidgetCtrlBase ctrl = null;
            WidgetBase sett = null;
            var widgetObj = MyBI.Me.Get_Widget_ByCode(widgetCode);
            var type = widgetObj.WidgetType;
            if (type == "chart")
            {
                ctrl = this.LoadControl("../UserControls/wcChart.ascx") as wcChart;                
                sett = widgetObj.JsonObj_Chart;
            }
            else if (type == "gauge")
            {
                sett = widgetObj.JsonObj_Gauge;
                var gaugeType = widgetObj.JsonObj_Gauge.VisibleType;
                if (gaugeType == GaugeType.CircleFull)
                    ctrl = this.LoadControl("../UserControls/wcFullCGauge.ascx") as wcFullCGauge;
                else if (gaugeType == GaugeType.CircleThreeFour)
                    ctrl = this.LoadControl("../UserControls/wcThreeFourCGauge.ascx") as wcThreeFourCGauge;
                else if (gaugeType == GaugeType.CircleHalf)
                    ctrl = this.LoadControl("../UserControls/wcHalfCGauge.ascx") as wcHalfCGauge;
                else if (gaugeType == GaugeType.CircleQuaterLeft || gaugeType == GaugeType.CircleQuaterRight)
                    ctrl = this.LoadControl("../UserControls/wcQuaterCGauge.ascx") as wcQuaterCGauge;
                else if (gaugeType == GaugeType.LinearHorizontal || gaugeType == GaugeType.LinearVertical)
                    ctrl = this.LoadControl("../UserControls/wcLGauge.ascx") as wcLGauge;
            }
            else if (type == "grid")
            {
                ctrl = this.LoadControl("../UserControls/wcGrid.ascx") as wcGrid;
                sett = widgetObj.JsonObj_Grid;
            }
            ctrl.Sett = sett;
            ctrl.ID = string.Format("genPortlet-{0}", widgetCode);
            ctrl.MyClientInstanceName = string.Format("{0}_{1}", widgetCode, type);
            return ctrl;
        }
        /// <summary>
        /// Tải một control param cho toàn dashboard.
        /// </summary>
        /// <param name="lstWidgetClientInstanceNames">Danh sách các tên thể hiện của các portlet trên dashboard đó ở phía client.</param>
        protected virtual ParamCtrlBase Load_DashboardParams(List<string> lstWidgetClientInstanceNames)
        {
            ParamCtrlBase ctrl = null;
            try
            {
                this.Container_Dashboard_Param.Controls.Clear();
                ctrl = this.Load_CtrlParams(this.DashboardCode);
                ctrl.WidgetClientInstanceNames = lstWidgetClientInstanceNames;
                ctrl.Is_DashboardParam = true;
                this.Container_Dashboard_Param.Controls.Add(ctrl);
            }
            catch { }
            return ctrl;
        }
        /// <summary>
        /// Tải danh sách các portlet ứng với dashboard đang chọn.
        /// <para>Vì nó phụ thuộc vào số lượng portlet trên dashboard (template) nên chưa thể hiện thực ở đây.</para>
        /// </summary>
        protected virtual List<WidgetCtrlBase> Load_Portlets(out ICollection<ParamCtrlBase> lstParams) 
        { 
            lstParams = null; return null; 
        }
        /// <summary>
        /// Lấy một chuỗi chứa tất cả các đoạn javascript có trong tất cả các portlet của dashboard hiện hành.
        /// <para>Vì nó phụ thuộc vào số lượng portlet trên dashboard (template) nên chưa thể hiện thực ở đây.</para>
        /// </summary>
        public virtual string Get_JsAllPortletParam() { return ""; }
        /// <summary>
        /// Đăng ký các đoạn mã javascript của dashboard trên UpdatePanel đang sử dụng (nếu có).
        /// </summary>
        public virtual bool RegisterStartupScript()
        {
            try
            {
                var jsDashParam = this.Ctrl_Dashboard_Param.Get_JsAll();
                var jsAll = this.Get_JsAllPortletParam();
                if(!string.IsNullOrEmpty(jsDashParam))
                    jsAll = jsDashParam + "\r\n" + jsAll;
                if (string.IsNullOrEmpty(jsAll)) return false;
                if (this.ContainerUpdatePanel == null) return false;
                ScriptManager.RegisterStartupScript(this.ContainerUpdatePanel, typeof(string), string.Format("{0}_Script", this.GetType().Name), jsAll, false);
                return true;
            }
            catch { }
            return false;
        }
        #endregion

        #region Events
        /// <summary>
        /// Hàm sự kiện tải của trang, sẽ tải danh sách portlet cho dashboard và control param cho dashboard.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                ICollection<ParamCtrlBase> outParams = null;
                var lstPortlet = this.Load_Portlets(out outParams);
                var lstParams = outParams as List<ParamCtrlBase>;

                var lstNames = lstPortlet.Select(p => p.MyClientInstanceName).ToList();
                var dbrdPr = this.Load_DashboardParams(lstNames);

                // Duyệt qua mỗi ParamCtrlBase để set lại javaScript cho việc lấy giá trị tham số.
                // JavaScript sẽ bao gồm cả trên filter của dashboard (nếu có) và trên filter portlet đó.
                var count = lstParams.Count;
                for (int i = 0; i < count; i++)
                {
                    var plPr = lstParams[i];
                }
            }
            catch { }
        }
        /// <summary>
        /// Sự kiện của ImageButton trên từng portlet gửi về khi user click lên nó.
        /// </summary>
        protected void imgBtn_Command(object sender, CommandEventArgs e)
        {
            var imgBtn = sender as ImageButton;            
        }
        #endregion
    }
}