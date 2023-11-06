﻿using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service
{
    public class MallIndexInfoService : IMallIndexInfoService
    {
        private readonly MallContext context;

        public MallIndexInfoService(MallContext mallContext)
        {
            this.context = mallContext;
        }
        // GetConfigGoodsForIndex 首页返回相关IndexConfig
        public async Task<List<MallIndexConfigGoodsResponse>> GetConfigGoodsForIndex(int configType, int num)
        {
            List<MallIndexConfig>? list = await context.MallIndexConfigs.
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
                var goodsInfos = await context.MallGoodsInfos.
                    Where(w => ids.Contains(w.GoodsId)).
                    AsNoTracking().
                    ToListAsync();

                var indexGoodsList = goodsInfos.Adapt<List<MallIndexConfigGoodsResponse>>();

                return indexGoodsList;
            }

            return new List<MallIndexConfigGoodsResponse>();
        }
    }
}
