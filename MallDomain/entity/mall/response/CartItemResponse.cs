using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mall.response {
    public  class CartItemResponse {
        public long CartItemId { get; set; }
        public long GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public int GoodsCount { get; set; }
        public string? GoodsCoverImg { get; set; }
        public int SellingPrice { get; set; }

       
    }
}
