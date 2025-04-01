namespace ClientMeetingHandler.domain.exceptions;

public class EntityNotFoundException<TEntity> : Exception
{
    public EntityNotFoundException(TEntity? entity) { }    
}