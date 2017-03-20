using System;
using InsightDerm.Core.Data.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsightDerm.Core.Data.Entities
{
	public static class ServiceCollectionExtensions
	{
		public static void AddEntityFramework(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<InsightDermContext>(options =>
					options.UseNpgsql(connectionString));
		}
	}
}
