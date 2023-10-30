using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mall.response {
    public class MallOrderResponse {

    public  long OrderId { get; set; }
       public string?  OrderNo { get; set; }

        public int TotalPrice { get; set; }

        public int PayType { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderStatusString { get; set; }
        public DateTime CreateTime { get; set; }

        public NewBeeMallOrderItemVO[]? NewBeeMallOrderItemVO { get; set; }
    }
    public class NewBeeMallOrderItemVO {
        public long GoodsId { get; set; }
        public int GoodsCount{ get; set; }
        public string? GoodsName{ get; set; }
      
      
        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }

    }
    public class MallOrderDetailVO {
        public string? OrderNo { get; set; }

        public int TotalPrice { get; set; }

        public int PayType { get; set; }
        public string? PayTypeString { get; set; }
        public DateTime PayTime { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderStatusString { get; set; }
        public DateTime CreateTime { get; set; }

        public NewBeeMallOrderItemVO[]? NewBeeMallOrderItemVO { get; set; }
    }
}
