using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
   public class OrderDetailEntity
    {
        public int IdCTDDH { get; set; }

        public long ProductID { get; set; }


        public long OrderID { get; set; }


        public int? Quantity { get; set; }
        public virtual Order Order { get; set; }
        public decimal? Price { get; set; }
        public virtual Product Product { get; set; }
        public OrderDetail TypeOf_Order()
        {
            OrderDetail order = new OrderDetail();
            PropertyInfo[] pithis = typeof(OrderDetailEntity).GetProperties();
            PropertyInfo[] pieClinet = typeof(OrderDetail).GetProperties();
            foreach (var items in pithis)
            {
                foreach (var itempiem in pieClinet)
                {
                    if (itempiem.Name == items.Name)
                    {
                        itempiem.SetValue(order, items.GetValue(this));
                        break;
                    }
                }
            }
            return order;
        }

        // convert tu model sang view

        public void TypeOf_OrderEntity(OrderDetail order)
        {

            PropertyInfo[] pithis = typeof(OrderDetailEntity).GetProperties();
            PropertyInfo[] pieClinet = typeof(OrderDetail).GetProperties();
            foreach (var items in pithis)
            {
                foreach (var itempiem in pieClinet)
                {
                    if (itempiem.Name == items.Name)
                    {
                        items.SetValue(this, itempiem.GetValue(order));
                        break;
                    }
                }
            }

        }
        //
        public OrderDetailEntity(OrderDetail order)
        {
            TypeOf_OrderEntity(order);

        }
        public OrderDetailEntity()
        {


        }
    }
}
