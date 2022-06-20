using Satellites.Core.ViewModel;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Satellites.Core.Request
{
    public class RequestSatellitesModelExample : IExamplesProvider<SatellitesViewModel>
    {
        SatellitesViewModel Satellites = new SatellitesViewModel();        
        public SatellitesViewModel GetExamples() 
        {
            Satellites.Satellites = new List<SatelliteViewModel>();
            Satellites.Satellites.Add(new SatelliteViewModel { Name= "kenobi", Distance= (float?)100.0, Message = new List<string> { "este","", "", "mensaje", ""} });
            Satellites.Satellites.Add(new SatelliteViewModel { Name = "skywalker", Distance = (float?)115.5, Message = new List<string> { "", "es", "", "", "secreto" } });
            Satellites.Satellites.Add(new SatelliteViewModel { Name = "sato", Distance = (float?)142.7, Message = new List<string> { "", "es", "un", "", "secreto" } });
            return Satellites;
        
        }
    }
}
