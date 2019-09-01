using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class MedicalLaboratory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ConsultationDiagnosisId { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }

        [Required]
        public Guid RequestedById { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        public ConsultationDiagnosis ConsultationDiagnosis { get; set; }

        public Doctor RequestedBy { get; set; }

        public MedicalLaboratoryType Type { get; set; }
    }
}
