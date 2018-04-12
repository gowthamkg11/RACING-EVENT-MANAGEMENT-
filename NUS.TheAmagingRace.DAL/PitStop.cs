using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.DAL
{
    public class PitStop:BaseEntity
    {
        [Key]
        public int PitStopID { get; set; }
    }
}