using MallDomain.entity.common.request;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;

namespace MallDomain.service.manage
{
    public interface IManageIndexConfigService
    {
        // CreateMallIndexConfig 创建MallIndexConfig记录
        public Task CreateMallIndexConfig(IndexConfigAddParams req);

        // DeleteMallIndexConfig 删除MallIndexConfig记录
        public Task DeleteMallIndexConfig(List<long> ids);

        // UpdateMallIndexConfig 更新MallIndexConfig记录
        public Task UpdateMallIndexConfig(IndexConfigUpdateParams req);

        // GetMallIndexConfig 根据id获取MallIndexConfig记录
        public Task<IndexConfig> GetMallIndexConfig(long id);

        // GetMallIndexConfigInfoList 分页获取MallIndexConfig记录
        public Task<(List<IndexConfig>,long)> GetMallIndexConfigInfoList(PageInfo info, sbyte configType);
    }
}
