using System;

namespace InsightDerm.Core.Dto
{
    public class MedicalLaboratoryDto
    {
        public Guid Id { get; set; }
   
        public Guid ConsultationDiagnosisId { get; set; }
        
        public DateTime RequestedDate { get; set; }
        
        public Guid RequestedById { get; set; }
        
        public Guid TypeId { get; set; }                        
    }
}
