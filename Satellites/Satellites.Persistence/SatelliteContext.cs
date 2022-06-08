using Microsoft.EntityFrameworkCore;
using Satellites.Core.Entities;
using Satellites.Persistence.Configuration;

namespace Satellites.Persistence
{
    public class SatelliteContext : DbContext
    {
        public SatelliteContext()
        {
        }

        public SatelliteContext(DbContextOptions<SatelliteContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Satellite> Satellites { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);            

            ModelConfig(modelBuilder);

        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new SatellitesConfiguration(modelBuilder.Entity<Satellite>());

        }

    }
}
