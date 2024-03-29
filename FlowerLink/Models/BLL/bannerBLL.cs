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
    public class reviewBLL
    {
        public int ReviewID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class bannerBLL
    {
        public int BannerID { get; set; }
        public string Title { get; set; }
        public string MainHeading { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int StatusID { get; set; }
        public int Row_Counter { get; set; }
        public string ArabicTitle { get; set; }
        public string ArabicMainHeading { get; set; }
        public string ArabicDescription { get; set; }
        public string FormName { get; set; }
        public string ButtonURL { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        public List<bannerBLL> GetBannerHeader()
        {
            try
            {
                var lst = new List<bannerBLL>();
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = (new DBHelper().GetTableFromSP)("sp_GetHeaderBanner_Vitamito", p);
                //_dt = (new DBHelper().GetTableFromSP)("sp_TestBanner",p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<bannerBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<bannerBLL> GetFeaturedBanner()
        {
            try
            {
                var lst = new List<bannerBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_GetFeaturedBanners_Vitamito", p);
                //_dt = (new DBHelper().GetTableFromSP)("sp_TestBanner",p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<bannerBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<reviewBLL> GetReviews()
        {
            try
            {
                var lst = new List<reviewBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetReviewsWeb");

                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<reviewBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<reviewBLL>();
            }
        }
    }
}