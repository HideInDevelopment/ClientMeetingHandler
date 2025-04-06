using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.note;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult> Get()
    {
        var response = await _noteService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("simple/{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _noteService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Note doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail")]
    public async Task<ActionResult> GetDetail()
    {
        var response = await _noteService.GetAllWithIncludesAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("detail/{key:guid}")]
    public async Task<ActionResult> GetDetail([FromRoute] Guid key)
    {
        var response = await _noteService.GetByIdWithIncludesAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Note doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] NoteDto entity)
    {
        try
        {
            await _noteService.AddAsync(entity);
            var storedNote = await _noteService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedNote);
        }
        catch (FailOnPersistEntityException<Note> e)
        {
            return BadRequest($"Note {entity.Title} can not be stored.");
        }
        catch (EntityAlreadyExistException<Note> e)
        {
            return BadRequest($"Note {entity.Title} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] NoteDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _noteService.UpdateAsync(entity);
            
            var updatedNote = await _noteService.GetByIdAsync(entity.Id);
        
            return Ok(updatedNote);
        }
        catch (FailOnPersistEntityException<Note> e)
        {
            return BadRequest($"Changes on Note {entity.Title} can not be saved.");
        }
        catch (EntityNotFoundException<Note> e)
        {
            return NotFound($"Note {entity.Title} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _noteService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<Note> e)
        {
            return BadRequest("Note can not be deleted.");
        }
        catch (EntityNotFoundException<Note> e)
        {
            return NotFound("Note doesn't exist yet.");
        }
    }
}