using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class ConsultationTreatment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ConsultationDiagnosisId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid ById { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual Doctor By { get; set; }

        public virtual ConsultationDiagnosis Diagnosis { get; set; }    
    }
}