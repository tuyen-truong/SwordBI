using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AnalysisServices.AdomdClient;
using HTLBIWebApp2012.Codes.Utils;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
	public partial class ucDatasourceSetting : UserControlBase
	{
		public PortletSetting MyPage
		{
			get
			{
				return Page as PortletSetting;
			}
		}

		public event EventHandler NewButtonClicked;
		public event EventHandler FieldAddButtonClicked;
		public event EventHandler FieldRemoveButtonClicked;

		public String Name
		{
			get { return txtDataSourceName.Text; }
			set { txtDataSourceName.Text = value; }
		}

		private String m_WHCode = String.Empty;
		public String WHCode
		{
			get { return m_WHCode; }
			set { m_WHCode = value; }
		}
		private String m_DSCode = String.Empty;
		public String DSCode
		{
			get
			{
				m_DSCode = cbDataSource.Value.ToString();
				return m_DSCode;
			}
			set { m_DSCode = value; }
		}
		public object DataWarehouse
		{
			get { return cbDataWarehouse.Value; }
			set
			{
				cbDataWarehouse.Value = value;
				cbDataWarehouse_ValueChanged(cbDataWarehouse, EventArgs.Empty);
			}
		}

		public object DataSource
		{
			get
			{
				return cbDataSource.Value; 
			}
			set
			{
				cbDataSource.Value = value;
				cbDataSource_ValueChanged(cbDataSource, EventArgs.Empty);
			}
		}

		protected List<InqFieldInfoMDX> m_SelectedFields = new List<InqFieldInfoMDX>();
		protected List<InqFieldInfoMDX> SelectedFields
		{
			get
			{
				ListEditItemCollection items = lbSelectedFields.Items;
				if (items.Count > 0)
				{
					int numOfItems = items.Count;
					for (int index = 0; index < numOfItems; index++)
					{
						ListEditItem item = items[index];
						InqFieldInfoMDX fld = new InqFieldInfoMDX(item);
						m_SelectedFields.Add(fld);
					}
				}
				return m_SelectedFields;
			}
		}

		protected List<InqSummaryInfoMDX> m_SelectedMetrics = new List<InqSummaryInfoMDX>();
		protected List<InqSummaryInfoMDX> SelectedMetrics
		{
			get
			{
				ListEditItemCollection items = lbSelectedMetricFields.Items;
				if (items.Count > 0)
				{
					int numOfItems = items.Count;
					for (int index = 0; index < numOfItems; index++)
					{
						ListEditItem item = items[index];
						InqSummaryInfoMDX fld = new InqSummaryInfoMDX(item);
						m_SelectedMetrics.Add(fld);
					}
				}
				return m_SelectedMetrics;
			}
		}

		protected List<InqFilterInfoMDX> m_SelectedFilters = new List<InqFilterInfoMDX>();
		protected List<InqFilterInfoMDX> SelectedFilters
		{
			get
			{
				foreach (FilterCtrlBase ctrl in filterContainer.Controls)
				{
					var filter = ctrl.Get_FilterInfo();
					if (filter == null) continue;
					// Set lại hàm tính toán trên field filter giống với hàm của field đó trong Summaries
					if (filter.HasHavingKey())
					{
						var objSummary = SelectedMetrics.FirstOrDefault(p => p.Field.KeyField == filter.HavingKey.Field.KeyField);
						if (objSummary != null)
							filter.HavingKey.FuncName = objSummary.FuncName;
					}
					m_SelectedFilters.Add(filter);
				}
				return m_SelectedFilters;
			}
		}

		protected FilterControlInfoCollection m_Filters = new FilterControlInfoCollection();

		public override void InitData()
		{
            // Data Warehouse
			if (!IsPostBack)
			{
				Helpers.SetDataSource(cbDataWarehouse, MyBI.Me.GetDW(), "Value", "Text");
				cbFieldSort.Items.AddRange(InqMDX.GetOrderByName());
				cbMetricSort.Items.AddRange(InqMDX.GetOrderByName());
				cbFuncs.Items.AddRange(InqMDX.GetSummatyFuncName());
			}
			// Query Information
			LoadFields();
			Helpers.SetDataSource(lbFields, DimFieldCollection, "UniqueName", "Caption");
			Helpers.SetDataSource(lbMetricFields, MeasureFieldCollection, "UniqueName", "Caption");
			// Events initialize
			btnNewDataSource.Click += new EventHandler(btnNewDataSource_Click);
			btnFieldAdd.Click += new EventHandler(FieldAdd_Click);
			btnFieldRemove.Click += new EventHandler(FieldRemove_Click);
			ValueChanged += new EventHandler(ucDatasourceSetting_ValueChanged);
		}

		protected void ucDatasourceSetting_ValueChanged(object sender, EventArgs e)
		{
			MyPage.My_wcKPISetting.RaiseEvent(String.Empty, EventArgs.Empty);
		}

		protected override object SaveControlState()
		{
			object[] controlState = new object[5];
			controlState[0] = base.SaveControlState();
			controlState[1] = new Olap.DimensionFieldInfoCollection(lbSelectedFields.Items);
			controlState[2] = new Olap.MeasureFieldInfoCollection(lbSelectedMetricFields.Items);
			controlState[3] = m_Filters;
			return controlState;
		}

		protected override void LoadControlState(object savedState)
		{
			object[] controlState = (object[])savedState;
			base.LoadControlState(controlState[0]);
			// Fields
			Olap.DimensionFieldInfoCollection list = (Olap.DimensionFieldInfoCollection)controlState[1];
			Helpers.SetDataSource(lbSelectedFields, list, "UniqueName", "Caption");
			// Metrics
			Olap.MeasureFieldInfoCollection list1 = (Olap.MeasureFieldInfoCollection)controlState[2];
			Helpers.SetDataSource(lbSelectedMetricFields, list1, "UniqueName", "Caption");
			// Filters
			m_Filters = (FilterControlInfoCollection)controlState[3];
			foreach (FilterControlInfo ctlFilter in m_Filters.Collection)
			{
				GenerateFilterControl(ctlFilter.Type, ctlFilter.ID);
			}
		}

		protected void btnNewDataSource_Click(object sender, EventArgs e)
		{
			cbDataSource.SelectedIndex = -1;
			Cleanup();

			MySession.DSDefine_CurEditing = null;

			if (NewButtonClicked != null)
			{
				NewButtonClicked(sender, e);
			}
		}

		protected void FieldAdd_Click(object sender, EventArgs e)
		{
			Olap.DimensionFieldInfoCollection selectedItems = new Olap.DimensionFieldInfoCollection(lbSelectedFields.Items);
			ListEditItem item = lbFields.SelectedItem;
			if (item != null
				&& selectedItems.Find(item.Value.ToString()) == null)
			{
				selectedItems.Add(new Olap.DimensionFieldInfo(item));
				Helpers.SetDataSource(lbSelectedFields, selectedItems, "UniqueName", "Caption");
			}
			if (FieldAddButtonClicked != null)
			{
				FieldAddButtonClicked(sender, e);
			}
		}

		protected void FieldRemove_Click(object sender, EventArgs e)
		{
			Olap.DimensionFieldInfoCollection selectedItems = new Olap.DimensionFieldInfoCollection(lbSelectedFields.Items);
			ListEditItem item = lbSelectedFields.SelectedItem;
			if (item != null
				&& item.GetValue("UniqueName") != null)
			{
				selectedItems.Remove(item.GetValue("UniqueName").ToString());
				Helpers.SetDataSource(lbSelectedFields, selectedItems, "UniqueName", "Caption");
				lbSelectedFields.SelectedIndex = -1;
			}
			if (FieldRemoveButtonClicked != null)
			{
				FieldRemoveButtonClicked(sender, e);
			}
		}

		protected void MeasureFieldAdd_Click(object sende, EventArgs e)
		{
			Olap.MeasureFieldInfoCollection selectedItems = new Olap.MeasureFieldInfoCollection(lbSelectedMetricFields.Items);
			ListEditItem item = lbMetricFields.SelectedItem;
			if (item != null
				&& selectedItems.Find(item.Value.ToString()) == null)
			{
				selectedItems.Add(new Olap.MeasureFieldInfo(item));
				Helpers.SetDataSource(lbSelectedMetricFields, selectedItems, "UniqueName", "Caption");
			}
		}

		protected void MeasureFieldRemove_Click(object sende, EventArgs e)
		{
			Olap.MeasureFieldInfoCollection selectedItems = new Olap.MeasureFieldInfoCollection(lbSelectedMetricFields.Items);
			ListEditItem item = lbSelectedMetricFields.SelectedItem;
			if (item != null
				&& item.GetValue("UniqueName") != null)
			{
				selectedItems.Remove(item.GetValue("UniqueName").ToString());
				Helpers.SetDataSource(lbSelectedMetricFields, selectedItems, "UniqueName", "Caption");
				lbSelectedFields.SelectedIndex = -1;
			}
		}

		protected void popFilterMenu_Click(object sender, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
		{
			String filterType = e.Item.Name;
			FilterCtrlBase ctl = GenerateFilterControl(filterType, String.Empty);
			m_Filters.Add(new FilterControlInfo(ctl) { Type = filterType });
		}

		protected FilterCtrlBase GenerateFilterControl(String filterType, String id)
		{
			if (string.IsNullOrWhiteSpace(filterType))
			{
				filterType = "NORMAL";
			}
			FilterCtrlBase ctl;
			if (filterType == "NUM")
			{
				ctl = LoadControl("~/App/Setting/wcNumFilter.ascx") as wcNumFilter;
			}
			else if (filterType == "DATE")
			{
				ctl = LoadControl("~/App/Setting/wcTimeFilter.ascx") as wcTimeFilter;
			}
			else
			{
				ctl = LoadControl("~/App/Setting/wcNormalFilter.ascx") as wcNormalFilter;
			}
			if (String.IsNullOrWhiteSpace(id))
			{
				id = Guid.NewGuid().ToString();
			}
			else
			{
				ctl.Mode = PartPlugCtrlBase.ControlMode.Edit;
			}
			ctl.ID = id;
			ctl.OnRemove += new EventHandler(FilterRemove_Click);
			filterContainer.Controls.Add(ctl);
			return ctl;
		}

		protected void FilterRemove_Click(object sender, EventArgs e)
		{
			String removedID = (sender.GetVal("Parent") as Control).ID;
			filterContainer.Controls.RemoveAll(c => c.ID == removedID);
			m_Filters.Remove(removedID);
		}

		protected void cbDataWarehouse_ValueChanged(object sender, EventArgs e)
		{
			MySession.DSDefine_CurEditing = null;

			ASPxComboBox cb = (ASPxComboBox)sender;
			object selectedItemValue = cb.SelectedItem != null ? cb.SelectedItem.Value : null;
			if (selectedItemValue != null)
			{
				m_WHCode = selectedItemValue.ToString();
				var datasource = MyBI.Me.Get_DashboardSource(m_WHCode, GlobalVar.SettingCat_DS);
				Helpers.SetDataSource(cbDataSource, datasource, "Code", "NameEN");
				cbDataSource.SelectedIndex = 0;
				cbDataSource_ValueChanged(cbDataSource, EventArgs.Empty);
			}
		}

		protected void cbDataSource_ValueChanged(object sender, EventArgs e)
		{
			Cleanup();

			// re-init information
			ASPxComboBox cb = (ASPxComboBox)sender;
			object selectedItemValue = cb.SelectedItem != null ? cb.SelectedItem.Value : null;
			if (selectedItemValue != null)
			{
				m_DSCode = selectedItemValue.ToString();
				lsttbl_DashboardSource datasource = MyBI.Me.Get_DashboardSourceBy(m_DSCode);
				if (datasource != null)
				{
					MySession.DSDefine_CurEditing = datasource.Code;

					txtDataSourceName.Text = datasource.NameEN;
					if (datasource.WHCode != Lib.NTE(cbDataWarehouse.Value))
					{
						cbDataWarehouse.Value = datasource.WHCode;
					}

					InqDefineSourceMDX inq = datasource.JsonObjMDX;
					Helpers.SetDataSource(lbSelectedFields, inq.Fields, "UniqueName", "Caption");
					Helpers.SetDataSource(lbSelectedMetricFields, inq.Summaries, "UniqueName", "Caption");
					foreach (InqFilterInfoMDX filter in inq.Filters)
					{
						FilterCtrlBase ctrl = GenerateFilterControl(filter.FilterType, String.Empty);
						ctrl.Set_Info(filter);
						m_Filters.Add(new FilterControlInfo(ctrl) { Type = filter.FilterType });
					}
				}
			}
			else
			{
				m_DSCode = String.Empty;
			}
            MyPage.My_wcKPISetting.DSCode = m_DSCode;
            MyPage.My_wcKPISetting.RaiseEvent("DS", EventArgs.Empty);
            if (IsPostBack)
            {
                ((UpdatePanel)MyPage.My_wcKPISetting.FindControl("KpiUpdatePanel")).Update();
            }
		}

		protected void lbSelectedFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			ASPxListBox _sender = (ASPxListBox)sender;

			txtFieldDispName.Text = _sender.SelectedItem.GetValue("DisplayName").ToString();
			cbFieldSort.Value = _sender.SelectedItem.GetValue("Sort");
		}

		protected void cbFieldSort_ValueChanged(object sender, EventArgs e)
		{
			ASPxComboBox _sender = (ASPxComboBox)sender;

			ListEditItem selectedItem = lbSelectedFields.SelectedItem;
			selectedItem.SetValue("Sort", _sender.SelectedItem.Value);
		}

		protected void txtFieldDispName_ValueChanged(object sender, EventArgs e)
		{
			ASPxTextBox _sender = (ASPxTextBox)sender;
			ListEditItem selectedItem = lbSelectedFields.SelectedItem;
			selectedItem.SetValue("DisplayName", _sender.Text.Trim());
		}

		protected void txtMetricDispName_ValueChanged(object sender, EventArgs e)
		{
			ASPxTextBox _sender = (ASPxTextBox)sender;
			ListEditItem selectedItem = lbSelectedMetricFields.SelectedItem;
			selectedItem.SetValue("DisplayName", _sender.Text.Trim());
		}

		protected void cbFuncs_SelectedIndexChanged(object sender, EventArgs e)
		{
			ASPxComboBox _sender = (ASPxComboBox)sender;

			ListEditItem selectedItem = lbSelectedMetricFields.SelectedItem;
			selectedItem.SetValue("Calc", _sender.SelectedItem.Value);
		}

		protected void cbMetricSort_SelectedIndexChanged(object sender, EventArgs e)
		{
			ASPxComboBox _sender = (ASPxComboBox)sender;

			ListEditItem selectedItem = lbSelectedMetricFields.SelectedItem;
			selectedItem.SetValue("Sort", _sender.SelectedItem.Value);
		}

		protected void lbSelectedMetricFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			ASPxListBox _sender = (ASPxListBox)sender;
			ListEditItem selectedItem = lbSelectedMetricFields.SelectedItem;
			if (selectedItem != null)
			{
				txtMetricDispName.Text = selectedItem.GetValue("DisplayName").ToString();
				cbFuncs.Value = selectedItem.GetValue("Calc").ToString();
				cbMetricSort.Value = selectedItem.GetValue("Sort").ToString();
			}
		}

		protected void Cleanup()
		{
			txtDataSourceName.Text = String.Empty;
			lbSelectedMetricFields.Items.Clear();
			lbSelectedFields.Items.Clear();
			// filters
			filterContainer.Controls.Clear();
			m_Filters.Clear();
			// Sort
			cbFieldSort.SelectedIndex = 0;
			cbMetricSort.SelectedIndex = 0;
			cbFuncs.SelectedIndex = 0;
		}

		public void Raise_OnChange(object sende, EventArgs e)
		{

		}

		private Olap.DimensionFieldInfoCollection m_DimFieldCollection = new Olap.DimensionFieldInfoCollection();
		public Olap.DimensionFieldInfoCollection DimFieldCollection
		{
			get { return m_DimFieldCollection; }
		}

		private Olap.MeasureFieldInfoCollection m_MeasureFieldCollection = new Olap.MeasureFieldInfoCollection();
		public Olap.MeasureFieldInfoCollection MeasureFieldCollection
		{
			get { return m_MeasureFieldCollection; }
		}

		private void LoadFields()
		{
			String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OLAPConnection"].ConnectionString;
			using (AdomdConnection conn = new AdomdConnection(connectionString))
			{
				conn.Open();
				CubeDef cubeDef = null;
				foreach (CubeDef cube in conn.Cubes)
				{
					if (cube.Type == CubeType.Cube)
					{
						cubeDef = cube;
						break;
					}
				}
				if (cubeDef == null)
				{
					m_DimFieldCollection.Clear();
					m_MeasureFieldCollection.Clear();
					return;
				}
				DimensionCollection dimCollection = cubeDef.Dimensions;
				foreach (Dimension dim in dimCollection)
				{
					if (dim.DimensionType == DimensionTypeEnum.Measure) { continue; }
					HierarchyCollection hierarchyColl = dim.Hierarchies;
					foreach (Hierarchy hier in hierarchyColl)
					{
						m_DimFieldCollection.Add(new Olap.DimensionFieldInfo(hier));

					}
				}
				MeasureCollection measureCollection = cubeDef.Measures;
				foreach (Measure m in measureCollection)
				{
					m_MeasureFieldCollection.Add(new Olap.MeasureFieldInfo(m));
				}
			}
		}

		[Serializable()]
		public class FilterControlInfo
		{
			private String m_ID = String.Empty;
			public String ID
			{
				get { return m_ID; }
				set { m_ID = value; }
			}

			private String m_Type = String.Empty;
			public String Type
			{
				get { return m_Type; }
				set { m_Type = value; }
			}

			private String m_SelectedValue = String.Empty;
			public String SelectedValue
			{
				get { return m_SelectedValue; }
				set { m_SelectedValue = value; }
			}

			public FilterControlInfo(Control ctl)
			{
				m_ID = ctl.ID;
				m_Type = ctl.GetType().Name;
			}
		}

		[Serializable()]
		public class FilterControlInfoCollection
		{
			private List<FilterControlInfo> m_list = new List<FilterControlInfo>();
			public List<FilterControlInfo> Collection
			{
				get { return m_list; }
			}

			public FilterControlInfoCollection() { }

			public void Add(FilterControlInfo ctrlInfo)
			{
				if (m_list.Find(item => item.ID == ctrlInfo.ID) == null)
				{
					m_list.Add(ctrlInfo);
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

		protected void btnSave_Click(object sender, EventArgs e)
		{
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OLAPConnection"].ConnectionString;
            var ret = new InqDefineSourceMDX(SelectedFields, SelectedMetrics, SelectedFilters)
			{
				PreffixDimTable = "AR",
                OlapCubeName = Helpers.GetCubeName(connectionString)
			};

			lsttbl_DashboardSource objDs = new lsttbl_DashboardSource()
			{
				Code = Lib.IfNOE(MySession.DSDefine_CurEditing, "ds_" + DateTime.Now.ToString("yyyyMMddHHmmss")),
				NameVI = txtDataSourceName.Text,
				NameEN = txtDataSourceName.Text,
				JsonStr = ret.ToJsonStr(),
				WHCode = Lib.NTE(cbDataWarehouse.Value),
				SettingCat = GlobalVar.SettingCat_DS 
			};
			MyBI.Me.Save_DashboardSource(objDs);
			if (objDs.Code == MySession.DSDefine_CurEditing)
			{
				cbDataSource.Text = txtDataSourceName.Text;
			}
			else
			{
				MySession.DSDefine_CurEditing = objDs.Code;
				// Reload Data Source
				m_WHCode = cbDataWarehouse.Value.ToString();
				var dsItems = MyBI.Me.Get_DashboardSource(m_WHCode, GlobalVar.SettingCat_DS);
				Helpers.SetDataSource(cbDataSource, dsItems, "Code", "NameEN");
				cbDataSource.Value = MySession.DSDefine_CurEditing;
			}
		}

		protected void dsGridPreviewData_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
		{
			var inq = new InqDefineSourceMDX(SelectedFields, SelectedMetrics, SelectedFilters)
			{
				PreffixDimTable = "AR",
				OlapCubeName = Helpers.GetCubeName(GlobalVar.DbOLAP_ConnectionStr_Tiny)
			};

			var ds = (new MdxExecuter(GlobalVar.DbOLAP_ConnectionStr_Tiny)).ExecuteDataSet(inq.ToMDX());
			dsGridPreviewData.DataSource = ds;
			dsGridPreviewData.DataBind();
		}

		protected void dsGridPreviewData_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs e)
		{

		}

		protected void dsGridPreviewData_PageIndexChanged(object sender, EventArgs e)
		{

		}

	}
}