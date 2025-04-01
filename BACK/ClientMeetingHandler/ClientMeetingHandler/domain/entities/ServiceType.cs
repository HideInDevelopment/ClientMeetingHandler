using System.Runtime.CompilerServices;

namespace ClientMeetingHandler.domain.entities;

public class ServiceType : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Sessions { get; set; }
}