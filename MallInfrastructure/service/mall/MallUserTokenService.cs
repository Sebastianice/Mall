using MallDomain.service.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MallInfrastructure.service.mall
{
    public class MallUserTokenService : IMallUserTokenService
    {
        private readonly MallContext context;
        private readonly IMemoryCache cache;

        public MallUserTokenService(IMemoryCache cache, MallContext context)
        {
            this.cache = cache;
            this.context = context;
        }

        public async Task DeleteMallUserToken(string token)
        {
            var userToken = await context.MallUserTokens.
                Where(p => p.Token == token).
                SingleOrDefaultAsync();


            if (userToken == null) throw new Exception("未查询到记录");

            cache.Remove("user" + userToken.UserId);

            context.MallUserTokens.Remove(userToken);
            await context.SaveChangesAsync();
        }


    }
}
