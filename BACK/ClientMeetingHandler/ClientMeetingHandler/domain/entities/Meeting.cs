namespace ClientMeetingHandler.domain.entities;

internal class Meeting
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public Guid ClientId { get; set; }

    public Meeting(Guid id, DateTime date, int duration, Guid clientId)
    {
        Id = id;
        Date = date;
        Duration = duration;
        ClientId = clientId;
    }
}