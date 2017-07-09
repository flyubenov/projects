using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repo;
        private ILogger _logger;

        public TripsController(IWorldRepository repo, ILogger<TripsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            var trips = _repo.GetAllTrips();
            return Ok(Mapper.Map<IEnumerable<TripViewModel>>(trips));
        }  
        
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repo.AddTrip(newTrip);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"api/trip/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Unable to save trip");
                }               
            }

            return BadRequest("Bad data.");
        }
                 
    }
}
