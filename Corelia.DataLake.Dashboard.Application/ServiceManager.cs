using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
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

		public ServiceManager(Func<IWorkspaceService> workspacefactory)
		{
			_workspaceService = new Lazy<IWorkspaceService>(workspacefactory, LazyThreadSafetyMode.ExecutionAndPublication);
		}

		public IWorkspaceService WorkspaceService => _workspaceService.Value;
	}
}
