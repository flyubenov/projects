using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}