using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL
{
    public class StaffLocation
    {
        public int StaffID { get; set; }

        public List<ApiLocation> Location { get; set; }
    }
}
