using Mall.Common.Result;
using Mall.Repository.Models;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{
    [ApiController]
    [Route("api/v1")]

    [Authorize(policy: "User")]
    public class MallUserAddressController : ControllerBase
    {
        private readonly MallUserAddressService mallUserAddressService;

        public MallUserAddressController(MallUserAddressService mallUserAddressService)
        {
            this.mallUserAddressService = mallUserAddressService;
        }

        [HttpGet("address/{addressId}")]
        public async Task<AppResult> GetMallUserAddress(long addressId)
        {
            var token = Request.Headers["Authorization"]!;
            var adrress = await mallUserAddressService.GetMallUserAddressById(token!, addressId);
            return AppResult.OkWithData(adrress);
        }
        [HttpPost("address")]
        public async Task<AppResult> SaveUserAddress([FromBody] AddAddressParam req)
        {
            var token = Request.Headers["Authorization"]!;
            await mallUserAddressService.SaveUserAddress(token!, req);
            return AppResult.OkWithMessage("保存地址成功");
        }
        [HttpPut("address")]
        public async Task<AppResult> UpdateMallUserAddress([FromBody] UpdateAddressParam req)
        {
            var token = Request.Headers["Authorization"]!;
            await mallUserAddressService.UpdateUserAddress(token!, req);
            return AppResult.OkWithMessage("更新地址成功");
        }
        [HttpGet("address")]
        public async Task<AppResult> AddressList()
        {
            var token = Request.Headers["Authorization"]!;
            var addressList = await mallUserAddressService.GetMyAddress(token!);

            return AppResult.OkWithData(addressList);
        }


        [HttpGet("address/default")]
        public async Task<AppResult> GetMallUserDefaultAddress()
        {
            var token = Request.Headers["Authorization"]!;
            UserAddress address = await mallUserAddressService.GetMallUserDefaultAddress(token!);

            return AppResult.OkWithData(address);
        }
        [HttpDelete("address/{addressId}")]
        public async Task<AppResult> DeleteUserAddress(long addressId)
        {

            var token = Request.Headers["Authorization"]!;
            await mallUserAddressService.DeleteUserAddress(token!, addressId);
            return AppResult.OkWithMessage("删除成功");
        }
    }
}
