using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class CurrentIllness
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public Guid MedicalHistoryId { get; set; }

        public virtual MedicalHistory MedicalHistory { get; set; }
    }
}
