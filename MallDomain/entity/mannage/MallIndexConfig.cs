using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mannage {
    public class MallIndexConfig {
        public long ConfigId { get; set; }
        public string? ConfigName { get; set; }
        public int ConfigType { get; set; }
        public long GoodsId { get; set; }
        public string? RedirectUrl { get; set; }
        public int ConfigRank { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateUser { get; set; }
    }
}
