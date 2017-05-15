using System;

namespace InsightDerm.Core.Dto
{
    public class ReasonDto
    {
        public Guid Id { get; set; }
        
        public DateTime ReasonDate { get; set; }

        public virtual MedicalHistoryDto MedicalHistory { get; set; }
    }
}
