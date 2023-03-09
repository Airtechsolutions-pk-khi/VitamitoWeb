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
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public int SortID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ArabicTitle { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public double? NewPrice { get; set; }
        public double DiscountedPrice { get; set; }
        public string Barcode { get; set; }
        public bool InStock { get; set; }
        public string Image { get; set; }
        public string HoveredImage { get; set; }
        public int StatusID { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsStockOut { get; set; }
        public int StockQty { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public double? DoublePrice { get; set; }
        public int? Stars { get; set; }
        public static DataTable _dt;
        public static DataSet _ds;
        public List<filterBLL> GetAll(filterBLL data)
        {
            try
            {
                var lst = new List<filterBLL>();
                if (!data.MaxPrice.Equals("") && !data.MaxPrice.Equals(" ") && data.MaxPrice != null)
                {
                    
                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);                   
                    p[1] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                    p[2] = new SqlParameter("@MinPrice", "");
                    p[3] = new SqlParameter("@MaxPrice", data.MaxPrice == "" ? null : data.MaxPrice);
                    p[4] = new SqlParameter("@SortID", data.SortID);
                    p[5] = new SqlParameter("@SubCategory", data.SubCategory == "" ? null : data.SubCategory);
                    _ds = (new DBHelper().GetDatasetFromSP)("sp_PricefilterProduct_Vitamito", p);

                    if (_ds != null)
                    {
                        if (_ds.Tables.Count > 0)
                        {
                            lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                        }
                    }                     
                }
                 
                
                else if (!data.Category.Equals("") && !data.Category.Equals(" ") || !data.SubCategory.Equals("") && !data.SubCategory.Equals(" "))
                {

                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                    p[1] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                    p[2] = new SqlParameter("@MinPrice", "");
                    p[3] = new SqlParameter("@MaxPrice", data.MaxPrice == "" ? null : data.MaxPrice);
                    p[4] = new SqlParameter("@SortID", data.SortID);
                    p[5] = new SqlParameter("@SubCategory", data.SubCategory == "" ? null : data.SubCategory);
                    _ds = (new DBHelper().GetDatasetFromSP)("sp_CategoryfilterProduct_Vitamito", p);

                    if (_ds != null)
                    {
                        if (_ds.Tables.Count > 0)
                        {
                            lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                        }
                    }
                }

                else if (data.SortID == 1 || data.SortID == 2 || data.SortID == 3 || data.SortID == 4)
                {

                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                    p[1] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                    p[2] = new SqlParameter("@MinPrice", "");
                    p[3] = new SqlParameter("@MaxPrice", data.MaxPrice == "" ? null : data.MaxPrice);
                    p[4] = new SqlParameter("@SortID", data.SortID);
                    p[5] = new SqlParameter("@SubCategory", data.SubCategory == "" ? null : data.SubCategory);
                    _ds = (new DBHelper().GetDatasetFromSP)("sp_SortfilterProduct_Vitamito", p);

                    if (_ds != null)
                    {
                        if (_ds.Tables.Count > 0)
                        {
                            lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                        }
                    }
                }
                else
                {

                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                    p[1] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                    p[2] = new SqlParameter("@MinPrice", "");
                    p[3] = new SqlParameter("@MaxPrice", data.MaxPrice == "" ? null : data.MaxPrice);
                    p[4] = new SqlParameter("@SortID", data.SortID);
                    p[5] = new SqlParameter("@SubCategory", data.SubCategory == "" ? null : data.SubCategory);
                    _ds = (new DBHelper().GetDatasetFromSP)("sp_filterProduct_Vitamito", p);

                    if (_ds != null)
                    {
                        if (_ds.Tables.Count > 0)
                        {
                            lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                        }
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