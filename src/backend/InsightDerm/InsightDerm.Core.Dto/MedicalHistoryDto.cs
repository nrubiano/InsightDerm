using System;

namespace InsightDerm.Core.Dto
{
    public class MedicalHistoryDto
    {
        public Guid Id { get; set; }

        public virtual PatientDto Patient { get; set; }
    }
}