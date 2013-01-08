using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            radio1.Attributes.Add("ViewIndex", "0");
            radio2.Attributes.Add("ViewIndex", "1");
            radio1.AutoPostBack = true;
            radio2.AutoPostBack = true;
            Control wc = LoadControl("/UserControls/WebUserControl1.ascx");
            PlaceHolder1.Controls.Add(wc);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio == null) { return; }
            int ViewIndex = 0;
            int.TryParse(radio.Attributes["ViewIndex"], out ViewIndex);
            MultiView1.ActiveViewIndex = ViewIndex;

        }
    }
}
