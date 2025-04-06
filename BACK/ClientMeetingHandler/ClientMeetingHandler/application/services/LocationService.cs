using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.location;

namespace ClientMeetingHandler.application.services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly LocationMapper _locationMapper;
    private readonly IReadOnlyList<string> _locationIncludes = ["Meetings"];

    public LocationService(ILocationRepository locationRepository, LocationMapper locationMapper)
    {
        _locationRepository = locationRepository;
        _locationMapper = locationMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableLocations = await _locationRepository.GetAllAsync();
        return queryableLocations.ToList().Select(_locationMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedLocation = await _locationRepository.GetByIdAsync(id);
        return storedLocation == null ? null : _locationMapper.MapEntityToDto(storedLocation);
    }

    public async Task AddAsync(IDto dto)
    {
        var locationToPersist = _locationMapper.MapDtoToEntity((LocationDto)dto);
        var existingLocation = await _locationRepository.GetByIdAsync(locationToPersist.Id);
        if (existingLocation == null)
        {
            throw new EntityAlreadyExistException<Location>(locationToPersist);
        }
        await _locationRepository.AddAsync(locationToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var locationToUpdate = _locationMapper.MapDtoToEntity((LocationDto)dto);
        var queryableLocations = await _locationRepository.GetAllAsync();
        if (queryableLocations.Any(x => x.Id == locationToUpdate.Id))
        {
            await _locationRepository.UpdateAsync(locationToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<Location>(locationToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _locationRepository.DeleteAsync(id);

    public async Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        var queryableLocationsWithIncludes = await _locationRepository.GetQueryWithIncludesAsync(_locationIncludes);
        return queryableLocationsWithIncludes.ToList().Select(_locationMapper.MapDetailEntityToDetailDto);
    }

    public async Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        var storedLocationWithIncludes = await _locationRepository.GetSingleWithIncludesAsync(x => x.Id.Equals(id), _locationIncludes);
        return storedLocationWithIncludes == null ? null : _locationMapper.MapDetailEntityToDetailDto(storedLocationWithIncludes);
    }
}