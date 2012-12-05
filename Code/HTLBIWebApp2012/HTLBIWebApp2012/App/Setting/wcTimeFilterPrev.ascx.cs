using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcTimeFilterPrev : FilterCtrlBase
    {
        public override void Load_InitData()
        {
            this.cbbKeyField.Items.Clear();
            Helpers.SetDataSource(this.cbbKeyField, MyBI.Me.GetTimePrev(), "Value", "Text", "Year");
            this.cbbAndOr.Items.Clear();
            this.cbbAndOr.Items.AddRange(InqMDX.GetLogicCombine());
            this.cbbAndOr.SelectedIndex = 0;
        }
        public override object Get_FilterInfo_General()
        {
            try
            {
                var ret = new COMCodeNameObj(Lib.NTE(this.cbbKeyField.Value), Lib.NTE(this.cbbAndOr.Value));
                return ret;
            }
            catch { return null; }
        }
        public override void Set_Info_General(object info)
        {
            try
            {
                var myInfo = info as COMCodeNameObj;
                if (myInfo == null) return;
                this.cbbKeyField.Value = myInfo.Code;
                this.cbbAndOr.Value = myInfo.Name;
            }
            catch { }
        }
    }
}