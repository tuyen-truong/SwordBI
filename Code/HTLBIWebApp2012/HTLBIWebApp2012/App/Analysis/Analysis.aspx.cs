using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Analysis
{
    public partial class Analysis : PageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ClientScriptManager csm = Page.ClientScript;
            Type csType = this.Page.GetType();
            String csKey = "BIResources";
            if (!csm.IsClientScriptBlockRegistered(csType, csKey))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type=\"text/javascript\">");
                sb.AppendFormat("var Milion_CurrencySymbol = '{0}';", Resources.BI.Milion_CurrencySymbol);
                sb.Append("</script>");
                csm.RegisterClientScriptBlock(csType, csKey, sb.ToString());
            }

            Control wc;
            switch(Get_Param_SubMenuCode())
            {
                case "asis_AC":
                    wc = LoadControl("wcACCube.ascx");
                    break;
                case "asis_IC":
                    wc = LoadControl("wcICCube.ascx");
                    break;
                default:
                    wc = LoadControl("wcSaleCube.ascx");
                    break;
            }
            AnalysisPlaceHolder.Controls.Add(wc);
        }
    }
}