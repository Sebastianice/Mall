using MallDomain.entity.common.response;
using Microsoft.AspNetCore.Mvc;

namespace MallApi.Controllers.mannage
{
    [Route("manage-api/v1")]
    [ApiController]
    public class ManageCarouselController : ControllerBase
    {

        [HttpPost("carousels")]
        public async Task<Result> CreateCarousel()
        {
            return Result.Ok();
        }
        [HttpDelete("carousels")]
        public async Task<Result> DeleteCarousel()
        {
            return Result.Ok();
        }
        [HttpPut("carousels")]
        public async Task<Result> UpdateCarousel()
        {
            return Result.Ok();
        }
        [HttpGet("carousels/{id}")]
        public async Task<Result> FindCarousel()
        {
            return Result.Ok();
        }
        [HttpGet("carousels")]
        public async Task<Result> GetCarouselList()
        {
            return Result.Ok();
        }

    }
}
