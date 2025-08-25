using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Entities.Common
{
	public class BaseAuditableEntity<TKey>
		: BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; } = null!;
		public DateTime LastModifiedOn { get; set; }
		public string LastModifiedBy { get; set; } = null!;
	}
}
