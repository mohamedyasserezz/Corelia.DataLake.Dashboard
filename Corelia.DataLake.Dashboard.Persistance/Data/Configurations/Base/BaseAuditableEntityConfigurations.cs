﻿using Corelia.DataLake.Dashboard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Persistance.Data.Configurations.Base
{
	public class BaseAuditableEntityConfigurations<TEntity, TKey>
		: BaseEntityConfiguration<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			base.Configure(builder);

			builder.Property(E => E.CreatedBy).IsRequired();

			builder.Property(E => E.CreatedOn).IsRequired();

			builder.Property(E => E.LastModifiedBy).IsRequired();

			builder.Property(E => E.LastModifiedOn).IsRequired();
		}
	}
}
