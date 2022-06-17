using Satellites.Core.Helpers;
using System.Collections.Generic;

namespace Satellites.Core.ViewModel
{
    public class SatellitesViewModel 
    {

        //[EnsureMinimumElementsList(min:3,max:3,ErrorMessage ="Array satellites should be equal to 3 objects")]
        public List<SatelliteViewModel> Satellites {get;set;}
    }
}
