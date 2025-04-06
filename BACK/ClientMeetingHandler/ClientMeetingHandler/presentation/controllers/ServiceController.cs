using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.service;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult> Get()
    {
        var response = await _serviceService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("simple/{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _serviceService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Service doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail")]
    public async Task<ActionResult> GetDetail()
    {
        var response = await _serviceService.GetAllWithIncludesAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("detail/{key:guid}")]
    public async Task<ActionResult> GetDetail([FromRoute] Guid key)
    {
        var response = await _serviceService.GetByIdWithIncludesAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Service doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] ServiceDto entity)
    {
        try
        {
            await _serviceService.AddAsync(entity);
            var storedService = await _serviceService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedService);
        }
        catch (FailOnPersistEntityException<Service> e)
        {
            return BadRequest($"Service {entity.Name} can not be stored.");
        }
        catch (EntityAlreadyExistException<Service> e)
        {
            return BadRequest($"Service {entity.Name} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] ServiceDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _serviceService.UpdateAsync(entity);
            
            var updatedService = await _serviceService.GetByIdAsync(entity.Id);
        
            return Ok(updatedService);
        }
        catch (FailOnPersistEntityException<Service> e)
        {
            return BadRequest($"Changes on Service {entity.Name} can not be saved.");
        }
        catch (EntityNotFoundException<Service> e)
        {
            return NotFound($"Service {entity.Name} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _serviceService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<Service> e)
        {
            return BadRequest("Service can not be deleted.");
        }
        catch (EntityNotFoundException<Service> e)
        {
            return NotFound("Service doesn't exist yet.");
        }
    }
}