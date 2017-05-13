using System;

namespace InsightDerm.Core.Dto
{
    public class PatientDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Identification { get; set; }

        public DateTime BirthDate { get; set; }

        public string Occupation { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }
    }
}
