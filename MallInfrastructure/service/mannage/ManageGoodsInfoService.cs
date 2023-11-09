using System;
using MallDomain.entity.common.enums;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using Microsoft.EntityFrameworkCore;
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

        public Task<(List<GoodsInfo>, long)> GetMallGoodsInfoInfoList(GoodsInfoSearch info, string goodsName, string goodsSellStatus)
        {
            ///TODO
            throw new NotImplementedException();
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
