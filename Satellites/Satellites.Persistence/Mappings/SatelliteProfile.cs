using AutoMapper;
using Satellites.Core.Entities;
using Satellites.Core.ViewModel;

namespace Satellites.Persistence.Mappings
{
    public class SatelliteProfile : Profile
    {
        public SatelliteProfile() 
        {
            CreateMap<Satellite, SatelliteViewModel>();
            CreateMap<SatelliteViewModel, Satellite>();
        }
    }
}
