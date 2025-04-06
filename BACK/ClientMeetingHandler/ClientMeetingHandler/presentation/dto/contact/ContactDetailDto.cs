using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto.client;

namespace ClientMeetingHandler.presentation.dto.contact;

public class ContactDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid ClientId { get; set; }
    public ClientDto Client { get; set; }
}