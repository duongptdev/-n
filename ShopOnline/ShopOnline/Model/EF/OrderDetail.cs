namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        
        public int IdCTDDH { get; set; }
        
        public long ProductID { get; set; }

        
        public long OrderID { get; set; }


        public int? Quantity { get; set; }
        public virtual Order Order { get; set; }
        public decimal? Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
