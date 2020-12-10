using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class BestSellModel
    {
        public long ID { get; set; }
        public string Image { get; set; }
        public string NameProduct { get; set; }
        public decimal? Price { get; set; }
        public int? Waranty { get; set; }
}
}