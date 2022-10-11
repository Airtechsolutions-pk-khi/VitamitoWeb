using Vitamito.Models;
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
    public class shopBLL
    {
        public int ID { get; set; }
        public int? SubCategoryID { get; set; }
        public int? UnitID { get; set; }
        public string Name { get; set; }
        public string NameOnRecipt { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Barcode { get; set; }
        public string SKU { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? SortByAlpha { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public string ItemType { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public int? StatusID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool? HasVariant { get; set; }
        public bool? IsVATApplied { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        public List<shopBLL> GetAll(string Category)
        {
            try
            {
                var lst = new List<shopBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Category", Category);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetshopList", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<shopBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<shopBLL> BestProducts(int LocationID)
        {
            try
            {
                var lst = new List<shopBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@LocationID", LocationID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetProducts_Vitamito", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<shopBLL>>();
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