namespace ClientMeetingHandler.domain.entities;

public class Meeting : Entity<Guid>
{
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid LocalizationId { get; set; }
    public Guid ClientId { get; set; }
    
    public virtual Localization Localization { get; set; }
    public virtual Client Client { get; set; }
}