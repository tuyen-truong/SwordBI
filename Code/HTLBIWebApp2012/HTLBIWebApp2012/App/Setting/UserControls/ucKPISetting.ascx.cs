using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
	public partial class ucKPISetting : UserControlBase
	{
		public PortletSetting MyPage
		{
			get
			{
				return Page as PortletSetting;
			}
		}
		private String m_DSCode = "ds_20120704154325";// String.Empty;
		public String DSCode
		{
			get { return m_DSCode; }
			set { m_DSCode = value; }
		}
		private String m_KPICode = String.Empty;
		public String KPICode
		{
			get
			{
				if (String.IsNullOrWhiteSpace(m_KPICode))
				{
					m_KPICode = Lib.NTE(cbKPI.Value);
				}
				return m_KPICode;
			}
			set
			{
				m_KPICode = value;
				cbKPI.Value = m_KPICode;
				cbKPI_ValueChanged(cbKPI, EventArgs.Empty);
			}
		}
		public lsttbl_DashboardSource Kpi
		{
			get
			{
				if (String.IsNullOrWhiteSpace(KPICode))
				{
					return null;
				}
				return MyBI.Me.Get_DashboardSourceBy(m_KPICode);
			}
		}
		protected PartControlInfoCollection m_PartControls = new PartControlInfoCollection();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				String imgPathF = "~/Images/Control/{0}.png";
				cbCtrlType.Items.Clear();
				foreach (var item in Helpers.ArrDashboardCtrlType)
				{
					var img = string.Format(imgPathF, item.Cat);
					var litem = new ListEditItem(item.Name, item.Code, img);
					cbCtrlType.Items.Add(litem);
				}
				if (MyPage == null) // it is not a PortletSetting page
				{
					String kpicode = Request.QueryString[PageBase.PageArgs.KPICode];//Get_Param(PageBase.PageArgs.KPICode);
					String whCode = Request.QueryString[PageBase.PageArgs.WHCode];//Get_Param(PageBase.PageArgs.WHCode);
					if (!String.IsNullOrEmpty(kpicode) && !String.IsNullOrEmpty(whCode))
					{
						Helpers.SetDataSource(cbKPI, MyBI.Me.Get_DashboardKPI_ByWH(whCode), "Code", "NameVI");
						this.KPICode = kpicode;
					}
				}
				else // MyPage is PortletSetting
				{
					var kpis = MyBI.Me.Get_DashboardKPI_ByDS(m_DSCode).ToList();
					Helpers.SetDataSource(cbKPI, kpis, "Code", "NameVI");
				}
			}
		}

		protected override object SaveControlState()
		{
			object[] controlState = new object[2];
			controlState[0] = base.SaveControlState();
			controlState[1] = m_PartControls;
			return controlState;
		}

		protected override void LoadControlState(object savedState)
		{
			object[] controlState = (object[])savedState;
			base.LoadControlState(controlState[0]);
			m_PartControls = (PartControlInfoCollection)controlState[1];
			foreach (PartControlInfo ctrlInfo in m_PartControls.List)
			{
				AddKPIPartControl(ctrlInfo.ControlType, ctrlInfo.ID);
			}
		}

		protected void btnAddDimension_Click(object sender, EventArgs e)
		{
			KPIPartCtrlBase ctrl = AddKPIPartControl("dimension", string.Empty);
			m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
		}

		protected void cbKPI_ValueChanged(object sender, EventArgs e)
		{
			ASPxComboBox _sender = (ASPxComboBox)sender;
			if (Kpi == null) return;

			MySession.KPIDefine_CurEditing = Kpi.Code;
			txtKPIDisplayName.Text = Kpi.NameVI;
			var kpi = Kpi.JsonObjKPI;
			// Set value to control
			if (!string.IsNullOrEmpty(kpi.CtrlTypeDefault) && !string.IsNullOrEmpty(kpi.VisibleTypeDefault))
			{
				cbCtrlType.Value = kpi.CtrlTypeDefault;
				cbCtrlType_ValueChanged(cbCtrlType, EventArgs.Empty);
				cbCtrl.Value = string.Format("{0}-{1}", kpi.CtrlTypeDefault, kpi.VisibleTypeDefault);
			}
			/*
			// Add new Part controls of KPI is selected
			foreach (var part in kpi.Dimensions)
			{
				var myCtrl = this.Add_PartControl("dimension", false);
				myCtrl.Set_Info(part);
			}
			foreach (var part in kpi.Measures)
			{
				var myCtrl = this.Add_PartControl("measure", false);
				myCtrl.Set_Info(part);
			}
			foreach (var part in kpi.Contexts)
			{
				var type = "";
				if (part.HasCalcFields())
					type = "context-Calc";
				else
					type = "context-Normal";
				var myCtrl = this.Add_PartControl(type, false);
				myCtrl.Set_Info(part);
			}
			foreach (var part in kpi.Filters)
			{
				var myCtrl = this.Add_FilterControl(part.GetTinyType(), false);
				myCtrl.Set_Info(part);
			}
			// Raise Event OnChange.
			this.MyPage.My_wcLayoutSetting.Raise_OnChange("KPI", null);
			this.MyPage.My_wcDSSetting.Raise_OnChange("KPI", new HTLBIEventArgs(item.ParentCode));
			*/
		}

		protected void cbCtrlType_ValueChanged(object sender, EventArgs e)
		{
			ASPxComboBox cbo = (ASPxComboBox)sender;
			var arrCtrl = Helpers.ArrDashboardCtrl.Where(p => p.Cat == Lib.NTE(cbo.Value));
			var imgPathF = "~/Images/Control/{0}.png";

			cbCtrl.Items.Clear();
			cbCtrl.Text = "";
			foreach (var item in arrCtrl)
			{
				var value = string.Format("{0}-{1}", item.Cat, item.Code);
				var img = string.Format(imgPathF, value);
				var litem = new ListEditItem(item.Name, value, img);
				cbCtrl.Items.Add(litem);
			}
			cbCtrl.SelectedIndex = 0;
		}

		protected void btnNew_Click(object sender, EventArgs e)
		{

		}

		protected void KPIPartControl_OnRemove(object sender, EventArgs e)
		{
			try
			{
				var ctrlID = (sender.GetVal("Parent") as Control).ID;
				tabPageDimensionsContainer.Controls.RemoveAll(c => c.ID == ctrlID);
				m_PartControls.Remove(ctrlID);
				/*
				this.CtrlKPIPartIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
				this.ctrl_Dimensions.Controls.RemoveAll(p => p.ID == ctrlID);
				this.ctrl_Measures.Controls.RemoveAll(p => p.ID == ctrlID);
				this.ctrl_ContextMetric.Controls.RemoveAll(p => p.ID == ctrlID);
				*/
			}
			catch { }
		}

		#region Utility functions
		[Serializable()]
		public class PartControlInfo
		{
			private String m_ID = String.Empty;
			public String ID
			{
				get { return m_ID; }
				set { m_ID = value; }
			}

			private String m_ControlType = String.Empty;
			public String ControlType
			{
				get { return m_ControlType; }
				set { m_ControlType = value; }
			}
		}

		[Serializable()]
		public class PartControlInfoCollection
		{
			private List<PartControlInfo> m_list = new List<PartControlInfo>();
			public List<PartControlInfo> List
			{
				get { return m_list; }
			}

			public PartControlInfoCollection() { }

			public void Add(PartControlInfo controlInfo)
			{
				if (m_list.Find(item => item.ID == controlInfo.ID) == null)
				{
					m_list.Add(controlInfo);
				}
			}

			public void Remove(String id)
			{
				m_list.RemoveAll(c => c.ID == id);
			}

			public void Clear()
			{
				m_list.Clear();
			}
		}

		private KPIPartCtrlBase AddKPIPartControl(String type, String id)
		{
			if (String.IsNullOrWhiteSpace(type))
			{
				return null;
			}
			Control container = null;
			KPIPartCtrlBase ctrl = null;
			switch (type)
			{
				case "dimension":
					ctrl = LoadControl("~/App/Setting/wcKPIDimension.ascx") as KPIPartCtrlBase;
					container = tabPageDimensionsContainer;
					break;
				case "measure":
					ctrl = LoadControl("~/App/Setting/wcKPIMeasure.ascx") as KPIPartCtrlBase;
					break;
				case "context":
					ctrl = this.LoadControl("wcKPIContextMetric.ascx") as KPIPartCtrlBase;
					break;
				default:
					break;
			}
			if (string.IsNullOrWhiteSpace(id))
			{
				id = Guid.NewGuid().ToString();
			}
			ctrl.ID = id;
			ctrl.OnRemove += new EventHandler(KPIPartControl_OnRemove);
			container.Controls.Add(ctrl);
			return ctrl;
		}
		#endregion

		protected void btnAddMeasure_Click(object sender, EventArgs e)
		{

		}

		protected void popMenAddFilter_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
		{

		}

		protected void popMenAddCalcField_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
		{

		}

		protected void gridPreviewData_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
		{

		}

		protected void gridPreviewData_PageIndexChanged(object sender, EventArgs e)
		{

		}

		protected void gridPreviewData_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs e)
		{

		}
	}
}