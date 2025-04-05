namespace ClientMeetingHandler.common.contracts;

public interface IMapToEntity<out TEntity> where TEntity : class
{
    TEntity ToEntity();
}