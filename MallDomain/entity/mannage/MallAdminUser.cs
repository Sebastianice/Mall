using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.entity.mannage {
    public class MallAdminUser {

        public long AdminUserId { get; set; }
        public string? LoginUserName { get; set; }
        public string? LoginPassword { get; set; }
        public string? NickName { get; set; }
        public bool Locked { get; set; }
    }
}
