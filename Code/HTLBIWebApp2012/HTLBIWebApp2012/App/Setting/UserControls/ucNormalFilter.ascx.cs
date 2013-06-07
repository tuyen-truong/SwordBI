using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012.App.Setting.UserControls
{
	public partial class ucNormalFilter : System.Web.UI.UserControl
	{
	
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Remove_Click(object sender, EventArgs e)
		{
			this.Parent.Controls.Remove(this);
		}
	}
}