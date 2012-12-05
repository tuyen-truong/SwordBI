using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace HTLBIWebApp2012
{
    public class GlobalSsn
    {
        public static InqDefineSource Get_InqDefineSource()
        {
            return Get_InqDefineSource(10);
        }
        public static InqDefineSource Get_InqDefineSource(int top)
        {
            var iqr = new InqDefineSource("abc");
            iqr.AddTblName("DimItem", "DimItemGroup", "DimSalePerson", "FACTSAR");
            // Select
            iqr.AddField("FACTSAR", "CardCode");
            iqr.AddField("FACTSAR", "ItemCode");
            iqr.AddField("FACTSAR", "SlpCode");
            iqr.AddField("DimItem", "ItemName");
            iqr.AddField("DimItem", "ItmsGrpCod");
            iqr.AddField("DimItemGroup", "ItmsGrpNam");
            iqr.AddField("DimSalePerson", "SlpName");
            // Summary
            iqr.AddSummary("FACTSAR", "Quantity", "SUM");
            iqr.AddSummary("FACTSAR", "VatSum", "SUM");
            // Filter
            iqr.AddFilter(new InqFieldInfo("FACTSAR", "ItemCode", "NTEXT"), ">=", "2X3RT4-BLACK");
            iqr.AddFilter(new InqFieldInfo("FACTSAR", "ItemCode", "NTEXT"), "<=", "DD2-1G-800-NB");
            iqr.AddFilter(iqr.Summaries[0], ">=", "0");
            //// Order By
            //iqr.AddOrder("FACTSAR", "VatSum_Sum", "DESC");
            //iqr.AddOrder("FACTSAR", "Quantity_Sum", "DESC");            
            return iqr;
        }
        public static InqDefineSource Get_InqDefineSource1(int top)
        {
            var iqr = new InqDefineSource("bcd");
            // Select
            iqr.AddField("FACTSAR", "CardCode");
            iqr.AddField("FACTSAR", "ItemCode");
            iqr.AddField("FACTSAR", "SlpCode");
            iqr.AddField("DimItem", "ItemName");
            iqr.AddField("DimItem", "ItmsGrpCod");
            iqr.AddField("DimItemGroup", "ItmsGrpNam");
            iqr.AddField("DimSalePerson", "SlpName");
            // Summary
            //iqr.AddSummary("FACTSAR", "Quantity", "SUM");
            //iqr.AddSummary("FACTSAR", "VatSum", "SUM");
            // Filter
            iqr.AddFilter(new InqFieldInfo("FACTSAR", "ItemCode", "NTEXT"), ">=", "2X3RT4-BLACK");
            iqr.AddFilter(new InqFieldInfo("FACTSAR", "ItemCode", "NTEXT"), "<=", "DD2-1G-800-NB");
            //iqr.AddFilter(iqr.Summaries[0], ">=", "0");

            foreach (var item in iqr.Fields)
            {
                item.OrderName = "ASC";
            }
            foreach (var item in iqr.Summaries)
            {
                item.Field.OrderName = "DESC";
            }
            return iqr;
        }
        public static InqDefineSourceMDX Get_InqDefineSourceMDX()
        {
            var iqr = new InqDefineSourceMDX("bcd");
            iqr.OlapCubeName = "[ARCube]";
            iqr.PreffixDimTable = "AR";
            iqr.Top = new InqTopMDX(3, "Quantity");
            // Select
            iqr.AddField("DimItem", "ItemCode",1);
            iqr.AddField("DimItem", "ItemName",1);
            iqr.AddField("DimItem", "ItemGroupName", 0);
            // Summary
            iqr.AddSummary("FACTAR", "Quantity", "SUM");
            iqr.AddSummary("FACTAR", "Quantity", "SUM");
            iqr.AddSummary(new InqSummaryInfoMDX(new InqFieldInfoMDX("FACTAR", "Quantity"), "SUM")
            {
                InnerFilters = new List<InqFilterInfoMDX>()
                {
                    new InqFilterInfoMDX(new InqFieldInfoMDX("DimItem", "ItemGroupName", "NTEXT"),"=","KHAC"),
                    new InqFilterInfoMDX(new InqFieldInfoMDX("DimTime", "Period", "NTEXT"),"=","201108"),
                }
            });            
            //iqr.AddSummary("FACTAR", "VatSum", "SUM");
            // Filter
            iqr.AddFilter(new InqFieldInfoMDX("DimItem", "ItemGroupName", "NTEXT"), ">=", "KHAC");
            iqr.AddFilter(new InqFieldInfoMDX("DimItem", "ItemGroupName", "NTEXT"), "<=", "TUI");
            //iqr.AddFilter(new InqFieldInfoMDX("DimItem", "ItemGroupName", "NTEXT"), "=", "Chiết khấu");
            iqr.AddFilter(new InqFieldInfoMDX("DimTime", "Period", "NTEXT"), ">=", "201108");
            iqr.AddFilter(new InqFieldInfoMDX("DimTime", "Period", "NTEXT"), "<=", "201110");
            iqr.AddFilter(iqr.Summaries[0], ">", "-10");
            iqr.AddFilter(iqr.Summaries[1], ">", "-10");

            iqr.Fields[0].OrderName = "ASC";
            iqr.Summaries[0].Field.OrderName = "DESC";
            return iqr;
        }

        public static CalcFieldCollection Get_CalcField()
        {
            var ret = new CalcFieldCollection(new[]
            {
                new CalcField { Name = "calcF_A", Member1 = "Quantity", Operator = "+", Member2 = "100", Order = 0 },
                new CalcField { Name = "calcF_B", Member1 = "calcF_A", Operator = "*", Member2 = "Price", Order = 1 },
                new CalcField { Name = "calcF_C", Member1 = "VAT", Operator = "+", Member2 = "20", Order = 2 },
                new CalcField { Name = "calcF_D", Member1 = "calcF_B", Operator = "+", Member2 = "calcF_C", Order = 3 },
                new CalcField { Name = "calcF_E", Member1 = "calcF_D", Operator = "+", Member2 = "calcF_A", Order = 4 },
                new CalcField { Name = "calcF_F", Member1 = "calcF_D", Operator = "%", Member2 = "calcF_E", Order = 5 }
            });
            return ret;
        }


        public static KPIDefineSource KPIDefSrc_SsnModel
        {
            get
            {
                if (MySession.Session["KPIDefSrc_SsnModel"] == null)
                    MySession.Session["KPIDefSrc_SsnModel"] = new KPIDefineSource();
                return MySession.Session["KPIDefSrc_SsnModel"] as KPIDefineSource;
            }
            set
            {
                MySession.Session["KPIDefSrc_SsnModel"] = value;
            }
        }

        public static WidgetChart Get_WidgetChart()
        {
            // Chuẩn bị dữ liệu (sau này sẽ lấy từ database)
            var ds = GlobalSsn.Get_InqDefineSource();
            var objX = ds.Fields[0];
            var objX1 = ds.Fields[1];
            var objY = ds.Summaries[1];

            // Khởi tạo đối tượng 'WidgetChart' với bộ dữ liệu.
            var ret = new WidgetChart();
            //ret.RotatedXY = true;
            ret.SelectCommand = ds.ToSql();
            ret.XTitle = objX.ColAliasVI;
            ret.YTitle = objY.FieldAlias;
            ret.AddXField(objX.ColName);
            ret.AddXField(objX1.ColName);
            ret.AddYField(objY.FieldAlias);
            return ret;
        }
        public static WidgetGauge WidgetGauge_SsnModel
        {
            get
            {
                if (MySession.Session["WidgetGauge_SsnModel"] == null)
                    MySession.Session["WidgetGauge_SsnModel"] = new WidgetGauge() 
                    {
                        MaxValue = 5000
                    };
                return MySession.Session["WidgetGauge_SsnModel"] as WidgetGauge;
            }
            set
            {
                MySession.Session["WidgetGauge_SsnModel"] = value;
            }
        }
        public static WidgetGrid WidgetGrid_SsnModel
        {
            get
            {
                if (MySession.Session["WidgetGrid_SsnModel"] == null)
                    MySession.Session["WidgetGrid_SsnModel"] = new WidgetGrid();
                return MySession.Session["WidgetGrid_SsnModel"] as WidgetGrid;
            }
            set
            {
                MySession.Session["WidgetGrid_SsnModel"] = value;
            }
        }
    }
}