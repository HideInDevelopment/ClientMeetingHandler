namespace ClientMeetingHandler.domain.exceptions;

public class EntityAlreadyExistException<TEntity> : Exception
{
    public EntityAlreadyExistException(TEntity entity) { }    
}