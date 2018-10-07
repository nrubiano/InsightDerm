using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;

namespace InsightDerm.Core.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {       
            CreateMap<City, CityDto>();
            
            CreateMap<CityDto, City>();

			CreateMap<Doctor, DoctorDto>();
            
            CreateMap<DoctorDto, Doctor>();

            CreateMap<MedicalCenter, MedicalCenterDto>();

            CreateMap<MedicalCenterDto, MedicalCenter>()
                .ForMember(mn => mn.CityId, con => con.Condition(c => c.CityId.HasValue));
            
            CreateMap<MaritalStatus, MaritalStatusDto>();
            
            CreateMap<MaritalStatusDto, MaritalStatus>();

            CreateMap<Patient, PatientDto>();
            
            CreateMap<PatientDto, Patient>();                       
        }
    }
}
