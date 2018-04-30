using System;
using AutoMapper;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;

namespace InsightDerm.Core.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ForAllMaps((tm, me) => me.ForAllMembers(mo => mo.Condition((src, target, srcValue, ctx) => srcValue != null)));

            CreateMap<City, CityDto>();

			CreateMap<Doctor, DoctorDto>();

            CreateMap<MedicalCenter, MedicalCenterDto>();

            CreateMap<MedicalCenterDto, MedicalCenter>();

            CreateMap<MedicalCenterDto, MedicalCenterDto>()
                .ForMember(mn => mn.CityId, con => con.Condition(c => c.CityId.HasValue));
            
            CreateMap<MaritalStatusDto, MaritalStatus>();

            CreateMap<MaritalStatus, MaritalStatusDto>();
            
            CreateMap<Patient, PatientDto>();
            
            CreateMap<PatientDto, Patient>();
        }
    }
}
