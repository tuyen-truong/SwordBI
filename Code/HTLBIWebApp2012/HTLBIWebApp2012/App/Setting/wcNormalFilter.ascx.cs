using System.Collections.Generic;
using System.Linq;
using CECOM;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
	public partial class wcNormalFilter : FilterCtrlBase
	{
		public override void Load_InitData()
		{
			this.cbbOperator.Items.Clear();
			this.cbbAndOr.Items.Clear();
			this.cbbOperator.Items.AddRange(InqMDX.GetFilterOperator());
			this.cbbOperator.SelectedIndex = 0;
			this.cbbAndOr.Items.AddRange(InqMDX.GetLogicCombine());
			this.cbbAndOr.SelectedIndex = 0;
			base.Load_InitData();
		}
		public override InqFilterInfoMDX Get_FilterInfo()
		{
			try
			{
				InqFilterInfoMDX ret = new InqFilterInfoMDX();
				
				InqFieldInfoMDX whereKey = new InqFieldInfoMDX();
				whereKey.Name = cbbKeyField.Text;
				whereKey.UniqueName = Lib.NTE(cbbKeyField.Value);
				ret.WhereKey = whereKey;

				ret.Logic = Lib.NTE(this.cbbAndOr.Value);
				ret.Operator = this.cbbOperator.Text;
				ret.Value = this.txtValue.Text;
				ret.FilterType = "NORMAL";
				return ret;
			}
			catch { return null; }
		}
		public override void Set_Info(InqFilterInfoMDX info)
		{
			if (!info.HasWhereKey()) return;
			this.cbbKeyField.Value = info.WhereKey.UniqueName;
			this.cbbOperator.Value = info.Operator;
			this.txtValue.Text = info.Value.ToString();
			this.cbbAndOr.Value = info.Logic;
		}
	}
}