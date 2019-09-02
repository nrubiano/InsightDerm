using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class ConsultationDiagnosisDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ConsultationId { get; set; }

        [Required]
        public Guid ById { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual DoctorDto By { get; set; }
    }
}
