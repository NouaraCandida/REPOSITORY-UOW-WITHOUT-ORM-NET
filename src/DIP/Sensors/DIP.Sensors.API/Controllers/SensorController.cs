using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIP.Core.Validate;
using DIP.Sensors.API.DTO;
using DIP.Sensors.API.Mappers;
using DIP.Sensors.Domain.Models;
using DIP.Sensors.Domain.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace DIP.Sensors.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {

        private readonly ILogger<SensorController> _logger;
        private readonly IUoWApplication _uoW;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IValidate _validate;

        public SensorController(ILogger<SensorController> logger, IUoWApplication uoW, IStringLocalizer stringLocalizer)
        {

            _logger = logger;
            _uoW = uoW;
            _stringLocalizer = stringLocalizer;
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Guid> PostSensor([FromBody] SensorDTO sensor)
        {
            try
            {
                if (sensor == null) return BadRequest(_stringLocalizer["SensorNotNull"]);
                sensor.Id = Guid.NewGuid();
                _uoW.BeginTransaction();
                _uoW.SensorRepository.Insert(SensorMapper.ToSensor(_validate, _stringLocalizer, sensor));
                _uoW.Commit();
                _uoW.Dispose();
                return Created(nameof(GetSensor),sensor.Id);
            }
            catch (Exception ex)
            {
                return this.CreateExceptionExtension(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SensorDTO> GetSensor(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid idG)) return NotFound(_stringLocalizer["SensorNotFound"]);
                _uoW.BeginTransaction();
                var sensor = _uoW.SensorRepository.Get(idG);

                _uoW.Dispose();
                return Ok(SensorDTO.ToSensorDTO(_validate, _stringLocalizer, sensor));


            }
            catch (Exception ex)
            {
                return this.CreateExceptionExtension(ex);
            }
        }
    }
}
