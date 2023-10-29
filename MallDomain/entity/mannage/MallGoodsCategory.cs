using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mannage {
    public class MallGoodsCategory {
        public long CategoryId { get; set; }
        public int CategoryLevel { get; set; }
        public long ParentId { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryRank { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
