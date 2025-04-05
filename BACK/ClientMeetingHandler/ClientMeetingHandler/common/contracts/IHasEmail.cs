namespace ClientMeetingHandler.domain.repositories.contracts;

public interface IHasEmail<TEntity> where TEntity : class
{
    Task<TEntity?> GetByEmail(string email);
}