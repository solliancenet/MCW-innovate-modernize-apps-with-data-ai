using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineTelemetryRead.Interfaces;
using MachineTelemetryRead.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineTelemetryRead.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly CitusContext _context;

        public EventRepository(CitusContext context)
        {
            _context = context;
        }
        
        public async Task<List<Event1>> GetAllEventsAsync(int limit, int skip)
        {
            return await _context.Event1
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }
    }
}