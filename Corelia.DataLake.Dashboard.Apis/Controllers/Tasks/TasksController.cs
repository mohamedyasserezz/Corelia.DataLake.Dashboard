using Corelia.DataLake.Dashboard.Domain.Contract.Service.Task;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Corelia.DataLake.Dashboard.Apis.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController(
        ITaskServices taskServices,
        ILogger<TasksController> logger
        ) : ControllerBase
    {
        private readonly ITaskServices _taskServices = taskServices;
        private readonly ILogger<TasksController> _logger = logger;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all tasks from Label Studio");

            var response = await _taskServices.GetAllTasksAsync();

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching task with id: {id}", id);

            var response = await _taskServices.GetTaskByIdAsync(new TaskRequest(id));

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpPost("{id:int}/complete")]
        public async Task<IActionResult> CompleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Completing task with id: {id}", id);

            var response = await _taskServices.CompleteTaskAsync(new TaskRequest(id));

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> CancelAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Canceling task with id: {id}", id);

            var response = await _taskServices.CancelTaskAsync(new TaskRequest(id));

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
    }
} 