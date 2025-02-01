namespace ClientMeetingHandler.domain.entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ContactId { get; set; }
    public Guid MeetingId { get; set; }
    public Guid ServiceId { get; set; }

    public Client(Guid id, string name, Guid contactId, Guid meetingId, Guid serviceId)
    {
        Id = id;
        Name = name;
        ContactId = contactId;
        MeetingId = meetingId;
        ServiceId = serviceId;
    }
}