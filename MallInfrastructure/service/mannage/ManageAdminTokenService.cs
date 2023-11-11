using MallDomain.service.mannage;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mannage
{
    public class ManageAdminTokenService : IManageAdminTokenService
    {
        private readonly MallContext context;
        //private readonly IMemoryCache cache;

        public ManageAdminTokenService(MallContext context)
        {
            this.context = context;

        }

        public async Task DeleteMallAdminUserToken(string token)
        {
            var adminToken = await context.AdminUserTokens.
                Where(p => p.Token == token).
                SingleOrDefaultAsync();


            if (adminToken == null) throw new Exception("未查询到记录");


            context.AdminUserTokens.Remove(adminToken);
            await context.SaveChangesAsync();
        }
    }
}
