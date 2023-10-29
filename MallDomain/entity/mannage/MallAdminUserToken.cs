using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mannage {
    public class MallAdminUserToken {
        
       public long AdminUserId { get; set; }
        public string? Token { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ExpireTime { get; set; }



    
    }
}
