using MallDomain.entity.common.enums;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service
{
    public class MallGoodsCategoryService : IMallGoodsCategoryService
    {
        private readonly MallContext context;

        public MallGoodsCategoryService(MallContext mallContext)
        {
            this.context = mallContext;
        }

        public async Task<List<NewBeeMallIndexCategoryVO>> GetCategoriesForIndex()
        {

          //获取一级分类的固定数量的数据
            var firstLevelCategories =
                await selectByLevelAndParentIdsAndNumber
                (new List<long> { 0 },
                GoodsCategoryLevel.LevelOne.Code(),
                10);


            List<NewBeeMallIndexCategoryVO>? NewBeeMallIndexCategoryVOS = new List<NewBeeMallIndexCategoryVO>();

            if (firstLevelCategories.Count != 0)
            {
                var firstLevelIds = new List<long>();

                foreach (var goodsCategory in firstLevelCategories)
                {
                    firstLevelIds.Add(goodsCategory.CategoryId);
                }

                //获取二级分类的数据
                var secondLevelCategories = await selectByLevelAndParentIdsAndNumber(
                    firstLevelIds, 
                    GoodsCategoryLevel.LevelTwo.Code(),
                    10);

                if (secondLevelCategories.Count != 0)
                {
                    var secondLevelIds = new List<long>();
                    foreach (var goodsCategory in secondLevelCategories)
                    {
                        secondLevelIds.Add(goodsCategory.CategoryId);
                    }

                    //获取三级分类的数据
                    var thirdLevelCategories = await selectByLevelAndParentIdsAndNumber(
                        secondLevelIds, 
                        GoodsCategoryLevel.LevelThree.Code(),
                        10);
                  
                    if (thirdLevelCategories.Count != 0)
                    {
                        ////根据 parentId 将 thirdLevelCategories 分组
                        var thirdLevelCategoryMap = new Dictionary<long, List<MallGoodsCategory>>();

                        //遍历出parentid分组
                        foreach (var thirdLevelCategory in thirdLevelCategories)
                        {
                            thirdLevelCategoryMap[thirdLevelCategory.ParentId] = new    List<MallGoodsCategory>();
                        
                        }

                        foreach (var mapk in thirdLevelCategoryMap)
                        {
                            foreach (var v in thirdLevelCategories)
                            {
                                if (v.ParentId == mapk.Key)
                                {
                                    mapk.Value.Add(v);
                                }

                            }
                        }

                        var secondLevelCategoryVOS = new List<SecondLevelCategoryVO>();
                        //处理二级分类
                        foreach (MallGoodsCategory cg in secondLevelCategories)
                        {
                            SecondLevelCategoryVO? secondLevelCategoryVO = cg.Adapt<SecondLevelCategoryVO>();
                            //如果该二级分类下有数据则放入 secondLevelCategoryVOS 对象中
                            if (thirdLevelCategoryMap.ContainsKey(cg.CategoryId))
                            {
                                //根据二级分类的id取出thirdLevelCategoryMap分组中的三级分类list
                                var thirdLevelCategoryRes =
                                thirdLevelCategoryMap[cg.CategoryId].Adapt<List<ThirdLevelCategoryVO>>();
                                secondLevelCategoryVO.ThirdLevelCategoryVOS = thirdLevelCategoryRes;

                                secondLevelCategoryVOS.Add(secondLevelCategoryVO);
                            }
                        }

                        //处理一级分类
                        if (secondLevelCategoryVOS.Count > 0)
                        {
                            //根据 parentId 将 secondLevelCategories 分组
                            var secondLevelCategoryMap = new Dictionary<long, List<SecondLevelCategoryVO>>();
                            foreach (var secondLevelCategoryVO in secondLevelCategoryVOS)
                            {
                                secondLevelCategoryMap[secondLevelCategoryVO.ParentId] = new List<SecondLevelCategoryVO>();

                            }

                            foreach (KeyValuePair<long, List<SecondLevelCategoryVO>> list in secondLevelCategoryMap)
                            {
                                foreach (var sec in secondLevelCategoryVOS)
                                {
                                    if (sec.ParentId == list.Key)
                                    {

                                        list.Value.Add(sec);
                                    }
                                }
                            }
                            foreach (var item in firstLevelCategories)
                            {
                                var first = item.Adapt<NewBeeMallIndexCategoryVO>();
                                if (secondLevelCategoryMap.TryGetValue(
                                    item.CategoryId, out var v)  && v.Count > 0)
                                {
                                    first.SecondLevelCategoryVOS =v.Adapt<List<SecondLevelCategoryVO>>();
                                    NewBeeMallIndexCategoryVOS.Add(first);
                                }

                            }
                        }
                    }
                }
                return NewBeeMallIndexCategoryVOS;
            }
            throw new Exception("查询失败");
          
        }



        async Task<List<MallGoodsCategory>> selectByLevelAndParentIdsAndNumber(List<long> ids, int level, int limit)
        {
            return await context.MallGoodsCategories.Where(p => ids.Contains(p.ParentId)).Where(p => p.CategoryLevel == level)
                .OrderByDescending(p => p.CategoryRank)
                .Take(limit)
                .ToListAsync();
        }

    }



}
