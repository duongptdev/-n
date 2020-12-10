namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public Menu()
        {
            this.Brand = new HashSet<Brand>();
            this.Category = new HashSet<Category>();
        }
        public long ID { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(250)]
        public string Link { get; set; }
        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Target { get; set; }
        public int? TypeID { get; set; }

        public bool? Status { get; set; }
        public virtual ICollection<Brand> Brand { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}