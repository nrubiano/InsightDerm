using System;

namespace InsightDerm.Core.Dto
{
    public class CurrentIllnessDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public virtual MedicalHistoryDto MedicalHistory { get; set; }
    }
}
