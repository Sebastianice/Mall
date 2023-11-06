using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;

namespace MallDomain.service.mall
{
    public interface IMallUserService
    {

        // RegisterUser 注册用户
        public Task RegisterUser(RegisterUserParam req);
        public Task<bool> UpdateUserInfo(string token, UpdateUserInfoParam req);

        public Task<MallUserDetailResponse?> GetUserDetail(string token);

        public Task<MallUserToken> UserLogin(UserLoginParam param);

        public Task<string> getNewToken(long timeInt, long userId);
    }
}
