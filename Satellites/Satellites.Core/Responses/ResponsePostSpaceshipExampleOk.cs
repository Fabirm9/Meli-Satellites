using Swashbuckle.AspNetCore.Filters;
using System;

namespace Satellites.Core.Responses
{
    public class ResponsePostSpaceshipExampleOk : IExamplesProvider<ResponseSpaceship>
    {
        public ResponseSpaceship GetExamples() {
            return new ResponseSpaceship
            {
                Status = 1,
                ResponseSuccess = true,
                Message = "sucess",
                Data = null
            };
        }
    }
}
