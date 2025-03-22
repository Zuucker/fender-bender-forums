namespace Application.Dtos.RequestDtos
{
    public class AddCityRequest
    {
        public string Name { get; set; } = string.Empty;

        public float Altitude { get; set; }

        public float Latitude { get; set; }
    }
}

