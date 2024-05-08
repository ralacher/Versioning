using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Versioning.V1.Controllers
{
    [ApiVersion(1.0)]
    [Route("V{version:apiVersion}/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        // GET: api/<StorageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StorageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StorageController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StorageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StorageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
