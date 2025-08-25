using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application.Services.Workspaces
{
	internal class WorkspaceService(IUnitOfWork _unitOfWork) : IWorkspaceService
	{
		public async Task<ReturnWorkspaceResponse> CreateWorkspace(CreateWorkspaceRequest workspaceRequest)
		{
		}

		public Task DeleteWorkspace(int workspaceId)
		{
			throw new NotImplementedException();
		}

		public Task<ReturnWorkspaceResponse> UpdateWorkspace(UpdateWorkspaceRequest workspaceRequest)
		{
			throw new NotImplementedException();
		}
	}
}
