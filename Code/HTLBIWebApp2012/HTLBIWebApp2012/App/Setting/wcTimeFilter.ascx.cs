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
    public partial class wcTimeFilter : FilterCtrlBase
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
                ret.WhereKey = new InqFieldInfoMDX(item.TblName_Virtual, item.ColName, item.DataType);
                ret.Logic = Lib.NTE(this.cbbAndOr.Value);
                ret.Operator = this.cbbOperator.Text;
                if (item.ColName == "Year") ret.Value = this.txtValue.Date.Year;
                else if (item.ColName == "Quarter") ret.Value = this.txtValue.Date.Day;
                else if (item.ColName == "Period") ret.Value = this.txtValue.Date.ToString("yyyyMM");
                else ret.Value = this.txtValue.Date.ToString("yyyy/MM/dd");
                return ret;
            }
            catch { return null; }
        }
        public override void Set_Info(InqFilterInfoMDX info)
        {
            if (!info.HasWhereKey()) return;
            this.cbbKeyField.Value = info.WhereKey.KeyField;
            this.cbbOperator.Value = info.Operator;
            this.txtValue.Value = info.ToTimeValue();
            this.cbbAndOr.Value = info.Logic;

            var ds = this.cbbKeyField.DataSource as List<lsttbl_DWColumn>;
            var fieldName = ds.FirstOrDefault(p => p.KeyField == info.WhereKey.KeyField).ColName;
            this.Set_FormatString(fieldName);
        }
        protected void Set_FormatString(string fieldName)
        {
            try
            {
                var fStr = "dd/MM/yyyy";
                if (fieldName == "Year")
                    fStr = "yyyy";
                else if (fieldName == "Quarter")
                    fStr = "dd";
                else if (fieldName == "Period")
                    fStr = "MM/yyyy";
                this.txtValue.DisplayFormatString = fStr;
                this.txtValue.EditFormatString = fStr;
                this.txtValue.ToolTip = string.Format("Time value, format by: {0}", fStr == "dd" ? "Quater" : fStr);
            }
            catch { }
        }
        protected void cbbKeyField_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var ds = this.cbbKeyField.DataSource as List<lsttbl_DWColumn>;
                var fieldName = ds.FirstOrDefault(p => p.KeyField == Lib.NTE(this.cbbKeyField.Value)).ColName;
                this.Set_FormatString(fieldName);
                this.txtValue.Focus();
            }
            catch { }
        }
    }
}