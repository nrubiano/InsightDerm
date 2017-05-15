using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class Patient
    {

        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Identification { get; set; }

        public DateTime BirthDate { get; set; }

        [MaxLength(255)]
        public string Occupation { get; set; } 

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string CellPhone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

    }
}
