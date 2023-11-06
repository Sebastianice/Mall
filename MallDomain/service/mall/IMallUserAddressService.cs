using MallDomain.entity.mall;
using MallDomain.entity.mall.request;

namespace MallDomain.service.mall
{
    public interface IMallUserAddressService
    {
        // GetMyAddress 获取收货地址
        public Task<List<MallUserAddress>?> GetMyAddress(string token);

        // SaveUserAddress 保存用户地址
        public Task SaveUserAddress(string token, AddAddressParam req);
        // UpdateUserAddress 更新用户地址
        public Task UpdateUserAddress(string token, UpdateAddressParam req);
        public Task<MallUserAddress> GetMallUserAddressById(string token, long id);
        public Task<MallUserAddress> GetMallUserDefaultAddress(string token);
        public Task DeleteUserAddress(string token, long id);
    }
}
