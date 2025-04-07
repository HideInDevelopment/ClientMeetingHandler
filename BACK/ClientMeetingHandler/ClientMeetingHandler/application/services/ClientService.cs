using System.Linq.Expressions;
using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.client;

namespace ClientMeetingHandler.application.services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ClientMapper _clientMapper;
    private readonly IReadOnlyList<string> _clientIncludes = ["Contact", "Meetings", "Services"];

    public ClientService(IClientRepository clientRepository, ClientMapper clientMapper)
    {
        _clientRepository = clientRepository;
        _clientMapper = clientMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableClients = await _clientRepository.GetAllAsync();
        return queryableClients.ToList().Select(_clientMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedClient = await _clientRepository.GetByIdAsync(id);
        return storedClient == null ? null : _clientMapper.MapEntityToDto(storedClient);
    }

    public async Task AddAsync(IDto dto)
    {
        var clientToPersist = _clientMapper.MapDtoToEntity((ClientDto)dto);
        var existingClient = await _clientRepository.GetByIdAsync(clientToPersist.Id);
        if (existingClient != null)
        {
            throw new EntityAlreadyExistException<Client>(clientToPersist);
        }
        await _clientRepository.AddAsync(clientToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var clientToUpdate = _clientMapper.MapDtoToEntity((ClientDto)dto);
        var queryableClients = await _clientRepository.GetAllAsync();
        if (queryableClients.Any(x => x.Id == clientToUpdate.Id))
        {
            await _clientRepository.UpdateAsync(clientToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<Client>(clientToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _clientRepository.DeleteAsync(id);
    
    public async Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        var queryableClientsWithIncludes = await _clientRepository.GetQueryWithIncludesAsync(_clientIncludes);
        return queryableClientsWithIncludes.ToList().Select(_clientMapper.MapDetailEntityToDetailDto);
    }

    public async Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        var storedClientWithIncludes = await _clientRepository.GetSingleWithIncludesAsync(x => x.Id.Equals(id), _clientIncludes);
        return storedClientWithIncludes == null ? null : _clientMapper.MapDetailEntityToDetailDto(storedClientWithIncludes);
    }
}