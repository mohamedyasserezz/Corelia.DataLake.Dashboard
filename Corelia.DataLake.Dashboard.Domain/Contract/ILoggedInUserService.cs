using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Contract
{
	public interface ILoggedInUserService
	{
		public string? UserId { get; set; }
	}
}
