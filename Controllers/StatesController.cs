using Microsoft.AspNetCore.Mvc;
using SpinitTest.Models;
using SpinitTest.Queries;
using SpinitTest.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SpinitTest.Controllers
{
    [Route("api/v1/")]

    public class StatesController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet("State")]
        public async Task<ActionResult<StateModel>> Get([FromQuery] string year, string id)
        {
            return Ok(await _stateService.Get(year, id));
        }

        [SwaggerOperation(Summary = "Query for all states",
            Description = "**Description**\n\nUse the endpoint in order perform a search on a set of different parameters. The endpoint supports paged reading, so you could extract necessary amount in batches. \n\n"
            + "The result will hold data according to data class allowed, and could be subset of following: \n\n"
            + "* Id\n" +
            "* State\n" +
            "* IdYear\n" +
            "* Year\n" +
            "* Population\n" +
            "* SlugState\n" +
            "* OrderBy\n" +
            "* PageNumber\n" +
            "* PageSize\n")]
        [HttpGet("States")]
        public async Task<ActionResult<List<StateModel>>> Get([FromQuery] StateQuery query)
        {
            var (items, pagination) = await _stateService.Get(query);

            Response?.Headers.Add("X-Pagination", pagination);
            return Ok(items);
        }
    }
}
