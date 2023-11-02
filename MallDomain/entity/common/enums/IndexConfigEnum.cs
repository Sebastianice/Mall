namespace MallDomain.entity.common.enums {
    public enum IndexConfigEnum {

        IndexSearchHots = 1,

        IndexSearchDownHots = 2,

        IndexGoodsHot = 3,

        IndexGoodsNew = 4,

        IndexGoodsRecommond = 5
    }

    public static class IndexConfigEnumExtensions {

        public static int Code(this IndexConfigEnum indexConfigEnum) {
            switch (indexConfigEnum) {
                case IndexConfigEnum.IndexSearchHots:
                    return 1;
                case IndexConfigEnum.IndexSearchDownHots:
                    return 2;
                case IndexConfigEnum.IndexGoodsHot:
                    return 3;

                case IndexConfigEnum.IndexGoodsNew:
                    return 4;

                case IndexConfigEnum.IndexGoodsRecommond:
                    return 5;

                default:
                    return 0;

            }
        }
    }
}
