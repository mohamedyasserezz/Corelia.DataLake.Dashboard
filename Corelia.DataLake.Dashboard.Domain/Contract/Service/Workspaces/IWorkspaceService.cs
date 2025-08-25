using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Responses;
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
		Task<ReturnWorkspaceResponse> CreateWorkspace(CreateWorkspaceRequest workspaceRequest);
		Task<ReturnWorkspaceResponse> UpdateWorkspace(UpdateWorkspaceRequest workspaceRequest);
		Task DeleteWorkspace(int workspaceId);

		// Task AddMembers(string userId, int workspaceId);
		// Task RemoveMembers(string userId, int workspaceId);
		// Task ListMembers(int workspaceId);
	}
}
