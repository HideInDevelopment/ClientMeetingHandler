using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.location;

namespace ClientMeetingHandler.domain.entities;

public class Location : Entity<Guid>, IMapToDto<LocationDto>
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    
    public virtual List<Meeting> Meetings { get; set; }
    
    public LocationDto ToDto()
    {
        return new LocationDto()
        {
            Id = Id,
            Country = Country,
            City = City,
            Street = Street
        };
    }
}