﻿using Vitamito.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace Vitamito.Models.BLL
{
    public class categoryBLL
    {
        public int ID { get; set; }
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int DisplayOrder { get; set; }
        public bool SortByAlpha { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int StatusID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        public List<categoryBLL> GetAll(int LocationID)
        {
            try
            {
                var lst = new List<categoryBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@LocationID", LocationID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetCategory_menu", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<categoryBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}