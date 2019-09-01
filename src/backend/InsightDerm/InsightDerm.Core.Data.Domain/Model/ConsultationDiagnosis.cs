using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class ConsultationDiagnosis
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ConsultationId { get; set; }

        [Required]
        public Guid By { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual Consultation Consultation { get; set; }

        public virtual List<MedicalLaboratory> MedicalLaboratories { get; set; }

        public virtual ConsultationTreatment Treatment { get; set; }
    }
}