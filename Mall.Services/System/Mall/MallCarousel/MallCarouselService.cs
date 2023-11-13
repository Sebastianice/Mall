

using Mall.Repository;
using Mall.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class MallCarouselService
    {
        private readonly MallContext context;
        public MallCarouselService(MallContext mallContext)
        {
            context = mallContext;
        }

        public async Task<List<CarouselIndexResponse>> GetCarouselsForIndex(int num)
        {
            var list = new List<CarouselIndexResponse>();
            var records = await context.Carousels.
                OrderByDescending(p => p.CarouselRank).
                Take(num).
                ToListAsync();

            foreach (var item in records)
            {
                list.Add(item.Adapt<CarouselIndexResponse>());
            }

            return list;
        }
    }
}
