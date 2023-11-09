using System;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MallInfrastructure.service.mannage
{
    public class ManageIndexConfigService : IManageIndexConfigService
    {
        private readonly MallContext context;

        public ManageIndexConfigService(MallContext context)
        {
            this.context = context;
        }

        public async Task CreateMallIndexConfig(IndexConfigAddParams req)
        {
            var info = context.GoodsInfos
                    .FirstOrDefaultAsync(i => i.GoodsId == req.GoodsId);
            if (info == null) throw new Exception("商品不存在");

            var config = await context.IndexConfigs
                    .FirstOrDefaultAsync(w =>
                    w.ConfigType == req.ConfigType
                    &&
                    w.GoodsId == req.GoodsId);

            if (config != null) throw new Exception("已存在相同配置");

            var indexconfig = new IndexConfig()
            {
                ConfigName = req.ConfigName,
                ConfigType = req.ConfigType,
                GoodsId = req.GoodsId,
                RedirectUrl = req.RedirectUrl,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,

            };

            context.IndexConfigs.Add(indexconfig);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMallIndexConfig(List<long> ids)
        {

            await context.IndexConfigs
                  .Where(w => ids.Contains(w.ConfigId))
                  .ExecuteDeleteAsync();
        }

        public async Task<IndexConfig> GetMallIndexConfig(long id)
        {
            return await context.IndexConfigs
                .FirstOrDefaultAsync(w => w.ConfigId == id)
                ?? throw new Exception("获取首页配置失败");
        }

        public Task<List<IndexConfig>> GetMallIndexConfigInfoList(IndexConfigSearch info)
        {
            ///TODO
            throw new NotImplementedException();
        }

        public async Task UpdateMallIndexConfig(IndexConfigUpdateParams req)
        {
            var info = context.GoodsInfos
                   .FirstOrDefaultAsync(i => i.GoodsId == req.GoodsId);
            if (info == null) throw new Exception("商品不存在");

            var config = await context.IndexConfigs
                    .FirstOrDefaultAsync(w => w.ConfigId == req.ConfigId);

            if (config == null) throw new Exception("未查询到记录");

            var oldConfig = await context.IndexConfigs
                   .FirstAsync(w =>
                   w.GoodsId == req.GoodsId
                   &&
                   w.ConfigType == req.ConfigType
                   &&
                   w.ConfigId == req.ConfigId
                   );

            if (oldConfig != null) throw new Exception("已存在相同配置");

            var indexConfig = new IndexConfig()
            {
                ConfigId = req.ConfigId,
                ConfigType = req.ConfigType,
                ConfigName = req.ConfigName,
                RedirectUrl = req.RedirectUrl,
                GoodsId = req.GoodsId,
                ConfigRank = req.ConfigRank,
                UpdateTime = DateTime.Now
            };

            context.IndexConfigs.Add(indexConfig);
            await context.SaveChangesAsync();
            
        }
    }
}
