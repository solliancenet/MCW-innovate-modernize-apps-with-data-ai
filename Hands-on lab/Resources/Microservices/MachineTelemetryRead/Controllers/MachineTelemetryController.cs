using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Common.DTOs;
using MachineTelemetryRead.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MachineTelemetryRead.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineTelemetryController
    {
        private IEventRepository _eventRepository;

        public MachineTelemetryController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<List<MachineTelemetryDto>> GetAsync([FromQuery] int limit = 100, [FromQuery] int skip = 0)
        {
            return (await _eventRepository.GetAllEventsAsync(limit, skip))
                .Select(e =>
                {
                    var eventData = JsonSerializer.Deserialize<MachineTelemetryEventData>(e.EventData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    
                    return new MachineTelemetryDto
                    {
                        Id = e.Id,
                        MachineId = e.MachineId,
                        EventId = Guid.Parse(e.EventId),
                        EventType = e.EventType,
                        EntityType = e.EntityType,
                        EntityId = Guid.Parse(e.EntityId),
                        FactoryId = eventData.FactoryId,
                        Machine = new MachineData
                        {
                            Temperature = eventData.Machine.Temperature,
                            Pressure = eventData.Machine.Pressure,
                            ElectricityUtilization = eventData.Machine.ElectricityUtilization,
                        },
                        Ambient = new MachineAmbientData
                        {
                            Temperature = eventData.Ambient.Temperature,
                            Humidity = eventData.Ambient.Humidity,
                        },
                        TimeCreated = DateTime.Parse(eventData.TimeCreated),
                    };
                })
                .ToList();
        }
    }
}