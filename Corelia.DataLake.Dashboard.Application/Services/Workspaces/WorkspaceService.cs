using AutoMapper;
using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces;
using Corelia.DataLake.Dashboard.Domain.Entities.Workspaces;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application.Services.Workspaces
{
	internal class WorkspaceService(IUnitOfWork _unitOfWork, IMapper _mapper)
		: IWorkspaceService
	{
		public async Task<Result<ReturnWorkspaceResponse>> CreateWorkspace(CreateWorkspaceRequest workspaceRequest)
		{
			var workspaceEntity = _mapper.Map<Workspace>(workspaceRequest);

			await _unitOfWork.GetRepository<Workspace, int>().AddAsync(workspaceEntity);

			var complete = await _unitOfWork.CompleteAsync() > 0;

			if (!complete)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error("WorkspaceCreationFailed", "Failed to Create Workspace", (int)ResponseStatusCode.BadRequest));
			}

			var workspaceResponse = _mapper.Map<ReturnWorkspaceResponse>(workspaceEntity);

			return Result.Success(workspaceResponse);

		}

		public async Task<Result<string>> DeleteWorkspace(int workspaceId)
		{
			var workspace = await _unitOfWork.GetRepository<Workspace, int>().GetByIdAsync(workspaceId);

			if (workspace == null)
			{
				return Result.Failure<string>(new Error("WorkspaceNotFound", "Workspace not found", (int)ResponseStatusCode.NotFound));
			}

			_unitOfWork.GetRepository<Workspace, int>().Delete(workspace);

			var complete = await _unitOfWork.CompleteAsync() > 0;

			if (!complete)
			{
				return Result.Failure<string>(new Error("WorkspaceDeletionFailed", "Failed to delete workspace", (int)ResponseStatusCode.BadRequest));
			}

			return Result.Success("Workspace deleted successfully");
		}

		public async Task<Result<ReturnWorkspaceResponse>> UpdateWorkspace(UpdateWorkspaceRequest workspaceRequest)
		{
			var workspace = await _unitOfWork.GetRepository<Workspace, int>().GetByIdAsync(workspaceRequest.id);
			
			if (workspace == null)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error("WorkspaceNotFound", "Workspace not found", (int)ResponseStatusCode.NotFound));
			}
			
			_mapper.Map(workspaceRequest, workspace);
			
			_unitOfWork.GetRepository<Workspace, int>().Update(workspace);
			
			var complete = await _unitOfWork.CompleteAsync() > 0;
			
			if (!complete)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error("WorkspaceUpdateFailed", "Failed to update workspace", (int)ResponseStatusCode.BadRequest));
			}
			
			var workspaceResponse = _mapper.Map<ReturnWorkspaceResponse>(workspace);
			
			return Result.Success(workspaceResponse);
		}
	}
}
