using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Dto
{
    public class MedicalLaboratoryTypeDto
    {
        public Guid Id { get; set; }
     
        public string Name { get; set; }
 
        public string Notes { get; set; }
    }
}
