﻿using Vitamito.Models.BLL;
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
        public ShopController()
        {
            _service = new shopService();
            filterService = new filterService();

        }
        // GET: Shop
        public ActionResult Shop(int LocationID= 2182, string Category = "", string CategoryIDs = "", string Searchtext = "", int SortID = 0)
        {
            var catlist = new categoryBLL().GetAll(LocationID);
            ViewBag.Category = catlist.Take(9).ToList();

            //ViewBag.BestProduct = new shopService().BestProducts(LocationID).Take(4).ToList();
            var itemData = new itemService().GetAll(LocationID);
            ViewBag.itemList = itemData.Take(48).ToList().Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();
            ViewBag.BestProduct = itemData.Take(4).ToList().Where(x => x.StatusID > 0).OrderBy(x => x.StatusID).ToList();

            var itemlist = new filterBLL().GetAll();
            ViewBag.TodaysSpecial = itemlist.Take(4).ToList();
            TempData["Category"] = Category;
            TempData["CategoryIDs"] = CategoryIDs;
            TempData["Searchtext"] = Searchtext;
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
                    if (TempData["CategoryIDs"].ToString() != "" ||

                    TempData["Searchtext"].ToString() != ""
                     )
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"].ToString();

                        data.Searchtxt = TempData["Searchtext"].ToString();


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
    }
}