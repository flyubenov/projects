using Microsoft.AspNetCore.Mvc;
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

        public TripsController(IWorldRepository repo)
        {
            _repo = repo;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllTrips());
        }  
        
        [HttpPost("")]
        public IActionResult Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                return Created($"api/trip{theTrip.Name}", theTrip);
            }

            return BadRequest("Bad data.");
        }
                 
    }
}
