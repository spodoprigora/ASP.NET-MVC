using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.Models
{
    public class getOrderList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Orderdate { get; set; }
        public string Status { get; set; }
        public string ChangeStatusDate { get; set; }
        public decimal Price { get; set; }
        public string comments { get; set; }

    }
}