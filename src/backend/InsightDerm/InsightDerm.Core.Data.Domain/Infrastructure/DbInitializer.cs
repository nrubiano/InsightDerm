using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsightDerm.Core.Data.Domain.Model;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(InsightDermContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cities.Any())
            {
                context.Cities.RemoveRange(context.Cities.ToList());
            }
            
            var cities = new List<City>
            {
                new City
                {
                    Id = Guid.Parse("9dd7d670-f839-46a3-b182-95e766aaf88b"),
                    Name = "Bogotá"
                },
                new City
                {
                    Id = Guid.Parse("1c2e4b4d-058f-4d5b-846f-eee8e5ec67b4"),
                    Name = "Medellin"
                },
                new City
                {
                    Id = Guid.Parse("739ed61c-6bc1-4bf3-9aac-1677c18cd6de"),
                    Name = "Cali"
                }
            };            
            context.Cities.AddRange(cities);
            
            
            if (context.MaritalStatuses.Any())
            {
                context.MaritalStatuses.RemoveRange(context.MaritalStatuses.ToList());
            }

            var maritalStatuses = new List<MaritalStatus>
            {
                new MaritalStatus
                {
                    Id = Guid.NewGuid(),
                    Description = "Soltero/a"
                },
                new MaritalStatus
                {
                    Id = Guid.NewGuid(),
                    Description = "Casado/a"
                },
                new MaritalStatus
                {
                    Id = Guid.NewGuid(),
                    Description = "Separado/a"
                },
                new MaritalStatus
                {
                    Id = Guid.NewGuid(),
                    Description = "Union Libre"
                },
                new MaritalStatus
                {
                    Id = Guid.NewGuid(),
                    Description = "Viudo/a"
                }
            };
            context.MaritalStatuses.AddRange(maritalStatuses);
            
            
            if (context.Specialities.Any())
            {
                context.Specialities.RemoveRange(context.Specialities.ToList());
            }

            var specialities = new List<Speciality>
            {
                new Speciality()
                {
                    Id = Guid.NewGuid(),
                    Name = "Medicina General",
                    Description = "Medicina General"
                },
                new Speciality()
                {
                    Id = Guid.NewGuid(),
                    Name = "Dermatología",
                    Description = "Especilista en Dermatología"
                }
            };
            context.Specialities.AddRange(specialities);
            
            context.SaveChanges();
        }
    }
}
