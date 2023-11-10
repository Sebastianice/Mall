using MallDomain.entity.common.request;
using MallDomain.entity.mall;
using MallDomain.entity.mannage.request;

namespace MallDomain.service.manage
{
    public interface IManageUserService
    {
        public Task LockUser(List<long> ids,sbyte status);

        public Task<(List<User> list,int total)> GetMallUserInfoList(PageInfo search);
    }
}
