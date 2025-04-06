using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto.contact;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult> Get()
    {
        var response = await _contactService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("simple/{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _contactService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Contact doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("simple/{email}")]
    public async Task<ActionResult> GetByEmail([FromRoute] string email)
    {
        var response = await _contactService.GetByEmail(email);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Contact doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail")]
    public async Task<ActionResult> GetDetail()
    {
        var response = await _contactService.GetAllWithIncludesAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("detail/{key:guid}")]
    public async Task<ActionResult> GetDetail([FromRoute] Guid key)
    {
        var response = await _contactService.GetByIdWithIncludesAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Contact doesn't exist yet.");
        }
        
        return Ok(response);
    }
    
    [HttpGet("detail/{email}")]
    public async Task<ActionResult> Get([FromRoute] string email)
    {
        var response = await _contactService.GetDetailByEmail(email);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Contact doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] ContactDto entity)
    {
        try
        {
            await _contactService.AddAsync(entity);
            var storedContact = await _contactService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedContact);
        }
        catch (FailOnPersistEntityException<Contact> e)
        {
            return BadRequest($"Contact {entity.Email} can not be stored.");
        }
        catch (EntityAlreadyExistException<Contact> e)
        {
            return BadRequest($"Contact {entity.Email} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] ContactDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _contactService.UpdateAsync(entity);
            
            var updatedContact = await _contactService.GetByIdAsync(entity.Id);
        
            return Ok(updatedContact);
        }
        catch (FailOnPersistEntityException<Contact> e)
        {
            return BadRequest($"Changes on Contact {entity.Email} can not be saved.");
        }
        catch (EntityNotFoundException<Contact> e)
        {
            return NotFound($"Contact {entity.Email} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _contactService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<Contact> e)
        {
            return BadRequest("Contact can not be deleted.");
        }
        catch (EntityNotFoundException<Contact> e)
        {
            return NotFound("Contact doesn't exist yet.");
        }
    }
}