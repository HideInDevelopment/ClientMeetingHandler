using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.location;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult> Get()
    {
        var response = await _locationService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("simple/{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _locationService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Location doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail")]
    public async Task<ActionResult> GetDetail()
    {
        var response = await _locationService.GetAllWithIncludesAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("detail/{key:guid}")]
    public async Task<ActionResult> GetDetail([FromRoute] Guid key)
    {
        var response = await _locationService.GetByIdWithIncludesAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Location doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] LocationDto entity)
    {
        try
        {
            await _locationService.AddAsync(entity);
            var storedLocation = await _locationService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedLocation);
        }
        catch (FailOnPersistEntityException<Location> e)
        {
            return BadRequest($"Location in {entity.Street} can not be stored.");
        }
        catch (EntityAlreadyExistException<Location> e)
        {
            return BadRequest($"Location in {entity.Street} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] LocationDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _locationService.UpdateAsync(entity);
            
            var updatedLocation = await _locationService.GetByIdAsync(entity.Id);
        
            return Ok(updatedLocation);
        }
        catch (FailOnPersistEntityException<Location> e)
        {
            return BadRequest($"Changes on Location in {entity.Street} can not be saved.");
        }
        catch (EntityNotFoundException<Location> e)
        {
            return NotFound($"Location in {entity.Street} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _locationService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<Location> e)
        {
            return BadRequest("Location can not be deleted.");
        }
        catch (EntityNotFoundException<Location> e)
        {
            return NotFound("Location doesn't exist yet.");
        }
    }
}