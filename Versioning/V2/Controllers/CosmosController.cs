using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Versioning.V2.Controllers
{
    [ApiController]
    [ApiVersion(2.0)]
    [Route("[controller]")]
    public class CosmosController : ControllerBase
    {

        public CosmosClient _client;

        public CosmosController(CosmosClient client)
        {
            _client = client;
        }

        // GET: api/<CosmosController>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            Database database = await _client.CreateDatabaseIfNotExistsAsync("versioning");
            Container container = database.GetContainer("versioning");
            var query = new QueryDefinition(query: "SELECT * FROM c");
            using FeedIterator<dynamic> feed = container.GetItemQueryIterator<dynamic>(
                queryDefinition: query
            );
            List<dynamic> results = new();
            while (feed.HasMoreResults)
            {
                FeedResponse<dynamic> response = await feed.ReadNextAsync();
                foreach (dynamic item in response)
                {
                    results.Add(item);
                }
            }
            return Ok(results);
        }

        // GET: api/<CosmosController>/{id}
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            Database database = await _client.CreateDatabaseIfNotExistsAsync("versioning");
            Container container = database.GetContainer("versioning");
            ItemResponse<dynamic> results = await container.ReadItemAsync<dynamic>(
                id: $"{id}",
                partitionKey: new PartitionKey(id)
            );
            return Ok(results);
        }
    }
}
