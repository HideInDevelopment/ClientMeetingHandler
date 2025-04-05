using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.presentation.dto;

namespace ClientMeetingHandler.domain.entities;

public class Localization : Entity<Guid>, IMapToDto<LocalizationDto>
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    
    public virtual List<Meeting> Meetings { get; set; }
    
    public LocalizationDto ToDto()
    {
        throw new NotImplementedException();
    }
}