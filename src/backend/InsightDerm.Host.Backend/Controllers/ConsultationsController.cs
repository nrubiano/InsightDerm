using System;
using AutoMapper;
using InsightDerm.Core.Dto;
using InsightDerm.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public partial class ConsultationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConsultationService _consultationService;
        private readonly DiagnosticImageService _diagnosticImageService;

        public ConsultationsController(ConsultationService consultationService, 
                                        DiagnosticImageService diagnosticImageService,
                                        IMapper mapper)
        {
            _consultationService = consultationService;
            _diagnosticImageService = diagnosticImageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = _consultationService.GetAll(null);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _consultationService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(ConsultationDto consultation)
        {
            consultation.CreationDate = DateTime.Now;
            consultation.RequestedById = Guid.Parse("7ed9364b-418b-481b-b8a7-c7f5bb3b5f7b");

            consultation = _consultationService.Create(consultation);

            return Ok(consultation);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, ConsultationDto consultation)
        {
            var model = _consultationService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            model = _mapper.Map(consultation, model);

            _consultationService.Update(model);

            return Ok(consultation);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Put(Guid id, JsonPatchDocument<ConsultationDto> consultation)
        {
            var model = _consultationService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            consultation.ApplyTo(model);

            _consultationService.Update(model);

            return Ok(consultation);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var model = _consultationService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            _consultationService.Remove(model);

            return Ok();
        }
    }
}