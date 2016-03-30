using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.Controllers
{
    public class AddToBasketController : Controller
    {
        // GET: AddToBasket
 //     [Authorize]
        public RedirectResult  Index(int id)
        {
            Dictionary<int, int> basket; 
            if(Session["basketSesion"]==null)
            {
                basket = new Dictionary<int, int>();
                basket.Add(id, 1);
                Session["basketSesion"] = basket;
            }
            else
            {
                basket = (Dictionary<int, int>)Session["basketSesion"];
                if (basket.ContainsKey(id))
                {
                    int count = basket[id];
                    count++;
                    basket[id] = count;
                }else
                {
                    basket.Add(id, 1);
                    Session["basketSesion"] = basket;
                }
           }
           return Redirect("/");
       }
    }
}