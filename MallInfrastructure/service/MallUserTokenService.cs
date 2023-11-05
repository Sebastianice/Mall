using MallDomain.service.mall;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MallInfrastructure.service {
    public class MallUserTokenService : IMallUserTokenService {
        private readonly MallContext context;
        private readonly IMemoryCache cache;

        public MallUserTokenService(IMemoryCache cache, MallContext context) {
            this.cache = cache;
            this.context = context;
        }

        public async Task<bool> DeleteMallUserToken(string token) {
            var userToken = await context.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if(userToken == null) {
                return false;
            }
            cache.Remove("user"+userToken.UserId);
            await context.SaveChangesAsync();
            context.MallUserTokens.Remove(userToken);
            return true;
        }

       
    }
}
