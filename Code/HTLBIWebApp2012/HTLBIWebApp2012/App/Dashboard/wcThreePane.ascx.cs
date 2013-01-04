using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Shared.UserControl;
using CECOM;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;
using DevExpress.Web.ASPxEditors;

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
                _usingPortlets = new List<string>();
                if (this.ChildControlsCreated)
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
                }
                return _usingPortlets;
            }
            set
            {
                _usingPortlets = value;
                ViewState["WcThreePane_UsingPortlets"] = _usingPortlets;

            }
        }

        protected wcPortletPicker picker1;
        protected wcPortletPicker picker2;
        protected wcPortletPicker picker3;
        protected Table TblLayout = new Table();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeComponent();

            //if (!IsPostBack)
            //{
            //    IQueryable<lsttbl_Widget> portlets = MyBI.Me.Get_Widget(this.WHCode).Where(wg => _usingPortlets.Contains(wg.Code));
            //    if (portlets.Count() > 0 && _usingPortlets.Count >= 3)
            //    {
            //        picker1.Items.Clear();
            //        picker2.Items.Clear();
            //        picker3.Items.Clear();
            //        lsttbl_Widget wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[0]) && p.Code == _usingPortlets[0]);
            //        if (wg != null)
            //        {
            //            picker1.Items.Add(new ListEditItem(wg.Name, wg.Code));
            //        }
            //        wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[1]) && p.Code == _usingPortlets[1]);
            //        if (wg != null)
            //        {
            //            picker2.Items.Add(new ListEditItem(wg.Name, wg.Code));
            //        }
            //        wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[2]) && p.Code == _usingPortlets[2]);
            //        if (wg != null)
            //        {
            //            picker3.Items.Add(new ListEditItem(wg.Name, wg.Code));
            //        }
            //    }
            //}
            IQueryable<lsttbl_Widget> portlets = MyBI.Me.Get_Widget(this.WHCode).Where(wg => _usingPortlets.Contains(wg.Code));
            if (portlets.Count() > 0)
            {
                picker1.Items.Clear();
                picker2.Items.Clear();
                picker3.Items.Clear();
                lsttbl_Widget wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[0]) && p.Code == _usingPortlets[0]);
                if (wg != null)
                {
                    picker1.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    picker1.SelectedIndex = 0;
                }
                if (_usingPortlets.Count > 1
                    && !string.IsNullOrEmpty(_usingPortlets[1]))
                {
                    wg = portlets.Single(p => p.Code == _usingPortlets[1]);
                    if (wg != null)
                    {
                        picker2.Items.Add(new ListEditItem(wg.Name, wg.Code));
                        picker2.SelectedIndex = 0;
                    }
                }
                if (_usingPortlets.Count > 2
                    && !string.IsNullOrEmpty(_usingPortlets[2]))
                {
                    wg = portlets.Single(p => p.Code == _usingPortlets[2]);
                    if (wg != null)
                    {
                        picker3.Items.Add(new ListEditItem(wg.Name, wg.Code));
                        picker3.SelectedIndex = 0;
                    }
                }
            }
        }

        void InitializeComponent()
        {
            if (String.IsNullOrEmpty(WHCode)) { throw new Exception(String.Format("Invalid WHCode = {0}", WHCode)); }

            TableRow tblRow;
            TableCell tblCell;

            TblLayout.Style.Add(HtmlTextWriterStyle.Width, "100%");

            if (CtrlMode == ControlMode.View)
            {
                throw new Exception("Under constructor.");
            }
            else
            {
                picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker3 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;

                picker1.WHCode = this.WHCode;
                picker2.WHCode = this.WHCode;
                picker3.WHCode = this.WHCode;

                switch (WcType)
                {
                    case PaneType.First:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                        tblCell.RowSpan = 2;
                        tblCell.VerticalAlign = VerticalAlign.Top;
                        picker1.Height = Unit.Pixel(225);
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
                        tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                        tblCell.Controls.Add(picker2);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                        tblCell.Controls.Add(picker3);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    case PaneType.Third:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                        tblCell.Controls.Add(picker1);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(50).ToString());
                        tblCell.RowSpan = 2;
                        tblCell.VerticalAlign = VerticalAlign.Top;
                        picker2.Height = Unit.Pixel(225);
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
            this.ChildControlsCreated = true;
        }
    }
}