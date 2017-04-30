using System;
using System.ComponentModel.DataAnnotations;

namespace InsightDerm.Core.Data.Domain.Model
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
    }
}
