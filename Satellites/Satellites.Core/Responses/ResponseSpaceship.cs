namespace Satellites.Core.Responses
{
    public class ResponseSpaceship
    {
        public bool ResponseSuccess { get; set; }
        public string Message { get; set; }
        public short Status { get; set; }
        public object Data { get; set; }

    }
}
