using MallDomain.entity.common.request;
using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;

namespace MallDomain.service.manage
{
    public interface IManageGoodsCategoryService
    {
        public Task AddCategory(GoodsCategoryReq req);

        public Task UpdateCategory(GoodsCategoryReq req);

        public Task<(List<GoodsCategory>, int)> SelectCategoryPage(PageInfo info, int categoryLevel, int parentId);

        // SelectCategoryById 获取单个分类数据
        public Task<GoodsCategory> SelectCategoryById(long categoryId);

        // DeleteGoodsCategoriesByIds 批量设置失效
        public Task DeleteGoodsCategoriesByIds(List<long> ids);
        public Task<List<GoodsCategory>> SelectByLevelAndParentIdsAndNumber(long parentId, long categoryLevel);
    }
}
