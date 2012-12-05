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
    public partial class wcNumFilter : FilterCtrlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var jsResource = new LiteralControl();
            //jsResource.Text =
            //    "<script src=\"../../Scripts/JQuery/jquery-1.7.2.min.js\" type=\"text/javascript\"></script>" +
            //    "<script src=\"../../Scripts/JQuery/autoNumeric-1.7.4.js\" type=\"text/javascript\"></script>" +
            //    "$(document).ready(function () {" +
            //        "$(\".numericInput\").autoNumeric({ aSep: '.', aDec: ',', mDec: '2', vMax: '999999999999999999' });" +
            //        "$(\".numericInput\").css(\"text-align\", \"right\");" +
            //    "});";
            //Page.Header.Controls.Add(jsResource);
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
                ret.HavingKey = new InqSummaryInfo(new InqFieldInfo(item.TblName, item.ColName, item.DataType), "SUM");
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
        public override void Set_FilterInfo(InqFilterInfo info)
        {
            this.cbbKeyField.Value = info.HavingKey.KeyField;
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