using MallDomain.entity.mall.response;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mall
{
    public class MallCarouselService : IMallCarouselService
    {
        private readonly MallContext context;
        public MallCarouselService(MallContext mallContext)
        {
            context = mallContext;
        }

        public async Task<List<MallCarouselIndexResponse>> GetCarouselsForIndex(int num)
        {
            var list = new List<MallCarouselIndexResponse>();
            var records = await context.MallCarousels.
                OrderByDescending(p => p.CarouselRank).
                Take(num).
                ToListAsync();

            foreach (var item in records)
            {
                list.Add(item.Adapt<MallCarouselIndexResponse>());
            }

            return list;
        }
    }
}
