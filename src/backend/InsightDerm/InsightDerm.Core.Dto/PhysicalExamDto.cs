using System;

namespace InsightDerm.Core.Dto
{
    public class PhysicalExamDto
    {
        public Guid Id { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Temperature { get; set; }

        public string Description { get; set; }

        public DateTime ExamDate { get; set; }

        public virtual MedicalHistoryDto MedicalHistory { get; set; }
    }
}
