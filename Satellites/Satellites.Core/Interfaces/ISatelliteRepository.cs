using Satellites.Core.Entities;
using Satellites.Core.Responses;
using Satellites.Core.ViewModel;
using System;
using System.Threading.Tasks;

namespace Satellites.Core.Interfaces
{
    public interface ISatelliteRepository
    {
        Task<ResponseSpaceship> CreateSatellites(SatellitesViewModel model);
        Task<ResponseSpaceship> GetLocationMessage();
        Task<ResponseSpaceship> UpdateSatellite(Satellite model);
    }
}
