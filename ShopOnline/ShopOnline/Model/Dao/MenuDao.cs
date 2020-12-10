using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        ShopOnlineDbContext db = null;
        public MenuDao()
        {
            db = new ShopOnlineDbContext();
        }

        public List<Menu> ListByGroupId(int groupId)
        {
            var menus =  db.Menu.Where(x => x.TypeID == groupId).OrderBy(x => x.DisplayOrder).ToList();
            menus.ForEach(C =>
            {
                C.Category = db.Category.Where(c => c.MenuID == C.ID).ToList();
                C.Brand=db.Brand.Where(c =>c.MenuID==c.ID).ToList();
            });
            return menus;
        }
    }
}
