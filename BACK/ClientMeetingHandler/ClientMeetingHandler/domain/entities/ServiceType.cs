using System.Runtime.CompilerServices;

namespace ClientMeetingHandler.domain.entities;

public class ServiceType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Sessions { get; set; }

    public ServiceType(Guid id, string name, string description, double price, int sessions)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Sessions = sessions;
    }
}