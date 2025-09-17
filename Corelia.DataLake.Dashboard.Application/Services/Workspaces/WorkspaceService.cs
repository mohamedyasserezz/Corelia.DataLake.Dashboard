using AutoMapper;
using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces;
using Corelia.DataLake.Dashboard.Domain.Entities.Workspaces;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests;
using Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Responses;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application.Services.Workspaces
{
	internal class WorkspaceService
		: IWorkspaceService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly HttpClient _httpClient;
		private readonly IMapper _mapper;
		private readonly ILoggedInUserService _loggedInUserService;
		public WorkspaceService(IUnitOfWork unitOfWork, HttpClient httpClient, IMapper mapper, ILoggedInUserService loggedInUserService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:8080/api/");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlbl90eXBlIjoicmVmcmVzaCIsImV4cCI6ODA2NTI0ODQ5NiwiaWF0IjoxNzU4MDQ4NDk2LCJqdGkiOiIxN2U0ZDM1YzYwYTU0ZDRhYWJkMmVhODczYjE3NzJhNyIsInVzZXJfaWQiOiIxIn0.XBj_1IpRX7a_5rev89E6ZwX0CGYlPQ3r_vNdKsEqxxY");
			_loggedInUserService = loggedInUserService;
		}

		public async Task<Result<ReturnWorkspaceResponse>> CreateWorkspace(CreateWorkspaceRequest workspaceRequest)
		{
			var payload = new
			{
				title = workspaceRequest.title,
				description = workspaceRequest.description,
				//color = workspaceRequest.color,
				//is_archived = workspaceRequest.is_archived,
				//is_personal = workspaceRequest.is_personal
			};

			var content = new StringContent(
				JsonSerializer.Serialize(payload),
				Encoding.UTF8,
				"application/json"
			);

			var response = await _httpClient.PostAsync("workspaces/", content);

			if (!response.IsSuccessStatusCode)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error(response.StatusCode.ToString(), $"Error: ({response.StatusCode})", (int)response.StatusCode));
			}

			var responseContent = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			try
			{
				var workspace = JsonSerializer.Deserialize<ReturnWorkspaceResponse>(responseContent, options);

				if (workspace is null)
				{
					return Result.Failure<ReturnWorkspaceResponse>(new Error(ResponseStatusCode.NotFound.ToString(), "Workspaces not found", (int)ResponseStatusCode.NotFound));
				}

				return Result.Success(workspace);
			}
			catch (Exception)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error(ResponseStatusCode.NotFound.ToString(), "Workspaces not found", (int)ResponseStatusCode.NotFound));
			}

		}

		public async Task<Result<string>> DeleteWorkspace(int workspaceId)
		{
			var workspace = await _unitOfWork.GetRepository<Workspace, int>().GetByIdAsync(workspaceId);

			if (workspace == null)
			{
				return Result.Failure<string>(new Error("WorkspaceNotFound", "Workspace not found", (int)ResponseStatusCode.NotFound));
			}

			if (workspace.CreatedBy != _loggedInUserService.UserId)
			{
				return Result.Failure<string>(new Error("Unauthorized", "You are not authorized to delete this workspace", (int)ResponseStatusCode.Unauthorized));
			}

			_unitOfWork.GetRepository<Workspace, int>().Delete(workspace);

			var complete = await _unitOfWork.CompleteAsync() > 0;

			if (!complete)
			{
				return Result.Failure<string>(new Error("WorkspaceDeletionFailed", "Failed to delete workspace", (int)ResponseStatusCode.BadRequest));
			}

			return Result.Success("Workspace deleted successfully");
		}

		public async Task<Result<ReturnWorkspaceResponse>> GetWorkspace(int workspaceId)
		{
			var workspace = await _unitOfWork.GetRepository<Workspace, int>().GetByIdAsync(workspaceId);
			if (workspace == null)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error("WorkspaceNotFound", "Workspace not found", (int)ResponseStatusCode.NotFound));
			}
			var workspaceResponse = _mapper.Map<ReturnWorkspaceResponse>(workspace);
			return Result.Success(workspaceResponse);
		}

		public async Task<Result<IEnumerable<ReturnWorkspaceResponse>>> ListWorkspaces()
		{

			var response = await _httpClient.GetAsync("workspaces");

			if (!response.IsSuccessStatusCode)
			{
				return Result.Failure<IEnumerable<ReturnWorkspaceResponse>>(new Error(ResponseStatusCode.NotFound.ToString(), "Workspaces not found", (int)ResponseStatusCode.NotFound));
			}

			var content = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			try
			{
				var workspaces = JsonSerializer.Deserialize<IEnumerable<ReturnWorkspaceResponse>>(content, options);

				if (workspaces is null)
				{
					return Result.Failure<IEnumerable<ReturnWorkspaceResponse>>(new Error(ResponseStatusCode.NotFound.ToString(), "Workspaces not found", (int)ResponseStatusCode.NotFound));
				}

				return Result.Success(workspaces);
			}
			catch (Exception ex)
			{
				return Result.Failure<IEnumerable<ReturnWorkspaceResponse>>(new Error(ResponseStatusCode.BadRequest.ToString(), $"Deserialization error: {ex.Message})", (int)ResponseStatusCode.BadRequest));
			}
		}

		public async Task<Result<ReturnWorkspaceResponse>> UpdateWorkspace(int workspaceId, UpdateWorkspaceRequest workspaceRequest)
		{

			var workspace = await _unitOfWork.GetRepository<Workspace, int>().GetByIdAsync(workspaceId);

			if (workspace == null)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error("WorkspaceNotFound", "Workspace not found", (int)ResponseStatusCode.NotFound));
			}
			if (workspace.CreatedBy != _loggedInUserService.UserId)
			{
				return Result.Failure<ReturnWorkspaceResponse>(new Error("Unauthorized", "You are not authorized to update this workspace", (int)ResponseStatusCode.Unauthorized));
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
