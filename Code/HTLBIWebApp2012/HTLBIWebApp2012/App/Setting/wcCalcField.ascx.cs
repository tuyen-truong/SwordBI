using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using CECOM;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcCalcField : CalcFieldCtrlBase
    {
        public override void Load_InitData()
        {
            this.cboOperator.Items.Clear();
            this.cboOperator.Items.AddRange(CalcField.GetCalcOperator());
            this.cboOperator.SelectedIndex = 0;
        }
        public override void Set_Source(object ds)
        {
            try
            {
                this.Reset_Info();
                var lstDS = ds.GetVal("DS");
                Helpers.SetDataSource(this.cboMember1, lstDS, "Code", "Name", this.cboMember1.Value);
                Helpers.SetDataSource(this.cboMember2, lstDS, "Code", "Name", this.cboMember2.Value);
                // Generate lblName
                var calcID = ds.GetInt32("CalcID");
                this.lblName.Text = string.Format("Calc_{0}", (calcID + 1).ToString("000"));
            }
            catch { }
        }
        public override void Set_Info(CalcField info)
        {
            try
            {
                this.lblName.Text = info.Name;
                this.cboMember1.Value = info.Member1;
                this.txtMember1.Text = Lib.IfNOE(info.Member1, "0");
                this.cboOperator.Value = info.Operator;
                this.cboMember2.Value = info.Member2;
                this.txtMember2.Text = Lib.IfNOE(info.Member2, "0");
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            this.cboMember1.Items.Clear();
            this.txtMember1.Text = "0";
            this.cboMember2.Items.Clear();
            this.txtMember2.Text = "0";
        }
        public override CalcField Get_CalcFieldInfo()
        {
            try
            {
                var ret = new CalcField
                {
                    Name = this.lblName.Text,
                    Member1 = this.txtMember1.Visible ? Lib.IfNOE(this.txtMember1.Text, "0") : Lib.NTE(this.cboMember1.Value),
                    Operator = Lib.NTE(this.cboOperator.Value),
                    Member2 = this.txtMember2.Visible ? Lib.IfNOE(this.txtMember2.Text, "0") : Lib.NTE(this.cboMember2.Value)
                };
                return ret;
            }
            catch { }
            return null;
        }
    }
}