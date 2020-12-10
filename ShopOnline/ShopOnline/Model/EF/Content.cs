namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }


        public DateTime? CreatedDate { get; set; }

        public string ShortTitle { get; set; }
    }
}
