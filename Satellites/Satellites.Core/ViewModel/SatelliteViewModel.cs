using Satellites.Core.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Satellites.Core.ViewModel
{
    public class SatelliteViewModel
    {
        
        [Required]
        public string Name { get; set; }

        [Required]
        public float? Distance { get; set; }

        
        [EnsureMinimumElements(min:1,max: 5, ErrorMessage ="Please min length major that 1 or menor that 5 or string empty ")]
        public List<string> Message { get; set; }

    }    
}
