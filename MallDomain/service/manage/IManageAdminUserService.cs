using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;

namespace MallDomain.service.mannage
{
    public interface IManageAdminUserService
    {
        public Task CreateMallAdminUser(MallAdminUser mallAdminUser);

        public Task UpdateMallAdminName(string token,MallUpdateNameParam param);

        public Task UpdateMallAdminPassWord(string token,MallUpdatePasswordParam param);

        public Task<MallAdminUser> GetMallAdminUser(string token);

        // AdminLogin 管理员登陆
        public Task<MallAdminUserToken> AdminLogin(MallAdminLoginParam param);
    }
}
