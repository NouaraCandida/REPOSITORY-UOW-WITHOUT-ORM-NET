using DIP.Core.Repository;

namespace DIP.Sensors.Domain.Repositorys
{
    public interface IUoWApplication : IUnitOfWork
    {
        public ISensorRepository SensorRepository { get; }


    }
}
