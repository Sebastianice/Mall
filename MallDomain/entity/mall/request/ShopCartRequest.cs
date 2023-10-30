namespace MallDomain.entity.mall.request {
    public class SaveCartItemParam {

        public int GoodsCount{ get; set; }
        public int intGoodsId{ get; set; }
    }

   public class UpdateCartItemParam {

        public int CartItemId{ get; set; }
        public int GoodsCount{ get; set; }
    }
}
