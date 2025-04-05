namespace ClientMeetingHandler.common.contracts;

public interface IMapToDto<out TDto> where TDto : class
{
    TDto ToDto();
}