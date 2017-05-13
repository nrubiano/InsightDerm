using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class CIE10
    {
        [Key]
        public Guid Id { get; set; }

        public int Ref { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
    }
}
