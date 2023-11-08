using MallDomain.entity.common.request;
using MallDomain.entity.mall;

namespace MallDomain.entity.mannage.request
{
    public class MallUserSearch
    {
        public User MallUser { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
