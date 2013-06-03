using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Setting.UserControls
{
	public partial class TestPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void uc1_click(object sender, EventArgs e)
		{
			uc1.Name = "New name";

		}
	}
}