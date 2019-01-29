using AutoMapper;
using NUS.TheAmagingRace.BusinessModels;
using NUS.TheAmagingRace.DAL;
using NUS.TheAmagingRace.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NUS.TheAmagingRace.BAL
{
    /// <summary>
    /// 
    /// </summary>
    public class TeamBAL
    {
        private readonly UOW _unitOfWork;
        private TARDBContext db = new TARDBContext();
        public TeamBAL()
        {
            _unitOfWork = new UOW();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public List<TeamViewModel> GetTeams(int EventID)
        {
            // Event @event=  _unitOfWork.EventRepository.GetByID(EventID);
            
           Event e= _unitOfWork.EventRepository.GetWithInclude(m=>m.EventID.Equals(EventID), "Teams").FirstOrDefault();
           
           return Mapper.Map<List<Team>, List<TeamViewModel>>(e.Teams.ToList()).ToList();
        }

        /*<summary>
          * Fetched list of teams registered for an event based on eventId
        </summary>
        <returns>
          List of teams
        </returns>*/
        /// <param name="EventId"></param>
        public List<Team> GetTeamByEvent(int EventId)
        {
            var teams = from s in db.Teams
                           select s;
            teams = teams.OrderByDescending(s => s.Distance).Where(s => s.Event.EventID == EventId);
     
            return teams.ToList();
        }

        public void DeleteTeam(int teamId)
        {
            Team team = db.Teams.SingleOrDefault(x => x.TeamID == teamId);
            db.Teams.Remove(team);
            db.SaveChanges();
        }

        public EventViewModel GetEventById(int EventID)
        {
          return  Mapper.Map<Event, EventViewModel>(_unitOfWork.EventRepository.GetByID(EventID));
        }

        public void SaveTeam(TeamViewModel teamViewModel)
        {
            Team team = new Team {
                Event = Mapper.Map<Event>(teamViewModel.EventViewModel),
                TeamName=teamViewModel.TeamName,
                Members=null
            };
            //Team team = Mapper.Map<Team>(teamViewModel);
            team.Event = _unitOfWork.EventRepository.GetByID(teamViewModel.EventViewModel.EventID);
            using (var Scope = new TransactionScope())
            {
                _unitOfWork.TeamRepository.Insert(team);
                _unitOfWork.Save();
                Scope.Complete();
            }
        }

        /*<summary>
          * Updates Team details from the values returned by the WEB API
        </summary>
        <returns>
         Saves vaues to Teams DB
        </returns>*/
        /// <param name="teamLocations"></param>
        /// <param name="EventID"></param>
        public void updateTeamLocation(List<TeamLocation> teamLocations, int EventID)
        {
            for(int i=0; i<teamLocations.Count; i++)
            {
                int teamId = teamLocations[i].TeamID;
                Team editTeam = db.Teams.SingleOrDefault(x => x.Event.EventID == EventID && x.TeamID == teamId);
                editTeam.Distance = teamLocations[i].Location[0].distance;
                editTeam.Time = teamLocations[i].Location[0].time;
                //editTeam.Position = team.Position;
                editTeam.NextPitStop = teamLocations[i].Location[0].pitstopName;
                editTeam.Latitude = teamLocations[i].Location[0].lat;
                editTeam.Longitude = teamLocations[i].Location[0].lng;
                db.SaveChanges();
            }

        }

    }
}
