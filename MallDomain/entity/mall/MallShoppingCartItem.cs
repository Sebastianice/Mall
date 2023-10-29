using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mall {
    public class MallShoppingCartItem {

        public long CartItemId {  get; set; }
        public long UserId {  get; set; }
        public long GoodsId { get; set; }
        public int GoodsCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
