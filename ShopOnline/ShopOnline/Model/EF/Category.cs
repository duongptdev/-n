namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [Key]
        public long ID { get; set; }

        [StringLength(250)]

        public string Name { get; set; }

        //[StringLength(250)]
        //[Display(Name = "Category_MetaTitle", ResourceType = typeof(StaticResources.Resources))]



        public int? DisplayOrder { get; set; }


        public DateTime? CreatedDate { get; set; }


        public bool? Status { get; set; }

        public string Language { set; get; }
        public long MenuID { get; set; }
 
        public virtual Menu Menu { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
