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
    public class MedicalLaboratoryTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MedicalLaboratoryTypeService _medicalLaboratoryTypeService;

        public MedicalLaboratoryTypesController(MedicalLaboratoryTypeService medicalLaboratoryTypeService, IMapper mapper)
        {
            _medicalLaboratoryTypeService = medicalLaboratoryTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = _medicalLaboratoryTypeService.GetAll(null);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _medicalLaboratoryTypeService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(MedicalLaboratoryTypeDto medicalLaboratoryType)
        {
            medicalLaboratoryType = _medicalLaboratoryTypeService.Create(medicalLaboratoryType);

            return Ok(medicalLaboratoryType);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, MedicalLaboratoryTypeDto medicalLaboratoryType)
        {
            var model = _medicalLaboratoryTypeService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            model = _mapper.Map(medicalLaboratoryType, model);

            _medicalLaboratoryTypeService.Update(model);

            return Ok(medicalLaboratoryType);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Put(Guid id, JsonPatchDocument<MedicalLaboratoryTypeDto> medicalLaboratoryType)
        {
            var model = _medicalLaboratoryTypeService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            medicalLaboratoryType.ApplyTo(model);

            _medicalLaboratoryTypeService.Update(model);

            return Ok(medicalLaboratoryType);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var model = _medicalLaboratoryTypeService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            _medicalLaboratoryTypeService.Remove(model);

            return Ok();
        }
    }
}