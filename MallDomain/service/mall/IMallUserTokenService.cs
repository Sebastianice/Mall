using MallDomain.entity.mall;

namespace MallDomain.service.mall {
    public interface IMallUserTokenService {
        public Task<MallUserToken> ExistUserToken(string token);
        public Task DeleteMallUserToken(string token);
    }
}
