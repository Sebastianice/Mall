using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service.mall
{
    public class MallUserAddressService : IMallUserAddressService
    {
        private readonly MallContext context;

        public MallUserAddressService(MallContext mallContext)
        {
            context = mallContext;
        }

        public async Task DeleteUserAddress(string token, long id)
        {
            var userToken = await context.UserTokens.SingleOrDefaultAsync(p => p.Token == token);


            if (userToken == null) throw new Exception("不存在的用户");

            var address = await context.UserAddresses.
                SingleOrDefaultAsync(w => w.UserId == userToken.UserId && w.AddressId == id);

            if (address == null) throw new Exception("不存在该地址");

            address.IsDeleted = 0;
            await context.SaveChangesAsync();
        }

        public async Task<UserAddress> GetMallUserAddressById(string token, long id)
        {
            var userToken = await context.UserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) throw new Exception("不存在的用户");

            var address = await context.UserAddresses.
                SingleOrDefaultAsync(w => w.UserId == userToken.UserId && w.AddressId == id);

            if (address == null) throw new Exception("不存在该地址");


            return address;
        }



        public async Task<UserAddress> GetMallUserDefaultAddress(string token)
        {


            var userToken = await context.UserTokens.
                SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) throw new Exception("不存在的用户");

            var uadress = await context.UserAddresses.Where(p => p.UserId == userToken.UserId && p.DefaultFlag == 0).SingleOrDefaultAsync();

            if (uadress is null) throw new Exception("该用户没有默认地址,请去设置默认地址");

            return uadress;


        }

        public async Task<List<UserAddress>> GetMyAddress(string token)
        {

            var userToken = await context.UserTokens.
                SingleOrDefaultAsync(p => p.Token == token);

            if (userToken is null) throw new Exception("该用户不存在");

            var list = await context.UserAddresses.
                Where(s => s.UserId == userToken.UserId).
                AsNoTracking().
                ToListAsync();

            return list;

        }

        public async Task SaveUserAddress(string token, AddAddressParam req)
        {

            var userToken = await context.UserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null)
            {
                throw new Exception("不存在的用户");
            }
            if (req.DefaultFlag == 1)
            {
                var oldAddress = await context.UserAddresses.SingleOrDefaultAsync(u => userToken.UserId == userToken.UserId && u.DefaultFlag == 0);
                if (oldAddress is not null)
                {

                    oldAddress.DefaultFlag = 0;
                }
            }
            var newAdress = req.Adapt<UserAddress>();
            newAdress.UserId = userToken.UserId;
            context.UserAddresses.Add(newAdress);
            await context.SaveChangesAsync();


        }

        public async Task UpdateUserAddress(string token, UpdateAddressParam req)
        {
            var userToken = await context.UserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null)
            {
                throw new Exception("不存在的用户");
            }

            var adress = await context.UserAddresses.
                SingleOrDefaultAsync(p => p.AddressId == req.AddressId && userToken.UserId == p.UserId);

            if (adress is null)
            {
                throw new Exception("不存在该地址");
            }
            adress = req.Adapt<UserAddress>();
            context.UserAddresses.Update(adress);

            if (req.DefaultFlag)
            {
                var oldAddress = await context.UserAddresses.SingleOrDefaultAsync(u => userToken.UserId == userToken.UserId && u.DefaultFlag == 0);
                if (oldAddress is not null)
                {
                    oldAddress.DefaultFlag = 0;
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
