using Corelia.DataLake.Dashboard.Domain.Contract.Service.Projects;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Contract
{
	public interface IServiceManager
	{
		IWorkspaceService WorkspaceService { get; }
		IProjectService ProjectService { get; }
	}
}
