namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Reflection;

    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        public long ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        
        public long UserID { get; set; }
        [StringLength(50)]
        public string ShipName { get; set; }
        public decimal? Price { get; set; }
        [StringLength(50)]
        public string ShipMobile { get; set; }

        [StringLength(50)]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        public string ShipEmail { get; set; }
        public string NameProduct { get; set; }

        public Nullable<bool> DaHuy { get; set; }
        public Nullable<bool> DaXoa { get; set; }
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public string GetStringDaHuy()
        {
            if (this.DaHuy == true)
            {

                return "Đã hủy";
            }
            else
            {
                return "Chưa hủy";
            }


        }
        public string GetStringTinhTrangGiaoHang()
        {
            if (this.TinhTrangGiaoHang == true)
            {

                return "Đã giao";
            }
            else
            {
                return "Chưa giao";
            }
        }
        public string GetStringDaThanhToan()
        {
            if (this.DaThanhToan == true)
            {

                return "Đã thanh toán";
            }
            else
            {
                return "Chưa thanh toán";
            }
        }
       
    }
}
