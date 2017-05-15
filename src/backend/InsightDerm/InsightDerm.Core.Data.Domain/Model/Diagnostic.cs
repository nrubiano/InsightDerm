using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class Diagnostic
    {
        [Key]
        public Guid MedicalHistoryId { get; set; }

        [Key]
        public Guid Cie10Id { get; set; }

        public int DoctorId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime DiagnosticDate { get; set; }

        public MedicalHistory MedicalHistory { get; set; }
        public Cie10 Cie10 { get; set; }
        
    }
}
