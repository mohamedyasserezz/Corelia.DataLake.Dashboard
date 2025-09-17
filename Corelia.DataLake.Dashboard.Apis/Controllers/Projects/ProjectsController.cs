using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Projects;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Corelia.DataLake.Dashboard.Apis.Controllers.Projects
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController(IServiceManager _serviceManager) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<Result<ProjectResponse>>> CreateProject([FromBody] CreateProjectRequest projectRequest)
		{
			var response = await _serviceManager.ProjectService.CreateProjectAsync(projectRequest);

			return response.IsSuccess
				? Ok(response.Value)
				: response.ToProblem();
		}

		[HttpGet]
		public async Task<ActionResult<Result<PagedResult<ProjectResponse>>>> ListProjects()
		{
			var response = await _serviceManager.ProjectService.ListProjectsAsync();
			return response.IsSuccess
				? Ok(response.Value)
				: response.ToProblem();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Result<ProjectResponse>>> GetProject([FromRoute] string id)
		{
			var response = await _serviceManager.ProjectService.GetProject(id);
			return response.IsSuccess
				? Ok(response.Value)
				: response.ToProblem();
		}
	}
}
