using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPICode.Helpers;

namespace Vitamito.Models.BLL
{
    public class filterBLL
    {
        public string Category { get; set; }
        public string Color { get; set; }
        public string SubCategory { get; set; }
        public string Searchtxt { get; set; }
        public int SortID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ArabicTitle { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Barcode { get; set; }
        public bool InStock { get; set; }
        public string Image { get; set; }
        public string HoveredImage { get; set; }
        public int StatusID { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsFeatured { get; set; }
        public int StockQty { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public double? DoublePrice { get; set; }
        public int? Stars { get; set; }
        public static DataTable _dt;
        public static DataSet _ds;
        public List<filterBLL> GetAll(filterBLL data)
        {
            try
            {
                var lst = new List<filterBLL>();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                p[1] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                
                _ds = (new DBHelper().GetDatasetFromSP)("sp_filterProduct_Vitamito", p);

                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<filterBLL> GetAllCat(filterBLL data)
        {
            try
            {
                var lst = new List<filterBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_filterCategory_Web", p);


                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<filterBLL> GetAll()
        {
            try
            {
                var lst = new List<filterBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetItemsList");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<filterBLL>>();
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