namespace ClientMeetingHandler.domain.entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ContactId { get; set; }
    public IReadOnlyCollection<Meeting> Meetings { get; set; }
    public IReadOnlyCollection<Service> Services { get; set; }

    public Client(Guid id, string name, Guid contactId, IReadOnlyCollection<Meeting> meetings, IReadOnlyCollection<Service> services)
    {
        Id = id;
        Name = name;
        ContactId = contactId;
        Meetings = meetings;
        Services = services;
    }
}