
using Mall.Common.Result;
using Mall.Repository;
using Mall.Repository.Models;
using Mall.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class MallGoodsInfoService
    {
        private readonly MallContext context;

        public MallGoodsInfoService(MallContext mallContext)
        {
            context = mallContext;
        }

        public async Task<GoodsInfoDetailResponse> GetMallGoodsInfo(long id)
        {

            var info = await
                context.
                GoodsInfos.
                AsNoTracking().
                FirstAsync(p => p.GoodsId == id);


            if (info is null) throw ResultException.FailWithMessage("商品信息不存在");

            if (info.GoodsSellStatus != 0) throw ResultException.FailWithMessage("商品已经下架");

            var gd = info.Adapt<GoodsInfoDetailResponse>();

            gd.GoodsCarouselList = new List<string> {
                info.GoodsCarousel!
            };

            return gd;
        }

        public async Task<(List<GoodsSearchResponse>, long)> MallGoodsListBySearch(int pageNumber, int goodsCategoryId, string? keyword, string? orderBy)
        {
            var searchList = new List<GoodsSearchResponse>();

            IQueryable<GoodsInfo>? query = context.GoodsInfos.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p =>
                EF.Functions.Like(p.GoodsName!, $"%{keyword}%")
                || EF.Functions.Like(p.GoodsIntro!, $"%{keyword}%"));



            }

            if (goodsCategoryId >= 0)
            {
                query = query.Where(q => q.GoodsCategoryId == goodsCategoryId);
            }

            int count = await query.CountAsync();

            if (count == 0) throw ResultException.FailWithMessage("查询失败，未查到数据");

            switch (orderBy)
            {
                case "new":
                    query = query.OrderByDescending(q => q.GoodsId);
                    break;
                case "price":
                    query = query.OrderBy(q => q.SellingPrice);
                    break;

                default:
                    query = query.OrderByDescending(q => q.StockNum);
                    break;
            }

            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            int limit = 10;
            int offset = 10 * (pageNumber - 1);

            var list = await query
                .Skip(offset)
               .Take(limit)
               .ToListAsync();

            foreach (var item in list)
            {
                searchList.Add(item.Adapt<GoodsSearchResponse>());
            }

            return (searchList, count);
        }


    }
}
