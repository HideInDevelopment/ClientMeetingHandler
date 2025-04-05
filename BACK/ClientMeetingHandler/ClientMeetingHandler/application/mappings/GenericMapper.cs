using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto;

namespace ClientMeetingHandler.application.mappings;

public abstract class GenericMapper<TEntity, TDto> 
    where TEntity : class, IMapToDto<TDto>
    where TDto : class, IMapToEntity<TEntity>
{
    public TEntity MapDtoToEntity(TDto dto)
    {
        return dto.ToEntity();
    }
    
    public TDto MapEntityToDto(TEntity entity)
    {
        return entity.ToDto();
    }
}