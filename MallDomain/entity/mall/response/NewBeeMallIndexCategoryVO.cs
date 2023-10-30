using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mall.response {
    public class NewBeeMallIndexCategoryVO {

        public long CategoryId { get; set; }
        public long ParentId { get; set; }
        public int CategoryLevel { get; set; }
        public string? CategoryName { get; set; }
        public List<SecondLevelCategoryVO> SecondLevelCategoryVOS { get;set;}=new List<SecondLevelCategoryVO>();
    }
    public class SecondLevelCategoryVO {
        public long CategoryId { get; set; }
        public long ParentId { get; set; }
        public int CategoryLevel { get; set; }
        public string? CategoryName { get; set; }
        public List<ThirdLevelCategoryVO> ThirdLevelCategoryVOS { get; set; } = new List<ThirdLevelCategoryVO>();
    }

    public class ThirdLevelCategoryVO {
        public long CategoryId { get; set; }
        public int CategoryLevel { get; set; }
        public string? CategoryName { get; set; }
       
    }
}
