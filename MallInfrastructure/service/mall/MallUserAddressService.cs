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
            var userToken = await context.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);


            if (userToken == null) throw new Exception("不存在的用户");

            var address = await context.MallUserAddresses.
                SingleOrDefaultAsync(w => w.UserId == userToken.UserId && w.AddressId == id);

            if (address == null) throw new Exception("不存在该地址");

            address.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task<MallUserAddress> GetMallUserAddressById(string token, long id)
        {
            var userToken = await context.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) throw new Exception("不存在的用户");

            var address = await context.MallUserAddresses.
                SingleOrDefaultAsync(w => w.UserId == userToken.UserId && w.AddressId == id);

            if (address == null) throw new Exception("不存在该地址");


            return address;
        }



        public async Task<MallUserAddress> GetMallUserDefaultAddress(string token)
        {


            var userToken = await context.MallUserTokens.
                SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) throw new Exception("不存在的用户");

            var uadress = await context.MallUserAddresses.Where(p => p.UserId == userToken.UserId && p.DefaultFlag == true).SingleOrDefaultAsync();

            if (uadress is null) throw new Exception("获取默认地址失败，该用户没有默认地址");

            return uadress;


        }

        public async Task<List<MallUserAddress>> GetMyAddress(string token)
        {

            var userToken = await context.MallUserTokens.
                SingleOrDefaultAsync(p => p.Token == token);

            if (userToken is null) throw new Exception("该用户不存在");

            var list = await context.MallUserAddresses.
                Where(s => s.UserId == userToken.UserId).
                AsNoTracking().
                ToListAsync();

            return list;

        }

        public async Task SaveUserAddress(string token, AddAddressParam req)
        {

            var userToken = await context.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null)
            {
                throw new Exception("不存在的用户");
            }
            if (req.DefaultFlag)
            {
                var oldAddress = await context.MallUserAddresses.SingleOrDefaultAsync(u => userToken.UserId == userToken.UserId && u.DefaultFlag == true);
                if (oldAddress is not null)
                {
                    oldAddress.DefaultFlag = false;
                }
            }
            var newAdress = req.Adapt<MallUserAddress>();
            context.MallUserAddresses.Add(newAdress);
            await context.SaveChangesAsync();


        }

        public async Task UpdateUserAddress(string token, UpdateAddressParam req)
        {
            var userToken = await context.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null)
            {
                throw new Exception("不存在的用户");
            }

            var adress = await context.MallUserAddresses.
                SingleOrDefaultAsync(p => p.AddressId == req.AddressId && userToken.UserId == p.UserId);

            if (adress is null)
            {
                throw new Exception("不存在该地址");
            }
            adress = req.Adapt<MallUserAddress>();
            context.MallUserAddresses.Update(adress);

            if (req.DefaultFlag)
            {
                var oldAddress = await context.MallUserAddresses.SingleOrDefaultAsync(u => userToken.UserId == userToken.UserId && u.DefaultFlag == true);
                if (oldAddress is not null)
                {
                    oldAddress.DefaultFlag = false;
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
