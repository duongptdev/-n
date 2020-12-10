using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class OrderDetailDao
    {
        ShopOnlineDbContext db = null;
        public OrderDetailDao()
        {
            db = new ShopOnlineDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetail.Add(detail);

                db.SaveChanges();
                
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
