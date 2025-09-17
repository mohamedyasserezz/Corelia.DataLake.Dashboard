using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Entities.Projects
{
	public class Project
	{
		public int Id { get; set; }  
		public string Title { get; set; }
		public string Description { get; set; }
		public string LabelConfig { get; set; }
		public bool IsPublished { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
