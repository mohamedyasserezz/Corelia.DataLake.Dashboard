using Corelia.DataLake.Dashboard.Domain.Contract.Service.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Task;
namespace Corelia.DataLake.Dashboard.Apis.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskServices taskServices) : ControllerBase
    {
        private readonly ITaskServices _taskServices = taskServices;


        [HttpGet]
        public async Task<IActionResult> GetTaskByProjectId([FromQuery] int id, CancellationToken cancellationToken)
        {
            var response = await _taskServices.GetTasksByProjectAsync(new TaskRequest(id));
            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _taskServices.GetTaskByIdAsync(new TaskRequest(id));
            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> CancelTaskAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _taskServices.CancelTaskAsync(new TaskRequest(id));
            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
        [HttpPost("{id:int}/complete")]
        public async Task<IActionResult> CompleteTaskAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _taskServices.CompleteTaskAsync(new TaskRequest(id));
            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
    }
}
