namespace ClientMeetingHandler.domain.entities;

public class Contact
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }

    public Contact(Guid id, string country, int phoneNumber, string email)
    {
        Id = id;
        Country = country;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}