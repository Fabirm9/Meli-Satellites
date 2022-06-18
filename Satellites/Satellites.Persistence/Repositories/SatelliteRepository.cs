using Microsoft.EntityFrameworkCore;
using Satellites.Core.Entities;
using Satellites.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Satellites.Persistence.Repositories
{
    public class SatelliteRepository : ISatelliteRepository
    {
        private readonly SatelliteContext _context;
        
        public SatelliteRepository(SatelliteContext context)
        {
            _context = context;
        }

        public async Task<IList<Satellite>> GetAll() 
        {
            return await _context.Satellites.ToListAsync();

        }
        public async Task<Satellite> GetByName(string name) {
            return await _context.Satellites.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<bool> UpdateSatellite(Satellite data, string[] message)
        {
            var getSatellite = this.GetByName(data.Name).Result;
            if (getSatellite == null)
                return false;
            getSatellite.Distance = data.Distance;
            getSatellite.Message = message;
            int rows = await _context.SaveChangesAsync();

            return rows > 0;

        }

        public async Task InsertSatellite(string name, float? distance, string nPosition, string message) 
        {
            var satellite = new Satellite
            {
                Name = name,
                Distance = distance,
                Position = nPosition,
                Message = message.Split(",")
            };
            _context.Satellites.Add(satellite);
            await _context.SaveChangesAsync();

        }

    }
}
