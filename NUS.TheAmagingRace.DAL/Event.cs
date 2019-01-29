using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace NUS.TheAmagingRace.DAL
{
    public class Event: BaseEntity
    {
        
        [Key]
        public int EventID { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

       
        [Display(Name = "Description")]
        public string EventDescription { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string EventCountry { get; set; }

        [Required]
        [Display(Name = "City")]
        public string EventCity { get; set; }
        [Required]
        [DataType(DataType.Date,ErrorMessage ="Enter Correct Format")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter Correct Format")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<PitStop> PitStops { get; set; }

    }
}