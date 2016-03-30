using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Models
{
    public class GlobalModel
    {
        public List<CategoryModel> Categorys { get; set; }
        public List<Product> Products { get; set; }
    }
}