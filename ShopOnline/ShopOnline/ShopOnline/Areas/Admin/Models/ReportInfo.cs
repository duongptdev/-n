using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Areas.Admin.Models
{
    public class ReportInfo
    {
        public int iGroup { get; set; }
        public String Group { get; set; }
        public int? Count { get; set; }
        public String NameProduct { get; set; }
        public decimal? Sum { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? Avg { get; set; }
    }
}