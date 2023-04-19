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
    
    public class navigationBLL
    {
        public int  ID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public int StatusID { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        public  List<SubCategory> SubCategories = new List<SubCategory>();
        public class SubCategory
        {
            public int ID { get; set; }
            public int CategoryID { get; set; }
            public string Name { get; set; }
            public string ArabicName { get; set; }
            public int StatusID { get; set; }
        }

        public List<navigationBLL> GetSubCategory()
        {
            try
            {
                List<navigationBLL> Categories = new List<navigationBLL>();
                List<SubCategory> SubCategories = new List<SubCategory>();
                SqlParameter[] p = new SqlParameter[0];
                _ds = (new DBHelper().GetDatasetFromSP)("sp_Navigation_Vitamito", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {

                        if (_ds.Tables[1] != null)
                        {
                            var subCatList = _ds.Tables[1] == null ? new List<SubCategory>() : JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<SubCategory>>();
                            var list = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<navigationBLL>>();
                            foreach (var _i in list)
                            {
                                SubCategories = new List<SubCategory>();
                                foreach (var _j in subCatList.Where(x => x.CategoryID == _i.ID).ToList())
                                {
                                    SubCategories.Add(new SubCategory
                                    {
                                        CategoryID = _j.CategoryID,
                                        Name = _j.Name,
                                        ArabicName = _j.ArabicName,                                        
                                        ID = _j.ID,
                                        StatusID = _j.StatusID
                                    });
                                }

                                Categories.Add(new navigationBLL
                                {
                                    ID = _i.ID,
                                    Name = _i.Name,
                                    ArabicName = _i.ArabicName,
                                    SubCategories = SubCategories
                                });
                            }

                        }
                        //Subcategory.CategoryList = Category;
                    }
                }
                return Categories;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }

}