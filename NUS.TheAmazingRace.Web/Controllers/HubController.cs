using Microsoft.AspNet.SignalR;
using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    public class HubController : Hub
    {
        private TeamBAL teamBAL = new TeamBAL();
        // GET: Hub

        /*<summary>
          * SiganlR broadCast method to javaScript
        </summary>*/
        /// <param name="EventId"></param>
        public void CallSignalR(int EventId)
        {
            var mappingHub = GlobalHost.ConnectionManager.GetHubContext<MappingHub>();
            List<Team> teams = teamBAL.GetTeamByEvent(EventId);
            int position;
            for (int i = 0; i<teams.Count; i++)
            {
                position = i + 1;
                var eachTeam = new
                {
                    TeamName = teams[i].TeamName,
                    TeamID = teams[i].TeamID,
                    Latitude = teams[i].Latitude,
                    Longitude = teams[i].Longitude,
                    Distance = teams[i].Distance,
                    Time = teams[i].Time,
                    NextPitStop = teams[i].NextPitStop,
                    Position = position
                };
                mappingHub.Clients.All.locationReceived(eachTeam);
            }
        }
    }
}