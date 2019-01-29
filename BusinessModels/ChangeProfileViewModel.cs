using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NUS.TheAmagingRace.BusinessModels
{
    public class ChangeProfileViewModel
    {
        [Required]
        public string Name { get; set; }
        [Display(Name = "Choose Profile Pic")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}
