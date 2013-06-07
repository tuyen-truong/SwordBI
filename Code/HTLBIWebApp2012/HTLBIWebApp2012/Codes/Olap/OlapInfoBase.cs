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
			get { return m_Caption; }
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