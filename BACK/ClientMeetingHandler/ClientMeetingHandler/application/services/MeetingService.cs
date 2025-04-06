using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.meeting;

namespace ClientMeetingHandler.application.services;

public class MeetingService : IMeetingService
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly MeetingMapper _meetingMapper;
    private readonly IReadOnlyList<string> _meetingIncludes = ["Location", "Meeting"];

    public MeetingService(IMeetingRepository meetingRepository, MeetingMapper meetingMapper)
    {
        _meetingRepository = meetingRepository;
        _meetingMapper = meetingMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableMeetings = await _meetingRepository.GetAllAsync();
        return queryableMeetings.ToList().Select(_meetingMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedMeeting = await _meetingRepository.GetByIdAsync(id);
        return storedMeeting == null ? null : _meetingMapper.MapEntityToDto(storedMeeting);
    }

    public async Task AddAsync(IDto dto)
    {
        var meetingToPersist = _meetingMapper.MapDtoToEntity((MeetingDto)dto);
        var existingMeeting = await _meetingRepository.GetByIdAsync(meetingToPersist.Id);
        if (existingMeeting == null)
        {
            throw new EntityAlreadyExistException<Meeting>(meetingToPersist);
        }
        await _meetingRepository.AddAsync(meetingToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var meetingToUpdate = _meetingMapper.MapDtoToEntity((MeetingDto)dto);
        var queryableMeetings = await _meetingRepository.GetAllAsync();
        if (queryableMeetings.Any(x => x.Id == meetingToUpdate.Id))
        {
            await _meetingRepository.UpdateAsync(meetingToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<Meeting>(meetingToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _meetingRepository.DeleteAsync(id);
    
    public async Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        var queryableMeetingsWithIncludes = await _meetingRepository.GetQueryWithIncludesAsync(_meetingIncludes);
        return queryableMeetingsWithIncludes.ToList().Select(_meetingMapper.MapDetailEntityToDetailDto);
    }

    public async Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        var storedMeetingWithIncludes = await _meetingRepository.GetSingleWithIncludesAsync(x => x.Id.Equals(id), _meetingIncludes);
        return storedMeetingWithIncludes == null ? null : _meetingMapper.MapDetailEntityToDetailDto(storedMeetingWithIncludes);
    }
}