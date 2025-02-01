namespace ClientMeetingHandler.domain.entities;

public class Meeting
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid LocalizationId { get; set; }
    public Guid ClientId { get; set; }

    public Meeting(Guid id, DateTime date, int duration, Guid localizationId, Guid clientId)
    {
        Id = id;
        Date = date;
        Duration = duration;
        LocalizationId = localizationId;
        ClientId = clientId;
    }
}