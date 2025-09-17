using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.Projects
{
	public interface IProjectService
	{
		Task<Result<PagedResult<ProjectResponse>>> ListProjectsAsync();
		Task<Result<ProjectResponse>> CreateProjectAsync(CreateProjectRequest req);
		Task<Result<ProjectResponse>> GetProject(string id);
		Task<Result<ProjectResponse>> UpdateProjectAsync(string id, CreateProjectRequest req);
		Task<Result<bool>> DeleteProjectAsync(string id);
	}
}
