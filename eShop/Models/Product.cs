using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int IdCategory{ get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }


    }
}