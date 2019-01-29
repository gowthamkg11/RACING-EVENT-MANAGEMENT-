using AutoMapper;
using NUS.TheAmagingRace.BusinessModels;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NUS.TheAmazingRace.Web.App_Start
{
    public class TARMapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TARUser, TARUserViewModel>();
                cfg.CreateMap<Event, EventViewModel>();
                cfg.CreateMap<PitStop, PitStopViewModel>();
                cfg.CreateMap<Team, TeamViewModel>();
            });
        }
    }
}