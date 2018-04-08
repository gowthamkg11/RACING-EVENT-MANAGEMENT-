using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.EF
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public int TotalPitStops { get; set; }
        public int TotalTeams { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}