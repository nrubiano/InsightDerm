using System;

namespace InsightDerm.Core.Dto
{
    public class MedicalCenterDto
    {
		public Guid? Id { get; set; }

		public string Name { get; set; }

		public Guid? CityId { get; set; }

		public virtual CityDto City { get; set; }
    }
}
