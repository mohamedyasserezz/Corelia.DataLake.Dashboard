using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Projects
{
	public record Creator(
		int id,
		string first_name,
		string last_name,
		string email,
		string? avatar
	);
}
