using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service {
    public class MallUserAddressService : IMallUserAddressService {
        private readonly MallContext mallContext;

        public MallUserAddressService(MallContext mallContext) {
            this.mallContext = mallContext;
        }

        public async Task DeleteUserAddress(string token, long id) {
            var userToken = await mallContext.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var address = await mallContext.MallUserAddresses.SingleOrDefaultAsync(w => w.UserId == userToken.UserId && w.AddressId == id);

            if (address == null) {
                throw new Exception("不存在该地址");
            }
            address.IsDeleted = true;
            await mallContext.SaveChangesAsync();
        }

        public async Task<MallUserAddress> GetMallUserAddressById(string token, long id) {
            var userToken = await mallContext.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            var address = await mallContext.MallUserAddresses.SingleOrDefaultAsync(w => w.UserId == userToken.UserId && w.AddressId == id);

            if (address == null) {
                throw new Exception("不存在该地址");
            }
            //address.IsDeleted = true;
            return address;
        }



        public async Task<MallUserAddress> GetMallUserDefaultAddress(string token) {


            var userToken = await mallContext.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }

            var uadress = await mallContext.MallUserAddresses.Where(p => p.UserId == userToken.UserId && p.DefaultFlag == true).SingleOrDefaultAsync();
            if (uadress is null) {
                return null;
            }


            return uadress;


        }

        public async Task<List<MallUserAddress>?> GetMyAddress(string token) {

            var userToken = await mallContext.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken is null) {
                return null;
            }
            var list = await mallContext.MallUserAddresses.Where(s => s.UserId == userToken.UserId).AsNoTracking().ToListAsync();

            return list;

        }

        public async Task SaveUserAddress(string token, AddAddressParam req) {

            var userToken = await mallContext.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }
            if (req.DefaultFlag) {
                var oldAddress = await mallContext.MallUserAddresses.SingleOrDefaultAsync(u => userToken.UserId == userToken.UserId&&u.DefaultFlag==true);
                if (oldAddress is not null) {
                    oldAddress.DefaultFlag = false;
                } 
            }
            var newAdress = req.Adapt<MallUserAddress>();
            mallContext.MallUserAddresses.Add(newAdress);
            await mallContext.SaveChangesAsync();


        }

        public async Task UpdateUserAddress(string token, UpdateAddressParam req) {
            var userToken = await mallContext.MallUserTokens.SingleOrDefaultAsync(p => p.Token == token);
            if (userToken == null) {
                throw new Exception("不存在的用户");
            }

            var adress = await mallContext.MallUserAddresses.
                SingleOrDefaultAsync(p => p.AddressId == req.AddressId && userToken.UserId == p.UserId);

            if (adress is  null) {
                throw new Exception("不存在该地址");
            }
            adress=req.Adapt<MallUserAddress>();
            mallContext.MallUserAddresses.Update(adress);

            if (req.DefaultFlag) {
                var oldAddress = await mallContext.MallUserAddresses.SingleOrDefaultAsync(u => userToken.UserId == userToken.UserId && u.DefaultFlag == true);
                if (oldAddress is not null) {
                    oldAddress.DefaultFlag = false;
                } 
            }

            await mallContext.SaveChangesAsync();
        }
    }
}
