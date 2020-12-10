﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        ShopOnlineDbContext db = null;
        public ProductCategoryDao()
        {
            db = new ShopOnlineDbContext();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategory.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategory.Find(id);
        }
    }
}
