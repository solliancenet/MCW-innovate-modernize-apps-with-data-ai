using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceAggregateRead.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenanceAggregateController
    {
        [HttpGet]
        public async Task<string> GetAsync()
        {
            return "Maintenance Aggregate";
        }
    }
}