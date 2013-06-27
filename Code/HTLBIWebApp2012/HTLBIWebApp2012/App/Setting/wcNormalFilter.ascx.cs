using System.Collections.Generic;
using System.Linq;
using CECOM;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
	public partial class wcNormalFilter : FilterCtrlBase
	{
		private InqFilterInfoMDX _info = null;
		protected void SetInfo()
		{
			if (_info == null) { return; }
			if (!_info.HasWhereKey()) return;
			this.cbbKeyField.Value = _info.WhereKey.UniqueName;
			this.cbbOperator.Value = _info.Operator;
			this.txtValue.Text = _info.Value.ToString();
			this.cbbAndOr.Value = _info.Logic;
		}

		public override void Load_InitData()
		{
			if (Mode == ControlMode.New)
			{
				this.cbbOperator.Items.Clear();
				this.cbbAndOr.Items.Clear();
				this.cbbOperator.Items.AddRange(InqMDX.GetFilterOperator());
				this.cbbAndOr.Items.AddRange(InqMDX.GetLogicCombine());
				base.Load_InitData();
				cbbOperator.SelectedIndex = 0;
				cbbAndOr.SelectedIndex = 0;
			}
			SetInfo();
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
			_info = info;
		}
	}
}