using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.contact;

namespace ClientMeetingHandler.application.services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly ContactMapper _contactMapper;
    private readonly IReadOnlyList<string> _contactIncludes = ["Client"];

    public ContactService(IContactRepository contactRepository, ContactMapper contactMapper)
    {
        _contactRepository = contactRepository;
        _contactMapper = contactMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableContacts = await _contactRepository.GetAllAsync();
        return queryableContacts.ToList().Select(_contactMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedContact = await _contactRepository.GetByIdAsync(id);
        return storedContact == null ? null : _contactMapper.MapEntityToDto(storedContact);
    }

    public async Task AddAsync(IDto dto)
    {
        var contactToPersist = _contactMapper.MapDtoToEntity((ContactDto)dto);
        var existingContact = await _contactRepository.GetByIdAsync(contactToPersist.Id);
        if (existingContact == null)
        {
            throw new EntityAlreadyExistException<Contact>(contactToPersist);
        }
        await _contactRepository.AddAsync(contactToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var contactToUpdate = _contactMapper.MapDtoToEntity((ContactDto)dto);
        var queryableContacts = await _contactRepository.GetAllAsync();
        if (queryableContacts.Any(x => x.Id == contactToUpdate.Id))
        {
            await _contactRepository.UpdateAsync(contactToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<Contact>(contactToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _contactRepository.DeleteAsync(id);

    public async Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        var queryableContactsWithIncludes = await _contactRepository.GetQueryWithIncludesAsync(_contactIncludes);
        return queryableContactsWithIncludes.ToList().Select(_contactMapper.MapDetailEntityToDetailDto);
    }

    public async Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        var storedContactWithIncludes = await _contactRepository.GetSingleWithIncludesAsync(x => x.Id.Equals(id), _contactIncludes);
        return storedContactWithIncludes == null ? null : _contactMapper.MapDetailEntityToDetailDto(storedContactWithIncludes);
    }

    public async Task<ContactDto?> GetByEmail(string email)
    {
        var queryableContacts = await _contactRepository.GetAllAsync();
        var contactByEmail = queryableContacts.FirstOrDefault(x => x.Email == email);
        return contactByEmail == null ? null : _contactMapper.MapEntityToDto(contactByEmail);
    }

    public async Task<ContactDetailDto?> GetDetailByEmail(string email)
    {
        var storedContact = await _contactRepository.GetSingleWithIncludesAsync(x => x.Email.Equals(email), _contactIncludes);
        return storedContact == null ? null : _contactMapper.MapDetailEntityToDetailDto(storedContact);
    }
}