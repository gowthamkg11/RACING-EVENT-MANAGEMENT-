using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NUS.TheAmagingRace.DAL
{
    public class Team: BaseEntity
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<TARUser> Members { get; set; }
    }
}