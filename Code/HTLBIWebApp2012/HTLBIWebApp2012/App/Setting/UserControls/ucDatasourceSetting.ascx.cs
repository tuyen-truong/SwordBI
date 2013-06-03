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

		protected void Page_Load(object sender, EventArgs e)
		{
			Helpers.SetDataSource(lbFields, LoadFields(), "UniqueName", "Caption");

			btnNewDataSource.Click += new EventHandler(btnNewDataSource_Click);
			btnFieldAdd.Click+= new EventHandler(FieldAdd_Click);
			btnFieldRemove.Click += new EventHandler(FieldRemove_Click);
		}

		protected void btnNewDataSource_Click(object sender, EventArgs e)
		{
			if (NewButtonClicked != null)
			{
				NewButtonClicked(sender, e);
			}
		}

		protected void FieldAdd_Click(object sender, EventArgs e)
		{
			List<FieldInfo> selectedItems = new List<FieldInfo>();
			ListEditItemCollection items = lbFields.Items;
			//SelectedItemCollection items = lbFields.SelectedItems;
			foreach (ListEditItem it in items)
			{
				//if (it.Selected)
				//{
				//    selectedItems.Add(new FieldInfo(it.Value.ToString(), it.Text));
				//}
				selectedItems.Add(new FieldInfo(it.Value.ToString(), it.GetValue("Caption").ToString()));
			}
			Helpers.SetDataSource(lbSelectedFields, selectedItems, "UniqueName", "Caption");
			if (FieldAddButtonClicked != null)
			{
				FieldAddButtonClicked(sender, e);
			}
		}

		protected void FieldRemove_Click(object sender, EventArgs e)
		{
			if (FieldRemoveButtonClicked != null)
			{
				FieldRemoveButtonClicked(sender, e);
			}
		}

		internal class FieldInfo
		{
			private String m_UniqueName = String.Empty;
			public String UniqueName
			{
				get
				{
					return this.m_UniqueName;
				}
			}
			private String m_Caption = String.Empty;
			public String Caption
			{
				get
				{
					return this.m_Caption;
				}
			}
			private String m_Sort = String.Empty;
			public String Sort
			{
				get
				{
					return m_Sort;
				}
				set
				{
					m_Sort = value;
				}
			}

			public FieldInfo(string uniqueName, string caption)
			{
				this.m_UniqueName = uniqueName;
				this.m_Caption = caption;
			}
		}
		private List<FieldInfo> LoadFields()
		{
			//String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[OLAPConnector.OLAPConnectionString].ConnectionString;
			String connectionString = "Data source=(local);Initial Catalog=SWordBI_SSAS";
			//Dictionary<String, String> fields = new Dictionary<String, String>();
			List<FieldInfo> fields = new List<FieldInfo>();
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
				if (cubeDef == null) { return fields; }
				DimensionCollection dimCollection = cubeDef.Dimensions;
				foreach (Dimension dim in dimCollection)
				{
					HierarchyCollection hierarchyColl = dim.AttributeHierarchies;
					foreach (Hierarchy h in hierarchyColl)
					{
						//fields.Add(h.UniqueName, h.Caption);
						fields.Add(new FieldInfo(h.UniqueName, h.Caption));

					}
				}
			}
			return fields;
		}
	}
}