using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class DoctorDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Identification { get; set; }

        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }
        
        [Required]
        public Guid MedicalCenterId { get; set; }
        
        [Required]
        public Guid SpecialityId { get; set; }
        
        [Required]
        public virtual MedicalCenterDto MedicalCenter { get; set; }
        
        [Required]
        public virtual SpecialityDto Speciality { get; set; }  
    }
}
