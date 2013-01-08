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