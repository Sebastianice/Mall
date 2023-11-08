using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mall
{
    public class MallIndexInfoService : IMallIndexInfoService
    {
        private readonly MallContext context;

        public MallIndexInfoService(MallContext mallContext)
        {
            context = mallContext;
        }
        // GetConfigGoodsForIndex 首页返回相关IndexConfig
        public async Task<List<IndexConfigGoodsResponse>> GetConfigGoodsForIndex(int configType, int num)
        {
            List<IndexConfig>? list = await context.IndexConfigs.
                Where(w => w.ConfigType == configType).
                OrderByDescending(o => o.ConfigRank).
                AsNoTracking().
                Take(num).
                ToListAsync();

            if (list.Count > 0)
            {
                List<long> ids = new();

                foreach (var item in list)
                {
                    ids.Add(item.GoodsId);
                }

                // 获取商品信息
                var goodsInfos = await context.GoodsInfos.
                    Where(w => ids.Contains(w.GoodsId)).
                    AsNoTracking().
                    ToListAsync();

                var indexGoodsList = goodsInfos.Adapt<List<IndexConfigGoodsResponse>>();

                return indexGoodsList;
            }

            return new List<IndexConfigGoodsResponse>();
        }
    }
}
