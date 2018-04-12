using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.DAL
{
    public class Location: BaseEntity
    {
        [Key]
        public int LocationId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}