using MallDomain.entity.common.request;
using MallDomain.entity.mall;

namespace MallDomain.entity.mannage.request
{
    public class UserSearch
    {
        public User? MallUser { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}
