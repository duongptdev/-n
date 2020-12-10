using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class Helmets
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
         public bool? Status { get; set; }
        public string Description { get; set; }

        public string MenuID { get; set; }
    }
}