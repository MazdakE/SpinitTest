using Microsoft.AspNetCore.Mvc;
using SpinitTest.Models;
using SpinitTest.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SpinitTest.Controllers
{
    [Route("api/v1/[Controller]")]

    public class HistoryLogController : ControllerBase
    {
        private readonly IHistoryLogService _historyLogService;
        public HistoryLogController(IHistoryLogService historyLogService)
        {
            _historyLogService = historyLogService;
        }

        [SwaggerOperation(Summary = "Get data of all queries",
            Description = "**Description**\n\nUse this endpoint to get data of all query searches made. ")]
        [HttpGet]
        public async Task<ActionResult<List<HistoryLogModel>>> Get() => 
            Ok(await _historyLogService.Get());
    }
}
