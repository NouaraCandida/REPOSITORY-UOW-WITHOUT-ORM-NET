using System;
using DIP.Core.Domain;

namespace DIP.Sensors.Domain.Models
{
    public class Sensor : Entity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RegionEnum NameRegion { get; set; }
        public CountryEnum NameCountry { get; set; }
    }
}
