using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTLBIWebApp2012.Codes.Models;
using CECOM;

namespace HTLBIWebApp2012.Codes.BLL
{
    public class MySys
    {
        private static MySys me;
        public static MySys Me
        {
            get
            {
                if (me == null) me = new MySys();
                return me;
            }
        }

        // Dashboard (Lưu thông tin dashboard do người dùng thiết lập)...
        public IQueryable<systbl_Menu> Get_Menu()
        {
            return GlobalVar.DbBI.systbl_Menus.Where(p => p.Visible).OrderBy(p => p.SortOrder);
        }
        public IQueryable<systbl_Menu> Get_MenuRoot()
        {
            return this.Get_Menu().Where(p => p.ParentCode == null || p.ParentCode == "");
        }
        public IQueryable<systbl_Menu> Get_MenuChild(string parentCode)
        {
            return this.Get_Menu().Where(p => p.ParentCode == parentCode);
        }
        public systbl_Menu Get_Menu(string code)
        {
            return this.Get_Menu().FirstOrDefault(p => p.Code == code);
        }
    }
}