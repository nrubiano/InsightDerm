using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class MedicalHistory
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
