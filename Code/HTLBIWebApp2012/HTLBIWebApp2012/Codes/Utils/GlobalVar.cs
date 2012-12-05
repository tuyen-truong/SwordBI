using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTLBIWebApp2012.Codes.Models;
using System.Configuration;
using CECOM;

namespace HTLBIWebApp2012
{
    public class GlobalVar
    {
        /// <summary>
        /// DS
        /// </summary>
        public const string SettingCat_DS = "DS";
        /// <summary>
        /// KPI
        /// </summary>
        public const string SettingCat_KPI = "KPI";
        /// <summary>
        /// CHART
        /// </summary>
        public const string WidgetType_CHART = "CHART";
        /// <summary>
        /// GAUGE
        /// </summary>
        public const string WidgetType_GAUGE = "GAUGE";
        /// <summary>
        /// GRID
        /// </summary>
        public const string WidgetType_GRID = "GRID";


        /// <summary>
        /// Thể hiện của database BI Manager
        /// </summary>
        public static HTLBI2012DataContext DbBI
        {
            get { return new HTLBI2012DataContext(GlobalVar.DbBI_ConnectionStr); }
        }
        /// <summary>
        /// Thể hiện của database warehouse
        /// </summary>
        public static HTLBI2012DWDataContext DbDW
        {
            get { return new HTLBI2012DWDataContext(GlobalVar.DbDW_ConnectionStr); }
        }
        public static string DbBI_ConnectionStr
        {
            get { return ConfigurationManager.ConnectionStrings["HTLBI2012_ConnectionString"].ConnectionString; }
        }
        public static string DbDW_ConnectionStr
        {
            get { return ConfigurationManager.ConnectionStrings["HTLBI2012_DWConnectionString"].ConnectionString; }
        }
        public static string DbOLAP_ConnectionStr
        {
            get { return ConfigurationManager.ConnectionStrings["HTLBI2012_OLAPConnectionString"].ConnectionString; }
        }
        public static string DbOLAP_ConnectionStr_Tiny
        {
            get { return DbOLAP_ConnectionStr.Replace(";Cube Name={0}", ""); }
        }

        public static int PopupGrid_PageSize
        {
            get
            {
                var ret = 15;
                int.TryParse(ConfigurationManager.AppSettings["popupGrid_PageSize"], out ret);
                return ret;
            }
        }
        public static string Default_culture
        {
            get
            {
                return ConfigurationManager.AppSettings["default_culture"];
            }
        }
        public static string Default_Page
        {
            get
            {
                return ConfigurationManager.AppSettings["default_Page"];
            }
        }
        public static int DropDownRow
        {
            get
            {
                try
                {
                    return int.Parse(ConfigurationManager.AppSettings["dropDownRow"]);
                }
                catch { return 10; }
            }
        }

        /// <summary>
        /// Đường dẫn vật lý của thư mục gốc chứa website hiện hành
        /// </summary>
        public static string RootPath
        {
            get { return HttpContext.Current.Server.MapPath("~"); } //~
        }
        /// <summary>
        /// Role hiện tại của user hiện tại
        /// </summary>
        public static string CurRole
        {
            get { return Lib.NTE(HttpContext.Current.Session["CurRole"]); }
            set { HttpContext.Current.Session["CurRole"] = value; }
        }
        public static bool FirstURL_IsRequested
        {
            get
            {
                if (HttpContext.Current.Session["FirstURL_IsRequested"] == null)
                    HttpContext.Current.Session["FirstURL_IsRequested"] = false;
                return (bool)HttpContext.Current.Session["FirstURL_IsRequested"];
            }
            set { HttpContext.Current.Session["FirstURL_IsRequested"] = value; }
        }
        /// <summary>
        /// Url mặc định lúc đầu khi load menu sub
        /// </summary>
        public static string FirstURL
        {
            get { return Lib.NTE(HttpContext.Current.Session["FirstURL"]); }
            set { HttpContext.Current.Session["FirstURL"] = value; }
        }
        /// <summary>
        /// Url tại tab (category) hiện tại đang chọn
        /// </summary>
        public static string CurTabURL
        {
            get { return Lib.NTE(HttpContext.Current.Session["CurTabURL"]); }
            set { HttpContext.Current.Session["CurTabURL"] = value; }
        }
        /// <summary>
        /// ID của tab (category) hiện tại đang chọn
        /// </summary>
        public static string CurMainMnuCode
        {
            get { return Lib.NTE(HttpContext.Current.Session["CurMainMnuCode"]); }
            set { HttpContext.Current.Session["CurMainMnuCode"] = value; }
        }
        public static string CurSubMnuCode
        {
            get { return Lib.NTE(HttpContext.Current.Session["CurSubMnuCode"]); }
            set { HttpContext.Current.Session["CurSubMnuCode"] = value; }
        }
        /// <summary>
        /// Caption của tab (category) hiện tại đang chọn
        /// </summary>
        public static string CurTabCaption
        {
            get { return Lib.NTE(HttpContext.Current.Session["CurTabCaption"]); }
            set { HttpContext.Current.Session["CurTabCaption"] = value; }
        }
        public static string CurERROR
        {
            get { return Lib.NTE(HttpContext.Current.Session["ERROR"]); }
            set { HttpContext.Current.Session["ERROR"] = value; }
        }
        public static void ResetAllSession()
        {
            try
            {
                GlobalVar.CurRole = null;
                GlobalVar.CurTabCaption = null;
                GlobalVar.CurMainMnuCode = null;
                GlobalVar.CurSubMnuCode = null;
                GlobalVar.FirstURL_IsRequested = false;
                GlobalVar.CurTabURL = null;
                GlobalVar.FirstURL = null;
                GlobalVar.CurERROR = null;
            }
            catch { }
        }
    }
}