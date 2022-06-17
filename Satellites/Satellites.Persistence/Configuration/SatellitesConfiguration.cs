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
        }
    }

}

