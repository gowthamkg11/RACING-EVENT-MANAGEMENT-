using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.BusinessModels
{
    public class PitStopViewModel
    {
        public int PitStopID { get; set; }

        public string PitStopName { get; set; }

        public string StaffId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int SequenceNumber { get; set; }

        public string Address { get; set; }

        public virtual EventViewModel Event { get; set; }
    }
}
