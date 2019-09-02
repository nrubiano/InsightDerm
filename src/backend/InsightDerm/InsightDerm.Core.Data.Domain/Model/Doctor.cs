using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Identification { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string CellPhone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
        
        [Required]
        public Guid MedicalCenterId { get; set; }
        
        [Required]
        public Guid SpecialityId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public virtual MedicalCenter MedicalCenter { get; set; }
        
        public virtual Speciality Speciality { get; set; }

        public virtual User User { get; set; }
    }
}
