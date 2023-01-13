using Vitamito.Models.BLL;
using Vitamito.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vitamito.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public ActionResult Login_Register(int id = 0)
        {
            Session["LoginRoute"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Login_Register(loginBLL service)
        {
            if (service.Mobile != null)
            {
                service.Register();
                Session["LoginNote"] = "Login Now";
                return RedirectToAction("Login_Register", "Account");
            }
            else
            {
                service = service.login();
                Session["LoginNote"] = null;
                Session["ID"] = service.ID;
                Session["CustomerEmail"] = service.Email;
                Session["CustomerContactNo"] = service.Mobile;
                Session["CustomerName"] = string.Concat(service.FullName);
                Session["StatusID"] = service.StatusID;
                if (Convert.ToInt32(Session["StatusID"]) != 0)
                {
                    if (Session["CustomerEmail"].ToString() != null)
                    {
                        Session["LoginNote"] = "Successfully Login";
                        if (Convert.ToInt32(Session["LoginRoute"]) != 0)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Checkout", "Order");
                        }
                    }
                    Session["LoginNote"] = "User is not verified";
                    return RedirectToAction("Login_Register", "Account");
                }
                else
                {
                    Session["CustomerName"] = null;
                    Session["LoginNote"] = "Invalid Email or Password";
                    return RedirectToAction("Login_Register", "Account");
                }
            }

        }
        public ActionResult Logout()
        {
            Session["LoginNote"] = null;
            Session["CustomerID"] = null;
            Session["CustomerEmail"] = null;
            Session["CustomerContactNo"] = null;
            Session["CustomerName"] = null;
            Session["IsVerified"] = null;
            Session["LoginRoute"] = null;
            return RedirectToAction("Index", "Home");
        }


        //public ActionResult GetLogin()
        //{
        //    var a = new loginBLL().login();
        //    return Json(a, JsonRequestBehavior.AllowGet);
        //}

    }
}