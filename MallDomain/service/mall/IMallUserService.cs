using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.entity.mall.response;

namespace MallDomain.service.mall
{
    public interface IMallUserService
    {

        // RegisterUser 注册用户
        public Task RegisterUser(RegisterUserParam req);
        public Task UpdateUserInfo(string token, UpdateUserInfoParam req);

        public Task<UserDetailResponse> GetUserDetail(string token);

        public Task<UserToken> UserLogin(UserLoginParam param);

        public Task<string> getNewToken(long timeInt, long userId);
    }
}
