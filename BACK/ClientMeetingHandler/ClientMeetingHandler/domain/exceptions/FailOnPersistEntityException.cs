namespace ClientMeetingHandler.domain.exceptions;

public class FailOnPersistEntityException<TEntity> : Exception
{
    public FailOnPersistEntityException(TEntity entity){}
}