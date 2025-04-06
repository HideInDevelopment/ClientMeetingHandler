using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.meeting;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class MeetingController : ControllerBase
{
    private readonly IMeetingService _meetingService;

    public MeetingController(IMeetingService meetingService)
    {
        _meetingService = meetingService;
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult> Get()
    {
        var response = await _meetingService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("simple/{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _meetingService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Meeting doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail")]
    public async Task<ActionResult> GetDetail()
    {
        var response = await _meetingService.GetAllWithIncludesAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("detail/{key:guid}")]
    public async Task<ActionResult> GetDetail([FromRoute] Guid key)
    {
        var response = await _meetingService.GetByIdWithIncludesAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Meeting doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] MeetingDto entity)
    {
        try
        {
            await _meetingService.AddAsync(entity);
            var storedMeeting = await _meetingService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedMeeting);
        }
        catch (FailOnPersistEntityException<Meeting> e)
        {
            return BadRequest($"Meeting in {entity.Date} can not be stored.");
        }
        catch (EntityAlreadyExistException<Meeting> e)
        {
            return BadRequest($"Meeting in {entity.Date} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] MeetingDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _meetingService.UpdateAsync(entity);
            
            var updatedMeeting = await _meetingService.GetByIdAsync(entity.Id);
        
            return Ok(updatedMeeting);
        }
        catch (FailOnPersistEntityException<Meeting> e)
        {
            return BadRequest($"Changes on Meeting in {entity.Date} can not be saved.");
        }
        catch (EntityNotFoundException<Meeting> e)
        {
            return NotFound($"Meeting in {entity.Date} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _meetingService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<Meeting> e)
        {
            return BadRequest("Meeting can not be deleted.");
        }
        catch (EntityNotFoundException<Meeting> e)
        {
            return NotFound("Meeting doesn't exist yet.");
        }
    }
}