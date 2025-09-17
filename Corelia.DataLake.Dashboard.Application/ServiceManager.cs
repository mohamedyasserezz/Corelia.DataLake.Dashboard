using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Projects;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application
{
	internal class ServiceManager : IServiceManager
	{

		private readonly Lazy<IWorkspaceService> _workspaceService;
		private readonly Lazy<IProjectService> _projectService;

		public ServiceManager(Func<IWorkspaceService> workspacefactory, Func<IProjectService> projectfactory)
		{
			_workspaceService = new Lazy<IWorkspaceService>(workspacefactory, LazyThreadSafetyMode.ExecutionAndPublication);
			_projectService = new Lazy<IProjectService>(projectfactory, LazyThreadSafetyMode.ExecutionAndPublication);
		}

		public IWorkspaceService WorkspaceService => _workspaceService.Value;

		public IProjectService ProjectService => _projectService.Value;
	}
}
