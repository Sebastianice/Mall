using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mall.response;
using MallDomain.entity.mannage;

namespace MallDomain.service.mall {
     interface IMallGoodsCategoryService {
        public Task<List<NewBeeMallIndexCategoryVO>>GetCategoriesForIndex();

        // 获取分类数据
        public Task<List<MallGoodsCategory>> selectByLevelAndParentIdsAndNumber(int[] ints, int level,int limit);
    }
}
