using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto.location;

public class LocationDto : IDto, IMapToEntity<Location>
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public Location ToEntity()
    {
        return new Location
        {
            Id = Id,
            Country = Country,
            City = City,
            Street = Street
        };
    }
}