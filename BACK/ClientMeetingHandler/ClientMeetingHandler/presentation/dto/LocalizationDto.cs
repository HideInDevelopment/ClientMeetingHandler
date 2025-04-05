using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;

namespace ClientMeetingHandler.presentation.dto;

public class LocalizationDto : IDto, IMapToEntity<Localization>
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public Localization ToEntity()
    {
        return new Localization()
        {
            Id = Id,
            Country = Country,
            City = City,
            Street = Street
        };
    }
}