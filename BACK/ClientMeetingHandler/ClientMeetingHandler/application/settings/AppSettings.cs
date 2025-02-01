namespace ClientMeetingHandler.application.settings;

public class AppSettings
{
    #nullable disable
    public ClientSettings Client { get; set; }
    public ContactSettings Contact { get; set; }
    public LocalizationSettings Localization { get; set; }
    public MeetingSettings Meeting { get; set; }
    public NoteSettings Note { get; set; }
    public ServiceSettings Service { get; set; }
    public ServiceTypeSettings ServiceType { get; set; }
}