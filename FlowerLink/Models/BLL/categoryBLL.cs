using Vitamito.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;
using static org.apache.catalina.comet.CometEvent;

namespace Vitamito.Models.BLL
{
    public enum Location
    {
        LocationID = 2195
    }
    public class categoryBLL
    {
        public int ID { get; set; }
        public Location LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? DisplayOrder { get; set; }
        //public bool SortByAlpha { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int? StatusID { get; set; }
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
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetCategory_menu", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<categoryBLL>>().ToList();
                        //lst = _dt.DataTableToList<categoryBLL>();
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