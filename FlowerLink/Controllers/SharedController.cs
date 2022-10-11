using Vitamito.Models.BLL;
using Vitamito.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vitamito.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Header()
        {
            return PartialView("Header", new navigationBLL().GetSubCategory());
        }
        public ActionResult MobileNavigation()
        {
            return PartialView("MobileNavigation", new navigationBLL().GetSubCategory());
        }
    }
}