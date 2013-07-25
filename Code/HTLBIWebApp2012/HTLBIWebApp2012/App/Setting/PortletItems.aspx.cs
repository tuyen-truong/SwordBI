using System;
using System.Web.UI;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;
using System.Linq;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
	public partial class PortletItems : PageBase
	{
		private string WHCode { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			Page.Title = "Portlet List";
			if (!IsPostBack)
			{
				// Load Data WareHouse
				Helpers.SetDataSource(cboDataDW, MyBI.Me.GetDW(), "Value", "Text");

				WHCode = Get_Param(PageArgs.WHCode);
				if (!string.IsNullOrEmpty(WHCode))
				{
					cboDataDW.Value = WHCode;
					cboDataDW_ValueChanged(this.cboDataDW, new EventArgs());
				}
			}

		}
		protected void cboDataDW_ValueChanged(object sender, EventArgs e)
		{
			WHCode = Lib.NTE(cboDataDW.Value);
			IQueryable<lsttbl_Widget> widgets = MyBI.Me.Get_Widget(WHCode);
			if (widgets.Count() > 0)
			{
				gridPortletList.DataSource = MyBI.Me.Get_Widget(WHCode);
				gridPortletList.DataBind();
			}
			// Set Navigation Url
			DataSource.NavigateUrl = String.Format("DatasourceList.aspx?whcode={0}", WHCode);
			KpiList.NavigateUrl = String.Format("KpiList.aspx?whcode={0}", WHCode);
			// Set new url
			lnkAddNew.NavigateUrl = String.Format("PortletSetting.aspx?whcode={0}", WHCode);
		}
	}
}