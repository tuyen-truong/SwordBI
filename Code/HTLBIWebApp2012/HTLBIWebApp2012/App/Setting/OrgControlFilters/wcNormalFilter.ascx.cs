using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Codes.Utils;
using CECOM;
using HTLBIWebApp2012.Codes.Models;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcNormalFilter : FilterCtrlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override void Load_InitData()
        {
            this.cbbOperator.Items.Clear();
            this.cbbAndOr.Items.Clear();
            this.cbbOperator.Items.AddRange(new string[] { "=", "<", "<=", ">", ">=", "<>" });
            this.cbbOperator.SelectedIndex = 0;
            this.cbbAndOr.Items.AddRange(new string[] { "AND", "OR" });
            this.cbbAndOr.SelectedIndex = 0;
        }
        public void Set_KeyFieldSource(object src, string valField, string txtField)
        {
            Helpers.SetDataSource(this.cbbKeyField, src, valField, txtField);
        }
        public override InqFilterInfo Get_FilterInfo()
        {
            try
            {
                var ds = this.cbbKeyField.DataSource as List<lsttbl_DWColumn>;
                if (ds == null) return null;

                var keyField = Lib.NTE(this.cbbKeyField.Value);
                var item = ds.FirstOrDefault(p => p.KeyField == keyField);
                var ret = new InqFilterInfo();
                ret.WhereKey = new InqFieldInfo(item.TblName, item.ColName, item.DataType);
                ret.Logic = Lib.NTE(this.cbbAndOr.Value);
                ret.Operator = this.cbbOperator.Text;
                ret.Value = this.txtValue.Text;
                return ret;
            }
            catch { return null; }
        }
        public override void Set_FilterInfo(InqFilterInfo info)
        {
            this.cbbKeyField.Value = info.WhereKey.KeyField;
            this.cbbOperator.Value = info.Operator;
            this.txtValue.Text = info.Value.ToString();
            this.cbbAndOr.Value = info.Logic;
        }
        protected void btnDelFilter_Click(object sender, EventArgs e)
        {
            this.Raise_RemoveFilter(e);
        }
    }
}