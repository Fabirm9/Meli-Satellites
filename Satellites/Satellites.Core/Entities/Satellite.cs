using System.Collections.Generic;

namespace Satellites.Core.Entities
{
    public class Satellite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Distance { get; set; }
        public string Position { get; set; }
        public string[] Message { get; set; }
    }
}
