using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.common.request;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;

namespace MallDomain.service.manage
{
    public interface IManageCarouselService
    {
        public Task CreateCarousel(CarouselAddParam req);
        public Task DeleteCarousel(List<long> ids);
        public Task UpdateCarousel(CarouselUpdateParam req);
        public Task<Carousel> GetCarousel(long id);
        public Task<(List<Carousel>,int)> GetCarouselInfoList(PageInfo sec);
    }
}
