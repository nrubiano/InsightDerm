using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class MaritalStatusDto
    {
        [Key]
        public Guid Id;

        [Required] 
        [MaxLength(100)]
        public string Description;
    }
}