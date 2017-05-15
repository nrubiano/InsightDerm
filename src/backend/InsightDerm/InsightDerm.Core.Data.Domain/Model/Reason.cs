using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class Reason
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime ReasonDate { get; set; }

        public Guid MedicalHistoryId { get; set; }

        public virtual MedicalHistory MedicalHistory { get; set; }
    }
}
