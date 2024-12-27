using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AddCityRequest
    {
        public string Name { get; set; } = string.Empty;

        public float Altitude { get; set; }

        public float Latitude { get; set; }
    }
}

