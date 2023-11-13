
using Mall.Common.Result;
using Mall.Repository;
using Mall.Repository.Models;
using Mall.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Mall.Services
{
    public class ManageGoodsCategoryService
    {
        private readonly MallContext context;

        public ManageGoodsCategoryService(MallContext context)
        {
            this.context = context;
        }

        public async Task AddCategory(GoodsCategoryReq req)
        {
            var goodsCategory = await context.GoodsCategories
                  .SingleOrDefaultAsync(w => w.CategoryLevel == req.CategoryLevel && w.CategoryName == req.CategoryName);

            if (goodsCategory != null) throw ResultException.FailWithMessage("存在相同分类");


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
                .SingleOrDefaultAsync(i => i.CategoryId == categoryId)
                ?? throw ResultException.FailWithMessage("未查询到记录");
        }
        // SelectCategoryPage 获取分类分页数据
        public async Task<(List<GoodsCategory>, int)> SelectCategoryPage(PageInfo info, int categoryLevel, int parentId)
        {
            var limit = info.PageSize;
            if (limit > 1000) limit = 1000;

            var offset = limit * (info.PageNumber - 1);
            var query = context.GoodsCategories.AsQueryable();

            if (new List<int>() { 1, 2, 3 }.Contains(categoryLevel))
                query = query.Where(i => i.CategoryLevel == categoryLevel);

            if (parentId >= 0)
                query = query.Where(i => i.ParentId == parentId);

            int total = await query.CountAsync();

            var list = query.AsNoTracking()
                            .Skip(offset)
                            .Take(limit)
                            .OrderByDescending(c => c.CategoryRank)
                            .ToList();

            return (list, total);
        }

        public async Task UpdateCategory(GoodsCategoryReq req)
        {
            var goodsCategory = await context.GoodsCategories
                  .SingleOrDefaultAsync(w =>
                  w.CategoryLevel == req.CategoryLevel
                  &&
                  w.CategoryId == req.CategoryId);

            if (goodsCategory == null) throw ResultException.FailWithMessage("不存存在分类");

            goodsCategory.CategoryName = req.CategoryName ?? goodsCategory.CategoryName;
            goodsCategory.CategoryRank = req.CategoryRank;
            goodsCategory.UpdateTime = DateTime.Now;
            await context.SaveChangesAsync();

        }
    }
}
