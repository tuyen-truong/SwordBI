using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Shared.UserControl;
using CECOM;

namespace HTLBIWebApp2012.App.Dashboard
{
    public partial class wcThreePane : System.Web.UI.UserControl
    {
        public enum PaneType
        {
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4
        }

        public enum ControlMode
        {
            New,
            Edit,
            View
        }

        public PaneType WcType = PaneType.First;
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
                return _usingPortlets;
            }
            set
            {
                _usingPortlets.AddRange(value);
                ViewState["WcThreePane_UsingPortlets"] = _usingPortlets;

            }
        }

        protected wcPortletPicker picker1;
        protected wcPortletPicker picker2;
        protected wcPortletPicker picker3;
        protected Table TblLayout = new Table();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(WHCode)) { throw new Exception(String.Format("Invalid WHCode = {0}", WHCode)); }

            TableRow tblRow;
            TableCell tblCell;

            TblLayout.Style.Add(HtmlTextWriterStyle.Width, "100%");

            if (CtrlMode == ControlMode.View)
            {

            }
            else
            {
                picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker3 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                
                picker1.WHCode = this.WHCode;
                picker2.WHCode = this.WHCode;
                picker3.WHCode = this.WHCode;

                switch(WcType)
                {
                    case PaneType.First:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.RowSpan = 2;
                        tblCell.VerticalAlign = VerticalAlign.Top;
                        tblCell.Controls.Add(picker1);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker2);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker3);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    case PaneType.Second:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.ColumnSpan = 2;
                        picker1.Width = Unit.Percentage(100);
                        tblCell.Controls.Add(picker1);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker2);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker3);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    case PaneType.Third:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker1);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.RowSpan = 2;
                        tblCell.VerticalAlign = VerticalAlign.Top;
                        tblCell.Controls.Add(picker2);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker3);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    case PaneType.Fourth:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker1);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker2);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.ColumnSpan = 2;
                        picker3.Width = Unit.Percentage(100);
                        tblCell.Controls.Add(picker3);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    default:
                        throw new Exception("Invalid.");
                }
            }
            WcPlaceHolder.Controls.Add(TblLayout);
        }
    }
}