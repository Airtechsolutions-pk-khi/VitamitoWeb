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
    public class productBLL
    {
        public int ID { get; set; }
        public int SubCategoryID { get; set; }
        public int? UnitID { get; set; }
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

        public List<ItemImages> ImgList = new List<ItemImages>();

        public class ItemImages
        {
            public string Image { get; set; }
            public int Row_Counter { get; set; }
        }

        public static DataTable _dt;
        public static DataSet _ds;

        public productBLL GetAll(int ID, int LocationID)
        {
            try
            {
                var obj = new productBLL();
                List<ItemImages> lstIM = new List<ItemImages>();
               // List<ReviewsBLL> lstR = new List<ReviewsBLL>();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@ID", ID);
                p[1] = new SqlParameter("@LocationID", LocationID);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_ProductVitamito", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        if (_ds.Tables[0] != null)
                        {
                            obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<productBLL>>().FirstOrDefault();
                        }
                        //if (_ds.Tables[1] != null)
                        //{
                        //    lstIM = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<ItemImages>>();
                        //}
                        //if (_ds.Tables[1] != null)
                        //{
                        //    lstR = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<ReviewsBLL>>();
                        //}
                        //obj.ImgList = lstIM;
                        //obj.Reviews = lstR;
                    }
                }
                return obj;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public productBLL GetAll(int ID)
        //{
        //    try
        //    {
        //        var obj = new productBLL();
        //        List<ItemImages> lstIM = new List<ItemImages>();
        //        //List<ReviewsBLL> lstR = new List<ReviewsBLL>();
        //        SqlParameter[] p = new SqlParameter[1];
        //        p[0] = new SqlParameter("@ID", ID);
        //        _ds = (new DBHelper().GetDatasetFromSP)("sp_ProductVitamito", p);
        //        if (_ds != null)
        //        {
        //            if (_ds.Tables.Count > 0)
        //            {
        //                if (_ds.Tables[0] != null)
        //                {
        //                    obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<productBLL>>().FirstOrDefault();
        //                }
        //                if (_ds.Tables[1] != null)
        //                {
        //                    lstIM = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<ItemImages>>();
        //                }
        //                //  if (_ds.Tables[2] != null)
        //                //   {
        //                //      lstR = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[2])).ToObject<List<ReviewsBLL>>();
        //                // }
        //                obj.ImgList = lstIM;
        //                //obj.Reviews = lstR;
        //            }
        //        }
        //        return obj;

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

    }
}