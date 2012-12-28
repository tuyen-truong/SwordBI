using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HTLBIWebApp2012.Shared.UserControl;
using CECOM;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Codes.Models;
using HTLBIWebApp2012.Codes.BLL;

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
                _usingPortlets = new List<string>();
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
                _usingPortlets = value;
                ViewState["WcFourPane_UsingPortlets"] = _usingPortlets;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeComponent();
            if (!IsPostBack)
            {
                IQueryable<lsttbl_Widget> portlets = MyBI.Me.Get_Widget(this.WHCode).Where(wg => _usingPortlets.Contains(wg.Code));
                if (portlets.Count() > 0 && _usingPortlets.Count >= 4)
                {
                    picker1.Items.Clear();
                    picker2.Items.Clear();
                    picker3.Items.Clear();
                    picker4.Items.Clear();
                    lsttbl_Widget wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[0]) && p.Code == _usingPortlets[0]);
                    if (wg != null)
                    {
                        picker1.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    }
                    wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[1]) && p.Code == _usingPortlets[1]);
                    if (wg != null)
                    {
                        picker2.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    }
                    wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[2]) && p.Code == _usingPortlets[2]);
                    if (wg != null)
                    {
                        picker3.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    }
                    wg = portlets.Single(p => !string.IsNullOrEmpty(_usingPortlets[3]) && p.Code == _usingPortlets[3]);
                    if (wg != null)
                    {
                        picker4.Items.Add(new ListEditItem(wg.Name, wg.Code));
                    }
                }
            }
        }

        void InitializeComponent()
        {
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
                picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker3 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                picker4 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                
                picker1.WHCode = this.WHCode;
                picker2.WHCode = this.WHCode;
                picker3.WHCode = this.WHCode;
                picker4.WHCode = this.WHCode;

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