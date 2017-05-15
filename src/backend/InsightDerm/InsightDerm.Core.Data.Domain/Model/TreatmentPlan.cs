using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class TreatmentPlan
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Guid MedicalHistoryId { get; set; }

        public virtual MedicalHistory MedicalHistory { get; set; }
    }
}
