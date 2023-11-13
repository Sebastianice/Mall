using Mall.Common.Result;
using Mall.Repository;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class ManageAdminTokenService
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


            if (adminToken == null) throw ResultException.FailWithMessage("未查询到记录");


            context.AdminUserTokens.Remove(adminToken);
            await context.SaveChangesAsync();
        }
    }
}
