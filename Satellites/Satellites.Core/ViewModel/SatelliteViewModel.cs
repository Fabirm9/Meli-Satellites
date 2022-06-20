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

        
        [EnsureMinimumElements(min:5,max: 5, ErrorMessage ="Please min length of array string should be major or equal to 5 ")]
        public List<string> Message { get; set; }

    }    
}
