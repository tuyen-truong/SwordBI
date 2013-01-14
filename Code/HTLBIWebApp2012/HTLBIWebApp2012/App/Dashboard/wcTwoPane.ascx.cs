using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CECOM;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.BLL;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Shared.UserControl;

namespace HTLBIWebApp2012.App.Dashboard
{
    public partial class wcTwoPane : System.Web.UI.UserControl
    {   
        public enum PaneType
        {
            First = 1,
            Second = 2
        }

        public enum ControlMode
        {
            New = 0,
            Edit = 1,
            View = 2
        }

        public PaneType WcType = PaneType.First;
        public ControlMode CtrlMode = ControlMode.View;
        public String WHCode { get; set; }
        private List<String> _usingPortlets = new List<string>();
        public List<String> UsingPortlets
        {
            get
            {
                _usingPortlets.Clear();
                if (this.ChildControlsCreated)
                {
                    if (m_picker1.SelectedItem != null)
                    {
                        _usingPortlets.Add(Lib.NTE(m_picker1.SelectedItem.Value));
                    }
                    else
                    {
                        _usingPortlets.Add(String.Empty);
                    }
                    if (m_picker2.SelectedItem != null)
                    {
                        _usingPortlets.Add(Lib.NTE(m_picker2.SelectedItem.Value));
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
                ViewState["WcTwoPane_UsingPortlets"] = _usingPortlets;
            }
        }

        protected Table TblLayout;
        protected wcPortletPicker m_picker1;
        protected wcPortletPicker m_picker2;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeComponent();
            /*
            if (!IsPostBack)
            {
                IQueryable<lsttbl_Widget> portlets = MyBI.Me.Get_Widget(this.WHCode).Where(wg => _usingPortlets.Contains(wg.Code));
                if (portlets.Count() > 0 && _usingPortlets.Count >= 2)
                {
                    m_picker1.Items.Clear();
                    m_picker2.Items.Clear();
                    lsttbl_Widget wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[0]) && p.Code == _usingPortlets[0]);
                    if (wg != null)
                    {
                        m_picker1.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    }
                    wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[1]) && p.Code == _usingPortlets[1]);
                    if (wg != null)
                    {
                        m_picker2.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    }
                }
            }
            */
            IQueryable<lsttbl_Widget> portlets = MyBI.Me.Get_Widget(this.WHCode).Where(wg => _usingPortlets.Contains(wg.Code));
            if (portlets.Count() > 0 && _usingPortlets.Count >= 2)
            {
                m_picker1.Items.Clear();
                m_picker2.Items.Clear();
                lsttbl_Widget wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[0]) && p.Code == _usingPortlets[0]);
                if (wg != null)
                {
                    m_picker1.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    m_picker1.SelectedIndex = 0;
                }
                wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[1]) && p.Code == _usingPortlets[1]);
                if (wg != null)
                {
                    m_picker2.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    m_picker2.SelectedIndex = 0;
                }
            }
        }

        private void InitializeComponent()
        {
            if (String.IsNullOrEmpty(WHCode)) { throw new Exception(String.Format("Invalid WHCode = {0}", WHCode)); }

            WcPlaceHolder.Controls.Clear();

            TableRow tblRow;
            TableCell tblCell;

            TblLayout = new Table();
            TblLayout.Style.Add(HtmlTextWriterStyle.Width, "100%");

            if (CtrlMode == ControlMode.View)
            {
                switch(WcType)
                {
                    case PaneType.First:
                    case PaneType.Second:
                    default:
                        throw new Exception("Invalid.");
                }
            }
            else
            {
                switch (WcType)
                {
                    case PaneType.First:
                        tblRow = new TableRow();
                        // Cell 1
                        tblCell = new TableCell();
                        tblCell.Width = Unit.Percentage(50);
                        m_picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker1.WHCode = WHCode;
                        m_picker1.Height = Unit.Pixel(225);
                        tblCell.Controls.Add(m_picker1);
                        tblRow.Cells.Add(tblCell);
                        // Cell 2
                        tblCell = new TableCell();
                        tblCell.Width = Unit.Percentage(50);
                        m_picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker2.WHCode = WHCode;
                        m_picker2.Height = Unit.Pixel(225);
                        tblCell.Controls.Add(m_picker2);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    case PaneType.Second:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Width = Unit.Percentage(50);
                        m_picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker1.WHCode = WHCode;
                        tblCell.Controls.Add(m_picker1);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Width = Unit.Percentage(50);
                        tblCell.Controls.Add(new LiteralControl("<br />"));
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Width = Unit.Percentage(50);
                        m_picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker2.WHCode = WHCode;
                        tblCell.Controls.Add(m_picker2);
                        tblRow.Cells.Add(tblCell);
                        tblCell = new TableCell();
                        tblCell.Width = Unit.Percentage(50);
                        tblCell.Controls.Add(new LiteralControl("<br />"));
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