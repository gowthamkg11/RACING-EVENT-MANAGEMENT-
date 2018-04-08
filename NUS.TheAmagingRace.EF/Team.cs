using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NUS.TheAmagingRace.EF
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public virtual Event Event { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}