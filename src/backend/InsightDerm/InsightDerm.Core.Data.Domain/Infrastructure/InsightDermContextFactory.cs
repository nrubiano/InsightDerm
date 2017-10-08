using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
	public class InsightDermContextFactory : IDesignTimeDbContextFactory<InsightDermContext>
	{
        public InsightDermContext CreateDbContext(string[] args)
        {
			var builder = new DbContextOptionsBuilder<InsightDermContext>();
			builder.UseNpgsql("User ID=postgres;Password=spmt0518;Host=localhost;Port=5432;Database=insightderm;Pooling=true;");
			return new InsightDermContext(builder.Options);
        }
    }
}
