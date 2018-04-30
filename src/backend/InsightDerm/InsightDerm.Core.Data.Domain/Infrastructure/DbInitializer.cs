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

            // Look for any doctors.
            if (context.Doctors.Any())
            {
                context.Doctors.RemoveRange(context.Doctors.ToList());
            }

            var doctors = new List<Doctor> {
                new Doctor(){
                    Id = Guid.NewGuid(),
                    Name = "Nicolas Rubiano"
                }
            };
            context.Doctors.AddRange(doctors);

            
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
            
            context.SaveChanges();
        }
    }
}
