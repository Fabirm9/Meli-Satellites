using Swashbuckle.AspNetCore.Filters;

namespace Satellites.Core.Responses
{
    public class ResponseSpaceshipExampleConflict : IExamplesProvider<ResponseSpaceship>
    {
        public ResponseSpaceship GetExamples()
        {

            return new ResponseSpaceship
            {
                Status = 3,
                ResponseSuccess = false,
                Message = "empty data/Object satellites aren't equals",
                Data = null
            };
        }
    }
}
