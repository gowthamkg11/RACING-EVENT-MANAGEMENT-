using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace NUS.TheAmagingRace.DAL
{
    public class Team: BaseEntity
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        [Display(Name ="Distance(KM)")]
        public string Distance { get; set; }
        [Display(Name ="Time(Minutes)")]
        public string Time { get; set; }

        public int Position { get; set; }

        public string NextPitStop { get; set; }
        public virtual Event Event { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}