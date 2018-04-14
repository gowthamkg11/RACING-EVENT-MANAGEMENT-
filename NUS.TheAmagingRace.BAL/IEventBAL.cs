using System.Collections.Generic;
using NUS.TheAmagingRace.DAL;

namespace NUS.TheAmagingRace.BAL
{
    public interface IEventBAL
    {
        bool DeleteEventfromList(int eventId);
        List<Event> EditEventList(Event eventModel);
        Event GetEditingValues(int eventId);
        List<Event> GetEventList();
    }
}