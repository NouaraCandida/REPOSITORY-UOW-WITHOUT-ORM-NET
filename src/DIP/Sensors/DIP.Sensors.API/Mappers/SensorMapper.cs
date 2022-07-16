using DIP.Core.Validate;
using DIP.Sensors.API.DTO;
using DIP.Sensors.Domain.Models;
using Microsoft.Extensions.Localization;

namespace DIP.Sensors.API.Mappers
{
    public class SensorMapper
    {
        public static Sensor ToSensor(IValidate validate, IStringLocalizer stringLocalizer, SensorDTO sensorDTO)
        {
            var sensor = new Sensor
            {
                Id = sensorDTO.Id,
                Name = sensorDTO.Name,
                NameCountry = sensorDTO.Locality.NameCountryEnum,
                NameRegion = sensorDTO.Locality.NameRegionEnum
            };

            return sensor;
        }
    }
}
