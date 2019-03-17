using System;

namespace InsightDerm.Core.Dto
{
    public class ConsultationDto
    {
        public Guid Id { get; set; }
        
        public Guid PatientId { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public Guid RequestedById { get; set; }

        public string Reason { get; set; }

        public string MedicalBackground { get; set; }

        public string PhysicalExam { get; set; }

        public PatientDto Patient { get; set; }

        public DoctorDto RequestedBy { get; set; }
    }
}
