namespace ClientMeetingHandler.common.contracts;

public interface IHasEmail<TDto, TDetailDto> 
    where TDto : class, IDto
    where TDetailDto : class, IDto
{
    Task<TDto?> GetByEmail(string email);
    Task<TDetailDto?> GetDetailByEmail(string email);
}