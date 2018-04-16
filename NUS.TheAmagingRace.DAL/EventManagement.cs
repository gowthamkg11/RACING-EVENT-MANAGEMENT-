using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL
{
    public class EventManagement
    {
        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<PitStop> PitStops { get; set; }
    }
}
