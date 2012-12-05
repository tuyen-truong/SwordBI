using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.App.UserControls;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;


namespace HTLBIWebApp2012.App.Dashboard.Base
{
    public class FourPortletCtrlBase : LayoutPortletCtrlBase
    {
        protected override List<WidgetCtrlBase> Load_Portlets(out ICollection<ParamCtrlBase> lstParams)
        {
            var ret = new List<WidgetCtrlBase>();
            lstParams = new List<ParamCtrlBase>();
            try
            {
                this.Container_Pl1.Controls.Clear();
                this.Container_Pl2.Controls.Clear();
                this.Container_Pl3.Controls.Clear();
                this.Container_Pl4.Controls.Clear();
                this.Container_Pl1_Param.Controls.Clear();
                this.Container_Pl2_Param.Controls.Clear();
                this.Container_Pl3_Param.Controls.Clear();
                this.Container_Pl4_Param.Controls.Clear();

                DashboardDefine objDef = null;
                var obj = MyBI.Me.Get_DashboardBy(this.DashboardCode);
                if (obj == null) return ret; 
                objDef = obj.JsonObj;
                var plts = objDef.UsingPortlets;

                var ctrl1 = this.Load_CtrlWidget(plts[0]);
                var ctrl2 = this.Load_CtrlWidget(plts[1]);
                var ctrl3 = this.Load_CtrlWidget(plts[2]);
                var ctrl4 = this.Load_CtrlWidget(plts[3]);
                this.Container_Pl1.Controls.Add(ctrl1);
                this.Container_Pl2.Controls.Add(ctrl2);
                this.Container_Pl3.Controls.Add(ctrl3);
                this.Container_Pl4.Controls.Add(ctrl4);

                var ctrl1_Param = this.Load_CtrlParams(plts[0]);
                var ctrl2_Param = this.Load_CtrlParams(plts[1]);
                var ctrl3_Param = this.Load_CtrlParams(plts[2]);
                var ctrl4_Param = this.Load_CtrlParams(plts[3]);
                ctrl1_Param.WidgetClientInstanceName = ctrl1.MyClientInstanceName;
                ctrl2_Param.WidgetClientInstanceName = ctrl2.MyClientInstanceName;
                ctrl3_Param.WidgetClientInstanceName = ctrl3.MyClientInstanceName;
                ctrl4_Param.WidgetClientInstanceName = ctrl4.MyClientInstanceName;
                this.Container_Pl1_Param.Controls.Add(ctrl1_Param);
                this.Container_Pl2_Param.Controls.Add(ctrl2_Param);
                this.Container_Pl3_Param.Controls.Add(ctrl3_Param);
                this.Container_Pl4_Param.Controls.Add(ctrl4_Param);
                ret.Add(ctrl1);
                ret.Add(ctrl2);
                ret.Add(ctrl3);
                ret.Add(ctrl4);
                lstParams.Add(ctrl1_Param);
                lstParams.Add(ctrl2_Param);
                lstParams.Add(ctrl3_Param);
                lstParams.Add(ctrl4_Param);
                this.Title_Pl1.Text = ctrl1.Sett.DisplayName;
                this.Title_Pl2.Text = ctrl2.Sett.DisplayName;
                this.Title_Pl3.Text = ctrl3.Sett.DisplayName;
                this.Title_Pl4.Text = ctrl4.Sett.DisplayName;
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
                    this.Ctrl_Pl2_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl3_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl4_Param.Get_JsAll();
                return jsAll;
            }
            catch { }
            return "";
        }
    }
}