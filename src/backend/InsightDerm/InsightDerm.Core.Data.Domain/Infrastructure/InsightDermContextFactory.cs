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
			builder.UseNpgsql("User ID=foxnet;Password=PVjY%XcF6xuxY@VR;Host=173.82.243.91;Port=5432;Database=insightderm;Pooling=true;");
	        var dbContext = new InsightDermContext(builder.Options);
	        
	        //DbInitializer.Initialize(dbContext);
	        
	        return dbContext;
        }
    }
}
