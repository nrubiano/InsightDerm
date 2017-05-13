using System;

namespace InsightDerm.Core.Dto
{
    public class PatientAntecedentDto
    {
        public string Description { get; set; }

        public DateTime AntecedentDate { get; set; }

        public MedicalHistoryDto MedicalHistory { get; set; }
        public AntecedentDto Antecedent { get; set; }
    }
}
