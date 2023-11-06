namespace MallDomain.entity.mall.response
{
    public class NewBeeMallIndexCategoryVO
    {

        public long CategoryId { get; set; }
        public long ParentId { get; set; }
        public int CategoryLevel { get; set; }
        public string? CategoryName { get; set; }
        public List<SecondLevelCategoryVO>? SecondLevelCategoryVOS { get; set; }
    }
    public class SecondLevelCategoryVO
    {
        public long CategoryId { get; set; }
        public long ParentId { get; set; }
        public int CategoryLevel { get; set; }
        public string? CategoryName { get; set; }
        public List<ThirdLevelCategoryVO>? ThirdLevelCategoryVOS { get; set; }

    }

    public class ThirdLevelCategoryVO
    {
        public long CategoryId { get; set; }
        public int CategoryLevel { get; set; }
        public string? CategoryName { get; set; }

    }
}
