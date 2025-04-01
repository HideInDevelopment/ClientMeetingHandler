namespace ClientMeetingHandler.domain.entities;

public class Contact : Entity<Guid>
{
    public string Country { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid ClientId { get; set; }
    
    public virtual Client Client { get; set; }
}