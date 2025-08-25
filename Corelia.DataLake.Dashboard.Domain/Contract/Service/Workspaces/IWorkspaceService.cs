using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Responses;

namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces
{
    public interface IWorkspaceService
    {
        Task<Result<IEnumerable<ReturnWorkspaceResponse>>> ListWorkspaces();
        Task<Result<ReturnWorkspaceResponse>> GetWorkspace(int workspaceId);
        Task<Result<ReturnWorkspaceResponse>> CreateWorkspace(CreateWorkspaceRequest workspaceRequest);
        Task<Result<ReturnWorkspaceResponse>> UpdateWorkspace(int workspaceId, UpdateWorkspaceRequest workspaceRequest);
        Task<Result<string>> DeleteWorkspace(int workspaceId);

        // Task AddMembers(string userId, int workspaceId);
        // Task RemoveMembers(string userId, int workspaceId);
        // Task ListMembers(int workspaceId);
    }
}
