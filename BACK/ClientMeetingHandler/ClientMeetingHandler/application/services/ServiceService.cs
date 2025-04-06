using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.service;

namespace ClientMeetingHandler.application.services;

public class ServiceService: IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ServiceMapper _serviceMapper;
    private readonly IReadOnlyList<string> _serviceIncludes = ["ServiceType", "Notes"];

    public ServiceService(IServiceRepository serviceRepository, ServiceMapper serviceMapper)
    {
        _serviceRepository = serviceRepository;
        _serviceMapper = serviceMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableServices = await _serviceRepository.GetAllAsync();
        return queryableServices.ToList().Select(_serviceMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedService = await _serviceRepository.GetByIdAsync(id);
        return storedService == null ? null : _serviceMapper.MapEntityToDto(storedService);
    }

    public async Task AddAsync(IDto dto)
    {
        var serviceToPersist = _serviceMapper.MapDtoToEntity((ServiceDto)dto);
        var existingService = await _serviceRepository.GetByIdAsync(serviceToPersist.Id);
        if (existingService == null)
        {
            throw new EntityAlreadyExistException<Service>(serviceToPersist);
        }
        await _serviceRepository.AddAsync(serviceToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var serviceToUpdate = _serviceMapper.MapDtoToEntity((ServiceDto)dto);
        var queryableServices = await _serviceRepository.GetAllAsync();
        if (queryableServices.Any(x => x.Id == serviceToUpdate.Id))
        {
            await _serviceRepository.UpdateAsync(serviceToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<Service>(serviceToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _serviceRepository.DeleteAsync(id);
    
    public async Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        var queryableServicesWithIncludes = await _serviceRepository.GetQueryWithIncludesAsync(_serviceIncludes);
        return queryableServicesWithIncludes.ToList().Select(_serviceMapper.MapDetailEntityToDetailDto);
    }

    public async Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        var storedServiceWithIncludes = await _serviceRepository.GetSingleWithIncludesAsync(x => x.Id.Equals(id), _serviceIncludes);
        return storedServiceWithIncludes == null ? null : _serviceMapper.MapDetailEntityToDetailDto(storedServiceWithIncludes);
    }
}