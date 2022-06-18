using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Satellites.Core.Entities;
using Satellites.Core.Interfaces;
using Satellites.Core.Request;
using Satellites.Core.Responses;
using Satellites.Core.ViewModel;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.Tasks;

namespace Satellites.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class SatelliteController : ControllerBase
    {
        private readonly ISatelliteManager _satelliteManager;
        public readonly IMapper _mapper;

        public SatelliteController(ISatelliteManager satelliteManager, IMapper mapper)
        {
            _satelliteManager = satelliteManager;
            _mapper = mapper;
        }

        /// POST: /meli/topsecret
        /// <summary>
        /// POST SATELLITES
        /// </summary>
        /// <remarks>
        /// We have post satellites with distance,message
        /// </remarks>
        /// <response code="200">Ok when message and position were genereted</response>              
        /// <response code="404">NotFound. when message/position weren't genereted</response>        
        /// <response code="500">InternalErrorServer. Have problems in process request, review log.</response> 
        [Route("/meli/topsecret")]
        [SwaggerRequestExample(typeof(SatelliteViewModel), typeof(RequestSatellitesModelExample))]
        [SwaggerResponse(200, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(200, typeof(ResponseSpaceshipExampleOk))]
        [SwaggerResponse(404, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(404, typeof(ResponseSpaceshipExampleNoFound))]
        [SwaggerResponse(409, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(409, typeof(ResponseSpaceshipExampleConflict))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> TopSecret([FromBody] SatellitesViewModel model)
        {
            var responseSatellite = await _satelliteManager.CreateSatellites(model);
            if (responseSatellite.ResponseSuccess)
            {
                return Ok(new ResponseSpaceship { ResponseSuccess = responseSatellite.ResponseSuccess, Status = responseSatellite.Status, Message = responseSatellite.Message, Data = responseSatellite.Data });
            }
            else
            {
                switch (responseSatellite.Status)
                {
                    case 2:
                        return NotFound(new ResponseSpaceship { Message = responseSatellite.Message, Status = responseSatellite.Status, ResponseSuccess = responseSatellite.ResponseSuccess });

                    case 3:
                        return Conflict(new ResponseSpaceship { Message = responseSatellite.Message, Status = responseSatellite.Status, ResponseSuccess = responseSatellite.ResponseSuccess });
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, responseSatellite.Message);
                }
            }
        }


        /// GET: /meli/topsecret_split
        /// <summary>
        /// GET POSITION/MESSAGE
        /// </summary>
        /// <remarks>
        /// We have get position/message
        /// </remarks>
        /// <response code="200">Ok when message and position were genereted</response>              
        /// <response code="404">NotFound. when message/position weren't genereted</response>        
        /// <response code="500">InternalErrorServer. Have problems in process request, review log.</response>
        [SwaggerResponse(200, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(200, typeof(ResponseSpaceshipExampleOk))]
        [SwaggerResponse(404, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(404, typeof(ResponseSpaceshipExampleNoFound))]
        [SwaggerResponse(409, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(409, typeof(ResponseSpaceshipExampleConflict))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Route("/meli/topsecret_split")]
        [HttpGet]
        public async Task<IActionResult> TopSecretSplit()
        {
            var getPosition = await _satelliteManager.GetLocationMessage();
            if (getPosition.ResponseSuccess)
            {
                return Ok(new ResponseSpaceship { ResponseSuccess = getPosition.ResponseSuccess, Status = getPosition.Status, Message = getPosition.Message, Data = getPosition.Data });
            }
            else
            {
                switch (getPosition.Status)
                {
                    case 2:
                        return NotFound(new ResponseSpaceship { Message = getPosition.Message, Status = getPosition.Status, ResponseSuccess = getPosition.ResponseSuccess });
                        
                    case 3:
                        return Conflict(new ResponseSpaceship { Message = getPosition.Message, Status = getPosition.Status, ResponseSuccess = getPosition.ResponseSuccess });
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, getPosition.Message);
                }
            }
        }


        /// POST: /meli/topsecret_split
        /// <summary>
        /// POST A SATELLITE
        /// </summary>
        /// <remarks>
        /// We can update a satellite by name
        /// </remarks>
        /// <response code="200">Ok when satellite was updated</response>              
        /// <response code="404">NotFound. when satellite wasn't updated or not found</response>        
        /// <response code="500">InternalErrorServer. Have problems in process request, review log.</response>
        [Route("/meli/topsecret_split/")]
        [SwaggerResponse(200, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(200, typeof(ResponsePostSpaceshipExampleOk))]
        [SwaggerResponse(404, Type = typeof(ResponseSpaceship))]
        [SwaggerResponseExample(404, typeof(ResponsePostSpaceshipExampleNoFound))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> UpdateSatellite(SatelliteViewModel model)
        {
            var satellite = _mapper.Map<Satellite>(model);
            var responseSatellite = await _satelliteManager.UpdateSatellite(satellite);
            if (responseSatellite.ResponseSuccess)
                return Ok(new ResponseSpaceship { ResponseSuccess = responseSatellite.ResponseSuccess, Status = responseSatellite.Status, Message = responseSatellite.Message });
            else
            {
                if (responseSatellite.Status == 2)
                    return NotFound(new ResponseSpaceship { ResponseSuccess = responseSatellite.ResponseSuccess, Status = responseSatellite.Status, Message = responseSatellite.Message });
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, responseSatellite.Message);
            }
        }

    }
}
