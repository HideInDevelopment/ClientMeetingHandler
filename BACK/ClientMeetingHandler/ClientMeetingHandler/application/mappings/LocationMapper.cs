using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.location;

namespace ClientMeetingHandler.application.mappings;

public class LocationMapper : GenericMapper<Location, LocationDto, LocationDetailDto>
{
    public override LocationDetailDto? MapDetailEntityToDetailDto(Location? entity)
    {
        if (entity == null) return null;

        return new LocationDetailDto
        {
            Id = entity.Id,
            Country = entity.Country,
            City = entity.City,
            Street = entity.Street,
            Meetings = entity.Meetings.Select(meeting => meeting.ToDto()).ToList(),
        };
    }
}