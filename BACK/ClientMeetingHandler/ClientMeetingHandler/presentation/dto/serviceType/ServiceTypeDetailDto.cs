using ClientMeetingHandler.common.contracts;

namespace ClientMeetingHandler.presentation.dto.serviceType;

public class ServiceTypeDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Sessions { get; set; }
}