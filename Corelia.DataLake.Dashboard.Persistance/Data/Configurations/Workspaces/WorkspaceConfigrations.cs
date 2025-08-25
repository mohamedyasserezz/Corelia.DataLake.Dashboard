using Corelia.DataLake.Dashboard.Domain.Entities.Workspaces;
using Corelia.DataLake.Dashboard.Persistance.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Persistance.Data.Configurations.Workspaces
{
	public class WorkspaceConfigrations : BaseAuditableEntityConfigurations<Workspace, int>
	{
		public override void Configure(EntityTypeBuilder<Workspace> builder)
		{
			base.Configure(builder);
			
			builder
				.Property(b => b.title)
				.IsRequired()
				.HasMaxLength(1000);
			
			builder
				.Property(b => b.color)
				.HasMaxLength(16)
				.IsRequired(false);


			builder
				.Property(b => b.description)
				.HasMaxLength(500)
				.IsRequired(false);

			builder
				.Property(b => b.is_archived)
				.IsRequired()
				.HasDefaultValue(false);

			builder
				.Property(b => b.is_personal)
				.IsRequired()
				.HasDefaultValue(false);

			builder
				.HasMany(b => b.Members)
				.WithMany();

		}
	}
}
