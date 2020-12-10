using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
  public  class ContactDao
    {
        ShopOnlineDbContext db = null;
        public ContactDao()
        {
            db = new ShopOnlineDbContext();
        }
        public Contact GetActiveContact()
        {
            return db.Contact.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedback.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
