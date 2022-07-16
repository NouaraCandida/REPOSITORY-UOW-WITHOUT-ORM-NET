using System;
using DIP.Core.Validate;
using DIP.Sensors.Domain.Models;
using Microsoft.Extensions.Localization;

namespace DIP.Sensors.API.DTO
{
    public class SensorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LocalityDTO Locality { get; set; }

        public static SensorDTO ToSensorDTO(IValidate validate, IStringLocalizer stringLocalizer, Sensor sensor)
        {
            var sensorDTO = new SensorDTO
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Locality = new LocalityDTO(validate, stringLocalizer, sensor.NameRegion, sensor.NameCountry),
            };

            return sensorDTO;
        }
    }
}
