using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Models
{
    public class editProduct
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool active { get; set; }
    }
}