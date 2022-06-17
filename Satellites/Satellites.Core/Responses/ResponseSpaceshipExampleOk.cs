using Swashbuckle.AspNetCore.Filters;

namespace Satellites.Core.Responses
{
    public class ResponseSpaceshipExampleOk : IExamplesProvider<ResponseSpaceship>
    {
        public ResponseSpaceship GetExamples() {

            return new ResponseSpaceship
            {
                Status=1,
                ResponseSuccess=true,
                Message="sucess",
                Data = new PositionAndMessage 
                {
                    X=100,
                    Y=200,
                    Message="esto es un mensaje secreto"
                }
            };      
        }
    }
}
