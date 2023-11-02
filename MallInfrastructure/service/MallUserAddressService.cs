using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using Microsoft.EntityFrameworkCore;

namespace MallInfrastructure.service {
    public class MallUserAddressService : IMallUserAddressService {
        private readonly MallContext mallContext;

        public MallUserAddressService(MallContext mallContext) {
            this.mallContext = mallContext;
        }

        public Task DeleteUserAddress(string token, long id) {
            throw new NotImplementedException();
        }

        public Task<MallUserAddress> GetMallUserAddressById(string token, long id) {
            throw new NotImplementedException();
        }

        public async Task<MallUserAddress> GetMallUserDefaultAddress(string token) {


          var userToken=await mallContext.MallUserTokens.Where(p => p.Token == token).SingleOrDefaultAsync();
            if(userToken == null) {
                throw new Exception("不存在的用户");
            }

         var uadress=   await mallContext.MallUserAddresses.Where(p => p.UserId == userToken.UserId && p.DefaultFlag == true).SingleOrDefaultAsync();
if(uadress is null) {
                throw new Exception("不存在默认地址失败");
            }


            return uadress;
    
            
        }

        public Task<List<MallUserAddress>> GetMyAddress(string token) {
            throw new NotImplementedException();
        }

        public Task SaveUserAddress(string token, AddAddressParam req) {
            throw new NotImplementedException();
        }

        public Task UpdateUserAddress(string token, UpdateAddressParam req) {
            throw new NotImplementedException();
        }
    }
}
