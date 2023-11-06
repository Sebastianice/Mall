using MallDomain.entity.common.response;
using MallDomain.entity.mall;
using MallDomain.entity.mall.request;
using MallDomain.service.mall;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mall
{
    [ApiController]
    [Route("api/v1")]

    [Authorize(policy: "User")]
    public class MallUserAddressController : ControllerBase
    {
        private readonly IMallUserAddressService mallUserAddressService;

        public MallUserAddressController(IMallUserAddressService mallUserAddressService)
        {
            this.mallUserAddressService = mallUserAddressService;
        }

        [HttpGet("address/{addressId}")]
        public async Task<Result> GetMallUserAddress(long addressId)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            var adrress = await mallUserAddressService.GetMallUserAddressById(token, addressId);
            return Result.OkWithData(adrress);
        }
        [HttpPost("address")]
        public async Task<Result> SaveUserAddress([FromBody] AddAddressParam req)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            await mallUserAddressService.SaveUserAddress(token, req);
            return Result.OkWithMessage("保存地址成功");
        }
        [HttpPut("address")]
        public async Task<Result> UpdateMallUserAddress([FromBody] UpdateAddressParam req)
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            await mallUserAddressService.UpdateUserAddress(token, req);
            return Result.OkWithMessage("更新地址成功");
        }
        [HttpGet("address")]
        public async Task<Result> AddressList()
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            var addressList = await mallUserAddressService.GetMyAddress(token);

            return Result.OkWithData(addressList);
        }


        [HttpGet("address/default")]
        public async Task<Result> GetMallUserDefaultAddress()
        {
            var token = Request.Headers["Authorization"].ToString()[7..];
            MallUserAddress address = await mallUserAddressService.GetMallUserDefaultAddress(token);

            return Result.OkWithData(address);
        }
        [HttpDelete("address/{addressId}")]
        public async Task<Result> DeleteUserAddress(long addressId)
        {

            var token = Request.Headers["Authorization"].ToString()[7..];
            await mallUserAddressService.DeleteUserAddress(token, addressId);
            return Result.OkWithMessage("删除成功");
        }
    }
}
