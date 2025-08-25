using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Persistance.Data.Interceptors
{
	public class AuditInterceptor(ILoggedInUserService _loggedInUserService) : SaveChangesInterceptor
	{

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateData(eventData.Context);

			return base.SavingChanges(eventData, result);


		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{

			UpdateData(eventData.Context);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private protected void UpdateData(DbContext? dbContext)
		{

			if (dbContext is null)
				return;

			var Entries = dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
				.Where(entity => entity.State is EntityState.Added or EntityState.Modified);
			foreach (var entry in Entries)
			{
				if (string.IsNullOrEmpty(_loggedInUserService.UserId))
				{
					_loggedInUserService.UserId = "";
				}

				if (entry.State is EntityState.Added)
				{

					entry.Entity.CreatedBy = _loggedInUserService.UserId!;
					entry.Entity.CreatedOn = DateTime.UtcNow;

				}

				entry.Entity.LastModifiedBy = _loggedInUserService.UserId!;
				entry.Entity.LastModifiedOn = DateTime.UtcNow;

			}
		}
	}
}
