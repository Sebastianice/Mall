using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.service.mannage
{
    public interface IManageAdminTokenService
    {
        public Task DeleteMallAdminUserToken(string token);
    }
}
