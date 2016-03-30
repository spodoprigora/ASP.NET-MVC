using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using eShop.Models;
using eShop.DAL;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;




namespace eShop.Controllers
{
    public class HomeController : Controller
    {
        int pageSize = 4; //количество товаров на страпнице
        //главная страница по умолчанию
        public async Task<ActionResult> Index()
        {
            //проверяем роль пользователя если админ то перернаправляем
            var user = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
                   .FindByNameAsync(User.Identity.Name);
            if (user!=null)
            {
                string idUser = user.Id;
                var roleUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(idUser);
                if (roleUser.Count!=0 && roleUser[0] == "admin")
                   return RedirectToAction("Index", "Admin"); //на контроллер Админ
            }
           List<Product> Products = null;
            //настраиваем фильтр
            if (Session["filtr"] != null)
            {
                ViewBag.target = Session["target"];
               switch ((int)Session["filtr"])
                {
                    case 0:                                 //сброс фильтра
                        ViewBag.filter = null;
                        Products = DAL.DAL.GetProductByPage(0, 0, (string)Session["target"]);
                        break;
                    case 1:                                 //цена
                        ViewBag.filter = 1;
                        Products = DAL.DAL.GetProductByPage(0, 1, (string)Session["target"]);
                        break;
                    case 2:                                 //высота
                        ViewBag.filter = 2;
                        Products = DAL.DAL.GetProductByPage(0, 2, (string)Session["target"]);
                        break;
                    case 3:                                 //ширина
                        ViewBag.filter = 3;
                        Products = DAL.DAL.GetProductByPage(0, 3, (string)Session["target"]);
                        break;
                    case 4:                                 //глубина
                        ViewBag.filter = 4;
                        Products = DAL.DAL.GetProductByPage(0, 4, (string)Session["target"]);
                        break;
                    default:
                        ViewBag.filter = 0;
                         Products = DAL.DAL.GetProductByPage(0, 0, (string)Session["target"]); //сброс фильтра
                        break;
                }
            }
            else
                Products = DAL.DAL.GetProductByPage(0, 0, null); //вывод по умолчанию

            Session["category"] = null;
           // List<Product> Products = DAL.DAL.GetProduct(); //удалить метод из DAL
            
            List<CategoryModel> Categorys = DAL.DAL.GetCategorys();
            GlobalModel gm = new GlobalModel();
            ViewBag.Pages = DAL.DAL.PageCount(null);
            ViewBag.CurentPage = 0;
            gm.Categorys = Categorys;
            gm.Products = Products;
            return View(gm);
        }
        //главная страница постраничный вывод
        public ActionResult Pages(int id)
        {
            List<Product> Products = null;

            if (Session["category"] != null)
            {
                int cat = (int)Session["category"];
                if (Session["filtr"] != null) //настраиваем фильтр
                {
                    switch ((int)Session["filtr"])
                    {
                        case 0:
                            ViewBag.filter = null;
                            Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 0, (string)Session["target"]);
                            break;
                        case 1:
                            ViewBag.filter = 1;
                            Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 1, (string)Session["target"]);
                            break;
                        case 2:
                            ViewBag.filter = 2;
                            Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 2, (string)Session["target"]);
                            break;
                        case 3:
                            ViewBag.filter = 3;
                            Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 3, (string)Session["target"]);
                            break;
                        case 4:
                            ViewBag.filter = 4;
                            Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 4, (string)Session["target"]);
                            break;
                        default:
                            ViewBag.filter = 0;
                            Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 0, null); //сброс фильтра
                            break;
                    }

                }
                else
                    Products = DAL.DAL.GetProductByCategoryIdPage(cat, id, 0, null);
                ViewBag.Pages = DAL.DAL.PageCount(cat);//for pages
                CategoryModel category = DAL.DAL.GetCategory(cat);
                ViewBag.category = category; //for breadcrumbs
            }
            else
            {
                if (Session["filtr"] != null) //настраиваем фильтр
                {
                    switch ((int)Session["filtr"])
                    {
                        case 0:
                            ViewBag.filter = null;
                            Products = DAL.DAL.GetProductByPage(id, 0, (string)Session["target"]);
                            break;
                        case 1:
                            ViewBag.filter = 1;
                            Products = DAL.DAL.GetProductByPage(id, 1, (string)Session["target"]);
                            break;
                        case 2:
                            ViewBag.filter = 2;
                            Products = DAL.DAL.GetProductByPage(id, 2, (string)Session["target"]);
                            break;
                        case 3:
                            ViewBag.filter = 3;
                            Products = DAL.DAL.GetProductByPage(id, 3, (string)Session["target"]);
                            break;
                        case 4:
                            ViewBag.filter = 4;
                            Products = DAL.DAL.GetProductByPage(id, 4, (string)Session["target"]);
                            break;
                        default:
                            ViewBag.filter = 0;
                           Products = DAL.DAL.GetProductByPage(id, 0, null); //сброс фильтра
                            break;
                    }

                }
                else
                    Products = DAL.DAL.GetProductByPage(id, 0, null);
                
                ViewBag.Pages = DAL.DAL.PageCount(null);//for pages
            }
           
            List<CategoryModel> Categorys = DAL.DAL.GetCategorys();
               
            ViewBag.CurentPage = id;
            GlobalModel gm = new GlobalModel();
            gm.Categorys = Categorys;
            gm.Products = Products;
            return View("Index", gm);
         }

        //постраничный вывод по категориям

        public ActionResult CategoryProduct(int id)
        {
            List<Product> Products = null;
            Session["category"] = id;
            if (Session["filtr"] != null) //настраиваем фильтр
            {
                switch ((int)Session["filtr"])
                {
                    case 0:
                        ViewBag.filter = null;
                        Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 0, (string)Session["target"]);
                        break;
                    case 1:
                        ViewBag.filter = 1;
                        Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 1, (string)Session["target"]);
                        break;
                    case 2:
                        ViewBag.filter = 2;
                        Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 2, (string)Session["target"]);
                        break;
                    case 3:
                        ViewBag.filter = 3;
                        Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 3, (string)Session["target"]);
                        break;
                    case 4:
                        ViewBag.filter = 4;
                        Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 4, (string)Session["target"]);
                        break;
                    default:
                        ViewBag.filter = 0;
                        Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 0, null); //сброс фильтра
                        break;
                }
            }
            else
                Products = DAL.DAL.GetProductByCategoryIdPage(id, 0, 0, null);

            List<CategoryModel> Categorys = DAL.DAL.GetCategorys();
            CategoryModel cat = DAL.DAL.GetCategory(id);
            ViewBag.category = cat; //for breadcrumbs
            ViewBag.Pages = DAL.DAL.PageCount(id);//for pages
            ViewBag.CurentPage = 0;
            GlobalModel gm = new GlobalModel();
            gm.Categorys = Categorys;
            gm.Products = Products;
            return View("Index", gm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        //подсчет товаров в корзине
        public ActionResult Count()
        {
            int count = 0;
            Dictionary<int, int> basket;
            if (Session["basketSesion"] != null)
            {
               basket = (Dictionary<int, int>)Session["basketSesion"];
                foreach(var item in  basket)
                {
                    count += item.Value;
                }
               
            }
            ViewBag.Count = count;
            return PartialView();
        }
        
        [Authorize]
        public async Task<ActionResult> Profil(bool edit =false, string id=null, string email=null, string tel=null, string name=null, string firstName = null, string adress=null)
        {
            //при редактировании пользователя user = null
            var user = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
                  .FindByNameAsync(User.Identity.Name);
            string idUser = user.Id;
            if (edit)
            {
                ViewBag.edit = true;
            }
            if(id!=null && email!=null && tel!=null && name!=null && firstName!=null && adress!=null){
                DAL.DAL.editUser(id, email, tel, name, firstName, adress);
                ViewBag.edit = false;
            }
           
            List<OrderList> userOrders =null;
            userOrders = DAL.DAL.getUserOrders(idUser);
            ViewBag.Details = userOrders;
            userModel userInfo = new userModel();
            userInfo = DAL.DAL.getUser(idUser);
            ViewBag.userInfo = userInfo;
            return View();
        }

    }
}