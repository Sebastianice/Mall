using MallDomain.entity.common.request;
using MallDomain.entity.mall;
using MallDomain.entity.mannage.request;

namespace MallDomain.service.manage
{
    public interface IManageUserService
    {
        public Task LockUser(List<long> ids);

        public Task<(List<User> list,int total)> GetMallUserInfoList(UserSearch search);
    }
}
