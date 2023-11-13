using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Common.Components.Authenrization.Jwt
{
    public class JwtTokenModel
    {
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string UserRole { get; set; } = null!;

        public DateTime? Exp { get; set; } = null;
    }
}
