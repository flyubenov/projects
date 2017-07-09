using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repo;
        private ILogger _logger;

        public StopsController(IWorldRepository repo, ILogger<StopsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            var trip = _repo.GetTripByName(tripName);
            return Ok(trip.Stops);
        }

        public async Task<IActionResult> AddStop(string tripName, Stop stop)
        {
            _repo.AddStop(tripName, stop);

            if(await _repo.SaveChangesAsync())
            {
                return Created($"/api/trips/{tripName}/stops{stop.Name}", stop);
            }

            return BadRequest("stop not added");
        }

    }
}
