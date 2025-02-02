namespace ClientMeetingHandler.domain.repositories.contracts;

public interface IHasEmailRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByEmail(string email);
}