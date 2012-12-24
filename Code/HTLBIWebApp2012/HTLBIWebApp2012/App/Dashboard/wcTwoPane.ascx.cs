﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using HTLBIWebApp2012.Shared.UserControl;
using CECOM;

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
                _usingPortlets.Add(Lib.NTE(m_picker1.SelectedItem.Value));
                _usingPortlets.Add(Lib.NTE(m_picker2.SelectedItem.Value));
                return _usingPortlets;

                //if (ViewState["WcTwoPane_UsingPortlets"] == null)
                //{
                //    return new List<String>();
                //}
                //return ViewState["WcTwoPane_UsingPortlets"] as List<String>;
            }
            set
            {
                ViewState["WcTwoPane_UsingPortlets"] = value;
            }
        }

        protected Table TblLayout;
        protected wcPortletPicker m_picker1;
        protected wcPortletPicker m_picker2;

        protected void Page_Load(object sender, EventArgs e)
        {
            TableRow tblRow;
            TableCell tblCell;

            TblLayout = new Table();

            if (CtrlMode == ControlMode.View)
            {

            }
            else
            {
                switch(WcType)
                {
                    case PaneType.First:
                        tblRow = new TableRow();
                        // Cell 1
                        tblCell = new TableCell();
                        m_picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker1.WHCode = WHCode;
                        tblCell.Controls.Add(m_picker1);
                        tblRow.Cells.Add(tblCell);
                        // Cell 2
                        tblCell = new TableCell();
                        m_picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker2.WHCode = WHCode;
                        tblCell.Controls.Add(m_picker2);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);
                        break;
                    case PaneType.Second:
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        m_picker1 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker1.WHCode = WHCode;
                        tblCell.Controls.Add(m_picker1);
                        tblRow.Cells.Add(tblCell);
                        TblLayout.Rows.Add(tblRow);

                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        m_picker2 = LoadControl("~/Shared/UserControl/wcPortletPicker.ascx") as wcPortletPicker;
                        m_picker2.WHCode = WHCode;
                        tblCell.Controls.Add(m_picker2);
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