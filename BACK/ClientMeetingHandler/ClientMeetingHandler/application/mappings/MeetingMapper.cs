using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.meeting;

namespace ClientMeetingHandler.application.mappings;

public class MeetingMapper : GenericMapper<Meeting, MeetingDto, MeetingDetailDto>
{
    public override MeetingDetailDto? MapDetailEntityToDetailDto(Meeting? entity)
    {
        if (entity == null) return null;

        return new MeetingDetailDto
        {
            Id = entity.Id,
            Date = entity.Date,
            Duration = entity.Duration,
            LocationId = entity.LocationId,
            ClientId = entity.ClientId,
            Location = entity.Location.ToDto(),
            Client = entity.Client.ToDto(),
        };
    }
}