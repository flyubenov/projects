using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.ViewModels
{
    public class DynamicMenuViewModel
    {
        public IEnumerable<TheWorld.Models.Trip> trips;
        public Menu menu;
    }
}
