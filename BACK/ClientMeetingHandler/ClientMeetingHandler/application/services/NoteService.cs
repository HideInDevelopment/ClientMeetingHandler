using ClientMeetingHandler.application.mappings;
using ClientMeetingHandler.common.contracts;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.repositories;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.note;

namespace ClientMeetingHandler.application.services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly NoteMapper _noteMapper;
    private readonly IReadOnlyList<string> _noteIncludes = ["Service"];

    public NoteService(INoteRepository noteRepository, NoteMapper noteMapper)
    {
        _noteRepository = noteRepository;
        _noteMapper = noteMapper;
    }
    
    public async Task<IEnumerable<IDto>> GetAllAsync()
    {
        var queryableNotes = await _noteRepository.GetAllAsync();
        return queryableNotes.ToList().Select(_noteMapper.MapEntityToDto);
    }

    public async Task<IDto?> GetByIdAsync(Guid id)
    {
        var storedNote = await _noteRepository.GetByIdAsync(id);
        return storedNote == null ? null : _noteMapper.MapEntityToDto(storedNote);
    }

    public async Task AddAsync(IDto dto)
    {
        var noteToPersist = _noteMapper.MapDtoToEntity((NoteDto)dto);
        var existingNote = await _noteRepository.GetByIdAsync(noteToPersist.Id);
        if (existingNote == null)
        {
            throw new EntityAlreadyExistException<Note>(noteToPersist);
        }
        await _noteRepository.AddAsync(noteToPersist);
    }

    public async Task UpdateAsync(IDto dto)
    {
        var noteToUpdate = _noteMapper.MapDtoToEntity((NoteDto)dto);
        var queryableNotes = await _noteRepository.GetAllAsync();
        if (queryableNotes.Any(x => x.Id == noteToUpdate.Id))
        {
            await _noteRepository.UpdateAsync(noteToUpdate);
        }
        else
        {
            throw new EntityNotFoundException<Note>(noteToUpdate);
        }
    }

    public async Task DeleteAsync(Guid id) => await _noteRepository.DeleteAsync(id);
    
    public async Task<IEnumerable<IDto?>> GetAllWithIncludesAsync()
    {
        var queryableNotesWithIncludes = await _noteRepository.GetQueryWithIncludesAsync(_noteIncludes);
        return queryableNotesWithIncludes.ToList().Select(_noteMapper.MapDetailEntityToDetailDto);
    }

    public async Task<IDto?> GetByIdWithIncludesAsync(Guid id)
    {
        var storedNoteWithIncludes = await _noteRepository.GetSingleWithIncludesAsync(x => x.Id.Equals(id), _noteIncludes);
        return storedNoteWithIncludes == null ? null : _noteMapper.MapDetailEntityToDetailDto(storedNoteWithIncludes);
    }
}