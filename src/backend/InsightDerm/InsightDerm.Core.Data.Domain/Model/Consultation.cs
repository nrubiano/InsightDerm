using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class Consultation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public Guid RequestedById { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Reason { get; set; }

        [Required]
        [MaxLength(4000)]
        public string MedicalBackground { get; set; }

        [MaxLength(1000)]
        public string PhysicalExam { get; set; }

        [Required]
        public ConsultationStatus Status { get; set; }

        public Patient Patient { get; set; }

        public Doctor RequestedBy { get; set; }

        public List<ConsultationDiagnosis> ConsultationDiagnoses { get; set; }

        public List<DiagnosticImage> DiagnosticImages { get; set; }
    }
}
