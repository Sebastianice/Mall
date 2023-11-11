using MallDomain.entity.mall.response;

namespace MallDomain.service.mall
{
    public interface IMallGoodsInfoService
    {
        // MallGoodsListBySearch 商品搜索分页
        public Task<(List<GoodsSearchResponse>, long)> MallGoodsListBySearch(int pageNumber, int goodsCategoryId, string? keyword, string? orderBy);

        // GetMallGoodsInfo 获取商品信息
        public Task<GoodsInfoDetailResponse> GetMallGoodsInfo(long id);
    }
}
