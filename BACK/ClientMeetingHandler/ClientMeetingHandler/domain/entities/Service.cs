namespace ClientMeetingHandler.domain.entities;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<Note> Notes { get; set; }
    public DateTime Date { get; set; }
    public DateTime Expiration { get; set; }
    public Guid ServiceTypeId { get; set; }

    public Service(Guid id, string name, IReadOnlyCollection<Note> notes, DateTime date, DateTime expiration, Guid serviceTypeId)
    {
        Id = id;
        Name = name;
        Notes = notes;
        Date = date;
        Expiration = expiration;
        ServiceTypeId = serviceTypeId;
    }


}