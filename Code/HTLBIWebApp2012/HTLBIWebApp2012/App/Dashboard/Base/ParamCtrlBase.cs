using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.App.UserControls;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.Jwc;
using CECOM;
using System.Data;


namespace HTLBIWebApp2012.App.Dashboard.Base
{
    /// <summary>
    /// Lớp định nghĩa các thông tin chung cho một control chứa các control con dùng làm tham số chọn lúc chạy cho một đối tượng nào đó (VD: Dashboard, Analysis...)
    /// </summary>
    public class ParamCtrlBase : PartPlugCtrlBase
    {
        #region Declares
        protected Control Container_Params { get { return this.FindControl("container_Params"); } }
        protected IEnumerable<jwcControlBase> Ctrl_Params { get { return this.Container_Params.Controls.OfType<jwcControlBase>(); } }
        protected List<string> Ctrl_ParamsID
        {
            get
            {
                if (ViewState["Ctrl_ParamsID"] == null)
                    ViewState["Ctrl_ParamsID"] = new List<string>();
                return ViewState["Ctrl_ParamsID"] as List<string>;
            }
            set
            {
                ViewState["Ctrl_ParamsID"] = value;
                if (value == null) GC.Collect();
            }
        }
        // Có thể dùng cache để hạn chế truy xuất xuống database (sau này phải cải tiến lại...).
        public List<InteractionFilter> IntrFilters
        {
            get
            {
                if (this.Is_DashboardParam)
                {
                    var obj = MyBI.Me.Get_DashboardBy(this.WidgetCode);
                    if (obj == null) return null;
                    var objDef = obj.JsonObj;
                    if (objDef == null) return null;
                    return objDef.Filters;
                }
                else
                {
                    var obj = MyBI.Me.Get_WidgetInteraction_ByCode(this.WidgetCode);
                    if (obj == null) return null;
                    var objDef = obj.JsonObj;
                    if (objDef == null) return null;
                    return objDef.Filters;
                }
            }
        }
        /// <summary>
        /// Mã của datawarehouse.
        /// </summary>
        //public string WhCode { get; set; }
        /// <summary>
        /// Mã dashboard hiện hành đang chọn.
        /// </summary>
        public string DashboardCode { get; set; }
        /// <summary>
        /// Mã của dashboard hoặc cũng có thể là mã portlet(Widget).
        /// </summary>
        public string WidgetCode { get; set; }
        /// <summary>
        /// Tên thể hiện của portlet(widget) mà nó dùng để truy xuất ở phía client.
        /// </summary>
        public string WidgetClientInstanceName { get; set; }
        /// <summary>
        /// Danh sách các tên thể hiện của portlet(widget) mà nó dùng để truy xuất ở phía client.
        /// <para>Thuộc tính này sẽ được sử dụng trong trường hợp Param control này nằm trên toàn dashboard</para>
        /// </summary>
        public List<string> WidgetClientInstanceNames { get; set; }
        /// <summary>
        /// Chỉ ra rằng PramControl hiện tại có phải là param dành cho Dashboard hay Portlet(widget).
        /// </summary>
        public bool Is_DashboardParam { get; set; }
        /// <summary>
        /// Đoạn mã javaScript bao gồm javaScript trên filter dashboard và javaScript trên portlet hiện hành
        /// </summary>
        public string JsOnGetParamValues_DashboardAndPortlet { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách các dữ liệu cho field truyền vào.
        /// </summary>
        protected List<string> Get_DataField(string fieldName)
        {
            try
            {
                var vTblObj = MyBI.Me.Get_DWColumn_ByColName(fieldName);
                var whCode = MyBI.Me.Get_DashboardBy(this.DashboardCode).WHCode;
                var mdx = string.Format("SELECT [{0}{1}].[{2}].[{2}] ON ROWS, [Measures].[Quantity] ON COLUMNS FROM [{0}Cube]", whCode, vTblObj.TblName_Virtual, fieldName);
                var dt = Lib.Db.ExecuteMDX(GlobalVar.DbOLAP_ConnectionStr_Tiny, mdx);
                if (dt != null)
                    return dt.Rows.OfType<DataRow>().Select(p => Lib.NTE(p[fieldName])).ToList();
            }
            catch { }
            return new List<string>();
        }
        public virtual void Load_Params()
        {
            try
            {
                var container = this.Container_Params;
                container.Controls.Clear();
                jwcControlBase ctrl = null;

                // ReCreate...
                if (this.Ctrl_ParamsID.Count > 0)
                {
                    foreach (var paramID in this.Ctrl_ParamsID)
                    {
                        if (string.IsNullOrEmpty(paramID)) continue;
                        ctrl = this.Get_SwitchControl(paramID);
                        container.Controls.Add(ctrl);
                    }
                }
                else // Add new...
                {
                    var filters = this.IntrFilters;
                    if (filters == null) return;
                    foreach (var filter in filters)
                    {
                        var paramID = string.Format("{0}_{1}-{2}", this.DashboardCode, this.WidgetCode, filter.Name);
                        ctrl = this.Get_SwitchControl(paramID);
                        container.Controls.Add(ctrl);
                        this.Ctrl_ParamsID.Add(ctrl.ID);
                    }
                }
                // Add Command Apply.
                if (container.Controls.Count > 0)
                {
                    var cmdCtrl = this.Get_CmdApplyParam();
                    container.Controls.Add(cmdCtrl);
                }
            }
            catch { }
        }
        /// <summary>
        /// Lấy mã javaScript mà nó dùng để lấy toàn bộ giá trị của các tham số trên (portlet hoặc dashboard) hiện hành từ phía client.
        /// </summary>
        public virtual string Get_JsOnGetParamValues()
        {
            try
            {
                if (this.Container_Params.Controls.Count == 0) return "";
                // chỉ duyệt trên những control nào là edit thôi.
                var myCtrls = this.Ctrl_Params.Where(p => !p.ID.StartsWith("btnApplyParamFor_")).ToList();

                var ret = "";
                var count = myCtrls.Count;
                for (int i = 0; i < count; i++)
                {
                    var ctrl = myCtrls[i];
                    if (i < count - 1)
                        ret += "if(" + ctrl.FunctionJSGetValue + "().length!=0)" +
                                    "ret +=" + ctrl.FunctionJSGetValue + "() + ',';";
                    else
                        ret += "if(" + ctrl.FunctionJSGetValue + "().length!=0)" +
                                    "ret +=" + ctrl.FunctionJSGetValue + "();";
                }
                return ret;
            }
            catch { }
            return "";
        }
        /// <summary>
        /// Lấy mã javaScript dùng để post các tham số về server cho nút Apply hiện hành.
        /// </summary>
        public virtual string Get_JsOnApply()
        {
            try
            {
                var jsStr = this.Get_JsOnGetParamValues();
                //var jsStr = this.JsOnGetParamValues_DashboardAndPortlet;
                if (string.IsNullOrEmpty(jsStr)) return "";

                var ret = "var ret = '[';" + jsStr + "ret += ']';" + "alert(ret);";
                if (this.Is_DashboardParam)
                {
                    foreach (var clientName in this.WidgetClientInstanceNames)
                        ret += clientName + ".PerformCallback(ret);";
                }
                else
                    ret += this.WidgetClientInstanceName + ".PerformCallback(ret);";

                return ret;
            }
            catch { }
            return "";
        }
        public virtual string Get_JsAll()
        {
            try
            {
                var ret = "";
                var arrScripts = this.Ctrl_Params.Select(p => p.Render_JS()).ToList();
                foreach (var jsStr in arrScripts) ret += jsStr + "\r\n";
                return ret;
            }
            catch { }
            return "";
        }
        protected jwcControlBase Get_SwitchControl(string ctrlID)
        {
            jwcControlBase ctrl = null;
            try
            {
                var name = ctrlID.Split('-').Last();
                var filters = this.IntrFilters;
                if (filters == null) return null;
                var obj = filters.FirstOrDefault(p => p.Name == name);
                if (obj == null) return null;

                var timeFieldName = "";
                var ctrlName = obj.Control;
                if (ctrlName == "ComboBox")
                {
                    ctrl = this.LoadControl("~/App/jwc/jwcComboBox.ascx") as jwcComboBox;
                }
                else if (ctrlName == "CheckedComboBox")
                {
                    ctrl = this.LoadControl("~/App/jwc/jwcComboBoxChecked.ascx") as jwcComboBoxChecked;
                }
                else if (ctrlName == "TreeListBox")
                {
                }
                else if (ctrlName == "Calendar_Year")
                {
                    ctrl = this.LoadControl("~/App/jwc/jwcDateEdit.ascx") as jwcDateEdit;
                    ctrl.DisplayFormat = "yy";
                    timeFieldName = "Year";
                }
                else if (ctrlName == "Calendar_Quarter")
                {
                    ctrl = this.LoadControl("~/App/jwc/jwcDateEdit.ascx") as jwcDateEdit;
                    ctrl.DisplayFormat = "q";
                    timeFieldName = "Quarter";
                }
                else if (ctrlName == "Calendar_Period")
                {
                    ctrl = this.LoadControl("~/App/jwc/jwcDateEdit.ascx") as jwcDateEdit;
                    ctrl.DisplayFormat = "mm/yy";
                    timeFieldName = "Period";
                }
                else if (ctrlName == "Calendar_Day")
                {
                    ctrl = this.LoadControl("~/App/jwc/jwcDateEdit.ascx") as jwcDateEdit;
                    ctrl.DisplayFormat = "dd/mm/yy";
                    timeFieldName = "DateKey";
                }
                else if (ctrlName == "Calendar_Prev")
                {
                }
                ctrl.ID = ctrlID;
                ctrl.IsEnableRange = obj.EnableRange;
                ctrl.FunctionJSGetValue = string.Format("{0}_GetParam", ctrl.ID.Replace('-', '_'));
                ctrl.KeyField = !string.IsNullOrEmpty(timeFieldName) ? timeFieldName : obj.SourceField;
                ctrl.DataSource = this.Get_DataField(obj.SourceField);
                ctrl.Caption = obj.Caption;
            }
            catch { }
            return ctrl;
        }
        protected jwcControlBase Get_CmdApplyParam()
        {
            try
            {
                var ctrl = this.LoadControl("~/App/jwc/jwcCommandPost.ascx") as jwcCommandPost;
                ctrl.ID = string.Format("btnApplyParamFor_{0}", this.WidgetCode);
                ctrl.ToolTipText = "Apply the params for this portlet.";
                ctrl.JsOnApply = this.Get_JsOnApply();
                return ctrl;
            }
            catch { }
            return null;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Load_Params();
        }
        #endregion
    }
}