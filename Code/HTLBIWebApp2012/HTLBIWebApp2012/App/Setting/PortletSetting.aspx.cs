using System;
using System.Web.UI;
using DevExpress.Web.ASPxTabControl;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace HTLBIWebApp2012.App.Setting
{
	public partial class PortletSetting : PageBase
	{
		public ucDatasourceSetting DataSourceSetting
		{
			get
			{
				return this.ucDatasourceSetting1;
			}
		}
		public ucKPISetting My_wcKPISetting
		{
			get
			{
				return this.wcKPISetting1;
			}
		}
		public wcLayoutSetting My_wcLayoutSetting
		{
			get
			{
				return this.wcLayoutSetting1;
			}
		}
		public wcInteractionSetting My_wcInteractionSetting
		{
			get
			{
				return this.wcInteractionSetting1;
			}
		}

		/// <summary>
		/// Đối tượng TabPage hiện hành nào đang được Active.
		/// </summary>
		public TabPage CurrentActiveTabPage { get { return this.tabCtrl_PortletSetting.ActiveTabPage; } }
		/// <summary>
		/// Mã DataWarehouse được tham khảo từ TabPage_DatasourceSetting
		/// </summary>
		public string WHCode { get { return this.ucDatasourceSetting1.WHCode; } }
		/// <summary>
		/// Mã Datasource được tham khảo từ TabPage_DatasourceSetting
		/// </summary>
		public string DSCode { get { return this.ucDatasourceSetting1.DSCode; } }
		/// <summary>
		/// Mã KPI được tham khảo từ TabPage_KPISetting
		/// </summary>
		public string KPICode { get { return this.wcKPISetting1.KPICode; } }
		/// <summary>
		/// Mã Layout được tham khảo từ TabPage_LayoutSetting
		/// </summary>
		public string LayoutCode { get { return this.My_wcLayoutSetting.LayoutCode; } }

		protected String WidgetCode { get; set; }

		protected override object SaveControlState()
		{
			object[] controlState = new object[2];
			controlState[0] = base.SaveControlState();
			controlState[1] = WidgetCode;
			return controlState;
		}

		protected override void LoadControlState(object savedState)
		{
			object[] controlState = (object[])savedState;
			base.LoadControlState(controlState[0]);
			WidgetCode = (String)controlState[1];
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page.RegisterRequiresControlState(this);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Page.Title = "Portlet Setting";
			if (!IsPostBack)
			{
				WidgetCode = Get_Param(PageArgs.WidgetCode);
				lsttbl_Widget widget = MyBI.Me.Get_Widget_ByCode(WidgetCode);
				if (widget == null)
				{
					MySession.DSDefine_CurEditing = "";
				}
				else
				{
					DataSourceSetting.WHCode = widget.WHCode;
					DataSourceSetting.DSCode = widget.DataSourceCode;
					// KPI
					My_wcKPISetting.DSCode = widget.DataSourceCode;
					My_wcKPISetting.KPICode = widget.KPICode;
					// Layout
					My_wcLayoutSetting.LayoutCode = widget.Code;
				}
			}
            ucDatasourceSetting1.ValueChanged += new EventHandler(ucDatasourceSetting1_ValueChanged);
		}
		protected void wcKPISetting1_ValueChanged(object sender, EventArgs e)
		{
			String a = String.Empty;
			ucKPISetting kpi = (ucKPISetting)sender;
			
			//kpi.SetSource(My_wcDSSetting.DSCode);
			
		}

        protected void ucDatasourceSetting1_ValueChanged(object sender, EventArgs e)
		{
			String a = String.Empty;
			My_wcKPISetting.DSCode = DataSourceSetting.DSCode;
            //My_wcKPISetting.RaiseEvent("DS", EventArgs.Empty);
            My_wcKPISetting.DisplayName.Text = DateTime.Now.ToLongTimeString();
		}
	}
}