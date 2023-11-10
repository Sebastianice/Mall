using System;
using LinqKit;
using MallDomain.entity.common.enums;
using MallDomain.entity.common.request;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MallInfrastructure.service.mannage
{
    public class ManageGoodsInfoService : IManageGoodsInfoService
    {
        private readonly MallContext context;

        public ManageGoodsInfoService(MallContext context)
        {
            this.context = context;
        }

        public async Task ChangeMallGoodsInfoByIds(List<long> ids, string sellStatus)
        {
            var status = sbyte.Parse(sellStatus);
          
            await  context.GoodsInfos
                .Where(w => ids.Contains(w.GoodsId))
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.GoodsSellStatus, status));


        }

        public async Task CreateMallGoodsInfo(GoodsInfoAddParam req)
        {
            var goodsCategory = await context.GoodsCategories
                  .FirstAsync(w => w.CategoryId == req.GoodsCategoryId);

            if (goodsCategory.CategoryLevel != GoodsCategoryLevel.LevelThree.Code()) throw new Exception("分类数据异常");

            var goodsInfo = new GoodsInfo()
            {
                GoodsName = req.GoodsName ?? "",
                GoodsIntro = req.GoodsIntro ?? "",
                GoodsCategoryId = req.GoodsCategoryId,
                GoodsCoverImg = req.GoodsCoverImg ?? "",
                GoodsDetailContent = req.GoodsDetailContent ?? "",
                OriginalPrice = req.OriginalPrice,
                SellingPrice = req.SellingPrice,
                StockNum = req.StockNum,
                Tag = req.Tag ?? "",
                GoodsSellStatus = sbyte.Parse(req.GoodsSellStatus),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            context.GoodsInfos.Add(goodsInfo);
            await context.SaveChangesAsync();

        }

        public async Task DeleteMallGoodsInfo(GoodsInfo info)
        {

            context.GoodsInfos.Remove(info);
            await context.SaveChangesAsync();
        }

        public async Task<GoodsInfo> GetMallGoodsInfo(long id)
        {
            return await context
                .GoodsInfos
                .SingleOrDefaultAsync(i => i.GoodsId == id) ?? throw new Exception("获取商品信息失败");

        }

        public async Task<(List<GoodsInfo>, long)> GetMallGoodsInfoInfoList(PageInfo pageInfo, string goodsName, string goodsSellStatus)
        {
            var limit = pageInfo.PageSize;

            var offset = limit * (pageInfo.PageNumber - 1);
         var query=   context.GoodsInfos.AsQueryable();
            int total = await query.CountAsync();
            
            var predicate = PredicateBuilder.New<GoodsInfo>(true);
            
            
            if (!string.IsNullOrEmpty(goodsName))
                query= query.Where(i => i.GoodsName == goodsName);

            if (!string.IsNullOrEmpty(goodsSellStatus))
                query = query.Where(i => i.GoodsSellStatus == sbyte.Parse(goodsSellStatus));

            var list =await query
                                  .OrderByDescending(i => i.GoodsId)
                                  .AsNoTracking()
                                  .Skip(offset)
                                  .Take(limit)
                                  .ToListAsync();


            return (list, total);
        }

        public async Task UpdateMallGoodsInfo(GoodsInfoUpdateParam req)
        {
            var goodsInfo = new GoodsInfo()
            {
                GoodsId = req.GoodsId,
                GoodsName = req.GoodsName,
                GoodsIntro = req.GoodsIntro,
                GoodsCategoryId = req.GoodsCategoryId,
                GoodsCoverImg = req.GoodsCoverImg,
                GoodsDetailContent = req.GoodsDetailContent,
                OriginalPrice = req.OriginalPrice,
                SellingPrice = req.SellingPrice,
                StockNum = req.StockNum,
                Tag = req.Tag,
                GoodsSellStatus = (sbyte)req.GoodsSellStatus,
                UpdateTime = DateTime.Now,
            };
            context.GoodsInfos.Update(goodsInfo);
            await context.SaveChangesAsync();
        }
    }
}
