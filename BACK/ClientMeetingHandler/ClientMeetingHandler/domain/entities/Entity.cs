namespace ClientMeetingHandler.domain.entities;

public class Entity<TKey>
{
    public TKey Id { get; set; }
    private bool IsTransient => Id != null && Id.Equals(default(TKey));

    public override bool Equals(object? obj) =>
        obj is Entity<TKey> { IsTransient: false } item
        && !IsTransient
        && Id != null
        && (ReferenceEquals(this, item) || Id.Equals(item.Id));

    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right) => 
        ReferenceEquals(left, right) || left is not null && left.Equals(right);
    
    public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !(left == right);
    
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}