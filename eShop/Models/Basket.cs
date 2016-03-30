using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Models
{
    public class Basket
    {
            public int Id { get; set; }
            public int IdProduct { get; set; }
            public int Count { get; set; }
            public int  OrderNumber { get; set; }

    }
}