using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.BAL
{
    public class PitStopBAL
    {
        private TARDBContext db = new TARDBContext();
        public List<PitStop> GetPitStopList()
        {
            return db.PitStops.ToList(); ;
        }

        public List<PitStop> CreatePitStopList(PitStop pitStop, string currentUser)
        {
            if (pitStop.PitStopID > 0)
            {
                PitStop editPitStops = db.PitStops.SingleOrDefault(x => x.PitStopID == pitStop.PitStopID);
                editPitStops.PitStopName = pitStop.PitStopName;
                editPitStops.SequenceNumber = pitStop.SequenceNumber;
                editPitStops.Address = pitStop.Address;
                editPitStops.LastModifiedBy = currentUser;
                editPitStops.LastModifiedAt = DateTime.Now;
                db.SaveChanges();

                pitStop.CreatedBy = currentUser;
                //db.PitStops.Add(pitStop);

                db.SaveChanges();
            }
            else
            {
                pitStop.CreatedBy = currentUser;
                db.PitStops.Add(pitStop);

                db.SaveChanges();
            }

            return db.PitStops.ToList();
        }

        public List<PitStop> getPitStopOfEvent(int eventId)
        {
            var pitstops = from s in db.PitStops
                         select s;
            pitstops = pitstops.Where(s => s.Event.EventID == eventId);
            
            return pitstops.ToList();
        }

    }
}
