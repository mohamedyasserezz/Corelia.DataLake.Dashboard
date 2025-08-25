using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Corelia.DataLake.Dashboard.Apis.Controllers.Workspaces
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspacesController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Result<ReturnWorkspaceResponse>>> CreateWorkspace([FromBody] CreateWorkspaceRequest workspaceRequest)
        {
            var response = await _serviceManager.WorkspaceService.CreateWorkspace(workspaceRequest);

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpPut("{workspaceId}")]
        public async Task<ActionResult<Result<ReturnWorkspaceResponse>>> UpdateWorkspace([FromRoute] int workspaceId, [FromBody] UpdateWorkspaceRequest workspaceRequest)
        {
            var response = await _serviceManager.WorkspaceService.UpdateWorkspace(workspaceId, workspaceRequest);

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpDelete("{workspaceId}")]
        public async Task<ActionResult<Result<string>>> DeleteWorkspace([FromRoute] int workspaceId)
        {
            var response = await _serviceManager.WorkspaceService.DeleteWorkspace(workspaceId);

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
        [HttpGet("{workspaceId}")]
        public async Task<ActionResult<Result<ReturnWorkspaceResponse>>> GetWorkspace([FromRoute] int workspaceId)
        {
            var response = await _serviceManager.WorkspaceService.GetWorkspace(workspaceId);
            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<ReturnWorkspaceResponse>>>> ListWorkspaces()
        {
            var response = await _serviceManager.WorkspaceService.ListWorkspaces();
            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }
    }
}
