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
    public class OnePortletCtrlBase : LayoutPortletCtrlBase
    {
        protected override List<WidgetCtrlBase> Load_Portlets(out ICollection<ParamCtrlBase> lstParams)
        {
            var ret = new List<WidgetCtrlBase>();
            lstParams = new List<ParamCtrlBase>();
            try
            {
                this.Container_Pl1.Controls.Clear();
                this.Container_Pl1_Param.Controls.Clear();

                DashboardDefine objDef = null;
                var obj = MyBI.Me.Get_DashboardBy(this.DashboardCode);
                if (obj == null) return ret; 
                objDef = obj.JsonObj;
                var plts = objDef.UsingPortlets;

                var ctrl1 = this.Load_CtrlWidget(plts[0]);
                this.Container_Pl1.Controls.Add(ctrl1);
                var ctrl1_Param = this.Load_CtrlParams(plts[0]);
                ctrl1_Param.WidgetClientInstanceName = ctrl1.MyClientInstanceName;
                this.Container_Pl1_Param.Controls.Add(ctrl1_Param);
                ret.Add(ctrl1);
                lstParams.Add(ctrl1_Param);
                this.Title_Pl1.Text = ctrl1.Sett.DisplayName;
            }
            catch { }
            return ret;
        }
        public override string Get_JsAllPortletParam()
        {
            try
            {               
                var jsAll = this.Ctrl_Pl1_Param.Get_JsAll();
                return jsAll;
            }
            catch { }
            return "";
        }
    }
}