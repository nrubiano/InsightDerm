using System;
using AutoMapper;
using InsightDerm.Core.Dto;
using InsightDerm.Core.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalCentersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MedicalCenterService _medicalCenterService;

        public MedicalCentersController(MedicalCenterService medicalCenterService, IMapper mapper)
        {
            _medicalCenterService = medicalCenterService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = _medicalCenterService.GetAll(null);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _medicalCenterService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(MedicalCenterDto medicalCenter)
        {
            medicalCenter = _medicalCenterService.Create(medicalCenter);

            return Ok(medicalCenter);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, MedicalCenterDto medicalCenter)
        {
            var model = _medicalCenterService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            model = _mapper.Map(medicalCenter, model);

            _medicalCenterService.Update(model);

            return Ok(medicalCenter);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Put(Guid id, JsonPatchDocument<MedicalCenterDto> medicalCenter)
        {
            var model = _medicalCenterService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            medicalCenter.ApplyTo(model);

            _medicalCenterService.Update(model);

            return Ok(medicalCenter);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var model = _medicalCenterService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            _medicalCenterService.Remove(model);

            return Ok();
        }
    }
}