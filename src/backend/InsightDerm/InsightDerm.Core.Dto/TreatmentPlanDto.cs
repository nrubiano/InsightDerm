using System;

namespace InsightDerm.Core.Dto
{
    public class TreatmentPlanDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual MedicalHistoryDto MedicalHistory { get; set; }
    }
}
