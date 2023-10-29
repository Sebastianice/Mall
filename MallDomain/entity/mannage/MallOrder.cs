using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mannage {
    public  class MallOrder {
        public long OrderId { get; set; }
        public string? OrderNo { get; set; }
        public long UserId { get; set; }
        public int TotalPrice { get; set; }
        public int PayStatus { get; set; }
        public int PayType { get; set; }
        public DateTime PayTime { get; set; }
        public int OrderStatus { get; set; }
        public string? ExtraInfo { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
