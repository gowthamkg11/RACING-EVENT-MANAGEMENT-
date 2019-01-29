using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL
{
    public class Member
    {
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public virtual Event Event { get; set; }

        public virtual Team Team { get; set; }
    }
}
