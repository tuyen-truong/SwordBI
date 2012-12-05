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
    public class TenPortletCtrlBase : LayoutPortletCtrlBase
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
                this.Container_Pl5.Controls.Clear();
                this.Container_Pl6.Controls.Clear();
                this.Container_Pl7.Controls.Clear();
                this.Container_Pl8.Controls.Clear();
                this.Container_Pl9.Controls.Clear();
                this.Container_Pl10.Controls.Clear();
                this.Container_Pl1_Param.Controls.Clear();
                this.Container_Pl2_Param.Controls.Clear();
                this.Container_Pl3_Param.Controls.Clear();
                this.Container_Pl4_Param.Controls.Clear();
                this.Container_Pl5_Param.Controls.Clear();
                this.Container_Pl6_Param.Controls.Clear();
                this.Container_Pl7_Param.Controls.Clear();
                this.Container_Pl8_Param.Controls.Clear();
                this.Container_Pl9_Param.Controls.Clear();
                this.Container_Pl10_Param.Controls.Clear();

                DashboardDefine objDef = null;
                var obj = MyBI.Me.Get_DashboardBy(this.DashboardCode);
                if (obj == null) return ret; 
                objDef = obj.JsonObj;
                var plts = objDef.UsingPortlets;

                var ctrl1 = this.Load_CtrlWidget(plts[0]);
                var ctrl2 = this.Load_CtrlWidget(plts[1]);
                var ctrl3 = this.Load_CtrlWidget(plts[2]);
                var ctrl4 = this.Load_CtrlWidget(plts[3]);
                var ctrl5 = this.Load_CtrlWidget(plts[4]);
                var ctrl6 = this.Load_CtrlWidget(plts[5]);
                var ctrl7 = this.Load_CtrlWidget(plts[6]);
                var ctrl8 = this.Load_CtrlWidget(plts[7]);
                var ctrl9 = this.Load_CtrlWidget(plts[8]);
                var ctrl10 = this.Load_CtrlWidget(plts[9]);
                this.Container_Pl1.Controls.Add(ctrl1);
                this.Container_Pl2.Controls.Add(ctrl2);
                this.Container_Pl3.Controls.Add(ctrl3);
                this.Container_Pl4.Controls.Add(ctrl4);
                this.Container_Pl5.Controls.Add(ctrl5);
                this.Container_Pl6.Controls.Add(ctrl6);
                this.Container_Pl7.Controls.Add(ctrl7);
                this.Container_Pl8.Controls.Add(ctrl8);
                this.Container_Pl9.Controls.Add(ctrl9);
                this.Container_Pl10.Controls.Add(ctrl10);

                var ctrl1_Param = this.Load_CtrlParams(plts[0]);
                var ctrl2_Param = this.Load_CtrlParams(plts[1]);
                var ctrl3_Param = this.Load_CtrlParams(plts[2]);
                var ctrl4_Param = this.Load_CtrlParams(plts[3]);
                var ctrl5_Param = this.Load_CtrlParams(plts[4]);
                var ctrl6_Param = this.Load_CtrlParams(plts[5]);
                var ctrl7_Param = this.Load_CtrlParams(plts[6]);
                var ctrl8_Param = this.Load_CtrlParams(plts[7]);
                var ctrl9_Param = this.Load_CtrlParams(plts[8]);
                var ctrl10_Param = this.Load_CtrlParams(plts[9]);
                ctrl1_Param.WidgetClientInstanceName = ctrl1.MyClientInstanceName;
                ctrl2_Param.WidgetClientInstanceName = ctrl2.MyClientInstanceName;
                ctrl3_Param.WidgetClientInstanceName = ctrl3.MyClientInstanceName;
                ctrl4_Param.WidgetClientInstanceName = ctrl4.MyClientInstanceName;
                ctrl5_Param.WidgetClientInstanceName = ctrl5.MyClientInstanceName;
                ctrl6_Param.WidgetClientInstanceName = ctrl6.MyClientInstanceName;
                ctrl7_Param.WidgetClientInstanceName = ctrl7.MyClientInstanceName;
                ctrl8_Param.WidgetClientInstanceName = ctrl8.MyClientInstanceName;
                ctrl9_Param.WidgetClientInstanceName = ctrl9.MyClientInstanceName;
                ctrl10_Param.WidgetClientInstanceName = ctrl10.MyClientInstanceName;
                this.Container_Pl1_Param.Controls.Add(ctrl1_Param);
                this.Container_Pl2_Param.Controls.Add(ctrl2_Param);
                this.Container_Pl3_Param.Controls.Add(ctrl3_Param);
                this.Container_Pl4_Param.Controls.Add(ctrl4_Param);
                this.Container_Pl5_Param.Controls.Add(ctrl5_Param);
                this.Container_Pl6_Param.Controls.Add(ctrl6_Param);
                this.Container_Pl7_Param.Controls.Add(ctrl7_Param);
                this.Container_Pl8_Param.Controls.Add(ctrl8_Param);
                this.Container_Pl9_Param.Controls.Add(ctrl9_Param);
                this.Container_Pl10_Param.Controls.Add(ctrl10_Param);
                ret.Add(ctrl1);
                ret.Add(ctrl2);
                ret.Add(ctrl3);
                ret.Add(ctrl4);
                ret.Add(ctrl5);
                ret.Add(ctrl6);
                ret.Add(ctrl7);
                ret.Add(ctrl8);
                ret.Add(ctrl9);
                ret.Add(ctrl10);
                lstParams.Add(ctrl1_Param);
                lstParams.Add(ctrl2_Param);
                lstParams.Add(ctrl3_Param);
                lstParams.Add(ctrl4_Param);
                lstParams.Add(ctrl5_Param);
                lstParams.Add(ctrl6_Param);
                lstParams.Add(ctrl7_Param);
                lstParams.Add(ctrl8_Param);
                lstParams.Add(ctrl9_Param);
                lstParams.Add(ctrl10_Param);
                this.Title_Pl1.Text = ctrl1.Sett.DisplayName;
                this.Title_Pl2.Text = ctrl2.Sett.DisplayName;
                this.Title_Pl3.Text = ctrl3.Sett.DisplayName;
                this.Title_Pl4.Text = ctrl4.Sett.DisplayName;
                this.Title_Pl5.Text = ctrl5.Sett.DisplayName;
                this.Title_Pl6.Text = ctrl6.Sett.DisplayName;
                this.Title_Pl7.Text = ctrl7.Sett.DisplayName;
                this.Title_Pl8.Text = ctrl8.Sett.DisplayName;
                this.Title_Pl9.Text = ctrl9.Sett.DisplayName;
                this.Title_Pl10.Text = ctrl10.Sett.DisplayName;
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
                    this.Ctrl_Pl4_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl5_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl6_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl7_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl8_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl9_Param.Get_JsAll() + "\r\n" +
                    this.Ctrl_Pl10_Param.Get_JsAll();
                return jsAll;
            }
            catch { }
            return "";
        }
    }
}