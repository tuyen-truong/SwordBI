using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class LayoutList : PageBase
    {
        protected String WHCode { get; set; }
        protected String LayoutCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WHCode = Get_Param(PageArgs.WHCode);
                Helpers.SetDataSource(cboDataDW, MyBI.Me.GetDW(), "Value", "Text");
                cboDataDW.Value = WHCode;
                cboDataDW_ValueChanged(cboDataDW, new EventArgs());
            }
        }

        protected void cboDataDW_ValueChanged(object sender, EventArgs e)
        {
            WHCode = Lib.NTE(cboDataDW.Value);
            gridLayoutList.DataSource = MyBI.Me.Get_Widget(WHCode);
            gridLayoutList.DataBind();
        }
    }
}