using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.EF
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
    }
}