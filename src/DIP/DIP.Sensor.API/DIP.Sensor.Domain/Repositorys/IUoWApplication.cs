using DIP.Core.Repository;

namespace DIP.Sensor.Domain.Repositorys
{
    public interface IUoWApplication: IUnitOfWork
    {
        public ISensorRepository SensorRepository { get; }

        
    }
}
