using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTLBIWebApp2012.Codes.Models;
using CECOM;

namespace HTLBIWebApp2012.Codes.BLL
{
    public class MyBI
    {
        private static MyBI me;
        public static MyBI Me
        {
            get
            {
                if (me == null) me = new MyBI();
                return me;
            }
        }

        /// <summary>
        /// Trả về danh sách các Mã và tên Warehouse (BizCat)
        /// <para>Cấu trúc một thông tin: New{Text, Value}</para>
        /// </summary>
        public object GetDW()
        {
            var ret = new[]
            { 
                new { Text = "SALES", Value = "AR" },
                new { Text = "AC", Value = "AC" }, 
                new { Text = "IC", Value = "IC" }
            };
            return ret;
        }
        public object GetTimePrev()
        {
            var ret = new[] 
            { 
                new { Value = "Year", Text = "Previous year" },
                new { Value = "Quarter", Text = "Previous Quarter" },
                new { Value = "Period", Text = "Previous Period" },
                new { Value = "DateKey", Text = "Previous Day" }
            };
            return ret;
        }
        public IQueryable<lsttbl_DWTable> Get_DWTable()
        {
            return GlobalVar.DbBI.lsttbl_DWTables;
        }
        public IQueryable<lsttbl_DWTableRelationship> Get_DWTableRelationship()
        {
            return GlobalVar.DbBI.lsttbl_DWTableRelationships.OrderBy(p => p.Parent);
        }
        public IQueryable<lsttbl_DWTableRelationship> Get_DWTableRelationship(string whCode)
        {
            return this.Get_DWTableRelationship().Where(p => p.WHCode == whCode);
        }
        public lsttbl_DWTableRelationship Get_DWTableRelationship_ByParent(string whCode,string tblNameParent)
        {
            return this.Get_DWTableRelationship(whCode).FirstOrDefault(p => p.Parent == tblNameParent);
        }
        public lsttbl_DWTableRelationship Get_DWTableRelationship_ByChildren(string whCode, string tblNameChildren)
        {
            return this.Get_DWTableRelationship(whCode).FirstOrDefault(p => p.Children == tblNameChildren);
        }
        public List<string> Get_DWTableName(string tblCat, string whCode)
        {
            try
            {                
                var lsTblObj = new List<lsttbl_DWTable>();

                if (string.IsNullOrEmpty(tblCat))
                    lsTblObj = this.Get_DWTable().ToList();
                else
                    lsTblObj = this.Get_DWTable().Where(p => p.TblCat == tblCat).ToList();
                
                if (string.IsNullOrEmpty(whCode))
                    return lsTblObj.Select(p => p.TblName).ToList();

                var lsTblName = new List<string>();
                foreach (var tbl in lsTblObj)
                {
                    if (lsTblName.Contains(tbl.TblName)) continue;
                    if (string.IsNullOrEmpty(tbl.BizCat)) continue;
                    var arr = tbl.BizCat.Split(',');
                    foreach (var cat in arr)
                    {
                        if (cat == whCode)
                        {
                            lsTblName.Add(tbl.TblName);
                            break;
                        }
                    }
                }
                return lsTblName;
            }
            catch { return new List<string>(); }
        }
        public IQueryable<lsttbl_DWTable> Get_DWTable(string whCode)
        {
            var tblNames = this.Get_DWTableName(null, whCode);
            return this.Get_DWTable().Where(p => tblNames.Contains(p.TblName));
        }
        public IQueryable<lsttbl_DWColumn> Get_DWColumn()
        {
            return GlobalVar.DbBI.lsttbl_DWColumns.OrderBy(p => p.ColName);
        }
        public IQueryable<lsttbl_DWColumn> Get_DWColumn(string whCode)
        {
            try
            {
                // Lấy ra những tên bảng dữ liệu thuộc 'bizCat' bảng chính là bảng FACT
                if (string.IsNullOrEmpty(whCode))
                    return new List<lsttbl_DWColumn>().AsQueryable();
                var lsTblContain = this.Get_DWTableName(null, whCode);
                // Lấy ra danh sách các cột dữ liệu thuộc về các bảng dữ liệu trên (loại bỏ các tên cột trừng nhau giữa các bảng)
                var ret = this.Get_DWColumn().Where(p => lsTblContain.Contains(p.TblName_Virtual));
                return ret;
            }
            catch { return new List<lsttbl_DWColumn>().AsQueryable(); }
        }
        public IQueryable<lsttbl_DWColumn> Get_DWColumn_ByTblName(string tblName)
        {
            return this.Get_DWColumn().Where(p => p.TblName_Actual == tblName);
        }
        public lsttbl_DWColumn Get_DWColumn_ByColName(string colName)
        {
            return this.Get_DWColumn().FirstOrDefault(p => p.ColName == colName);
        }

        // Lấy danh sách các cột ở cấp con của cột truyền vào (dựa vào mối quan hệ giữa các bảng 'TblName_Actual').
        public IQueryable<lsttbl_DWColumn> Get_DWColumn_Childrens(string whCode, string colNameParent)
        {
            var colParent = this.Get_DWColumn_ByColName(colNameParent);
            var objRel = this.Get_DWTableRelationship_ByParent(whCode, colParent.TblName_Actual);
            if (objRel == null || objRel.Children.StartsWith("Fact")) 
                return new List<lsttbl_DWColumn>().AsQueryable();
            return this.Get_DWColumn_ByTblName(objRel.Children);
        }

        // DashboardSource (Lưu thông tin datasource & KPI)...
        public IQueryable<lsttbl_DashboardSource> Get_DashboardSource()
        {
            return GlobalVar.DbBI.lsttbl_DashboardSources;
        }
        public IQueryable<lsttbl_DashboardSource> Get_DashboardSource(string whCode)
        {
            var ret = this.Get_DashboardSource().Where(p => p.WHCode == whCode);
            return ret;
        }
        public IQueryable<lsttbl_DashboardSource> Get_DashboardSource(string whCode, string settingCat)
        {
            var ret = this.Get_DashboardSource(whCode).Where(p => p.SettingCat == settingCat);
            return ret;
        }
        public IQueryable<lsttbl_DashboardSource> Get_DashboardKPI_ByDS(string dsCode)
        {
            var ret = this.Get_DashboardSource()
                .Where(p => p.ParentCode != null && p.ParentCode == dsCode);
            return ret;
        }
        public IQueryable<lsttbl_DashboardSource> Get_DashboardKPI_ByWH(string whCode)
        {
            var ret = this.Get_DashboardSource(whCode, GlobalVar.SettingCat_KPI);
            return ret;
        }
        public lsttbl_DashboardSource Get_DashboardSourceBy(string code)
        {
            return this.Get_DashboardSource().FirstOrDefault(p => p.Code == code);
        }
        public void Save_DashboardSource(lsttbl_DashboardSource info)
        {
            var db = GlobalVar.DbBI;
            try
            {
                var objFound = db.lsttbl_DashboardSources.FirstOrDefault(p => p.Code == info.Code);
                if (objFound == null)
                    db.lsttbl_DashboardSources.InsertOnSubmit(info);
                else
                    objFound.UpdateOnSubmit(info);

                db.SubmitChanges();
            }
            catch { }
        }

        // Widget (Lưu thông tin layout)...
        public IQueryable<lsttbl_Widget> Get_Widget()
        {
            return GlobalVar.DbBI.lsttbl_Widgets;
        }
        public IQueryable<lsttbl_Widget> Get_Widget(string whCode)
        {
            var ret = this.Get_Widget().Where(p => p.WHCode == whCode);
            return ret;
        }
        public IQueryable<lsttbl_Widget> Get_Widget_ByDS(string dsCode)
        {
            var ret = this.Get_Widget().Where(p => p.DSCode == dsCode);
            return ret;
        }
        public IQueryable<lsttbl_Widget> Get_Widget_ByWidgetType(string whCode, string widgetType)
        {
            var ret = this.Get_Widget(whCode).Where(p => p.WidgetType == widgetType);
            return ret;
        }
        public IQueryable<lsttbl_Widget> Get_Widget_ByWidgetType(string widgetType)
        {
            var ret = this.Get_Widget().Where(p => p.WidgetType == widgetType);
            return ret;
        }
        public lsttbl_Widget Get_Widget_ByCode(string code)
        {
            var ret = this.Get_Widget().FirstOrDefault(p => p.Code == code);
            return ret;
        }
        public void Save_Widget(lsttbl_Widget info)
        {
            var db = GlobalVar.DbBI;
            try
            {
                var objFound = db.lsttbl_Widgets.FirstOrDefault(p => p.Code == info.Code);
                if (objFound == null)
                    db.lsttbl_Widgets.InsertOnSubmit(info);
                else
                    objFound.UpdateOnSubmit(info);

                db.SubmitChanges();
            }
            catch { }
        }

        // WidgetInteraction (Lưu thông tin Interaction của layout)...
        public IQueryable<lsttbl_WidgetInteraction> Get_WidgetInteraction()
        {
            return GlobalVar.DbBI.lsttbl_WidgetInteractions;
        }
        public IQueryable<lsttbl_WidgetInteraction> Get_WidgetInteraction(string whCode)
        {
            var arrWgCode = this.Get_Widget(whCode).Select(p => p.Code).ToArray();
            var ret = this.Get_WidgetInteraction().Where(p => arrWgCode.Contains(p.WidgetCode));
            return ret;
        }
        public lsttbl_WidgetInteraction Get_WidgetInteraction_ByCode(string code)
        {
            var ret = this.Get_WidgetInteraction().FirstOrDefault(p => p.WidgetCode == code);
            return ret;
        }
        public void Save_WidgetInteraction(lsttbl_WidgetInteraction info)
        {
            var db = GlobalVar.DbBI;
            try
            {
                var objFound = db.lsttbl_WidgetInteractions.FirstOrDefault(p => p.WidgetCode == info.WidgetCode);
                if (objFound == null)
                    db.lsttbl_WidgetInteractions.InsertOnSubmit(info);
                else
                    objFound.UpdateOnSubmit(info);

                db.SubmitChanges();
            }
            catch { }
        }

        // Dashboard (Lưu thông tin dashboard do người dùng thiết lập)...
        public IQueryable<lsttbl_Dashboard> Get_Dashboard()
        {
            return GlobalVar.DbBI.lsttbl_Dashboards;
        }
        public IQueryable<lsttbl_Dashboard> Get_Dashboard(string whCode)
        {
            var ret = this.Get_Dashboard().Where(p => p.WHCode == whCode);
            return ret;
        }
        public lsttbl_Dashboard Get_DashboardByDefault(string whCode)
        {
            var ret = this.Get_Dashboard(whCode).FirstOrDefault(p => p.IsDefault);
            if (ret == null) ret = this.Get_Dashboard(whCode).FirstOrDefault();
            return ret;
        }
        public lsttbl_Dashboard Get_DashboardBy(string code)
        {
            return this.Get_Dashboard().FirstOrDefault(p => p.Code == code);
        }
        public void Save_Dashboard(lsttbl_Dashboard info)
        {
            var db = GlobalVar.DbBI;
            try
            {
                var objFound = db.lsttbl_Dashboards.FirstOrDefault(p => p.Code == info.Code);
                if (objFound == null)
                    db.lsttbl_Dashboards.InsertOnSubmit(info);
                else
                    objFound.UpdateOnSubmit(info);

                db.SubmitChanges();
            }
            catch { }
        }
        public void Set_DashboardDefault(string dbrdCode)
        {
            var db = GlobalVar.DbBI;
            try
            {
                var objFound = db.lsttbl_Dashboards.FirstOrDefault(p => p.Code == dbrdCode);
                if (objFound != null)
                {
                    var curWhCode = objFound.WHCode;
                    objFound.IsDefault = true;
                    // Set lại default = false cho các dashboard khác.
                    foreach (var item in db.lsttbl_Dashboards.Where(p => p.Code != dbrdCode && p.WHCode == curWhCode))
                        item.IsDefault = false;
                }
                db.SubmitChanges();
            }
            catch { }
        }
    }
}