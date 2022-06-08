using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Satellites.Core.Entities;

namespace Satellites.Persistence.Configuration
{
    public class SatellitesConfiguration
    {
        public SatellitesConfiguration(EntityTypeBuilder<Satellite> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.Id);
            

            entityBuilder
            .Property(x => x.Message)
                .HasConversion(arr => string.Join(',', arr), arr => arr.Split(',', System.StringSplitOptions.None));


            var satellite1 = new Satellite
            {
                Id = 1,
                Name = "kenobi",
                Position = "-500,-200",
                Distance = null,
                Message = new string[] { },

            };

            entityBuilder.HasData(satellite1);

            var satellite2 = new Satellite
            {
                Id = 2,
                Name = "skywalker",
                Position = "100,-100",
                Distance = null,
                Message = new string[] { },

            };

            entityBuilder.HasData(satellite2);

            var satellite3 = new Satellite
            {
                Id = 3,
                Name = "sato",
                Position = "500,100",
                Distance = null,
                Message = new string[] { },

            };

            entityBuilder.HasData(satellite3);


        }

    }

}

