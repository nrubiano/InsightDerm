using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
	public class InsightDermContextFactory : IDbContextFactory<InsightDermContext>
	{
		public InsightDermContext Create(DbContextFactoryOptions options)
		{
			var builder = new DbContextOptionsBuilder<InsightDermContext>();
			builder.UseNpgsql("User ID=postgres;Password=spmt0518;Host=localhost;Port=5432;Database=insightderm;Pooling=true;");
			return new InsightDermContext(builder.Options);
		}
	}
}
