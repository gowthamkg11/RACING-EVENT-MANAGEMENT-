using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.DAL
{
    public class PitStop:BaseEntity
    {
        [Key]
        public int PitStopID { get; set; }

        public string PitStopName { get; set; }

        public string StaffId { get; set; }

        public int SequenceNumber { get; set; }

        public string Address { get; set; }

        public virtual Location Location { get; set; }

        public virtual Event Event { get; set; }
    }
}