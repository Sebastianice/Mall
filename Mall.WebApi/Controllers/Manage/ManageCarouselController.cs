using Mall.Common.Result;
using Mall.Services;
using Mall.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageCarouselController : ControllerBase
    {
        private readonly ManageCarouselService mallCarouselService;

        public ManageCarouselController(ManageCarouselService mallCarouselService)
        {
            this.mallCarouselService = mallCarouselService;
        }

        [HttpPost("carousels")]
        public async Task<AppResult> CreateCarousel([FromBody] CarouselAddParam carouselAddParam)
        {
            /*ar rs = await ValidatorFactory.CreateValidator(carouselAddParam)!.ValidateAsync(carouselAddParam);

            if (!rs.IsValid)
            {
                return AppResult.FailWithMessage("参数不合法");
            }*/
            await mallCarouselService.CreateCarousel(carouselAddParam);
            return AppResult.OkWithMessage("创建轮播图成功");
        }
        [HttpDelete("carousels")]
        public async Task<AppResult> DeleteCarousel([FromBody] IdsReq ids)
        {
            await mallCarouselService.DeleteCarousel(ids.Ids);
            return AppResult.Ok();
        }


        [HttpPut("carousels")]
        public async Task<AppResult> UpdateCarousel(CarouselUpdateParam req)
        {
            /* var rs = await ValidatorFactory.CreateValidator(req)!.ValidateAsync(req);

             if (!rs.IsValid)
             {
                 return AppResult.FailWithMessage("参数不合法");
             }*/
            await mallCarouselService.UpdateCarousel(req);

            return AppResult.OkWithMessage("更新成功");
        }
        [HttpGet("carousels/{id}")]
        public async Task<AppResult> FindCarousel(long id)
        {
            var cas = await mallCarouselService.GetCarousel(id);
            return AppResult.OkWithData(cas);
        }
        [HttpGet("carousels")]
        public async Task<AppResult> GetCarouselList([FromQuery] PageInfo csh)
        {
            var (list, total) = await mallCarouselService.GetCarouselInfoList(csh);
            return AppResult.OkWithDetailed(new PageResult()
            {
                List = list,
                CurrPage = csh.PageNumber,
                TotalCount = total,
                PageSize = csh.PageSize,
                TotalPage = (int)Math.Ceiling((double)total / csh.PageSize)
            }, "获取成功");


        }

    }


}
