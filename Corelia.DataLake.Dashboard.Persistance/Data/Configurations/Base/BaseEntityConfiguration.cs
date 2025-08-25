using Corelia.DataLake.Dashboard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Persistance.Data.Configurations.Base
{
	public class BaseEntityConfiguration<TEntity, TKey>
		: IEntityTypeConfiguration<TEntity>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(b => b.Id).ValueGeneratedOnAdd();
		}
	}
}
