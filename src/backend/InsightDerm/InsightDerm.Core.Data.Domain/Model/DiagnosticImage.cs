using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class DiagnosticImage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ConsultationId { get; set; }

        [Required]
        public string Image { get; set; }

        public Consultation Consultation { get; set; }
    }
}
