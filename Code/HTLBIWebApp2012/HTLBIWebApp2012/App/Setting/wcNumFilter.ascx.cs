using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcNumFilter : FilterCtrlBase
    {
        public override void Load_InitData()
        {
            this.cbbOperator.Items.Clear();
            this.cbbAndOr.Items.Clear();
            this.cbbOperator.Items.AddRange(InqMDX.GetFilterOperator());
            this.cbbOperator.SelectedIndex = 0;
            this.cbbAndOr.Items.AddRange(InqMDX.GetLogicCombine());
            this.cbbAndOr.SelectedIndex = 0;
        }
        public override InqFilterInfoMDX Get_FilterInfo()
        {
            try
            {
                var ds = this.cbbKeyField.DataSource as List<lsttbl_DWColumn>;
                if (ds == null) return null;

                var keyField = Lib.NTE(this.cbbKeyField.Value);
                var item = ds.FirstOrDefault(p => p.KeyField == keyField);
                var ret = new InqFilterInfoMDX();
                ret.HavingKey = new InqSummaryInfoMDX(new InqFieldInfoMDX(item.TblName_Virtual, item.ColName, item.DataType), "SUM");
                ret.Logic = Lib.NTE(this.cbbAndOr.Value);
                ret.Operator = this.cbbOperator.Text;
                if (Lib.StringIsDigit(this.txtValue.Text))
                    ret.Value = this.txtValue.Text;
                else
                    ret.Value = Lib.GetStringDigit(this.txtValue.Text);
                return ret;
            }
            catch { return null; }
        }
        public override void Set_Info(InqFilterInfoMDX info)
        {
            if (!info.HasHavingKey()) return;
            this.cbbKeyField.Value = info.HavingKey.Field.KeyField;
            this.cbbOperator.Value = info.Operator;
            this.txtValue.Text = info.Value.ToString();
            this.cbbAndOr.Value = info.Logic;
        }
    }
}