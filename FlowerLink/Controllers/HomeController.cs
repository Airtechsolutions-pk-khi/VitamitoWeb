using Vitamito.Models.BLL;
using Vitamito.Models.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Vitamito.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string[] args)
        {
            Locations location = Locations.LocationID; 
            ViewBag.Banner = new bannerBLL().GetBanner();
            ViewBag.FeaturedBanner = new bannerBLL().GetFeaturedBanner();
            var itemData = new itemService().GetAll((int)location);
            ViewBag.itemList = itemData.Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();
            //ViewBag.Featureditems = itemData.OrderByDescending(x => x.DisplayOrder).Where(x => x.IsFeatured == true).OrderBy(c => Guid.NewGuid()).Take(6).ToList();
            ViewBag.NewArrivals = itemData.OrderByDescending(c => c.LastUpdatedDate).Take(7).ToList();
            ViewBag.LowestPrice = itemData.OrderBy(c => c.Price).Take(7).ToList();

            ViewBag.TenItems = itemData.Where(x => x.ID > 0).OrderBy(x => x.Name).Take(7).ToList();

            var catlist = new categoryBLL().GetAll((int)location);
            ViewBag.categoryList = catlist.Take(6).ToList();
            ViewBag.Category = catlist.ToList();
            //var popularProduct = new itemService().GetAllPopular();
            //ViewBag.PopularProducts = popularProduct.Take(8).ToList();
            return View();
        }
        public ActionResult About()
        {
            //ViewBag.Banner = new bannerBLL().GetBanner("About");
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            //ViewBag.Banner = new bannerBLL().GetBanner("Contact");
            return View();
        }
        [HttpPost]
        public ActionResult Contact(contactBLL obj)
        {
            ViewBag.Contact = "";
            string ToEmail, SubJect, cc, Bcc;
            cc = "";
            Bcc = "";
            ToEmail = ConfigurationManager.AppSettings["From"].ToString();
            SubJect = "New Query From Customer";
            string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "contact.txt");
            DateTime dateTime = DateTime.UtcNow.Date;
            BodyEmail = BodyEmail.Replace("#Date#", dateTime.ToString("dd/MMM/yyyy"))
            .Replace("#Name#", obj.Name.ToString())
            .Replace("#lastName#", obj.Name.ToString())
            .Replace("#Email#", obj.Email.ToString())
            .Replace("#Phone#", obj.Phone.ToString())
            .Replace("#Message#", obj.Message.ToString());
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
                ViewBag.Contact = "Your Query is received. Our support department contact you soon.";
            }
            catch (Exception ex)
            {
                ViewBag.Contact = "";
            }
            return View();
        }
        //Get Setting Details
        public ActionResult GetSetting(string[] args)
        {
            Locations location = Locations.LocationID;
            UsersID users = UsersID.UserID;

            return Json(new settingBLL().GetSettings((int)users, (int)location), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Policy()
        {
            return View();
        }
    }
}