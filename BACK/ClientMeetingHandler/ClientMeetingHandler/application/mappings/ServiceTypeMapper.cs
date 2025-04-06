using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.presentation.dto.serviceType;

namespace ClientMeetingHandler.application.mappings;

public class ServiceTypeMapper: GenericMapper<ServiceType, ServiceTypeDto, ServiceTypeDetailDto>
{
    public override ServiceTypeDetailDto? MapDetailEntityToDetailDto(ServiceType? entity)
    {
        if (entity == null) return null;

        return new ServiceTypeDetailDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Sessions = entity.Sessions
        };
    }
}