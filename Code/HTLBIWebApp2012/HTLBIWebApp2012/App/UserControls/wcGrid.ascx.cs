using System;
using DevExpress.Web.ASPxGridView;

namespace HTLBIWebApp2012.App.UserControls
{
    public partial class wcGrid : GridCtrlBase
    {
        #region Events
        protected void gvData_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
        }
        protected void gvData_PageIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvData_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colLine")
                {
                    e.Value = e.ListSourceRowIndex + 1;
                }
            }
            catch { }
        }
        #endregion
    }
}