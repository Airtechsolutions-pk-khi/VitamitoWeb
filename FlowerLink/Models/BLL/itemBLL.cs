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
    public class itemBLL
    {
        public int ID { get; set; }
        public int SubCategoryID{ get; set; }
        public int? UnitID{ get; set; }
        public string Name { get; set; }
        public string NameOnReceipt { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double? Barcode { get; set; }
        public string SKU { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? SortByAlpha { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public string ItemType { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int? StatusID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool? HasVariant { get; set; }
        public bool? IsVATApplied { get; set; }
        public double? CurrentStock { get; set; }
        public static DataTable _dt;
        public static DataSet _ds;

        public List<itemBLL> GetAll(int LocationID)
        {
            try
            {
                var lst = new List<itemBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@LocationID", LocationID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetItem_menu", p);

                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<itemBLL> GetSelecteditems(int ID,int LocationID)
        {
            try
            {
                var lst = new List<itemBLL>();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", ID);
                p[1] = new SqlParameter("@LocationID", LocationID);
                _dt = (new DBHelper().GetTableFromSP)("sp_itemListselected_Vitamito",p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<itemBLL> GetAllFeatured()
        {
            try
            {
                var lst = new List<itemBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_Featureditems");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst= JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<itemBLL> GetAllPopular()
        {
            try
            {
                var lst = new List<itemBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_PopularProducts");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<itemBLL> GetAllValentineDay()
        {
            try
            {
                var lst = new List<itemBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_ValentineDaySpecial");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
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