using System.Collections.Generic;
using System.Threading.Tasks;
using MachineTelemetryRead.Models;

namespace MachineTelemetryRead.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event1>> GetAllEventsAsync(int limit, int skip);
    }
}