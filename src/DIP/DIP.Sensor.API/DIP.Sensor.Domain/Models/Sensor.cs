using DIP.Core.Domain;

namespace DIP.Sensor.Domain.Models
{
    public class Sensor:Entity, IAggregateRoot
    {
        public string Nome { get; set; }
    }
}
