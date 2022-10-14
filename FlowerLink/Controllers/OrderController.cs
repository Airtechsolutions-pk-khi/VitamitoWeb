using Vitamito.Models.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static Vitamito.Models.BLL.checkoutBLL;
using Vitamito.Models;
using System.Configuration;
using com.fss.plugin;
using System.Net.Mail;

namespace Vitamito.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Cart()
        {
            //ViewBag.Banner = new bannerBLL().GetBanner("Cart");
            return View();
        }
        public ActionResult Checkout(int id = 0)
        {
            int CustomerID = id;
            if (CustomerID == 0)
            {
                Session["CustomerID"] = 0;
                return View();
            }
            else
            {
                if (Session["CustomerID"] != null && Convert.ToInt32(Session["CustomerID"]) != 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login_Register", "Account");
                }
            }
            //return View();
        }
        public ActionResult OrderComplete(int OrderID = 0, string OrderNo = "")
        {
            checkoutBLL check = new checkoutBLL();
            if (OrderNo == "Reject")
            {
                ViewBag.OrderNo = "Reject";
            }
            else
            {
                var data = new myorderBLL().GetDetails(OrderID);
                //if (data.PaymentMethodTitle != "Cash on Delivery")
                //{
                //    check.OrderUpdate(OrderID, 101);//In progress
                //}
                string ToEmail, SubJect, cc, Bcc;
                ToEmail = data.Email;
                SubJect = "Your order on Vitamito - " + data.OrderNo;
                string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "emailpattern.txt");
                string BodyEmailadmin = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "emailpattern-admin.txt");
                string items = "";
                foreach (var item in data.OrderDetail)
                {
                    try
                    {
                        items += "<table border = '0' cellpadding = '0' cellspacing = '0' align = 'center' width = '100%' role = 'module' data - type = 'columns' style = 'padding:20px 20px 20px 30px;' bgcolor = '#FFFFFF'>"
                        + "<tbody>"
                        + "<tr role = 'module-content'>"
                        + "<td height = '100%' valign = 'top'>"
                        + "<table class='column' width='137' style='width:137px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                        + "<tbody>"
                        + "<tr>"
                        + "<td style = 'padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='239f10b7-5807-4e0b-8f01-f2b8d25ec9d7'>"
                        + "<tbody>"
                        + "<tr>"
                        + "<td style = 'font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='left'>"
                        + "<img src = '" + System.Configuration.ConfigurationManager.AppSettings["Image"].ToString() + item.Image + "' class='max-width' border='0' style='display:block;width: 108px;height: 108px;object-fit: contain;' alt='' >"
                        + "</td>"
                        + "</tr>"
                        + "</tbody>"
                        + "</table></td>"
                        + "</tr>"
                        + "</tbody>"
                        + "</table>"
                        + "<table class='column' style='display: contents; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                        + "<tbody>"
                        + "<tr>"
                        + "<td style = 'padding:0px;margin:0px;border-spacing:0;' ><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='f404b7dc-487b-443c-bd6f-131ccde745e2'>"
                        + "<tbody>"
                        + "<tr>"
                        + "<td style = 'padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'> " + item.Name + "</div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'> Qty : " + item.Quantity + "</div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'><span style='color: #006782'>RS " + item.Price + "</span></div>"
                        + "<div></div></div></td>"
                        + "</tr>"
                        + "</tbody>"
                        + "</table>"
                        + "</td>"
                        + "</tr>"
                        + "</tbody>"
                        + "</table>"
                        + "</td>"
                        + "</tr>"
                        + "</tbody>"
                        + "</table>";

                    }
                    catch (Exception ex)
                    {
                    }
                }
                BodyEmail = BodyEmail.Replace("#Customer#", data.CustomerName.ToString());
                BodyEmail = BodyEmail.Replace("#ReceiverContact#", data.ContactNo.ToString());
                BodyEmail = BodyEmail.Replace("#OrderNo#", data.OrderNo.ToString());
                BodyEmail = BodyEmail.Replace("#items#", items.ToString());

                BodyEmailadmin = BodyEmailadmin.Replace("#Customer#", data.CustomerName.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ReceiverContact#", data.ContactNo.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#CustomerAddress#", data.Address.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#OrderNo#", data.OrderNo.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#items#", items.ToString());
                DateTime dateTime = DateTime.UtcNow.AddMinutes(180);

                //BodyEmail = BodyEmail.Replace("#CustomerContact#", data.SenderName.ToString());
                BodyEmail = BodyEmail.Replace("#SelectedTime#", dateTime.ToString());
                BodyEmail = BodyEmail.Replace("#DeliveryDate#", data.DeliveryDate.ToString());
                BodyEmail = BodyEmail.Replace("#OrderDate#", dateTime.ToString("dd/MMM/yyyy"));
                BodyEmail = BodyEmail.Replace("#Address#", data.Address.ToString());

                BodyEmailadmin = BodyEmailadmin.Replace("#SelectedTime#", dateTime.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#DeliveryDate#", data.DeliveryDate.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#OrderDate#", dateTime.ToString("dd/MMM/yyyy"));
                BodyEmailadmin = BodyEmailadmin.Replace("#Address#", data.Address.ToString());
                 
                BodyEmail = BodyEmail.Replace("#Description#", data.CardNotes.ToString());
                //BodyEmail = BodyEmail.Replace("#PaymentMethod#", data.PaymentMethodTitle.ToString());
                //BodyEmail = BodyEmail.Replace("#TotalItems#", data.TotalItems.ToString());
                BodyEmail = BodyEmail.Replace("#SubTotal#", data.AmountTotal.ToString());
                BodyEmail = BodyEmail.Replace("#Tax#", data.Tax.ToString());
                BodyEmail = BodyEmail.Replace("#DeliveryAmount#", data.DeliveryAmount.ToString());
                BodyEmail = BodyEmail.Replace("#GrandTotal#", data.GrandTotal.ToString());

                //BodyEmailadmin = BodyEmailadmin.Replace("#PaymentType#", PaymentType.ToString());
                //BodyEmailadmin = BodyEmailadmin.Replace("#Description#", data.CardNotes.ToString());
                //BodyEmailadmin = BodyEmailadmin.Replace("#PaymentMethod#", data.PaymentMethodTitle.ToString());
                //BodyEmailadmin = BodyEmailadmin.Replace("#TotalItems#", data.TotalItems.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#SubTotal#", data.AmountTotal.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#Tax#", data.Tax.ToString());
                //BodyEmailadmin = BodyEmailadmin.Replace("#DeliveryAmount#", data.DeliveryAmount.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#GrandTotal#", data.GrandTotal.ToString());
                cc = "";
                Bcc = ConfigurationManager.AppSettings["From"].ToString();
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(ToEmail);

                    mail.From = new MailAddress(ConfigurationManager.AppSettings["From"].ToString());
                    mail.Subject = SubJect;
                    string Body = BodyEmail;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                    smtp.Host = ConfigurationManager.AppSettings["SmtpServer"].ToString(); //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         (ConfigurationManager.AppSettings["From"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = false;

                    smtp.Send(mail);

                    ViewBag.Status = "Order Invoice will be sent to your Email.";
                }
                catch (Exception ex)
                {
                    ViewBag.Status = "";
                }
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(ConfigurationManager.AppSettings["From"].ToString());

                    mail.From = new MailAddress(ConfigurationManager.AppSettings["From"].ToString());
                    mail.Subject = "NEW ORDER | " + SubJect;
                    string Body = BodyEmailadmin;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                    smtp.Host = ConfigurationManager.AppSettings["SmtpServer"].ToString(); //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         (ConfigurationManager.AppSettings["From"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = false;

                    smtp.Send(mail);
                }
                catch (Exception)
                {
                }
                ViewBag.OrderNo = data.OrderNo;
            }

            return View();
        }
        //Coupon
        public JsonResult PunchOrder(checkoutBLL data)
        {
            try
            {
                checkoutBLL _service = new checkoutBLL();
                //orderdetails
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(JArray.Parse(data.OrderDetailString));
                JArray jsonResponse = JArray.Parse(json);
                data.OrderDetail = jsonResponse.ToObject<List<checkoutBLL.OrderDetails>>();              
                int rtn = _service.InsertOrder(data);
                return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult MyOrders()
       {
            ViewBag.Banner = new bannerBLL().GetBanner("Other");
            var a = Session["ID"];
            if (Session["ID"] != null && Convert.ToInt32(Session["ID"]) != 0)
            {
                return View(new myorderBLL().GetAll(Convert.ToInt32(Session["ID"])));
            }
            else
            {
                return RedirectToAction("Login_Register", "Account");
            }
        }
        public ActionResult OrderDetails(int OrderID)
        {
            ViewBag.Banner = new bannerBLL().GetBanner("Other");
            ViewBag.BillingInfo = new myorderBLL().GetAll(Convert.ToInt32(Session["ID"]));
            return View(new myorderBLL().GetDetails(OrderID));
        }
    }
}