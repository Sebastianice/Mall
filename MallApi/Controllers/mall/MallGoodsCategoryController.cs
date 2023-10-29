using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallApi.Controllers.mall {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MallGoodsCategoryController : ControllerBase {
        // GET: api/<MallGoodsCategoryController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MallGoodsCategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<MallGoodsCategoryController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<MallGoodsCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<MallGoodsCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
