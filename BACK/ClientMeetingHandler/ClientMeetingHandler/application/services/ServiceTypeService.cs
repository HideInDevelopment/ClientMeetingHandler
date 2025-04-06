using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.serviceType;

namespace ClientMeetingHandler.application.services;

public class ServiceTypeService : IServiceTypeService
{
    private readonly IServiceTypeRepository _serviceTypeRepository;
    private readonly ServiceTypeMapper _serviceTypeMapper;

    public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, ServiceTypeMapper serviceTypeMapper)
    {
        _serviceTypeRepository = serviceTypeRepository;
        _serviceTypeMapper = serviceTypeMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableServiceTypes = await _serviceTypeRepository.GetAllAsync();
        return queryableServiceTypes.ToList().Select(_serviceTypeMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedServiceType = await _serviceTypeRepository.GetByIdAsync(id);
        return storedServiceType == null ? null : _serviceTypeMapper.MapEntityToDto(storedServiceType);
    }

    public async Task AddAsync(IDto dto)
    {
        var serviceTypeToPersist = _serviceTypeMapper.MapDtoToEntity((ServiceTypeDto)dto);
        var existingServiceType = await _serviceTypeRepository.GetByIdAsync(serviceTypeToPersist.Id);
        if (existingServiceType == null)
        {
            throw new EntityAlreadyExistException<ServiceType>(serviceTypeToPersist);
        }
        await _serviceTypeRepository.AddAsync(serviceTypeToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var serviceTypeToUpdate = _serviceTypeMapper.MapDtoToEntity((ServiceTypeDto)dto);
        var queryableServiceTypes = await _serviceTypeRepository.GetAllAsync();
        if (queryableServiceTypes.Any(x => x.Id == serviceTypeToUpdate.Id))
        {
            await _serviceTypeRepository.UpdateAsync(serviceTypeToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<ServiceType>(serviceTypeToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _serviceTypeRepository.DeleteAsync(id);
    public Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}