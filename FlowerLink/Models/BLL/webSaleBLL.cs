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
    public class webSaleBLL
    {

        public int WebCustomisedSaleID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string Title { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Type { get; set; }
        public List<WebSalesDetailBLL> WebSaleDetails { get; set; }
        public static DataTable _dt;
        public static DataSet _ds;
        public class WebSalesDetailBLL
        {
            public int WebCustomizedSaleDetailID { get; set; }
            public Nullable<int> WebCustomizedSaleID { get; set; }
            public Nullable<int> LocationID { get; set; }
            public Nullable<int> ItemID { get; set; }
            public string Type { get; set; }
            public string Name { get; set; }
            public Nullable<int> ID { get; set; }

            //Item
            public string ArabicName { get; set; }
            public int? StatusID { get; set; }
            public int SubCategoryID { get; set; }
            public int? UnitID { get; set; }
            public string NameOnReceipt { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public double? Barcode { get; set; }
            public string SKU { get; set; }
            public int? DisplayOrder { get; set; }
            public bool? SortByAlpha { get; set; }
            public double Price { get; set; }
            public double NewPrice { get; set; }
            public double? Cost { get; set; }
            public string ItemType { get; set; }
            public string LastUpdatedBy { get; set; }
            public Nullable<System.DateTime> LastUpdatedDate { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public bool? HasVariant { get; set; }
            public bool? IsVATApplied { get; set; }
            public bool? IsFeatured { get; set; }
            public bool? IsStockOut { get; set; }
            public double CurrentStock { get; set; }
            public bool? IsInventoryItem { get; set; }
            public int? Stars { get; set; }
        }

        public List<webSaleBLL> GetSelectedFlashItems(int LocationID)
        {
            try
            {
                List<webSaleBLL> lst = new List<webSaleBLL>(); //navigationBLL
                List<WebSalesDetailBLL> lstW = new List<WebSalesDetailBLL>(); //SubCategory

                SqlParameter[] p = new SqlParameter[1];

                p[0] = new SqlParameter("@LocationID", LocationID);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetSelectedFlashItem", p);

                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {

                        if (_ds.Tables[1] != null)
                        {
                            var WebSalesDetailList = _ds.Tables[1] == null ? new List<WebSalesDetailBLL>() : JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<WebSalesDetailBLL>>();
                            var list = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<webSaleBLL>>();
                            foreach (var _i in list)
                            {
                                lstW = new List<WebSalesDetailBLL>();
                                foreach (var _j in WebSalesDetailList.Where(x => x.LocationID == _i.LocationID && x.Type == _i.Type).ToList())
                                {
                                    lstW.Add(new WebSalesDetailBLL
                                    {
                                        ID = _j.ID,
                                        Name = _j.Name,
                                        WebCustomizedSaleID = _j.WebCustomizedSaleID,
                                        WebCustomizedSaleDetailID = _j.WebCustomizedSaleDetailID,
                                        StatusID = _j.StatusID,
                                        LocationID = _j.LocationID,
                                        Price = _j.Price,
                                        NewPrice = _j.NewPrice,
                                        DisplayOrder = _j.DisplayOrder,
                                        Type = _j.Type,
                                        Image = _j.Image
                                    });
                                }

                                lst.Add(new webSaleBLL
                                {
                                    WebCustomisedSaleID = _i.WebCustomisedSaleID,
                                    LocationID = _i.LocationID,
                                    Title = _i.Title,
                                    StatusID = _i.StatusID,
                                    StartDate = _i.StartDate,
                                    EndDate = _i.EndDate,
                                    Type = _i.Type,
                                    WebSaleDetails = lstW
                                });
                            }

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

    }
}