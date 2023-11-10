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
    public interface IManageGoodsInfoService
    {
        // CreateMallGoodsInfo 创建MallGoodsInfo
        public Task CreateMallGoodsInfo(GoodsInfoAddParam req);

        // DeleteMallGoodsInfo MallGoodsInfo记录
        public Task DeleteMallGoodsInfo(GoodsInfo info);
        // ChangeMallGoodsInfoByIds 上下架
        public Task ChangeMallGoodsInfoByIds(List<long> ids, string sellStatus);

        // UpdateMallGoodsInfo 更新MallGoodsInfo记录
        public Task UpdateMallGoodsInfo(GoodsInfoUpdateParam req);

        // GetMallGoodsInfo 根据id获取MallGoodsInfo记录
        public Task<GoodsInfo> GetMallGoodsInfo(long id);

        // GetMallGoodsInfoInfoList 分页获取MallGoodsInfo记录
        public Task<(List<GoodsInfo>, long)> GetMallGoodsInfoInfoList(PageInfo pageInfo, string goodsName, string goodsSellStatus);

    }
}
