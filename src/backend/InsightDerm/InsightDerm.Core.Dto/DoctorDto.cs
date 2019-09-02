using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class DoctorDto
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Identification { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CellPhone { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public Guid MedicalCenterId { get; set; }
        
        [Required]
        public Guid SpecialityId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        
        public virtual MedicalCenterDto MedicalCenter { get; set; }
        
        public virtual SpecialityDto Speciality { get; set; }
    }
}
