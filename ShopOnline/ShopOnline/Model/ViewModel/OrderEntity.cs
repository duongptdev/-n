using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
   public class OrderEntity
    {

        public long ID { get; set; }

        public DateTime? CreatedDate { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }

        public long UserID { get; set; }

        public string ShipName { get; set; }
        public decimal? Price { get; set; }

        public string ShipMobile { get; set; }


        public string ShipAddress { get; set; }


        public string ShipEmail { get; set; }

        public Nullable<bool> DaHuy { get; set; }
        public Nullable<bool> DaXoa { get; set; }
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public Order TypeOf_Order()
        {
            Order order = new Order();
            PropertyInfo[] pithis = typeof(OrderEntity).GetProperties();
            PropertyInfo[] pieClinet = typeof(Order).GetProperties();
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

        public void TypeOf_OrderEntity(Order order)
        {

            PropertyInfo[] pithis = typeof(OrderEntity).GetProperties();
            PropertyInfo[] pieClinet = typeof(Order).GetProperties();
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
        public OrderEntity(Order order)
        {
            TypeOf_OrderEntity(order);

        }
        public OrderEntity()
        {


        }
    }
}
