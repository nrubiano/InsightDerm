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
                return; // DB has been seeded
            }

            var doctors = new List<Doctor> {
                new Doctor(){
                    Id = Guid.NewGuid(),
                    Name = "Nicolas Rubiano"
                }
            };

            context.Doctors.AddRange(doctors);

            context.SaveChanges();
        }
    }
}
