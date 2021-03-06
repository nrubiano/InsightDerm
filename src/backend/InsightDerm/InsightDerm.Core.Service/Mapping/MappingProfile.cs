﻿using AutoMapper;
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
            
            CreateMap<Consultation, ConsultationDto>();
            CreateMap<ConsultationDto, Consultation>()
                .ForMember(mn => mn.ConsultationDiagnoses, con => con.Ignore())
                .ForMember(mn => mn.DiagnosticImages, con => con.Ignore());

            CreateMap<MedicalLaboratory, MedicalLaboratoryDto>();
            CreateMap<MedicalLaboratoryDto, MedicalLaboratory>()
                .ForMember(mn => mn.ConsultationDiagnosis, con => con.Ignore())
                .ForMember(mn => mn.Type, con => con.Ignore())
                .ForMember(mn => mn.RequestedBy, con => con.Ignore());

            CreateMap<Speciality, SpecialityDto>();
            CreateMap<SpecialityDto, Speciality>();
            
			CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>()
                .ForMember(mn => mn.User, con => con.Ignore());

            CreateMap<MedicalCenter, MedicalCenterDto>();
            CreateMap<MedicalCenterDto, MedicalCenter>()
                .ForMember(mn => mn.CityId, con => con.Condition(c => c.CityId.HasValue));
            
            CreateMap<MaritalStatus, MaritalStatusDto>();
            CreateMap<MaritalStatusDto, MaritalStatus>();

            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();

            CreateMap<MedicalLaboratoryType, MedicalLaboratoryTypeDto>();
            CreateMap<MedicalLaboratoryTypeDto, MedicalLaboratoryType>();

            CreateMap<ConsultationDiagnosis, ConsultationDiagnosisDto>();
            CreateMap<ConsultationDiagnosisDto, ConsultationDiagnosis>()
                .ForMember(mn => mn.Consultation, con => con.Ignore())
                .ForMember(mn => mn.Treatment, con => con.Ignore())
                .ForMember(mn => mn.MedicalLaboratories, con => con.Ignore())
                .ForMember(mn => mn.By, con => con.Ignore());

            CreateMap<ConsultationTreatment, ConsultationTreatmentDto>();
            CreateMap<ConsultationTreatmentDto, ConsultationTreatment>()
                .ForMember(mn => mn.Diagnosis, con => con.Ignore())
                .ForMember(mn => mn.By, con => con.Ignore());

            CreateMap<DiagnosticImage, DiagnosticImageDto>();
            CreateMap<DiagnosticImageDto, DiagnosticImage>()
                .ForMember(mn => mn.Consultation, con => con.Ignore());
        }
    }
}
