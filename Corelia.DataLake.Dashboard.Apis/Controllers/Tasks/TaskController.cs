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
    }
}
