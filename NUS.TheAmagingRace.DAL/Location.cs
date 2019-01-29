using System;
using System.ComponentModel.DataAnnotations;

namespace NUS.TheAmagingRace.DAL
{
    public class Location: BaseEntity
    {
        [Key]
        public int LocationId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Distance { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime TimeCovered { get; set; }
    }
}