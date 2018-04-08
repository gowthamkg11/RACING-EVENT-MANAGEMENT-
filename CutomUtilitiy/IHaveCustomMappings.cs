using AutoMapper;
using System;

namespace NUS.TheAmagingRace.CutsomUtilitiy
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
