using MallDomain.entity.mannage;
using MallDomain.entity.mannage.request;
using MallDomain.service.manage;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mannage
{
    public class ManageGoodsCategoryService : IManageGoodsCategoryService
    {
        private readonly MallContext context;

        public ManageGoodsCategoryService(MallContext context)
        {
            this.context = context;
        }

        public async Task AddCategory(GoodsCategoryReq req)
        {
            var goodsCategory = await context.GoodsCategories
                 .SingleOrDefaultAsync(w => w.CategoryLevel == req.CategoryLevel);

            if (goodsCategory != null) throw new Exception("存在相同分类");


            goodsCategory = new GoodsCategory()
            {
                CategoryLevel = (sbyte)req.CategoryLevel,
                CategoryName = req.CategoryName!,
                CategoryRank = req.CategoryRank,
                IsDeleted = 0,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,

            };
            context.GoodsCategories.Add(goodsCategory);
            await context.SaveChangesAsync();

        }

        public async Task DeleteGoodsCategoriesByIds(List<long> ids)
        {

            await context.GoodsCategories
                .Where(g => ids.Contains(g.CategoryId))
                .ExecuteUpdateAsync(p =>
                p.SetProperty(s => s.IsDeleted, 1));


        }

        public async Task<List<GoodsCategory>> SelectByLevelAndParentIdsAndNumber(long parentId, long categoryLevel)
        {
            return await context.GoodsCategories
                   .Where(w => w.ParentId == parentId && w.CategoryLevel == categoryLevel)
                   .AsNoTracking()
                   .ToListAsync();
        }

        public async Task<GoodsCategory> SelectCategoryById(long categoryId)
        {
            return await context.GoodsCategories
                .SingleOrDefaultAsync(i=>i.CategoryId==categoryId) 
                ?? throw new Exception("未查询到记录");
        }

        public Task<(List<GoodsCategory>, int)> SelectCategoryPage(SearchCategoryParams req)
        {

            ///TODO
            throw new NotImplementedException();
        }

        public async Task UpdateCategory(GoodsCategoryReq req)
        {
            var goodsCategory = await context.GoodsCategories
                  .SingleOrDefaultAsync(w => w.CategoryLevel == req.CategoryLevel);

            if (goodsCategory == null) throw new Exception("不存存在分类");

            goodsCategory.CategoryName = req.CategoryName ?? goodsCategory.CategoryName;
            goodsCategory.CategoryRank = req.CategoryRank;
            goodsCategory.UpdateTime = DateTime.Now;
            await context.SaveChangesAsync();

        }
    }
}
