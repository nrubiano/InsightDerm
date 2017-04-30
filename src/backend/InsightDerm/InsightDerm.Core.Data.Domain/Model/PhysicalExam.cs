using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class PhysicalExam
    {
        [Key]
        public Guid Id { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Temperature { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime ExamDate { get; set; }

        public Guid MedicalHistoryId { get; set; }

        public virtual MedicalHistory MedicalHistory { get; set; }
    }
}
