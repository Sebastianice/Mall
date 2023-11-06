using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallInfrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallDomain.service.mall
{
    public class MallGoodsInfoService : IMallGoodsInfoService
    {
        private readonly MallContext context;

        public MallGoodsInfoService(MallContext mallContext)
        {
            this.context = mallContext;
        }

        public async Task<GoodsInfoDetailResponse> GetMallGoodsInfo(long id)
        {

            var info = await
                context.
                MallGoodsInfos.
                AsNoTracking().
                FirstAsync(p => p.GoodsId == id);


            if (info is null) throw new Exception("商品信息不存在");
            
            if (info.GoodsSellStatus != 0)  throw new Exception("商品已经下架");
            
            var gd = info.Adapt<GoodsInfoDetailResponse>();

            gd.GoodsCarouselList = new List<string> {
                info.GoodsCarousel!
            };

            return gd;
        }

        public async Task<(List<GoodsSearchResponse>, long)> MallGoodsListBySearch(int pageNumber, int goodsCategoryId, string keyword, string orderBy)
        {
            var searchList = new List<GoodsSearchResponse>();

            IQueryable<MallGoodsInfo>? query = context.MallGoodsInfos.AsQueryable();
          
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

            int count = query.Count();

            if (count == 0) throw new Exception("查询失败，未查到数据");
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

            var list = await 
                query.
                Skip(offset).
                Take(limit).
                ToListAsync();

            foreach (var item in list)
            {
                searchList.Add(item.Adapt<GoodsSearchResponse>());
            }

            return (searchList, count);
        }


    }
}
