using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class CityDto
    {
        public CityDto(City org)
        {
            Id = org.CityId;
            Name = org.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
