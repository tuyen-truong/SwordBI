using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcInteractionFilter : PartPlugCtrlBase
    {
        public override void Load_InitData()
        {
            var prevValSel = this.cbbControl.Value;
            this.cbbControl.Items.Clear();
            var ds = InteractionFilter.Get_Control();
            Helpers.SetDataSource(this.cbbControl, ds, "Code", "Name", prevValSel);
        }
        public InteractionFilter Get_FilterInfo()
        {
            try
            {
                var ret = new InteractionFilter();
                ret.Name = this.ID;
                ret.Caption = this.txtCaption.Text;
                ret.Control = Lib.NTE(this.cbbControl.Value);
                ret.SourceField = Lib.NTE(this.cbbSourceField.Value);
                ret.EnableRange = this.chkEnableRange.Checked;
                return ret;
            }
            catch { return null; }
        }
        public void Set_FilterInfo(InteractionFilter info)
        {
            if (info == null) return;
            if (info.Control == "CheckedComboBox" || info.Control == "TreeListBox" || 
                info.Control == "Calendar_Prev")
                this.chkEnableRange.ClientVisible = false;
            else
                this.chkEnableRange.ClientVisible = true;

            if (info.Control == "Calendar_Year" || info.Control == "Calendar_Quarter" ||
                info.Control == "Calendar_Period" || info.Control == "Calendar_Day" || 
                info.Control == "Calendar_Prev")
                this.cbbSourceField.ClientVisible = false;
            else
                this.cbbSourceField.ClientVisible = true;
            this.ID = info.Name;            
            this.cbbControl.Value = info.Control;
            this.cbbSourceField.Value = info.SourceField;
            this.txtCaption.Text = info.Caption;
            this.chkEnableRange.Checked = info.EnableRange;
        }
        public void Set_Source(object src, string valueField, string textField)
        {
            Helpers.SetDataSource(this.cbbSourceField, src, valueField, textField);
        }
        protected void cbo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var cbo = sender as ASPxComboBox;
                if (cbo == null) return;
                var selVal = Lib.NTE(cbo.Value);
                if (cbo.ID == this.cbbControl.ID)
                {
                    if (selVal == "CheckedComboBox" || selVal == "TreeListBox" ||
                        selVal == "Calendar_Prev")
                        this.chkEnableRange.ClientVisible = false;
                    else
                        this.chkEnableRange.ClientVisible = true;
                    if (selVal == "Calendar_Year" || selVal == "Calendar_Quarter" ||
                        selVal == "Calendar_Period" || selVal == "Calendar_Day" ||
                        selVal == "Calendar_Prev")
                        this.cbbSourceField.ClientVisible = false;
                    else                        
                        this.cbbSourceField.ClientVisible = true;
                }
                if (string.IsNullOrEmpty(this.txtCaption.Text))
                    this.txtCaption.Text = this.cbbSourceField.Text;
            }
            catch { }
        }
    }
}