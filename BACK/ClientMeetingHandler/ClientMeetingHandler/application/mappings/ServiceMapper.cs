using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.service;

namespace ClientMeetingHandler.application.mappings;

public class ServiceMapper: GenericMapper<Service, ServiceDto, ServiceDetailDto>
{
    public override ServiceDetailDto? MapDetailEntityToDetailDto(Service? entity)
    {
        if (entity == null) return null;

        return new ServiceDetailDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Date = entity.Date,
            Expiration = entity.Expiration,
            ServiceTypeId = entity.ServiceTypeId,
            ServiceType = entity.ServiceType.ToDto(),
            Notes = entity.Notes.Select(x => x.ToDto()).ToList(),
        };
    }
}