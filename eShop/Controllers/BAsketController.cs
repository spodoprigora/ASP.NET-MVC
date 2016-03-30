using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using eShop.Models;
using eShop.DAL;
using cDal = eShop.DAL.DAL;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web.Helpers;



namespace eShop.Controllers
{
    public class BAsketController : Controller
    {
        // GET: BAsket
        public async Task<ActionResult> Index()
        {

            var user = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
                   .FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                string idUser = user.Id;
                var roleUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(idUser);
                if (roleUser.Count != 0 && roleUser[0] == "admin")
                    return RedirectToAction("Index", "Admin"); //на контроллер Админ
            }

            //вытягиваем из сесии список продуктов
            Dictionary<int, int> basket;
            IEnumerable<KeyValuePair<Product, int>> prod = null;
            decimal summa = 0;
            if (Session["basketSesion"] != null)
            {
                basket = (Dictionary<int, int>)Session["basketSesion"];
                prod = cDal.GetBasketProductFromSesion(basket);
                summa = DAL.DAL.CalcSumma(prod);
                ViewBag.Summa = summa;
            }
            else
            {
                return RedirectToAction("Index", "Home");
                //prod = new List<KeyValuePair<Product, int>>();

            }
            return View(prod);
        }

        public ActionResult dell(int id)
        {
            //вытягиваем из сесии список продуктов
            Dictionary<int, int> basket;
            IEnumerable<KeyValuePair<Product, int>> prod = null;
            decimal summa = 0;
            basket = (Dictionary<int, int>)Session["basketSesion"]; //<id, count>
            if (basket[id] == 1)
                basket.Remove(id);
            else
            {
                int val = basket[id];
                val--;
                basket[id] = val;
            }
            prod = DAL.DAL.GetBasketProductFromSesion(basket);
            summa = DAL.DAL.CalcSumma(prod);
            ViewBag.Summa = summa;
            return View("Index", prod);
           
        }

        //оформленеи заказа запись в БД таблица Basket
       
        [Authorize]
        public async Task<ActionResult> CreateOrder()
        {
            Dictionary<int, int> basket;
            basket = (Dictionary<int, int>)Session["basketSesion"]; //<id, count>
            if (basket!=null)
            {
                //получаем текущего пользователя
                string adminEmail = ConfigurationManager.AppSettings["email"];
                string adminEmailPassword = ConfigurationManager.AppSettings["password"];
                var user = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
                    .FindByNameAsync(User.Identity.Name);
                string idUser = user.Id;
                string userEmail = DAL.DAL.getUserEmail(idUser);

                Basket obj = new Basket();
                int id = DAL.DAL.createOrder(idUser, basket);
                
                foreach (var item in basket)
                {
                    obj.IdProduct = item.Key;
                    obj.Count = item.Value;
                    obj.OrderNumber = id;
                    DAL.DAL.InsertToBasket(obj);
                }

                ViewBag.OrderNumber = id;
                //очищаем корзину
                Session["basketSesion"] = null;
         //отправка письма -----------------------------------------------------
                //SmtpClient Smtp = new SmtpClient("smtp.ukr.net", 465);
                //Smtp.EnableSsl = true;
                //Smtp.Credentials = new NetworkCredential(adminEmail, adminEmailPassword);
                //MailMessage Message = new MailMessage();
                //Message.From = new MailAddress(adminEmail);
                //Message.To.Add(new MailAddress(adminEmail));
                //Message.Subject = "Заказ";
                //Message.Body = "получен новый заказ номер" + id;

                //try
                //{
                //    Smtp.Send(Message);
                //}
                //catch (SmtpException)
                //{

                //}

                //Message = new MailMessage();
                //Message.From = new MailAddress(adminEmail);
                //Message.To.Add(new MailAddress(userEmail));
                //Message.Subject = "Заказ";
                //Message.Body = "Ваш заказ номер" + id + "принят в обработку";

                //try
                //{
                //    Smtp.Send(Message);
                //}
                //catch (SmtpException)
                //{

                //}
    





          

 
                // -------------------------------------------------------
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}