using System;

namespace InsightDerm.Core.Dto
{
    public class DiagnosticDto
    {
        public int DoctorId { get; set; }

        public string Description { get; set; }

        public DateTime DiagnosticDate { get; set; }

        public MedicalHistoryDto MedicalHistory { get; set; }
        public Cie10Dto CIE10 { get; set; }
    }
}
