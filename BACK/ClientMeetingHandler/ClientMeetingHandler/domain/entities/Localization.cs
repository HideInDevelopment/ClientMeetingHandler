namespace ClientMeetingHandler.domain.entities;

public class Localization : Entity<Guid>
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    
    public virtual List<Meeting> Meetings { get; set; }
}