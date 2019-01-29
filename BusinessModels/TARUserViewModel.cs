
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.BusinessModels
{
    public class TARUserViewModel
    {
        public string Id { get; set; }
        public string Password { get; set;}
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }
        public bool isAssigned { get; set; }

    }
}
