using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class PatientDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(12)]
        public string IdentificationType { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string IdentificationNumber { get; set; }

        [Required]
        [MaxLength(12)]
        public string Genre { get; set; }

        [Required]
        [MaxLength(255)]
        public DateTime BornDate { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Occupation { get; set; }

        [Required]  
        [MaxLength(500)]
        public string Address { get; set; }
        
        [MaxLength(100)]
        public string Phone { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string MobilePhone { get; set; }

        [Required]
        [MaxLength(250)]
        public string Mail { get; set; }
        
        [Required]
        public Guid MaritalStatusId { get; set; }
    }
}