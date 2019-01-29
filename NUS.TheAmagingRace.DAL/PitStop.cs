using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NUS.TheAmagingRace.DAL
{
    public class PitStop:BaseEntity
    {
        [Key]
        public int PitStopID { get; set; }
        [Required(ErrorMessage = "Pitstop Name is mandatory")]
        [Display(Name ="Name")]
        public string PitStopName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Required(ErrorMessage = "Sequence Number is mandatory")]
        [Display(Name = "Order")]

        public int SequenceNumber { get; set; }

        public string Address { get; set; }

        public virtual Event Event { get; set; }
       
        public virtual TARUser Staff { get; set; }
    }
}