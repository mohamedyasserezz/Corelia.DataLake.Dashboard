using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Workspaces.Requests
{
	public record CreateWorkspaceRequest(
		string title,
		string? color,
		string? description,
		bool? is_archived,
		bool? is_personal
		);
}
