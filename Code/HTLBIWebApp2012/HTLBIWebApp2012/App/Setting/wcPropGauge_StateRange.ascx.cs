using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcPropGauge_StateRange : PartPlugCtrlBase
    {
        protected void ctrl_ValueChanged(object sender, EventArgs e)
        {
            var curState = GlobalSsn.WidgetGauge_SsnModel.StateRanges
                .FirstOrDefault(p => p.Name == this.ID);
            if (curState == null) return;

            if (sender is TextBox)
            {
                var ctrl = sender as TextBox;
                if (ctrl == null) return;
                if (ctrl.ID == this.txtFromValue.ID)
                {
                    curState.RangeVal.Start = float.Parse(ctrl.Text);
                    this.txtToValue.Focus();
                }
                else if (ctrl.ID == this.txtToValue.ID)
                {
                    curState.RangeVal.End = float.Parse(ctrl.Text);
                    this.cboColor.Focus();
                }
            }
            else if (sender is ASPxColorEdit)
            {
                var ctrl = sender as ASPxColorEdit;
                if (ctrl == null) return;
                if (ctrl.ID == this.cboColor.ID)
                {
                    curState.Color = ctrl.Color;
                    this.txtDes.Focus();
                }
            }
        }
        public void Set_StateRange(StatusRange<float, float> info)
        {
            this.txtFromValue.Text = info.RangeVal.Start.ToString();
            this.txtToValue.Text = info.RangeVal.End.ToString();
            this.cboColor.Color = info.Color;
            this.txtDes.Text = info.Des;
        }
        public StatusRange<float, float> Get_StateRange()
        {
            try
            {
                var ret = new StatusRange<float, float>
                {
                    Name = this.ID,
                    RangeVal = new ValueRange<float, float>(float.Parse(this.txtFromValue.Text), float.Parse(this.txtToValue.Text)),
                    Color = this.cboColor.Color,
                    Des = this.txtDes.Text,
                    ForPerc = false
                };
                return ret;
            }
            catch {}
            return null;
        }
    }
}