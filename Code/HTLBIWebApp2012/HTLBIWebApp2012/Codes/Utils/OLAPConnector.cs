using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxRoundPanel;
using System.Web.UI;
using DevExpress.Web.ASPxPivotGrid;
using System.Text;
using DevExpress.XtraPivotGrid.Data;
using System.Configuration;

namespace HTLBIWebApp2012
{
    public class OLAPConnector
    {
        const string ProviderDownloadUrl = "http://www.microsoft.com/downloads/details.aspx?FamilyID=50b97994-8453-4998-8226-fa42ec403d17#ASOLEDB",
    NoProviderErrorString = "To connect to olap cubes, you should have Microsoft SQL Server 2005<br />" +
                            "Analysis Services 9.0 OLE DB Provider installed on your system. You can get<br />" +
                            "the latest version of this provider here:<br />" +
                            "<a href=\"" + ProviderDownloadUrl + "\" target=\"_blank\">" + ProviderDownloadUrl + "</a>.",
    ExceptionErrorString = "Unfortunately, an unexpected exception was raised when trying to connect to the OLAP datasource:";

        /// <summary>
        /// Thử kết nối đến database Olap
        /// </summary>
        /// <param name="pivot">Lưới hiển thị dữ liệu</param>
        /// <returns>Trả về chuỗi lỗi nếu không kết nối được, ngược lại trả về null</returns>
        public static string TryConnect(ASPxPivotGrid pivotGrid, string cnnsName, string cubeName)
        {
            if (ConfigurationManager.ConnectionStrings.Count > 0 && ConfigurationManager.ConnectionStrings[cnnsName] != null)
            {
                string cnns = string.Format(ConfigurationManager.ConnectionStrings[cnnsName].ConnectionString, cubeName);
                pivotGrid.OLAPConnectionString = cnns;
            }
            else
            {
                StringBuilder res = new StringBuilder();
                res.Append("<br/><pre>").Append(string.Format("Kết nối chưa được thiết lập cho cube '{0}', vui lòng kiểm tra chuỗi kết nối trong Web.Config", cubeName)).Append("</pre>");
                return res.ToString();
            }

            if (!OLAPMetaGetter.IsProviderAvailable)
            {
                pivotGrid.OLAPConnectionString = null;
                return NoProviderErrorString;
            }

            try
            {
                pivotGrid.DataBind();
            }
            catch (OLAPConnectionException exception)
            {
                pivotGrid.OLAPConnectionString = null;
                StringBuilder res = new StringBuilder(ExceptionErrorString);
                res.Append("<br/><pre>").Append(exception.ToString()).Append("</pre>");
                return res.ToString();
            }
            return null;
        }

        public static string TryConnect(ASPxPivotGrid pivotGrid, string cubeName)
        {
            return TryConnect(pivotGrid, "OLAPCNNS", cubeName);
        }
        public static Control CreateErrorPanel(string errorMessage)
        {
            ASPxRoundPanel panel = new ASPxRoundPanel();
            panel.ShowHeader = false;
            panel.Style["margin-bottom"] = "10px";
            Table table = new Table();
            table.Rows.Add(new TableRow());
            table.Rows[0].Cells.Add(new TableCell());
            table.Rows[0].Cells.Add(new TableCell());
            panel.Controls.Add(table);
            Image errorIcon = new Image();
            errorIcon.ImageUrl = "~/Images/Error.gif";
            errorIcon.AlternateText = "Error";
            table.Rows[0].Cells[0].Controls.Add(errorIcon);
            table.Rows[0].Cells[1].Text = errorMessage;
            return panel;
        }
    }
}