using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NUS.TheAmagingRace.BAL
{
    /*<summary>
		EventBAL is the layer that is used to do CRUD operation with the Database for 
		the Events
	 </summary>*/
    public class EventBAL
    {

        /*<summary>
		   Initializing the DB context and Event object
		</summary>*/

        private TARDBContext db = new TARDBContext();
        private Event events = new Event();



        /*<summary>
		   Gets the list of event according to latest date
		   from the Database
		 </summary>
		 <returns>The List of Events</returns>*/

        public List<Event> GetEventList()
        {
            return db.Events.OrderBy(o => o.StartDate).ToList(); ;
        }



        /*<summary>
			Edits the event if EventId>0 else Adds the event to the
			Database
		  </summary>*/
        /// <param name="eventModel"> Gets the Event object from controller that is given
        /// as input by user to create or edit</param>
        /// <param name="currentUser"> gets the user who edits or creates it</param>
        /*<returns>The List of Events</returns>*/

        public List<Event> EditEventList(Event eventModel, string currentUser)
        {
            if (eventModel.EventID > 0)
            {
                Event editEvents = db.Events.Include("Teams").Include("PitStops").SingleOrDefault(x => x.EventID == eventModel.EventID);

                editEvents.EventName = eventModel.EventName;
                editEvents.EventDescription = eventModel.EventDescription;
                editEvents.EventCountry = eventModel.EventCountry;
                editEvents.EventCity = eventModel.EventCity;
                editEvents.StartDate = eventModel.StartDate;
                editEvents.EndDate = eventModel.EndDate;
                editEvents.LastModifiedBy = currentUser;
                editEvents.LastModifiedAt = DateTime.Now;
                editEvents.ImagePath = eventModel.ImagePath;
                db.SaveChanges();
            }
            else
            {
                eventModel.CreatedBy = currentUser;
                db.Events.Add(eventModel);

                db.SaveChanges();
            }

            return db.Events.OrderBy(o => o.StartDate).ToList(); ;
        }



        /*<summary>
		   This method is used to get the event to be edited by User
		  </summary>*/
        /// <param name="eventId"> Selects the event to edit from database</param>
        /*<returns>Event Object to be edited</returns>*/

        public Event GetEditingValues(int eventId)
        {
            Event editEvents = db.Events.SingleOrDefault(x => x.EventID == eventId);

            return editEvents;

        }



        /*<summary>
		   This method is used to get the event to be viewed by User from
			Database
		  </summary>*/
        /// <param name="eventId"> Selects the event to edit from database</param>
        /*<returns>Event Object to be edited</returns>*/

        public Event GetSelectedEvent(int eventId)
        {
            Event currentEvent = db.Events.SingleOrDefault(x => x.EventID == eventId);

            return currentEvent;

        }



        /*<summary>
		   This method is used to get the event to be deleted by User from
			Database
		  </summary>*/
        /// <param name="eventId"> Selects the event to delete from database</param>
        /*<returns>Returns the Event List</returns>*/

        public List<Event> DeleteEventfromList(int eventId)
        {
            var events = db.Events.Find(eventId);
            db.Events.Remove(events);
            db.SaveChanges();

            return GetEventList();
        }



        /*<summary>
		   This method is used to Search the event from Database
		  </summary>*/
        /// <param name="searchString"> Selects the event to search from database
        /// by using the string input given by user</param>
        /*<returns>Event List for the String input</returns>*/

        public List<Event> SearchEvent(string searchString)
        {
            var events = from s in db.Events
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.EventName.Contains(searchString));
            }

            return events.OrderBy(o => o.StartDate).ToList();
        }



        /*<summary>
		  This method is used to Search the Current event from Database
		 </summary>*/
        /*<returns>Event List for the Current Events</returns>*/

        public List<Event> CurrentEvent()
        {
            var events = from s in db.Events
                         select s;
            events = events.Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now);

            return events.OrderBy(o => o.StartDate).ToList();
        }



        /*<summary>
		  This method is used to Search the Upcoming events from Database
		 </summary>*/
        /*<returns>Event List for the Upcoming Events</returns>*/

        public List<Event> UpcomingEvents()
        {
            var events = from s in db.Events
                         select s;
            events = events.Where(s => s.StartDate > DateTime.Now);

            return events.OrderBy(o => o.StartDate).ToList();
        }
    }
}
