using Vitamito.Models.BLL;
using Vitamito.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vitamito.Controllers
{
    public class ProductController : Controller
    {
        productService _service;
        public ProductController()
        {
            _service = new productService();

        }
        // GET: Product
        public ActionResult ProductDetails(int ID, int LocationID = 2195)
        {
            ViewBag.ProductDetails = _service.GetAll(ID, LocationID);

            //var _items = new itemService().GetSelecteditems(ID, LocationID);
             
            //ViewBag.itemList = _items.Take(3).Where(x => x.StatusID == 1).ToList();

            return View(_service.GetAll(ID, LocationID));
            //return View();
        }
        public ActionResult Wishlist()
        {
            // ViewBag.Banner = new bannerBLL().GetBanner("Other");
            return View();
        }
          public JsonResult PostProductReview(productBLL.ReviewsBLL data)
        {
            return Json(new
            {
                data = new productBLL().InsertProductReview(data)
            }, JsonRequestBehavior.AllowGet);
        }

    }
}