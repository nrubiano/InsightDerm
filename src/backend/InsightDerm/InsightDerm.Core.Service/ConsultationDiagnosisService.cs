﻿using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Data.Domain.Model;
using InsightDerm.Core.Dto;

namespace InsightDerm.Core.Service
{
    public class ConsultationDiagnosisService : BaseService<ConsultationDiagnosis, ConsultationDiagnosisDto>
    {
        public ConsultationDiagnosisService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
