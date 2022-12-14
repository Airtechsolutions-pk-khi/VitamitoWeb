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
    public enum Locations :int
    {
        LocationID = 2195,
    }
    public enum UsersID : int
    {
        UserID = 2313,
    }
    public class notificationBLL
    {
        public int StatusID { get; set; }
        public int NotificationID { get; set; }

        public string Title { get; set; }

        public string ButtonURL { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
    public class settingBLL
    {
        public int SettingID { get; set; }
        public int ID { get; set; }
        public double DeliveryCharges { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double ServiceCharges { get; set; }
        public double MinOrderAmount { get; set; }
        public double OtherCharges { get; set; }
        public double TaxPercent { get; set; }
        public double MinimumOrderValue { get; set; }
        public double COD { get; set; }
        public double Credimax { get; set; }
        public double PayPal { get; set; }
        public double BenefitPay { get; set; }
        public string TopHeadDescription { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string ShopUrl { get; set; }
        public int? Facebook { get; set; }
        public int? Instagram { get; set; }
        public int? Twitter { get; set; }
        public List<notificationBLL> NotificationsList { get; set; }
        public static DataTable _dt;
        public static DataSet _ds;
        public settingBLL GetSettings(int ID, int LocationID)
        {
            try
            {
                var obj = new settingBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@UserID", ID);
                
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetSettings_Vitamito", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        if (_ds.Tables[0] != null)
                        {
                            obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<settingBLL>>().FirstOrDefault();
                        }
                    }
                }
                return obj;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}