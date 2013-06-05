using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AnalysisServices.AdomdClient;
using HTLBIWebApp2012.Codes.Utils;
using DevExpress.Web.ASPxEditors;

namespace HTLBIWebApp2012.App.Setting.UserControls
{
	public partial class ucDatasourceSetting : System.Web.UI.UserControl
	{
		public event EventHandler NewButtonClicked;
		public event EventHandler FieldAddButtonClicked;
		public event EventHandler FieldRemoveButtonClicked;

		public String Name
		{
			get { return txtDataSourceName.Text; }
			set { txtDataSourceName.Text = value; }
		}

		public object DataWarehouse
		{
			get { return cbDataWarehouse.SelectedItem.Value; }
			set { cbDataWarehouse.SelectedItem.Value = value; }
		}

		public object DataSource
		{
			get { return cbDataSource.SelectedItem.Value;  }
			set { cbDataSource.SelectedItem.Value = value; }
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page.RegisterRequiresControlState(this);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			LoadFields();
			Helpers.SetDataSource(lbFields, DimFieldCollection, "UniqueName", "Caption");

			btnNewDataSource.Click += new EventHandler(btnNewDataSource_Click);
			btnFieldAdd.Click+= new EventHandler(FieldAdd_Click);
			btnFieldRemove.Click += new EventHandler(FieldRemove_Click);
		}

		protected override object SaveControlState()
		{
			object[] controlState = new object[3];
			controlState[0] = base.SaveControlState();
			controlState[1] = new Olap.DimensionFieldInfoCollection(lbSelectedFields.Items);
			//controlState[2] = filterContainer.Controls;
			return controlState;
		}

		protected override void LoadControlState(object savedState)
		{
			object[] controlState = (object[])savedState;
			base.LoadControlState(controlState[0]);
			Olap.DimensionFieldInfoCollection list = (Olap.DimensionFieldInfoCollection)controlState[1];
			Helpers.SetDataSource(lbSelectedFields, list, "UniqueName", "Caption");
			//ControlCollection ctls = (ControlCollection)controlState[2];
			//foreach (Control ctl in ctls)
			//{
			//    filterContainer.Controls.Add(ctl);
			//}
		}

		protected void btnNewDataSource_Click(object sender, EventArgs e)
		{
			lbSelectedFields.Items.Clear();
			if (NewButtonClicked != null)
			{
				NewButtonClicked(sender, e);
			}
		}

		protected void FieldAdd_Click(object sender, EventArgs e)
		{
			Olap.DimensionFieldInfoCollection selectedItems = new Olap.DimensionFieldInfoCollection(lbSelectedFields.Items);
			ListEditItem item = lbFields.SelectedItem;
			if (selectedItems.Find(item.Value.ToString()) == null)
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

		protected void popFilterMenu_Click(object sender, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
		{
			Control ctl = LoadControl("~/App/Setting/wcNormalFilter.ascx");
			filterContainer.Controls.Add(ctl);
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
			String connectionString = "Data source=(local);Initial Catalog=SWordBI_SSAS";
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
	}
}