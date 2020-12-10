using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("Brand")]
    public class Brand
    {
        public Brand()
        {
            this.Product = new HashSet<Product>();
        }
        public long ID { get; set; }

        public string Name { get; set; }

        public long MenuID { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
