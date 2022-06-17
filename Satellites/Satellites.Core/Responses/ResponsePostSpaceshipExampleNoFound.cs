using Swashbuckle.AspNetCore.Filters;

namespace Satellites.Core.Responses
{
    public class ResponsePostSpaceshipExampleNoFound : IExamplesProvider<ResponseSpaceship>
    {
        public ResponseSpaceship GetExamples() 
        {
            return new ResponseSpaceship
            {
                Status = 2,
                ResponseSuccess = false,
                Message = "no found",
                Data = null
            };
        }
    }
}
