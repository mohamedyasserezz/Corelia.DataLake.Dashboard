using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Projects
{

	public class ProjectResponse
	{
		public int id { get; set; }
		public string title { get; set; }
		public string? description { get; set; }
		public string? label_config { get; set; }
		public string? expert_instruction { get; set; }
		public bool? show_instruction { get; set; }
		public bool? show_skip_button { get; set; }
		public bool? enable_empty_annotation { get; set; }
		public bool? show_annotation_history { get; set; }
		public int? organization { get; set; }
		public string? color { get; set; }
		public int? maximum_annotations { get; set; }
		public bool? is_published { get; set; }
		public string? model_version { get; set; }
		public bool? is_draft { get; set; }
		public Creator? created_by { get; set; }
		public DateTime? created_at { get; set; }
	}
}
