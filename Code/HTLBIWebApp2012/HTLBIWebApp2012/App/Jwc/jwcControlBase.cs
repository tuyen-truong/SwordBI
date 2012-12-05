using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace HTLBIWebApp2012.App.Jwc
{
    public class jwcControlBase : System.Web.UI.UserControl
    {
        public string HtmlSpace = "&nbsp;&nbsp;";
        public string NewLine = "\r\n";
        public List<string> DataSource { get; set; }
        /// <summary>
        /// Chỉ ra control có hiện ra dạng từ...tới hay không.
        /// </summary>
        public bool IsEnableRange { get; set; }
        /// <summary>
        /// Tên field sẽ được nhận dạng cùng với giá trị khi trả về giá trị cho người dùng.
        /// </summary>
        public string KeyField { get; set; }
        /// <summary>
        /// Nhãn cho control
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Tên của hàm javascript sẽ dùng để lấy giá trị trả về
        /// </summary>
        public string FunctionJSGetValue { get; set; }
        public Control ContainerUpdatePanel { get; set; }
        /// <summary>
        /// Chuỗi mã javascript sử dụng cho sự kiện click của control ở phía client.
        /// </summary>
        public string JsOnApply { get; set; }
        public string ToolTipText { get; set; }
        /// <summary>
        /// Định dạng cho việc hiển thị nội dung bên trong control.
        /// </summary>
        public string DisplayFormat { get; set; }
        public string MyID { get { return this.ID.Replace('-', '_'); } }
        protected string Get_IDFrom()
        {
            return string.Format("{0}_From", this.MyID);
        }
        protected string Get_IDTo()
        {
            return string.Format("{0}_To", this.MyID);
        }
        public virtual string Render_HTML() { return ""; }
        public virtual string Render_JS() { return ""; }
        public virtual string Render_FullHTML() 
        {
            return this.Render_JS() + "\r\n" + this.Render_HTML(); 
        }
        public bool Has_Datasource()
        {
            return this.DataSource != null && this.DataSource.Count > 0;
        }
    }
}
