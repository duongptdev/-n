using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class CartItem
    {
        ShopOnlineDbContext data = new ShopOnlineDbContext();
        public string Image { get; set; }
        public long OrderID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int TotalMoney
        {
            get
            {
                return Price * Quantity;
            }
        }

    }
}