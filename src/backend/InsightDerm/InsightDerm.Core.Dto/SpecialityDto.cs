using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class SpecialityDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        
        public IList<DoctorDto> Doctors { get; set; }
    }
}