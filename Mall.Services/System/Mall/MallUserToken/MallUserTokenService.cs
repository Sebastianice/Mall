
using Mall.Common.Result;
using Mall.Repository;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class MallUserTokenService
    {
        private readonly MallContext context;
        // private readonly IMemoryCache cache;

        public MallUserTokenService(MallContext context)
        {

            this.context = context;
        }

        public async Task DeleteMallUserToken(string token)
        {
            var userToken = await context.UserTokens.
                Where(p => p.Token == token).
                SingleOrDefaultAsync();


            if (userToken == null) throw ResultException.FailWithMessage("未查询到记录");


            context.UserTokens.Remove(userToken);
            await context.SaveChangesAsync();
        }


    }
}
