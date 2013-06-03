using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTLBIWebApp2012.Olap
{
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
	}
}