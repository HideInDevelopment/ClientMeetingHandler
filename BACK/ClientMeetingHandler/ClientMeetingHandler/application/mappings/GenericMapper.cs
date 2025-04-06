using ClientMeetingHandler.common.contracts;

namespace ClientMeetingHandler.application.mappings;

public abstract class GenericMapper<TEntity, TDto, TDetailDto> 
    where TEntity : class, IMapToDto<TDto>
    where TDto : class, IMapToEntity<TEntity>
    where TDetailDto : class
{
    public TEntity MapDtoToEntity(TDto dto)
    {
        return dto.ToEntity();
    }
    
    public TDto MapEntityToDto(TEntity entity)
    {
        return entity.ToDto();
    }

    public abstract TDetailDto? MapDetailEntityToDetailDto(TEntity? entity);

}