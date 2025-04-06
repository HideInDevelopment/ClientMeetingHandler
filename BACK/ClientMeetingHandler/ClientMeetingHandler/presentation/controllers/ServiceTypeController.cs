using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.serviceType;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class ServiceTypeController : ControllerBase
{
    private readonly IServiceTypeService _serviceTypeService;

    public ServiceTypeController(IServiceTypeService serviceTypeService)
    {
        _serviceTypeService = serviceTypeService;
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult> Get()
    {
        var response = await _serviceTypeService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("simple/{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _serviceTypeService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("ServiceType doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail")]
    public async Task<ActionResult> GetDetail()
    {
        var response = await _serviceTypeService.GetAllWithIncludesAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("detail/{key:guid}")]
    public async Task<ActionResult> GetDetail([FromRoute] Guid key)
    {
        var response = await _serviceTypeService.GetByIdWithIncludesAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("ServiceType doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] ServiceTypeDto entity)
    {
        try
        {
            await _serviceTypeService.AddAsync(entity);
            var storedServiceType = await _serviceTypeService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedServiceType);
        }
        catch (FailOnPersistEntityException<ServiceType> e)
        {
            return BadRequest($"ServiceType {entity.Name} can not be stored.");
        }
        catch (EntityAlreadyExistException<ServiceType> e)
        {
            return BadRequest($"ServiceType {entity.Name} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] ServiceTypeDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _serviceTypeService.UpdateAsync(entity);
            
            var updatedServiceType = await _serviceTypeService.GetByIdAsync(entity.Id);
        
            return Ok(updatedServiceType);
        }
        catch (FailOnPersistEntityException<ServiceType> e)
        {
            return BadRequest($"Changes on ServiceType {entity.Name} can not be saved.");
        }
        catch (EntityNotFoundException<ServiceType> e)
        {
            return NotFound($"ServiceType {entity.Name} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _serviceTypeService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<ServiceType> e)
        {
            return BadRequest("ServiceType can not be deleted.");
        }
        catch (EntityNotFoundException<ServiceType> e)
        {
            return NotFound("ServiceType doesn't exist yet.");
        }
    }
}