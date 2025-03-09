namespace Application.Dtos.RequestDtos
{
    public class AddCarRequest
    {
        public string Manufacturer { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public DateTime Year { get; set; }

        public string Generation { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
