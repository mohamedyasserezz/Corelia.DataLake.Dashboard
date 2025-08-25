using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.Workspaces.Requests
{
	public record UpdateWorkspaceRequest(
		int id,
		string title,
		string? color,
		string? description,
		bool? is_archived,
		bool? is_personal
		);
}
