using DIP.Core.Repository;
using DIP.Sensors.Domain.Models;

namespace DIP.Sensors.Domain.Repositorys
{
    public interface IEventRepository : IRepositoryBase<Event, OptionsSearch>
    {
    }
}
