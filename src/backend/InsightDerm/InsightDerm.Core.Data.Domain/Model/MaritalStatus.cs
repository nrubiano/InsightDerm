using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class MaritalStatus
    {
        [Key] 
        public Guid Id { get; set; }

        [Required] 
        [MaxLength(100)]
        public string Description { set; get; }
    }
}