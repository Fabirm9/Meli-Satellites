using Satellites.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Satellites.Core.Interfaces
{
    public interface ISatelliteRepository
    {
        Task<IList<Satellite>> GetAll();
        Task<bool> UpdateSatellite(Satellite data, string[] message);
        Task InsertSatellite(string name, float? distance, string nPosition, string message);

    }
}
