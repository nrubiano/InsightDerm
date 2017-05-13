using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class PatientAntecedent
    {
        [Key, Column(Order = 0)]
        public Guid MedicalHistoryId { get; set; }

        [Key, Column(Order = 1)]
        public Guid AntecedentId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        
        public DateTime AntecedentDate { get; set; }

        public MedicalHistory MedicalHistory { get; set; }
        public Antecedent Antecedent { get; set; }
    }
}
