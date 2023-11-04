using MallDomain.entity.mall;

namespace MallDomain.service.mall {
    public interface IMallUserTokenService {
   
        public Task<bool> DeleteMallUserToken(string token);
    }
}
