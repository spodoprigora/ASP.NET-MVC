using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.DAL;
using eShop.Models;

namespace eShop.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Index()
        {
            List<CategoryModel> Categorys = DAL.DAL.GetCategorys();
            return PartialView(Categorys);
        }
    }
}