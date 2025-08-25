using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Corelia.DataLake.Dashboard.Domain.Entities.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Entities.Workspaces
{
	public class Workspace : BaseAuditableEntity<int>
	{
		public required string title { get; set; }
		public string? color { get; set; }
		public string? description { get; set; }
		public bool is_archived { get; set; } = false;
		public bool is_personal { get; set; } = false;

		public virtual ICollection<ApplicationUser> Members { get; set; } = new HashSet<ApplicationUser>();
	}
}
