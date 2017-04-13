using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class MedicalCenter
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public Guid CityId { get; set; }

        public virtual City City { get; set; }
    }
}
