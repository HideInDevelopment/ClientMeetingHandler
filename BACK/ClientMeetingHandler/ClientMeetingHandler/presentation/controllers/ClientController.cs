using ClientMeetingHandler.common.Validators;
using ClientMeetingHandler.domain.entities;
using ClientMeetingHandler.domain.exceptions;
using ClientMeetingHandler.domain.services;
using ClientMeetingHandler.presentation.dto;
using Microsoft.AspNetCore.Mvc;

namespace ClientMeetingHandler.presentation.controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet()]
    public async Task<ActionResult> Get()
    {
        var response = await _clientService.GetAllAsync();

        if (ListValidator.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("{key:guid}")]
    public async Task<ActionResult> Get([FromRoute] Guid key)
    {
        var response = await _clientService.GetByIdAsync(key);

        if (EntityValidator.IsNullOrDefault(response))
        {
            return NotFound("Client doesn't exist yet.");
        }
        
        return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> Create([FromBody] ClientDto entity)
    {
        try
        {
            await _clientService.AddAsync(entity);
            var storedClient = await _clientService.GetByIdAsync(entity.Id);
            
            return CreatedAtAction(nameof(Get), new { key = entity.Id }, storedClient);
        }
        catch (FailOnPersistEntityException<Client> e)
        {
            return BadRequest($"Client {entity.Name} can not be stored.");
        }
        catch (EntityAlreadyExistException<Client> e)
        {
            return BadRequest($"Client {entity.Name} already exists.");
        }
    }

    [HttpPut("{key:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid key, [FromBody] ClientDto entity)
    {
        if (key != entity.Id)
        {
            return BadRequest("ID in route must match ID in body");
        }
        
        try
        {
            await _clientService.UpdateAsync(entity);
            
            var updatedClient = await _clientService.GetByIdAsync(entity.Id);
        
            return Ok(updatedClient);
        }
        catch (FailOnPersistEntityException<Client> e)
        {
            return BadRequest($"Changes on Client {entity.Name} can not be saved.");
        }
        catch (EntityNotFoundException<Client> e)
        {
            return NotFound($"Client {entity.Name} doesn't exist yet.");
        }
    }
    
    [HttpDelete("{key:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid key){
        try
        {
            await _clientService.DeleteAsync(key);
            return NoContent();
        }
        catch (FailOnPersistEntityException<Client> e)
        {
            return BadRequest("Client can not be deleted.");
        }
        catch (EntityNotFoundException<Client> e)
        {
            return NotFound("Client doesn't exist yet.");
        }
    }
}