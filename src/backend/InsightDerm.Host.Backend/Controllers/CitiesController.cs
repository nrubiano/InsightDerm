using System;
using InsightDerm.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace InsightDerm.Host.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private CityService _citiesService;

        public CitiesController(CityService citiesService)
        {
            _citiesService = citiesService;
        }

        public IActionResult Get()
        {
            var entities = _citiesService.GetAll(null);

            return Ok(entities);
        }

        [Route("{id}")]
        public IActionResult GetSingle(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var entity = _citiesService.GetSingle(x => x.Id == id);

            if (entity != null)
                return Ok(entity);

            return NotFound();
        }
    }
}