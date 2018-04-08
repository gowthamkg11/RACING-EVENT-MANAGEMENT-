using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NUS.TheAmagingRace.EF
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public int ContactNo { get; set; }
        public string email { get; set; }
        public virtual Team Team { get; set; }
    }
}