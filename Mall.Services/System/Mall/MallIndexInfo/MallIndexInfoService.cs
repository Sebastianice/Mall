
using Mall.Repository;
using Mall.Repository.Models;
using Mall.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class MallIndexInfoService
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
