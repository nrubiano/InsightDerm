using System;

namespace InsightDerm.Core.Dto
{
    public class DiagnosticImageDto
    {
        public Guid Id { get; set; }

        public Guid ConsultationId { get; set; }

        public string Image { get; set; }
    }
}
