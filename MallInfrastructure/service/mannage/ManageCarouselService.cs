using MallDomain.entity.common.request;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mannage
{
    public class ManageCarouselService : IManageCarouselService
    {
        private readonly MallContext context;

        public ManageCarouselService(MallContext context)
        {
            this.context = context;
        }

        public async Task CreateCarousel(CarouselAddParam req)
        {
            var carouserl = req.Adapt<Carousel>();
            carouserl.UpdateTime = DateTime.Now;
            carouserl.CreateTime = DateTime.Now;

            context.Carousels.Add(carouserl);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCarousel(List<long> ids)
        {
            //ef core新特性

            await context.Carousels
                 .Where(w => ids.Contains(w.CarouselId))
                 .ExecuteUpdateAsync
                 (p => p.SetProperty(f => f.IsDeleted, 1));

        }

        public async Task<Carousel> GetCarousel(long id)
        {
            var carousel = await context
                .Carousels
                .SingleOrDefaultAsync(s => s.CarouselId == id);

            if (carousel == null) throw new Exception("未找到轮播图，轮播图不存在");

            return carousel;
        }

        public async Task<(List<Carousel>, int)> GetCarouselInfoList(PageInfo sec)
        {

            int limit = sec.PageSize;
            int offset = limit * (sec.PageNumber - 1);


            int total = await context.Carousels.CountAsync();
            var list = await context.Carousels
                .OrderByDescending(u => u.CarouselRank)
                .Skip(offset)
                .Take(limit)
                .AsNoTracking()
                .ToListAsync();


            return (list, total);
        }

        public async Task UpdateCarousel(CarouselUpdateParam req)
        {
            var carouserl = await context.Carousels
                   .SingleOrDefaultAsync(q => q.CarouselId == req.CarouselId);


            if (carouserl is null) throw new Exception("轮播图不存在，无法更新");

            carouserl.CarouselRank = req.CarouselRank ?? carouserl.CarouselRank;
            carouserl.CarouselUrl = req.CarouselUrl ?? carouserl.CarouselUrl;
            carouserl.RedirectUrl = req.RedirectUrl ?? carouserl.RedirectUrl;

            await context.SaveChangesAsync();

        }
    }
}
