using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL
{
    internal interface IAuditedEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime LastModifiedAt { get; set; }
    }
   
}
