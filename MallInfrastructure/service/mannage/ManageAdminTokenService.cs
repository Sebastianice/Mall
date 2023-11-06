using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.service.mannage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MallInfrastructure.service.mannage
{
    public class ManageAdminTokenService : IManageAdminTokenService
    {
        private readonly MallContext context;
        private readonly IMemoryCache cache;

        public ManageAdminTokenService(MallContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public async Task DeleteMallAdminUserToken(string token)
        {
            var adminToken = await context.MallAdminUserTokens.
                Where(p => p.Token == token).
                SingleOrDefaultAsync();


            if (adminToken == null) throw new Exception("未查询到记录");

            cache.Remove("Admin" + adminToken.AdminUserId);

            context.MallAdminUserTokens.Remove(adminToken);
            await context.SaveChangesAsync();
        }
    }
}
