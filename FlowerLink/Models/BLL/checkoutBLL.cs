using Vitamito.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WebAPICode.Helpers;
using System.Configuration;

namespace Vitamito.Models.BLL
{
    public class checkoutBLL
    {
        public int? PaymentMethodID { get; set; }
        public int? OrderID { get; set; }
        public int OrderNo { get; set; }
        public int? CustomerID { get; set; }
        public double? AmountTotal { get; set; }
        public double? GrandTotal { get; set; }
        public double? Tax { get; set; }
        public double? DeliveryAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public int? TotalItems { get; set; }
        public int? StatusID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? CustOrderInfoID { get; set; }
        /*public string OrderID { get; set; }*/
        public string Address { get; set; }
        public string NearestPlace { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PlaceType { get; set; }
        public string Email { get; set; }
        public string CardNotes { get; set; }
        public string SelectedTime { get; set; }
        public List<OrderDetails> OrderDetail = new List<OrderDetails>();
        public string OrderDetailString { get; set; }
        /*Order Details*/
        public class OrderDetails
        {
            public int OrderDetailID { get; set; }
            public int? OrderID { get; set; }            
            public int ID { get; set; }
            public string Name { get; set; }
            public string ProNote { get; set; }
            public string Image { get; set; }
            public int GiftID { get; set; }
            public int Qty { get; set; }
            public double Price { get; set; }
            public double Cost { get; set; }
            public double DiscountAmount { get; set; }
            public Nullable<System.DateTime> LastUpdatedDate { get; set; }
            public int LastUpdatedBy { get; set; }
            public int DealID { get; set; }
            public int Key { get; set; }
        }
        /*Order Details*/
        public class OrderGiftDetails
        {
            public int OrderDetailID { get; set; }
            public int ItemID { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public int GiftID { get; set; }
            public int Quantity { get; set; }
            public double DisplayPrice { get; set; }
            public double Cost { get; set; }
            public double DiscountAmount { get; set; }
            public Nullable<System.DateTime> LastUpdatedDate { get; set; }
            public int LastUpdatedBy { get; set; }
            public int ItemKey { get; set; }
        }
        public static DataSet _ds;
        public int InsertOrder(checkoutBLL data)
        {
            try
            {
                int rtn = 0;

               
                var OrderDate = DateTime.UtcNow.AddMinutes(180);
                 
                SqlParameter[] p = new SqlParameter[21];
                //ORDER MASTER
                
                if (data.CustomerID == 0)
                {
                    p[0] = new SqlParameter("@CustomerID", DBNull.Value);
                }
                else
                {
                    p[0] = new SqlParameter("@CustomerID", data.CustomerID);
                }
                p[1] = new SqlParameter("@AmountTotal", data.AmountTotal);
                p[2] = new SqlParameter("@GrandTotal", data.GrandTotal);
                p[3] = new SqlParameter("@Tax", data.Tax);
                p[4] = new SqlParameter("@AmountDiscount", data.DiscountAmount);
                if (data.PaymentMethodID == 1)
                {
                    p[5] = new SqlParameter("@StatusID", 2);
                }
                else
                {
                    p[5] = new SqlParameter("@StatusID", 103);
                }
                p[6] = new SqlParameter("@OrderDate", OrderDate);
                p[7] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[8] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                //CUSTOMER ORDER INFO
                p[9] = new SqlParameter("@Address", data.Address);
                p[10] = new SqlParameter("@NearestPlace", data.NearestPlace);
                p[11] = new SqlParameter("@Country", data.Country);
                p[12] = new SqlParameter("@ContactNo", data.ContactNo);
                p[13] = new SqlParameter("@DeliveryTime", data.DeliveryTime);
                p[14] = new SqlParameter("@CustomerName", data.CustomerName);
                p[15] = new SqlParameter("@Latitude", data.Latitude);
                p[16] = new SqlParameter("@Longitude", data.Longitude);
                p[17] = new SqlParameter("@PlaceType", data.PlaceType);
                p[18] = new SqlParameter("@Email", data.Email);
                p[19] = new SqlParameter("@CardNotes", data.CardNotes);
                p[20] = new SqlParameter("@ServiceCharges", data.DeliveryAmount);  
                int OrderID = int.Parse(new DBHelper().GetTableFromSP("sp_InsertOrder_Vitamito", p).Rows[0]["ID"].ToString());
                rtn = OrderID;
                //Payment 
                try
                {
                    SqlParameter[] pay = new SqlParameter[6];
                    pay[0] = new SqlParameter("@OrderID", OrderID);
                    pay[1] = new SqlParameter("@CashPayment", data.GrandTotal);                     
                    pay[2] = new SqlParameter("@CardPayment", 0);                                        
                    pay[3] = new SqlParameter("@CreditPayment", 0);                                          
                    pay[4] = new SqlParameter("@PaymentType", "Cash");                    
                    pay[5] = new SqlParameter("@Total", data.GrandTotal);
                  
                    (new DBHelper().ExecuteNonQueryReturn)("sp_InsertPayment_Vitamito", pay);

                }
                catch(Exception ex) 
                {
                    
                }
                try
                {
                    
                    foreach (var item in data.OrderDetail)
                    {                       
                        SqlParameter[] dst = new SqlParameter[4];
                        dst[0] = new SqlParameter("@ItemID", item.ID);
                        dst[1] = new SqlParameter("@LocationID", 2195);
                        dst[2] = new SqlParameter("@Quantity", item.Qty);
                        dst[3] = new SqlParameter("@LastUpdatedDate", item.LastUpdatedDate);

                        (new DBHelper().GetTableFromSP)("sp_DeductStockAdmin", dst);
                    }                    

                }
                catch(Exception ex) { 
                
                }
                try
                {
                    int OrderDetailID = 0;
                    foreach (var item in data.OrderDetail)
                    {
                        SqlParameter[] para = new SqlParameter[6];
                        para[0] = new SqlParameter("@OrderID", OrderID);//Hard Coded Value Pass
                        para[1] = new SqlParameter("@ItemID", item.ID);                        
                        para[2] = new SqlParameter("@Quantity", item.Qty);
                        para[3] = new SqlParameter("@Price", item.Price);
                        para[4] = new SqlParameter("@LastUpdateDT", item.LastUpdatedDate);
                        para[5] = new SqlParameter("@LastUpdateBy", item.LastUpdatedBy);
                        OrderDetailID = int.Parse(new DBHelper().GetTableFromSP("sp_OrderDetails_Vitamito", para).Rows[0]["ID"].ToString());
                    }
                }
                
               

                catch (Exception ex)
                {
                    using (StreamWriter writer = new StreamWriter(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template") + "\\" + "error.txt"), true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();

                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message + "##" + data);
                            writer.WriteLine("Object : " + data);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);

                            ex = ex.InnerException;
                        }
                    }
                }
                return rtn;
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template") + "\\" + "error.txt"), true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message + "##" + data);
                        writer.WriteLine("Object : " + data);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return 0;
            }
        }
        public int OrderUpdate(int OrderID, int StatusID)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@OrderID", OrderID);
                p[1] = new SqlParameter("@StatusID", StatusID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_OrderReject", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}