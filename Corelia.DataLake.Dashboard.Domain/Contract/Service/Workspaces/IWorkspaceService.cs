using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces
{
	public interface IWorkspaceService
	{
		// Task<List<ReturnWorkspaceRequest>> ListWorkspaces();
		// Task<ReturnWorkspaceRequest> GetWorkspace(int workspaceId);	
		Task<Result<ReturnWorkspaceResponse>> CreateWorkspace(CreateWorkspaceRequest workspaceRequest);
		Task<Result<ReturnWorkspaceResponse>> UpdateWorkspace(int workspaceId, UpdateWorkspaceRequest workspaceRequest);
		Task<Result<string>> DeleteWorkspace(int workspaceId);

		// Task AddMembers(string userId, int workspaceId);
		// Task RemoveMembers(string userId, int workspaceId);
		// Task ListMembers(int workspaceId);
	}
}
