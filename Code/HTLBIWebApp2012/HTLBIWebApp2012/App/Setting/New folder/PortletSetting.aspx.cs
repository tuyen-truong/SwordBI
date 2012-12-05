using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraCharts;
using CECOM;
using HTLBIWebApp2012.Codes.Utils;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.App.UserControls;
using DevExpress.Web.ASPxTabControl;
using System.Web.Services;

namespace HTLBIWebApp2012.App.Setting
{
    public partial class PortletSetting : PageBase
    {
        public wcDatasourceSetting My_wcDSSetting { get { return this.wcDatasourceSetting1; } }
        public wcKPISetting My_wcKPISetting { get { return this.wcKPISetting1; } }
        public wcLayoutSetting My_wcLayoutSetting { get { return this.wcLayoutSetting1; } }
        public wcInteractionSetting My_wcInteractionSetting { get { return this.wcInteractionSetting1; } }

        /// <summary>
        /// Đối tượng TabPage hiện hành nào đang được Active.
        /// </summary>
        public TabPage CurrentActiveTabPage { get { return this.tabCtrl_PortletSetting.ActiveTabPage; } }
        /// <summary>
        /// Mã DataWarehouse được tham khảo từ TabPage_DatasourceSetting
        /// </summary>
        public string WHCode { get { return this.wcDatasourceSetting1.WHCode; } }
        /// <summary>
        /// Mã Datasource được tham khảo từ TabPage_DatasourceSetting
        /// </summary>
        public string DSCode { get { return this.wcDatasourceSetting1.DSCode; } }
        /// <summary>
        /// Mã KPI được tham khảo từ TabPage_KPISetting
        /// </summary>
        public string KPICode { get { return this.wcKPISetting1.KPICode; } }
        /// <summary>
        /// Mã Layout được tham khảo từ TabPage_LayoutSetting
        /// </summary>
        public string LayoutCode { get { return this.My_wcLayoutSetting.LayoutCode; } }

        /// <summary>
        /// Forward sự kiện OnChange cho control 'My_wcKPISetting'
        /// </summary>
        //protected void wcControlInTab_Changed(object sender, EventArgs args)
        //{
        //    if (sender is string) return;

        //    var ctrl = sender.GetVal("Ctrl") as Control;
        //    var cat = sender.GetStr("Cat");
        //    if (ctrl.ID == this.My_wcDSSetting.ID)
        //    {
        //        this.My_wcKPISetting.Raise_OnChange(cat, args);
        //        this.My_wcLayoutSetting.Raise_OnChange(cat, args);
        //    }
        //    else if (ctrl.ID == this.My_wcKPISetting.ID)
        //    {
        //        this.My_wcLayoutSetting.Raise_OnChange(cat, args);
        //    }
        //}
    }
}