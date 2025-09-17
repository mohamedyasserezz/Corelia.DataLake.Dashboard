using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Projects
{
	public record CreateProjectRequest(
		string title,
		string? description,
		string? labelConfig,
		bool? isPublished
		);
	
}
