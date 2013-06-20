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
        const int MAX_CONTROL_STATE = 5;
        public ASPxTextBox DisplayName { get { return txtKPIDisplayName; } }
		public ASPxComboBox CtlKPI { get { return cbKPI; } }

		public PortletSetting MyPage
		{
			get
			{
				return Page as PortletSetting;
			}
		}
		private String m_DSCode = String.Empty;
		public String DSCode
		{
			get
            {
                if (MyPage != null)
                {
                    m_DSCode = MyPage.DataSourceSetting.DSCode;
                }
                return m_DSCode;
            }
			set
			{
				m_DSCode = value;
			}
		}

        private String m_WHCode = String.Empty;
        public String WHCode 
        {
            get
            {
                if (MyPage != null)
                {
                    m_WHCode = MyPage.DataSourceSetting.WHCode;
                }
                return m_WHCode;
            }
            set
            {
                m_WHCode = value;
            }
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

		public override void InitData()
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
			ValueChanged += new EventHandler(ucKPISetting_ValueChanged);
		}

		protected void ucKPISetting_ValueChanged(object senderm, EventArgs e)
		{
            Cleanup();
            var kpis = MyBI.Me.Get_DashboardKPI_ByDS(m_DSCode).ToList();
			Helpers.SetDataSource(cbKPI, kpis, "Code", "NameVI");
			cbKPI.SelectedIndex = 0;
			cbKPI_ValueChanged(cbKPI, EventArgs.Empty);
		}

		protected override object SaveControlState()
		{
            object[] controlState = new object[MAX_CONTROL_STATE];
			controlState[0] = base.SaveControlState();
			controlState[1] = m_PartControls;
            controlState[2] = DSCode;
            controlState[3] = WHCode;
			return controlState;
		}

		protected override void LoadControlState(object savedState)
		{
			object[] controlState = (object[])savedState;
			base.LoadControlState(controlState[0]);
			// PartControl
            m_PartControls = (PartControlInfoCollection)controlState[1];
			foreach (PartControlInfo ctrlInfo in m_PartControls.List)
			{
				AddKPIPartControl(ctrlInfo.ControlType, ctrlInfo.ID);
			}
            DSCode = (String)controlState[2];
            // WHCode
            WHCode = (String)controlState[3];
		}

		protected void btnAddDimension_Click(object sender, EventArgs e)
		{
			KPIPartCtrlBase ctrl = AddKPIPartControl("dimension", string.Empty);
			m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
		}

		protected void cbKPI_ValueChanged(object sender, EventArgs e)
		{
			ASPxComboBox _sender = (ASPxComboBox)sender;
			lsttbl_DashboardSource _Kpi = Kpi;
            if (_Kpi == null)
            {
                return;
            }

			MySession.KPIDefine_CurEditing = _Kpi.Code;
			txtKPIDisplayName.Text = _Kpi.NameVI;
			var kpi = _Kpi.JsonObjKPI;
			// Set value to control
			if (!string.IsNullOrEmpty(kpi.CtrlTypeDefault) && !string.IsNullOrEmpty(kpi.VisibleTypeDefault))
			{
				cbCtrlType.Value = kpi.CtrlTypeDefault;
				cbCtrlType_ValueChanged(cbCtrlType, EventArgs.Empty);
				cbCtrl.Value = string.Format("{0}-{1}", kpi.CtrlTypeDefault, kpi.VisibleTypeDefault);
			}
			
			// Add new Part controls of KPI is selected
			foreach (var part in kpi.Dimensions)
			{
				KPIPartCtrlBase ctrl = AddKPIPartControl("dimension", String.Empty);
				ctrl.Set_Info(part);
				m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
			}
			
			foreach (var part in kpi.Measures)
			{
				KPIPartCtrlBase ctrl = AddKPIPartControl("measure", String.Empty);
				ctrl.Set_Info(part);
				m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
			}
			
			foreach (var part in kpi.Contexts)
			{
				var type = "";
				if (part.HasCalcFields())
					type = "context-calc";
				else
					type = "context-normal";
				KPIPartCtrlBase ctrl = AddKPIPartControl(type, String.Empty);
				ctrl.Set_Info(part);
				m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
			}
			/*
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

        void Cleanup()
        {
            txtKPIDisplayName.Text = String.Empty;
            cbKPI.Text = String.Empty;
            cbKPI.Items.Clear();
            cbKPI.SelectedIndex = -1;
            cbCtrl.Items.Clear();
            if (cbCtrlType.Items.Count > 0)
            {
                cbCtrlType.SelectedIndex = 0;
                cbCtrlType_ValueChanged(cbCtrlType, EventArgs.Empty);
            }
            // clear added control
            tabPageDimensionsContainer.Controls.Clear();
            measureContainer.Controls.Clear();
            kpiFilterContainer.Controls.Clear();
            kpiContextMetricContainer.Controls.Clear();
            m_PartControls.Clear();
        }
        protected void btnNew_Click(object sender, EventArgs e)
		{
            Cleanup();
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

		protected void btnAddMeasure_Click(object sender, EventArgs e)
		{
			KPIPartCtrlBase ctrl = AddKPIPartControl("measure", string.Empty);
			m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
		}

		protected void popMenAddFilter_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
		{
			KPIPartCtrlBase ctrl = AddKPIPartControl(String.Format("filter-{0}", e.Item.Name).ToLower(), string.Empty);
			m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
		}

		protected void popMenAddCalcField_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
		{
			KPIPartCtrlBase ctrl = AddKPIPartControl(String.Format("context-{0}", e.Item.Name).ToLower(), string.Empty);
			m_PartControls.Add(new PartControlInfo() { ID = ctrl.ID, ControlType = ctrl.PartType });
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
					container = measureContainer;
					break;
				case "context-normal":
					ctrl = this.LoadControl("~/App/Setting/wcKPIContextMetric.ascx") as KPIPartCtrlBase;
					container = kpiContextMetricContainer;
					break;
				case "context-calc":
					ctrl = this.LoadControl("~/App/Setting/wcKPIContextMetric_Calc.ascx") as KPIPartCtrlBase;
					container = kpiContextMetricContainer;
					break;
				case "filter-normal":
					ctrl = this.LoadControl("~/App/Setting/wcNormalFilter.ascx") as KPIPartCtrlBase;
					ctrl.PartType = "filter-normal";
					container = kpiFilterContainer;
					break;
				case "filter-num":
					ctrl = this.LoadControl("~/App/Setting/wcNumFilter.ascx") as KPIPartCtrlBase;
					ctrl.PartType = "filter-num";
					break;
				default: // date
					ctrl = this.LoadControl("~/App/Setting/wcTimeFilter.ascx") as KPIPartCtrlBase;
					ctrl.PartType = "filter-date";
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

		public string DSCode_Target { get; set; }

		internal void Raise_OnChange(string p, EventArgs eventArgs)
		{
			
		}

		public void SetSource(String dscode)
		{
			var kpis = MyBI.Me.Get_DashboardKPI_ByDS(dscode).ToList();
			Helpers.SetDataSource(cbKPI, kpis, "Code", "NameVI");
			cbKPI.SelectedIndex = 0;
		}

        KPIDefineSource GetDefineInfo()
        {
            KPIDefineSource kpiDefineInfo = new KPIDefineSource()
            {
                DisplayName = txtKPIDisplayName.Text,
                DatasourceID = DSCode,
                CtrlTypeDefault = Lib.IsNOE(cbCtrlType.Value) ? "" : Lib.NTE(cbCtrlType.Value),
                VisibleTypeDefault = Lib.IsNOE(cbCtrl.Value) ? "" : Lib.NTE(cbCtrl.Value).Split('-', StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
                MaxValue = double.Parse(this.txtMaxValue.Text),
                MinValue = double.Parse(this.txtMinValue.Text)
            };
            return kpiDefineInfo;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KPIDefineSource kpiDefine = GetDefineInfo();
                // Gọi hàm save
                var obj = new lsttbl_DashboardSource()
                {
                    Code = Lib.IfNOE(MySession.KPIDefine_CurEditing, "kpi_" + DateTime.Now.ToString("yyyyMMddHHmmss")),
                    ParentCode = kpiDefine.DatasourceID,
                    JsonStr = kpiDefine.ToJsonStr(),
                    NameVI = txtKPIDisplayName.Text,
                    NameEN = txtKPIDisplayName.Text,
                    WHCode = WHCode,
                    SettingCat = GlobalVar.SettingCat_KPI
                };
                MyBI.Me.Save_DashboardSource(obj);
                MySession.KPIDefine_CurEditing = obj.Code;
            }
            catch(Exception ex)
            {
#if DEBUG
                throw ex;
#else
#endif
            }
        }
	}
}