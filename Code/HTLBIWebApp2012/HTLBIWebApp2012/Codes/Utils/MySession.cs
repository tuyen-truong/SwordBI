using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CECOM;
using System.Collections;

namespace HTLBIWebApp2012
{
    public class MySession
    {
        public static System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }

        public static WidgetBase LayoutDefine_Preview_Widget
        {
            get
            {
                return Session["LayoutDefine_Preview_Widget"] as WidgetBase;
            }
            set
            {
                Session["LayoutDefine_Preview_Widget"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu danh sách các cột số liệu thống kê đang được chọn trong danh sách chọn 'Summaries' của định nghĩa 'Datasource'.
        /// </summary>
        public static List<InqSummaryInfoMDX> DSDefine_SelSumInfo
        {
            get
            {
                if (Session["DSDefine_SelSumInfo"] == null)
                    Session["DSDefine_SelSumInfo"] = new List<InqSummaryInfoMDX>();
                return Session["DSDefine_SelSumInfo"] as List<InqSummaryInfoMDX>;
            }
            set
            {
                Session["DSDefine_SelSumInfo"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu danh sách các cột thông tin đang được chọn trong danh sách chọn 'Fields' của định nghĩa 'Datasource'.
        /// </summary>
        public static List<InqFieldInfoMDX> DSDefine_SelFieldInfo
        {
            get
            {
                if (Session["DSDefine_SelFieldInfo"] == null)
                    Session["DSDefine_SelFieldInfo"] = new List<InqFieldInfoMDX>();
                return Session["DSDefine_SelFieldInfo"] as List<InqFieldInfoMDX>;
            }
            set
            {
                Session["DSDefine_SelFieldInfo"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu thông tin Khóa của đối tượng 'Datasource' hiện hành đang được chọn để Edit.
        /// </summary>
        public static string DSDefine_CurEditing
        {
            get
            {
                return Lib.NTE(Session["DSDefine_CurEditing"]);
            }
            set
            {
                Session["DSDefine_CurEditing"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu thông tin Khóa của đối tượng 'KPI' hiện hành đang được chọn để Edit.
        /// </summary>
        public static string KPIDefine_CurEditing
        {
            get
            {
                return Lib.NTE(Session["KPIDefine_CurEditing"]);
            }
            set
            {
                Session["KPIDefine_CurEditing"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu thông tin Khóa của đối tượng 'Layout' hiện hành đang được chọn để Edit.
        /// </summary>
        public static string LayoutDefine_CurEditing
        {
            get
            {
                return Lib.NTE(Session["LayoutDefine_CurEditing"]);
            }
            set
            {
                Session["LayoutDefine_CurEditing"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu thông tin Khóa của đối tượng 'Dashboard' hiện hành đang được chọn để Edit.
        /// </summary>
        public static string DashboardDefine_CurEditing
        {
            get
            {
                return Lib.NTE(Session["DashboardDefine_CurEditing"]);
            }
            set
            {
                Session["DashboardDefine_CurEditing"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu thông tin các field ở trục Y của LayoutDefine_Chart mà nó đang được chọn vào danh sách chọn.
        /// </summary>
        public static ArrayList LayoutDefine_Chart_SelAxisY
        {
            get
            {
                if (Session["LayoutDefine_Chart_SelAxisY"] == null)
                    Session["LayoutDefine_Chart_SelAxisY"] = new ArrayList();
                return Session["LayoutDefine_Chart_SelAxisY"] as ArrayList;
            }
            set
            {
                Session["LayoutDefine_Chart_SelAxisY"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Session lưu mã các portlet sẽ được dùng trong dashboard hiện hành.
        /// </summary>
        public static ArrayList DashboardDefine_UsingPortlet
        {
            get
            {
                if (Session["DashboardDefine_UsingPortlet"] == null)
                    Session["DashboardDefine_UsingPortlet"] = new ArrayList();
                return Session["DashboardDefine_UsingPortlet"] as ArrayList;
            }
            set
            {
                Session["DashboardDefine_UsingPortlet"] = value;
                GC.Collect();
            }
        }
        /// <summary>
        /// Giải phóng tất cả các session.
        /// </summary>
        public static void ReleaseAllSession()
        {
            try
            {               
                MySession.DSDefine_SelSumInfo = null;
                MySession.DSDefine_SelFieldInfo = null;
                MySession.DSDefine_CurEditing = null;
                MySession.KPIDefine_CurEditing = null;
                
                MySession.LayoutDefine_Preview_Widget = null;
                MySession.LayoutDefine_CurEditing = null;
                MySession.LayoutDefine_Chart_SelAxisY = null;

                MySession.DashboardDefine_CurEditing = null;                
                MySession.DashboardDefine_UsingPortlet = null;
                
                GlobalSsn.WidgetGauge_SsnModel = null;
                GlobalSsn.WidgetGrid_SsnModel = null;
            }
            catch { }
        }
    }
}