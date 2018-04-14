using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.BAL
{
    public class EventBAL : IEventBAL
    {
        private TARDBContext db = new TARDBContext();
        private Event events = new Event();

        public List<Event> GetEventList()
        {
            return db.Events.ToList(); ;
        }

        public List<Event> EditEventList(Event eventModel)
        {
            if (eventModel.EventID > 0)
            {
                Event editEvents = db.Events.SingleOrDefault(x => x.EventID == eventModel.EventID);
                editEvents.EventName = eventModel.EventName;
                editEvents.EventDescription = eventModel.EventDescription;
                editEvents.EventCountry = eventModel.EventCountry;
                editEvents.EventCity = eventModel.EventCity;
                editEvents.StartDate = eventModel.StartDate;
                editEvents.EndDate = eventModel.EndDate;
                editEvents.TotalPitStops = eventModel.TotalPitStops;
                editEvents.TotalTeams = eventModel.TotalTeams;
                db.SaveChanges();
            }
            else
            {
                db.Events.Add(eventModel);
                db.SaveChanges();
            }

            return db.Events.ToList(); ;
        }

        public Event GetEditingValues(int eventId)
        {
            Event editEvents = db.Events.SingleOrDefault(x => x.EventID == eventId);
            events.EventID = editEvents.EventID;
            events.EventName = editEvents.EventName;
            events.EventDescription = editEvents.EventDescription;
            events.EventCountry = editEvents.EventCountry;
            events.EventCity = editEvents.EventCity;
            events.StartDate = editEvents.StartDate;
            events.EndDate = editEvents.EndDate;
            events.TotalPitStops = editEvents.TotalPitStops;
            events.TotalTeams = editEvents.TotalTeams;
            return events;

        }

        public bool DeleteEventfromList(int eventId)
        {
            bool result = false;
            var events = db.Events.Find(eventId);
            db.Events.Remove(events);
            db.SaveChanges();
            result = true;
            return result;
        }
    }
}
