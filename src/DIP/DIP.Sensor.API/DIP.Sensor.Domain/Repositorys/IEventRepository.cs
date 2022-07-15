using DIP.Core.Repository;
using DIP.Domain.Models;

namespace DIP.Sensor.Domain.Repositorys
{
    public interface IEventRepository: IRepositoryBase<Event,OptionsSearch>
    {
    }
}
