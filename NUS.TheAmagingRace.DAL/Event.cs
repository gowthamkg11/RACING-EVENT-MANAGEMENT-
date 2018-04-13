using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.DAL
{
    public class Event: BaseEntity
    {
        [Key]
        public int EventID { get; set; }


        [Display(Name = "Event Name")]
        public string EventName { get; set; }


        [Display(Name = "Description")]
        public string EventDescription { get; set; }


        [Display(Name = "Country")]
        public string EventCountry { get; set; }


        [Display(Name = "City")]
        public string EventCity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Range(1, 5)]
        [Display(Name = "PitStops")]
        public int TotalPitStops { get; set; }

        [Range(1, 10)]
        [Display(Name = "Teams")]
        public int TotalTeams { get; set; }


        public virtual ICollection<Team> Teams { get; set; }
    }
}