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
    public class DoctorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = _doctorService.GetAll(null);

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _doctorService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(DoctorDto doctor)
        {
            doctor = _doctorService.Create(doctor);

            return Ok(doctor);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, DoctorDto doctor)
        {
            var model = _doctorService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            model = _mapper.Map(doctor, model);

            _doctorService.Update(model);

            return Ok(doctor);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Put(Guid id, JsonPatchDocument<DoctorDto> doctor)
        {
            var model = _doctorService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            doctor.ApplyTo(model);

            _doctorService.Update(model);

            return Ok(doctor);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var model = _doctorService.GetSingle(x => x.Id == id);

            if (model == null)
                return NotFound();

            _doctorService.Remove(model);

            return Ok();
        }
    }
}