using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.Models;

namespace eShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id)
        {
            Product Prod = DAL.DAL.GetProductById(id);
            List<CategoryModel> Categorys = DAL.DAL.GetCategorys();
            CategoryModel cat = DAL.DAL.GetCategory(Prod.IdCategory);
            List<Characteristicscs> Characteristicsc = DAL.DAL.GetCharacteristicByProductId(id);
            ViewBag.characteristics = Characteristicsc;
            ViewBag.category = cat; //for breadcrumbs
            ViewBag.Product = Prod;
            return View(Categorys);
        }
    }
}