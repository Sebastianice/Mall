using MallDomain.entity.mall.response;

namespace MallDomain.service.mall {
    // GetCarouselsForIndex 返回固定数量的轮播图对象(首页调用)
    public interface IMallCarouselService {
        public Task<List<MallCarouselIndexResponse>> GetCarouselsForIndex(int num);

    }
}
