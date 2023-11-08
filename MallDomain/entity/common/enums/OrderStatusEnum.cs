namespace MallDomain.entity.common.enums
{
    public enum OrderStatusEnum
    {
        DEFAULT = -9,
        ORDER_PRE_PAY = 0,
        ORDER_PAID = 1,
        ORDER_PACKAGED = 2,
        ORDER_EXPRESS = 3,
        ORDER_SUCCESS = 4,
        ORDER_CLOSED_BY_MALLUSER = -1,
        ORDER_CLOSED_BY_EXPIRED = -2,
        ORDER_CLOSED_BY_JUDGE = -3
    }

    public static class MallOrderStatusExtensions
    {
        public static string GetNewBeeMallOrderStatusEnumByStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return "待支付";
                case 1:
                    return "已支付";
                case 2:
                    return "配货完成";
                case 3:
                    return "出库成功)";
                case 4:
                    return "交易成功";
                case -1:
                    return "手动关闭";
                case -2:
                    return "超时关闭";
                case -3:
                    return "商家关闭";
                default:
                    return "error";
            }
        }

        public static sbyte Code(this OrderStatusEnum status)
        {
            switch (status)
            {
                case OrderStatusEnum.ORDER_PRE_PAY:
                    return 0;
                case OrderStatusEnum.ORDER_PAID:
                    return 1;
                case OrderStatusEnum.ORDER_PACKAGED:
                    return 2;
                case OrderStatusEnum.ORDER_EXPRESS:
                    return 3;
                case OrderStatusEnum.ORDER_SUCCESS:
                    return 4;
                case OrderStatusEnum.ORDER_CLOSED_BY_MALLUSER:
                    return -1;
                case OrderStatusEnum.ORDER_CLOSED_BY_EXPIRED:
                    return -2;
                case OrderStatusEnum.ORDER_CLOSED_BY_JUDGE:
                    return -3;
                default:
                    return -9;
            }
        }
    }

}
