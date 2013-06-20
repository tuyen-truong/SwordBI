using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;
using Microsoft.AnalysisServices.AdomdClient;

namespace HTLBIWebApp2012.Olap
{
	[Serializable()]
	public class OlapInfoBase
	{
		private String m_Caption = String.Empty;
		public virtual String Caption
		{
			get
            {
                if (String.IsNullOrWhiteSpace(m_Caption))
                {
                    m_Caption = m_Name;
                }
                return m_Caption;
            }
			set { m_Caption = value; }
		}

		private String m_Description = String.Empty;
		public virtual String Description
		{
			get { return m_Description; }
			set { m_Description = value; }
		}

		private String m_Name = String.Empty;
		public virtual String Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		private String m_UniqueName = String.Empty;
		public virtual String UniqueName
		{
			get { return m_UniqueName; }
			set { m_UniqueName = value; }
		}

		private String m_DisplayName = String.Empty;
		public virtual String DisplayName
		{
			get
			{
				if (String.IsNullOrWhiteSpace(m_DisplayName))
				{
					m_DisplayName = m_Caption;
				}
				return m_DisplayName;
			}
			set { m_DisplayName = value; }
		}

		protected String ToStr(object obj, String strDefaultValue)
		{
			if (obj is string)
			{
				return obj as string;
			}
			return String.IsNullOrWhiteSpace(strDefaultValue) ? String.Empty : strDefaultValue;
		}
	}

	[Serializable()]
	public sealed class DimensionFieldInfo : OlapInfoBase
	{
		private String m_Sort = String.Empty;
		public String Sort
		{
			get { return m_Sort; }
			set { m_Sort = value; }
		}

		public DimensionFieldInfo() { }

		public DimensionFieldInfo(Hierarchy hierarchy)
		{
			Caption = hierarchy.Caption;
			Name = hierarchy.Name;
			Description = hierarchy.Description;
			UniqueName = hierarchy.UniqueName;
		}

		public DimensionFieldInfo(ListEditItem item)
		{
			Caption = ToStr(item.GetValue("Caption"), String.Empty);
			Name = ToStr(item.GetValue("Name"), String.Empty);
			Description = ToStr(item.GetValue("Description"), String.Empty);
			UniqueName = ToStr(item.GetValue("UniqueName"), String.Empty);
			Sort = ToStr(item.GetValue("Sort"), String.Empty);
			DisplayName = ToStr(item.GetValue("DisplayName"), String.Empty);
		}
	}

	[Serializable()]
	public class DimensionFieldInfoCollection : List<DimensionFieldInfo>
	{
		public DimensionFieldInfoCollection() { }
		public DimensionFieldInfoCollection(ListEditItemCollection items)
		{
			foreach(ListEditItem item in items)
			{
				Add(new DimensionFieldInfo(item));
			}
		}

		public DimensionFieldInfoCollection(List<InqFieldInfoMDX> fields)
		{
			foreach (InqFieldInfoMDX fld in fields)
			{
				DimensionFieldInfo fldInfo = new DimensionFieldInfo();
				fldInfo.Caption = fld.ColName;
				fldInfo.DisplayName = fld.ColName;
				fldInfo.UniqueName = Guid.NewGuid().ToString();
				Add(fldInfo);
			}
		}

		public DimensionFieldInfo Find(String uniqueName)
		{
			return base.Find(item => item.UniqueName == uniqueName);
		}

		public bool Remove(String uniqueName)
		{
			return base.Remove(this.Find(uniqueName));
		}
	}

	[Serializable()]
	public sealed class MeasureFieldInfo : OlapInfoBase
	{
		private String m_Sort = String.Empty;
		public String Sort
		{
			get { return m_Sort; }
			set { m_Sort = value; }
		}

		private String m_Calc = String.Empty;
		public String Calc
		{
			get { return m_Calc; }
			set { m_Calc = value; }
		}

		public MeasureFieldInfo(ListEditItem item)
		{
			Caption = ToStr(item.GetValue("Caption"), String.Empty);
			Name = ToStr(item.GetValue("Name"), String.Empty);
			Description = ToStr(item.GetValue("Description"), String.Empty);
			UniqueName = ToStr(item.GetValue("UniqueName"), String.Empty);
			DisplayName = ToStr(item.GetValue("DisplayName"), String.Empty);
			Sort = ToStr(item.GetValue("Sort"), String.Empty);
			Calc = ToStr(item.GetValue("Calc"), String.Empty);
		}

		public MeasureFieldInfo(Measure measure)
		{
			Caption = measure.Caption;
			Description = measure.Description;
			UniqueName = measure.UniqueName;
			Name = measure.Name;
		}
	}

	[Serializable()]
	public class MeasureFieldInfoCollection : List<MeasureFieldInfo>
	{
		public MeasureFieldInfoCollection() { }
		public MeasureFieldInfoCollection(ListEditItemCollection items)
		{
			foreach (ListEditItem item in items)
			{
				Add(new MeasureFieldInfo(item));
			}
		}

		public MeasureFieldInfo Find(String uniqueName)
		{
			return base.Find(item => item.UniqueName == uniqueName);
		}

		public bool Remove(String uniqueName)
		{
			return base.Remove(this.Find(uniqueName));
		}
	}
}