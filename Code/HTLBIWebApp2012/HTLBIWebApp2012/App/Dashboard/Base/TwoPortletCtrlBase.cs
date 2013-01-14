﻿using System.Collections.Generic;
using HTLBIWebApp2012.Codes.BLL;


namespace HTLBIWebApp2012.App.Dashboard.Base
{
    public class TwoPortletCtrlBase : LayoutPortletCtrlBase
    {
        protected override List<WidgetCtrlBase> Load_Portlets(out ICollection<ParamCtrlBase> lstParams)
        {
            var ret = new List<WidgetCtrlBase>();
            lstParams = new List<ParamCtrlBase>();
            try
            {
                this.Container_Pl1.Controls.Clear();
                this.Container_Pl2.Controls.Clear();
                this.Container_Pl1_Param.Controls.Clear();
                this.Container_Pl2_Param.Controls.Clear();

                DashboardDefine objDef = null;
                var obj = MyBI.Me.Get_DashboardBy(this.DashboardCode);
                if (obj == null) return ret; 
                objDef = obj.JsonObj;
                var plts = objDef.UsingPortlets;

                var ctrl1 = this.Load_CtrlWidget(plts[0]);
                var ctrl2 = this.Load_CtrlWidget(plts[1]);
                this.Container_Pl1.Controls.Add(ctrl1);
                this.Container_Pl2.Controls.Add(ctrl2);
                var ctrl1_Param = this.Load_CtrlParams(plts[0]);
                var ctrl2_Param = this.Load_CtrlParams(plts[1]);
                ctrl1_Param.WidgetClientInstanceName = ctrl1.MyClientInstanceName;
                ctrl2_Param.WidgetClientInstanceName = ctrl2.MyClientInstanceName;
                this.Container_Pl1_Param.Controls.Add(ctrl1_Param);
                this.Container_Pl2_Param.Controls.Add(ctrl2_Param);
                ret.Add(ctrl1);
                ret.Add(ctrl2);
                lstParams.Add(ctrl1_Param);
                lstParams.Add(ctrl2_Param);
                this.Title_Pl1.Text = ctrl1.Sett.DisplayName;
                this.Title_Pl2.Text = ctrl2.Sett.DisplayName;
            }
            catch { }
            return ret;
        }
        public override string Get_JsAllPortletParam()
        {
            try
            {
                var jsAll =
                    this.Ctrl_Pl1_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl2_Param.Get_JsAll();
                return jsAll;
            }
            catch { }
            return "";
        }
    }
}