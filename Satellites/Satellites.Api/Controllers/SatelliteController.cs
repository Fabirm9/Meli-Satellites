using Microsoft.AspNetCore.Mvc;
using Satellites.Core.Interfaces;

namespace Satellites.Api.Controllers
{
    [Route("api/meli")]
    [ApiController]
    public class SatelliteController : ControllerBase
    {
        private readonly ISatelliteRepository _satelliteRepository;

        public SatelliteController(ISatelliteRepository satelliteRepository) 
        {
            _satelliteRepository = satelliteRepository;
        }

        [HttpGet]
        public IActionResult TopSecret() 
        {
            return Ok("Test");
        }
    }
}
