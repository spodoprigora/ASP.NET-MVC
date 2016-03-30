using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.Controllers
{
    public class filtrController : Controller
    {
        // GET: filtr
        public ActionResult Index(int id)
        {
            switch (id){
                case 0:
                    Session["filtr"] = null;
                    Session["target"] = null;
                    break;
                case 1:
                    if (Session["filtr"] == null ){
                        Session["target"] = "down";
                    }
                    else if ((int)Session["filtr"] == 1){
                        if (Session["target"] == "down"){
                            Session["target"] = "up";
                        }
                        else
                        {
                            Session["target"] = "down";
                        }
                    }
                    else
                        Session["target"] = "down";
                                     
                    Session["filtr"] = 1; //цена
                    break;
                case 2:
                    if (Session["filtr"] == null)
                    {
                        Session["target"] = "down";
                    }
                    else if ((int)Session["filtr"] == 2)
                    {
                        if (Session["target"] == "down")
                        {
                            Session["target"] = "up";
                        }
                        else
                        {
                            Session["target"] = "down";
                        }
                    }
                    else
                        Session["target"] = "down";

                    Session["filtr"] = 2;//высота
                    break;
                case 3:
                    if (Session["filtr"] == null)
                    {
                        Session["target"] = "down";
                    }
                    else if ((int)Session["filtr"] == 3)
                    {
                        if (Session["target"] == "down")
                        {
                            Session["target"] = "up";
                        }
                        else
                        {
                            Session["target"] = "down";
                        }
                    }
                    else
                        Session["target"] = "down";
                    Session["filtr"] = 3;//ширина
  
                    break;
                case 4:
                    if (Session["filtr"] == null)
                    {
                        Session["target"] = "down";
                    }
                    else if ((int)Session["filtr"] == 4)
                    {
                        if (Session["target"] == "down")
                        {
                            Session["target"] = "up";
                        }
                        else
                        {
                            Session["target"] = "down";
                        }
                    }
                    else
                        Session["target"] = "down";

                    Session["filtr"] = 4;//глубина
                   break;
                default:
                     Session["filtr"] = null;//сброс
                     Session["target"] = null;
                   break;
            }

            if (Session["category"] != null)
            {
                int cat = (int)Session["category"];
                return RedirectToAction("CategoryProduct", "Home", new { id = cat });
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}