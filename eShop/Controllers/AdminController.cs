using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShop.Models;
using eShop.DAL;

namespace eShop.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(int id = 0, string editCat = null, string editProd = null,
                                    string charact = null, int tabIndex = default(int),
                                    int page = 0, int Opage = 0,
                                    int orderStatus = 0, string orderStartDate=null, string orderEndDate = null, 
                                    int categoryFiltr = 0, string activeFiltr = null, string UserId=null)
        {

            if (editCat != null)
               ViewBag.editCatId = Convert.ToInt32(editCat);
           
            if (editProd != null)
               ViewBag.editProdId = Convert.ToInt32(editProd);

            if (charact != null)
            {
                int c = Convert.ToInt32(charact);
                ViewBag.characteristics = DAL.DAL.getCharacteristics(c);
            }
            if(UserId !=null){
               ViewBag.user = DAL.DAL.getUser(UserId);
            }

            ViewBag.TabIndex = tabIndex;

             List<getOrderList> Orders= null;
             List<Description> Descriptions = null;
             List<string> status = null;
            int Id = id;
            decimal sum=0;
            if(id!=0)
            {
                //вытягиваем из базы список продуктов для описания
                Descriptions = DAL.DAL.getDescription(id);
                foreach(var obj in Descriptions){
                   sum+= obj.count * obj.price;
                }
                ViewBag.selectId = id;
                ViewBag.Details = Descriptions;
                ViewBag.Sum = sum;
            }
            status = DAL.DAL.getStatusList();
            ViewBag.status = status;

            if ((orderStatus != 0 && orderStartDate == null && orderEndDate == null) || Session["orderFiltrStatus"] != null)
            {         //фильтруем по статусу
                if (Session["orderFiltrStatus"] != null)
                   orderStatus = (int)Session["orderFiltrStatus"];
                              
                Orders = DAL.DAL.getOrder(Opage, orderStatus);
                ViewBag.OrderPageCount = (int)DAL.DAL.OrderPageCount(orderStatus);
                ViewBag.orderFiltrStatus = orderStatus;
                Session["orderFiltrStatus"] = orderStatus;

            }
            else if(orderStatus == 0 && orderStartDate !=null && orderEndDate !=null){    //фильтруем по датам
                if (Session["orderFiltrStart"] != null && Session["orderFiltrEnd"] != null)
                {
                    orderStartDate = (string)Session["orderFiltrStart"];
                    orderEndDate = (string)Session["orderFiltrEnd"];
                }
                Orders = DAL.DAL.getOrder(Opage, 0, orderStartDate, orderEndDate);
                ViewBag.OrderPageCount = (int)DAL.DAL.OrderPageCount(0, orderStartDate, orderEndDate);
                ViewBag.orderFiltrStartDate = orderStartDate;
                ViewBag.orderFilterEndDate = orderEndDate;
                Session["orderFiltrStart"] = orderStartDate;
                Session["orderFiltrEnd"] = orderEndDate;

            }
            else if (orderStatus != 0 && orderStartDate != null && orderEndDate != null){   //фильтруем по статусу и датам
                if (Session["orderFiltrStatus"] != null && Session["orderFiltrStart"] != null && Session["orderFiltrEnd"] != null)
                {
                    orderStatus = (int)Session["orderFiltrStatus"];
                    orderStartDate = (string)Session["orderFiltrStart"];
                    orderEndDate = (string)Session["orderFiltrEnd"];
                }
                Orders = DAL.DAL.getOrder(Opage, orderStatus, orderStartDate, orderEndDate);
                ViewBag.OrderPageCount = (int)DAL.DAL.OrderPageCount(orderStatus, orderStartDate, orderEndDate);
                ViewBag.orderFiltrStartDate = orderStartDate;
                ViewBag.orderFilterEndDate = orderEndDate;
                ViewBag.orderFiltrStatus = orderStatus;
                Session["orderFiltrStart"] = orderStartDate;
                Session["orderFiltrEnd"] = orderEndDate;
                Session["orderFiltrStatus"] = orderStatus;
            }
            else{                                                                       // без фильтра
                Orders = DAL.DAL.getOrder(Opage);
                ViewBag.OrderPageCount = (int)DAL.DAL.OrderPageCount();
            }

            ViewBag.OrderCurentPage = Opage;
            List<CategoryModel> Categorys = DAL.DAL.GetCategorys();
            ViewBag.Category = Categorys;
           
            //работа с продуктами
            if ((categoryFiltr != 0 && activeFiltr == null) || Session["productFiltrCategory"] != null)    //фильтруем по категориям
            {
                if (Session["productFiltrCategory"] != null )
                    categoryFiltr = (int)Session["productFiltrCategory"];
                List<editProduct> Products = DAL.DAL.getEditProduct(page, categoryFiltr);
                ViewBag.editProducts = Products;
                ViewBag.PPageCount = (int)DAL.DAL.EditPageCount(categoryFiltr);
                ViewBag.productFiltrCategory = categoryFiltr;
                Session["productFiltrCategory"] = categoryFiltr;
            }
            else if (categoryFiltr == 0 && activeFiltr != null || Session["productFiltractive"]!=null) //фильтруем по активности
            {
                if (Session["productFiltractive"] != null)
                   activeFiltr = (string)Session["productFiltractive"];
                List<editProduct> Products = DAL.DAL.getEditProduct(page, categoryFiltr, activeFiltr);
                ViewBag.editProducts = Products;
                ViewBag.PPageCount = (int)DAL.DAL.EditPageCount(categoryFiltr, activeFiltr);
                Session["productFiltractive"] = activeFiltr;
                ViewBag.productFiltrActive = activeFiltr;

            }
            else if (categoryFiltr != 0 && activeFiltr != null || Session["productFiltrCategory"] != null && Session["productFiltractive"] != null) //фильтруем по категориям и активности
            {
                if (Session["productFiltrCategory"] != null && Session["productFiltractive"] != null)
                {
                    categoryFiltr = (int)Session["productFiltrCategory"];
                    activeFiltr = (string)Session["productFiltractive"];
                }
                List<editProduct> Products = DAL.DAL.getEditProduct(page, categoryFiltr, activeFiltr);
                ViewBag.editProducts = Products;
                ViewBag.PPageCount = (int)DAL.DAL.EditPageCount(categoryFiltr, activeFiltr);
                Session["productFiltrCategory"] = categoryFiltr;
                Session["productFiltractive"] = activeFiltr;
                ViewBag.productFiltrCategory = categoryFiltr;
                ViewBag.productFiltrActive = activeFiltr;
            }
            else                                                 //без фильтра
            {   
                List<editProduct> Products = DAL.DAL.getEditProduct(page);
                ViewBag.editProducts = Products;
                ViewBag.PPageCount = (int)DAL.DAL.EditPageCount();
            }

            ViewBag.PCurentPage = page;
            return View(Orders);
        }

        public RedirectToRouteResult Status(string id, string status)
        {
            int Id = Convert.ToInt32(id);
            int Status = Convert.ToInt32(status);
            DAL.DAL.changeStatus(Id, Status);
            return RedirectToAction("Index", "Admin", new { tabIndex = 1 });
        }
        public RedirectToRouteResult comments(int id, string coment)
        {
            DAL.DAL.changeComent(id, coment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult addCategory(string cat)
        {
            DAL.DAL.addCategory(cat);
            return RedirectToAction("Index", "Admin", new { tabIndex = 3 });
        }

        public RedirectToRouteResult addProduct(int cat, string name = "", decimal price = 0, string description = "", string img = "", bool active = false)
        {
            DAL.DAL.addProduct(cat, name, price, description, img, active);
            return RedirectToAction("Index", "Admin", new { tabIndex = 2});
        }

        public RedirectToRouteResult delCategory(int? id)
        {
          DAL.DAL.delCategory((int)id);
          return RedirectToAction("Index", "Admin", new { tabIndex = 3 });
        }
        public RedirectToRouteResult editCat(int id)
        {
            return RedirectToAction("Index", "Admin", new { editCat = id, tabIndex = 3 });
        }
        public RedirectToRouteResult editCategory(string id, string name)
        {
            int Id = Convert.ToInt32(id);
            DAL.DAL.editCategory(Id, name);
             return RedirectToAction("Index", "Admin", new {tabIndex = 3});
        }
        public RedirectToRouteResult delProduct(int id)
        {
            DAL.DAL.delProduct(id);
            return RedirectToAction("Index", "Admin", new { tabIndex = 2 });
        }

        public RedirectToRouteResult getEditProductForm(int id)
        {
            return RedirectToAction("Index", "Admin", new { editProd = id, tabIndex = 2 });
        }
        
        [ValidateInput(false)]
        public RedirectToRouteResult editProduct(int id, int categoryId, string img, string name, string description, decimal price, bool active)
        {
           
            DAL.DAL.editProduct(id, categoryId, name, description, price, img, active);
            return RedirectToAction("Index", "Admin", new { tabIndex = 2 });
        }

        public  RedirectToRouteResult characteristic(int id)
        {
            return RedirectToAction("Index", "Admin", new { editProd = id, charact = id, tabIndex = 2 });
        }

        public RedirectToRouteResult editCharacteristics(int id1, int id2, int id3, string strVal1, string strVal2, string strVal3, decimal intVal1, decimal intVal2, decimal intVal3)
        {

             DAL.DAL.editCharacteristics(id1, id2, id3, strVal1, strVal2, strVal3, intVal1, intVal2, intVal3);

             return RedirectToAction("Index", "Admin", new { tabIndex = 2 });
        }

        public RedirectToRouteResult addCharacteristics(int id, string strVal1=null, string strVal2=null, string strVal3=null, decimal? intVal1=null, decimal? intVal2=null, decimal? intVal3=null)
        {
            DAL.DAL.addCharacteristics(id, strVal1, strVal2, strVal3, intVal1, intVal2, intVal3);
            return RedirectToAction("Index", "Admin", new { tabIndex = 2 });
        }

        public RedirectToRouteResult orderFiltr(int status, string startDate, string endDate)
        {
            Session["orderFiltrEnd"] = null;
            Session["orderFiltrStart"] = null;
            Session["orderFiltrStatus"] = null;
            return RedirectToAction("Index", "Admin", new { tabIndex = 1, orderStatus = status, orderStartDate = startDate, orderEndDate = endDate });
        }

        public RedirectToRouteResult productFiltr(int categoryId, string active, string flag)
        {
            Session["productFiltrCategory"] = null;
            Session["productFiltractive"] = null;
            if (flag != "true")
            {
                active = null;
            }
            else if (flag == "true" && active==null) {
                active = "false";
            }

            return RedirectToAction("Index", "Admin", new { tabIndex = 2, categoryFiltr = categoryId, activeFiltr = active });
        }
        public RedirectToRouteResult filtrReset()
        {
            Session["orderFiltrEnd"] = null;
            Session["orderFiltrStart"] = null;
            Session["orderFiltrStatus"] = null;
            return RedirectToAction("Index", "Admin", new { tabIndex = 1 });
        }

        public RedirectToRouteResult productFiltrReset()
        {
            Session["productFiltrCategory"] = null;
            Session["productFiltractive"] = null;
            return RedirectToAction("Index", "Admin", new { tabIndex = 2 });
        }
    }
} 