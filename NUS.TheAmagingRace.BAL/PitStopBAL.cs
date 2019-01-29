using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.BAL
{
    public class PitStopBAL
    {
        private TARDBContext db = new TARDBContext();
        private PitStop pitStop = new PitStop();

        /*<summary>
          * get all pitStops
        </summary>
        <returns>
          returns all pitStops ordered by sequence number
        </returns>*/
        public List<PitStop> GetPitStopList()
        {
            return db.PitStops.OrderBy(seq=>seq.SequenceNumber).ToList(); ;
        }

        /*<summary>
          * Create and edit pitStops for the eventId
        </summary>
        <returns>
          List of pitStops created/updated
        </returns>*/
        /// <param name="pitStop"></param>
        /// <param name="currentUser"></param>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        public List<PitStop> CreatePitStopList(PitStop pitStop, string currentUser, int eventId,string userId)
        {
            if (pitStop.PitStopID > 0)
            {
                PitStop editPitStops = db.PitStops.SingleOrDefault(x => x.PitStopID == pitStop.PitStopID);
                editPitStops.PitStopName = pitStop.PitStopName;
                editPitStops.SequenceNumber = pitStop.SequenceNumber;
                editPitStops.Address = pitStop.Address;
                editPitStops.Latitude = pitStop.Latitude;
                editPitStops.Longitude = pitStop.Longitude;
                editPitStops.LastModifiedBy = currentUser;
                Event currentEvent = db.Events.SingleOrDefault(x => x.EventID == eventId);
                pitStop.Event = currentEvent;


                TARUser staffInfo = db.Users.FirstOrDefault(d => d.Id == userId);
                editPitStops.Staff = staffInfo;

               
                editPitStops.LastModifiedAt = DateTime.Now;
                db.SaveChanges();

                pitStop.CreatedBy = currentUser;
                //db.PitStops.Add(pitStop);

                db.SaveChanges();
            }
            else
            {
                pitStop.CreatedBy = currentUser;
                pitStop.Staff.Id = userId;
                Event currentEvent = db.Events.SingleOrDefault(x => x.EventID == eventId);
                TARUser staffInfo = db.Users.FirstOrDefault(d => d.Id == userId);
                pitStop.Staff = staffInfo;
                pitStop.Event = currentEvent;
                db.PitStops.Add(pitStop);
                db.SaveChanges();
            }

            return getPitStopOfEvent(eventId);
        }

        /*<summary>
          * Fetch list of pitStops for the given eventId
        </summary>
        <returns>
          List of pitStops
        </returns>*/
        /// <param name="eventId"></param>
        public List<PitStop> getPitStopOfEvent(int eventId)
        {
            var pitstops = from s in db.PitStops
                         select s;
            pitstops = pitstops.Where(s => s.Event.EventID == eventId);
            
            return pitstops.OrderBy(seq => seq.SequenceNumber).ToList();
        }

        /*<summary>
          * Fetch list of pitStops and details for the given pitStopId
        </summary>
        <returns>
          List of pitStops and its details
        </returns>*/
        /// <param name="pitStopId"></param>
        public List<PitStop> getPitStopOfPitStopId(int pitStopId)
        {
            var pitstops = from s in db.PitStops
                           select s;
            pitstops = pitstops.Where(s => s.PitStopID == pitStopId);

            return pitstops.OrderBy(seq => seq.SequenceNumber).ToList();
        }

        /*<summary>
          * Fetch pitStop Details for the given pitStopId to edit pitStop
        </summary>
        <returns>
          single pitStop object
        </returns>*/
        /// <param name="pitStopId"></param>
        public PitStop GetValuesToEdit(int pitStopId)
        {
            PitStop editPitStops = db.PitStops.SingleOrDefault(x => x.PitStopID == pitStopId);
            pitStop.PitStopID = editPitStops.PitStopID;
            pitStop.PitStopName = editPitStops.PitStopName;
            pitStop.Address = editPitStops.Address;
            pitStop.SequenceNumber = editPitStops.SequenceNumber;
            
            return pitStop;

        }

        /*<summary>
          * Fetch pitStop and details for the given pitStopId
        </summary>
        <returns>
          single pitStop object
        </returns>*/
        /// <param name="PitSTopId"></param>
        public PitStop GetSelectedPitStop(int PitSTopId)
        {
            PitStop currentPitStop = db.PitStops.SingleOrDefault(x => x.PitStopID == PitSTopId);
            return currentPitStop;

        }

        /*<summary>
          * Delets PitStop from DB
        </summary>
        
        <returns>
          list of pitstops for the event Id
        </returns>*/
        /// <param name="pitStopId"></param>
        /// <param name="eventId"></param>
        public List<PitStop> DeletePitStopfromList(int pitStopId, int eventId)
        {
            var pitStop = db.PitStops.Find(pitStopId);
            PitStop stops = db.PitStops.SingleOrDefault(x => x.PitStopID == pitStopId);
            db.PitStops.Remove(stops);
            db.SaveChanges();
            //result = true;
            return getPitStopOfEvent(eventId);
        }

    }
}
