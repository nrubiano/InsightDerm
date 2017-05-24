using System;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class DoctorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Identification { get; set; }

        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }
    }
}
