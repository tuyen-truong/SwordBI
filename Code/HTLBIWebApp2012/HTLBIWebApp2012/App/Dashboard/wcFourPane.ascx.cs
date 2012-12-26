using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Shared.UserControl;
using CECOM;
using DevExpress.Web.ASPxEditors;

namespace HTLBIWebApp2012.App.Dashboard
{
    public partial class wcFourPane : PartPlugCtrlBase
    {
        protected wcPortletPicker picker1;
        protected wcPortletPicker picker2;
        protected wcPortletPicker picker3;
        protected wcPortletPicker picker4;
        protected Table TblLayout;

        public ControlMode CtrlMode = ControlMode.View;
        public String WHCode { get; set; }
        private List<String> _usingPortlets = new List<string>();
        public List<String> UsingPortlets
        {
            get
            {
                if (picker1.SelectedItem != null)
                {
                    _usingPortlets.Add(Lib.NTE(picker1.SelectedItem.Value));
                }
                else
                {
                    _usingPortlets.Add(String.Empty);
                }
                if (picker2.SelectedItem != null)
                {
                    _usingPortlets.Add(Lib.NTE(picker2.SelectedItem.Value));
                }
                else
                {
                    _usingPortlets.Add(String.Empty);
                }
                if (picker3.SelectedItem != null)
                {
                    _usingPortlets.Add(Lib.NTE(picker3.SelectedItem.Value));
                }
                else
                {
                    _usingPortlets.Add(String.Empty);
                }
                if (picker4.SelectedItem != null)
                {
                    _usingPortlets.Add(Lib.NTE(picker4.SelectedItem.Value));
                }
                else
                {
                    _usingPortlets.Add(String.Empty);
                }
                return _usingPortlets;
            }
            set
            {
                _usingPortlets.AddRange(value);
                ViewState["WcFourPane_UsingPortlets"] = _usingPortlets;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (String.IsNullOrEmpty(WHCode)) { throw new Exception(String.Format("Invalid WHCode = {0}", WHCode)); }

            TblLayout = new Table();
            TblLayout.Style.Add(HtmlTextWriterStyle.Width, "100%");

            TableRow tblRow;
            TableCell tblCell;

            if (CtrlMode == ControlMode.View)
            {

            }
            else
            {
                picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker1.WHCode = this.WHCode;
                picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker3 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker4 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                foreach(String portletID in this._usingPortlets)
                {
                    picker1.Items.Add(new ListEditItem(portletID, portletID));
                }

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                tblCell.Controls.Add(picker1);
                tblRow.Cells.Add(tblCell);
                tblCell = new TableCell();
                tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                tblCell.Controls.Add(picker2);
                tblRow.Cells.Add(tblCell);
                TblLayout.Rows.Add(tblRow);

                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                tblCell.Controls.Add(picker3);
                tblRow.Cells.Add(tblCell);
                tblCell = new TableCell();
                tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                tblCell.Controls.Add(picker4);
                tblRow.Cells.Add(tblCell);
                TblLayout.Rows.Add(tblRow);
            }
            WcPlaceHolder.Controls.Add(TblLayout);
        }
    }
}