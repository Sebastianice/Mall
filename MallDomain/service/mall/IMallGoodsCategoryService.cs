using MallDomain.entity.mall.response;

namespace MallDomain.service.mall
{
    public interface IMallGoodsCategoryService
    {
        public Task<List<NewBeeMallIndexCategoryVO>> GetCategoriesForIndex();


    }
}
