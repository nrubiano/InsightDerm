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
    public class PatientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PatientService _patientService;

        public PatientsController(PatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Request.Query.TryGetValue("$filter", out var filter);

            var entities = _patientService.GetAll(filter, "");

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _patientService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(PatientDto patient)
        {
            patient = _patientService.Create(patient);

            return Ok(patient);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, PatientDto patient)
        {
            var model = _patientService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            model = _mapper.Map(patient, model);

            _patientService.Update(model);

            return Ok(patient);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Put(Guid id, JsonPatchDocument<PatientDto> patient)
        {
            var model = _patientService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            patient.ApplyTo(model);

            _patientService.Update(model);

            return Ok(patient);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var model = _patientService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            _patientService.Remove(model);

            return Ok();
        }
    }
}