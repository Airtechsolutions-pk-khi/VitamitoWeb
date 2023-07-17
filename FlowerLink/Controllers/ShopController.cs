using Vitamito.Models.BLL;
using Vitamito.Models.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Vitamito.Controllers
{
    public class ShopController : Controller
    {
        shopService _service;
        filterService filterService;
        blogfilterService blogfilterService;
        public ShopController()
        {
            _service = new shopService();
            filterService = new filterService();
            blogfilterService = new blogfilterService();    

        }
        // GET: Shop
        public ActionResult BlogCategory(string Category = "", string CategoryIDs = "", string SubCategoryIDs = "", string Searchtext = "", int SortID = 0, string MinPrice = "", string MaxPrice = "")
        {
            Location location = Location.LocationID;
            var catlist = new blogCategoryBLL().GetAll((int)location);
            ViewBag.Category = catlist.Take(9).ToList();

             
            var blogData = new itemService().GetAllBlog((int)location);
            ViewBag.itemList = blogData.Take(48).ToList().Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();
            ViewBag.BestProduct = blogData.Take(4).ToList().Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();

            var itemlist = new blogfilterBLL().GetAll();
            ViewBag.TodaysSpecial = itemlist.Take(4).ToList();
            TempData["Category"] = Category;
            TempData["CategoryIDs"] = CategoryIDs;
            TempData["SubCategoryIDs"] = SubCategoryIDs;
            TempData["Searchtext"] = Searchtext;
            TempData["MaxPrice"] = MaxPrice;
            TempData["MinPrice"] = MinPrice;
            TempData["SortID"] = SortID.ToString();
            return View();
        }
        public ActionResult Shop(string Category = "", string CategoryIDs = "", string SubCategoryIDs = "", string Searchtext = "", int SortID = 0, string MinPrice = "", string MaxPrice = "")
        {
            Location location = Location.LocationID;
            var catlist = new categoryBLL().GetAll((int)location);
            ViewBag.Category = catlist.Take(9).ToList();

            //ViewBag.BestProduct = new shopService().BestProducts(LocationID).Take(4).ToList();
            var itemData = new itemService().GetAll((int)location);
            ViewBag.itemList = itemData.Take(48).ToList().Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();
            ViewBag.BestProduct = itemData.Take(4).ToList().Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();

            var itemlist = new filterBLL().GetAll();
            ViewBag.TodaysSpecial = itemlist.Take(4).ToList();
            TempData["Category"] = Category;
            TempData["CategoryIDs"] = CategoryIDs;
            TempData["SubCategoryIDs"] = SubCategoryIDs;
            TempData["Searchtext"] = Searchtext;
            TempData["MaxPrice"] = MaxPrice;
            TempData["MinPrice"] = MinPrice;             
            TempData["SortID"] = SortID.ToString();
            return View();
        }

        public JsonResult Filter(filterBLL data)
        {
            ViewBag.shopList = filterService.GetAll(data);
            return Json(new { data = ViewBag.shopList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Products(List<filterBLL> Products)
        {
            ViewBag.Message = "";
            if (Products != null)
            {
                ViewBag.shopList = Products;
                if (ViewBag.shopList.Count < 1)
                {
                    ViewBag.Message = "No Product Found";
                }
                return PartialView("AllProducts");
            }
            else
            {
                if (TempData.Count > 1)
                {
                    if (TempData["CategoryIDs"].ToString() == "" ||
                        TempData["SubCategoryIDs"].ToString() == "" ||
                    TempData["Searchtext"].ToString() == ""  ||
                    TempData["MaxPrice"].ToString() == "" ||
                    TempData["MinPrice"].ToString() == "" ||
                    TempData["SortID"].ToString() != "5"
                     )
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"].ToString();
                        data.SubCategory = TempData["SubCategoryIDs"].ToString();
                        data.Searchtxt = TempData["Searchtext"].ToString();
                        data.MaxPrice = TempData["MaxPrice"].ToString();
                        data.MinPrice = TempData["MinPrice"].ToString();
                        data.SortID = Convert.ToInt32(TempData["SortID"].ToString());

                        ViewBag.shopList = filterService.GetAll(data);
                        if (ViewBag.shopList.Count < 1)
                        {
                            ViewBag.Message = "No Product Found";
                        }
                    }
                }
                else
                {

                    ViewBag.shopList = "";
                    ViewBag.Message = "No Product Found";

                }

                return PartialView("AllProducts");
            }

        }
        public JsonResult BlogFilter(blogfilterBLL data)
        {
            ViewBag.blogList = blogfilterService.GetAll(data);
            return Json(new { data = ViewBag.blogList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogProducts(List<blogfilterBLL> Products)
        {
            ViewBag.Message = "";
            if (Products != null)
            {
                ViewBag.shopList = Products;
                if (ViewBag.shopList.Count < 1)
                {
                    ViewBag.Message = "No Product Found";
                }
                return PartialView("AllBlogs");
            }
            else
            {
                if (TempData.Count > 1)
                {
                    if (TempData["CategoryIDs"].ToString() == "" ||
                        TempData["SubCategoryIDs"].ToString() == "" ||
                    TempData["Searchtext"].ToString() == "" ||
                    TempData["MaxPrice"].ToString() == "" ||
                    TempData["MinPrice"].ToString() == "" ||
                    TempData["SortID"].ToString() != "5"
                     )
                    {
                        blogfilterBLL data = new blogfilterBLL();
                        data.Category = TempData["CategoryIDs"].ToString();
                        data.SubCategory = TempData["SubCategoryIDs"].ToString();
                        data.Searchtxt = TempData["Searchtext"].ToString();
                        data.MaxPrice = TempData["MaxPrice"].ToString();
                        data.MinPrice = TempData["MinPrice"].ToString();
                        data.SortID = Convert.ToInt32(TempData["SortID"].ToString());

                        ViewBag.blogList = blogfilterService.GetAll(data);
                        ViewBag.blogCatHeading = blogfilterService.GetAll(data).Select(x => x.BCatName).FirstOrDefault();
                        ViewBag.blogCatArHeading = blogfilterService.GetAll(data).Select(x => x.BCatArName).FirstOrDefault();
                        if (ViewBag.blogList.Count < 1)
                        {
                            ViewBag.Message = "No Product Found";
                        }
                    }
                }
                else
                {

                    ViewBag.shopList = "";
                    ViewBag.Message = "No Product Found";

                }

                return PartialView("AllBlogs");
            }

        }
    }
}