using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using CECOM;
using HTLBIWebApp2012.Codes.BLL;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class wcKPIContextMetric_Calc : KPIPartCtrlBase
    {
        #region Declares
        protected List<string> CtrlCalcFieldIDs
        {
            get
            {
                if (ViewState["CtrlCalcFieldIDs"] == null)
                    ViewState["CtrlCalcFieldIDs"] = new List<string>();
                return ViewState["CtrlCalcFieldIDs"] as List<string>;
            }
            set
            {
                ViewState["CtrlCalcFieldIDs"] = value;
                if (value == null) GC.Collect();
            }
        }
        public PortletSetting MyPage { get { return this.Page as PortletSetting; } }
        #endregion

        public override void Load_InitData()
        {
            if (Page.IsPostBack)
                this.Add_CalcFieldControl(null, true);
            this.cboAggregator.Items.Clear();
            this.cboAggregator.Items.AddRange(InqMDX.GetSummatyFuncName());
        }
        public override void Set_Info(KPIField info)
        {
            try
            {
                var myInfo = info as KPICtxtMetric;
                this.txtField.Value = myInfo.FieldName;
                this.txtDisplayName.Text = myInfo.DisplayName;
                this.cboAggregator.Value = myInfo.Aggregator;
                // Add filter...
                foreach (var calc in myInfo.CalcFields)
                {
                    var myCtrl = this.Add_CalcFieldControl(calc.Types, false);
                    myCtrl.Set_Info(calc);
                }
            }
            catch { }
        }
        public override void Reset_Info(params object[] param)
        {
            this.txtField.Text = string.Empty;
            this.txtDisplayName.Text = string.Empty;
        }
        public override KPIField Get_KPIPartInfo()
        {
            var calcFields = new CalcFieldCollection();
            var lstCalcFieldIDs = this.CtrlCalcFieldIDs;
            foreach (CalcFieldCtrlBase ctrl in this.ctrl_CalcFields.Controls)
            {
                if (ctrl == null) continue;
                var info = ctrl.Get_CalcFieldInfo();
                var objCalcFieldID = lstCalcFieldIDs.FirstOrDefault(p => p.StartsWith(ctrl.ID));
                var arr = objCalcFieldID.Split(',', StringSplitOptions.RemoveEmptyEntries);
                info.Types = arr.Last();
                calcFields.Add(info);
            }
            var ret = new KPICtxtMetric
            {
                FieldName = this.txtField.Text,
                DisplayName = this.txtDisplayName.Text,
                Aggregator = Lib.NTE(this.cboAggregator.Value),
                CalcFields = calcFields
            };
            return ret;
        }
        private CalcFieldCtrlBase Add_CalcFieldControl(string typeStr, bool isReCreate)
        {            
            CalcFieldCtrlBase ctrl = null;
            var ds = MyBI.Me.Get_DashboardSourceBy(this.MyPage.My_wcKPISetting.DSCode_Target);
            if (ds == null || ds.JsonObjMDX == null) return null;
            var dsField = ds.JsonObjMDX.Summaries.Select(p => new COMCodeNameObj(p.Field.ColName, p.Field.ColAliasVI)).ToList();

            // ReCreate...
            if (isReCreate)
            {
                foreach (var obj in this.CtrlCalcFieldIDs)
                {
                    var arr = obj.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var ctrlID = arr.First();
                    var ctrlType = arr.Last();
                    ctrl = this.LoadControl("wcCalcField.ascx") as CalcFieldCtrlBase;
                    ctrl.ID = ctrlID;
                    ctrl.OnRemove += this.RemoveFilter;
                    //ctrl.Set_Source(dsField);
                    ctrl.Set_Source(new { CalcID = this.ctrl_CalcFields.Controls.Count, DS = dsField });
                    ctrl.Set_Type(ctrlType);
                    this.ctrl_CalcFields.Controls.Add(ctrl);
                }
                return null;
            }
            // Add new...
            ctrl = this.LoadControl("wcCalcField.ascx") as CalcFieldCtrlBase;
            var guiID = Guid.NewGuid().ToString();
            ctrl.ID = string.Format("gen_{1}_{0}_{1}", guiID, typeStr);
            ctrl.OnRemove += this.RemoveFilter;
            var ctrlCount = this.ctrl_CalcFields.Controls.Count;
            if (ctrlCount > 0)
            {
                try
                {
                    var prevCalcFields = this.ctrl_CalcFields.Controls.OfType<CalcFieldCtrlBase>().Select(p => p.Get_CalcFieldInfo());
                    dsField.AddRange(prevCalcFields.Where(p => p != null).Select(p => new COMCodeNameObj(p.Name, p.Name)));
                }
                catch { }
            }
            ctrl.Set_Source(new { CalcID = ctrlCount, DS = dsField });
            ctrl.Set_Type(typeStr);
            this.ctrl_CalcFields.Controls.Add(ctrl);
            this.CtrlCalcFieldIDs.Add(string.Format("{0},{1}", ctrl.ID, typeStr));
            return ctrl;
        }
        protected void popMen_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
        {
            this.Add_CalcFieldControl(e.Item.Name, false);
        }
        private void RemoveFilter(object sender, EventArgs args)
        {
            try
            {
                var ctrlID = (sender.GetVal("Parent") as Control).ID;
                this.CtrlCalcFieldIDs.RemoveAll(p => p.Split(',', StringSplitOptions.RemoveEmptyEntries).First() == ctrlID);
                this.ctrl_CalcFields.Controls.RemoveAll(p => p.ID == ctrlID);
            }
            catch { }
        }
    }
}