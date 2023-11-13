namespace Mall.Repository.Enums
{
    public enum GoodsStatusEnum
    {
        GOODS_DEFAULT = -9,//错误

        GOODS_UNDER = 1//已下架
    }

    public static class GoodsStatusEnumExtension
    {


        public static int Code(this GoodsStatusEnum enm)
        {
            switch (enm)
            {
                case GoodsStatusEnum.GOODS_UNDER:
                    return 1;
                default:
                    return -9;
            }
        }
    }
}
