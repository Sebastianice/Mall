namespace MallDomain.entity.common.enums
{
    public enum GoodsCategoryLevel
    {
        Default = 0,
        LevelOne = 1,
        LevelTwo = 2,
        LevelThree = 3
    }

    public static class GoodsCategoryLevelExtensions
    {
        public static (int, string) Info(this GoodsCategoryLevel g)
        {
            switch (g)
            {
                case GoodsCategoryLevel.LevelOne:
                    return (1, "一级分类");
                case GoodsCategoryLevel.LevelTwo:
                    return (2, "二级分类");
                case GoodsCategoryLevel.LevelThree:
                    return (3, "三级分类");
                default:
                    return (0, "error");
            }
        }

        public static int Code(this GoodsCategoryLevel g)
        {
            switch (g)
            {
                case GoodsCategoryLevel.LevelOne:
                    return 1;
                case GoodsCategoryLevel.LevelTwo:
                    return 2;
                case GoodsCategoryLevel.LevelThree:
                    return 3;
                default:
                    return 0;
            }
        }
    }

}
