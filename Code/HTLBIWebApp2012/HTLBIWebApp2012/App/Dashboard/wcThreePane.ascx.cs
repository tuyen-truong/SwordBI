using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Shared.UserControl;

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

        protected wcPortletPicker picker1;
        protected wcPortletPicker picker2;
        protected wcPortletPicker picker3;
        protected Table TblLayout = new Table();

        protected void Page_Load(object sender, EventArgs e)
        {
            TableRow tblRow;
            TableCell tblCell;

            if (CtrlMode == ControlMode.View)
            {

            }
            else
            {
                picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker3 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;

                switch(WcType)
                {
                    case PaneType.First:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.RowSpan = 2;
                        tblCell.Controls.Add(picker1);
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
                        tblCell.Controls.Add(picker1);
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
                        tblCell = new TableCell();
                        tblCell.RowSpan = 2;
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
                        tblCell = new TableCell();
                        tblCell.Controls.Add(picker2);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.ColumnSpan = 2;
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