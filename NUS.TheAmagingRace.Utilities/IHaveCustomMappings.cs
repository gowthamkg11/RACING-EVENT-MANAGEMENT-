using AutoMapper;
using System;

namespace NUS.TheAmagingRace.Utilities
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
