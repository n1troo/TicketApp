using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketApp_API.Contracts;
using TicketApp_API.Data;
using TicketApp_API.DTOs;

namespace TicketApp_API.Controllers;

/// <summary>
///     Interacts with the tickets Table
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TicketController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;

    public TicketController(ITicketRepository ticketRepository,
        ILoggerService logger,
        IWebHostEnvironment env,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _logger = logger;
        _mapper = mapper;
        _env = env;
    }

    private string GetImagePath(string fileName)
    {
        return $"{_env.ContentRootPath}\\uploads\\{fileName}";
    }


    /// <summary>
    ///     Get All tickets
    /// </summary>
    /// <returns>A List of tickets</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTicket()
    {
        var location = GetControllerActionNames();
        try
        {
            _logger.LogInfo($"{location}: Attempted Call");
            var tickets = await _ticketRepository.FindAll();
            var response = _mapper.Map<IList<TicketDTO>>(tickets);
            foreach (var item in response)
                if (!string.IsNullOrEmpty(item.Image))
                {
                    var imgPath = GetImagePath(item.Image);
                    if (System.IO.File.Exists(imgPath))
                    {
                        var imgBytes = System.IO.File.ReadAllBytes(imgPath);
                        item.File = Convert.ToBase64String(imgBytes);
                    }
                }

            _logger.LogInfo($"{location}: Successful");
            return Ok(response);
        }
        catch (Exception e)
        {
            return InternalError($"{location}: {e.Message} - {e.InnerException}");
        }
    }

    /// <summary>
    ///     Gets a Ticket by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Ticket record</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Getticket(int id)
    {
        var location = GetControllerActionNames();
        try
        {
            _logger.LogInfo($"{location}: Attempted Call for id: {id}");
            var ticket = await _ticketRepository.FindById(id);
            if (ticket == null)
            {
                _logger.LogWarn($"{location}: Failed to retrieve record with id: {id}");
                return NotFound();
            }

            var response = _mapper.Map<TicketDTO>(ticket);
            if (!string.IsNullOrEmpty(response.Image))
            {
                var imgPath = GetImagePath(ticket.Image);
                if (System.IO.File.Exists(imgPath))
                {
                    var imgBytes = System.IO.File.ReadAllBytes(imgPath);
                    response.File = Convert.ToBase64String(imgBytes);
                }
            }

            _logger.LogInfo($"{location}: Successfully got record with id: {id}");
            return Ok(response);
        }
        catch (Exception e)
        {
            return InternalError($"{location}: {e.Message} - {e.InnerException}");
        }
    }

    /// <summary>
    ///     Creates a new ticket
    /// </summary>
    /// <param name="TicketDTO"></param>
    /// <returns>Ticket Object</returns>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] TicketCreateDTO ticketDTO)
    {
        var location = GetControllerActionNames();
        try
        {
            _logger.LogInfo($"{location}: Create Attempted");
            if (ticketDTO == null)
            {
                _logger.LogWarn($"{location}: Empty Request was submitted");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn($"{location}: Data was Incomplete");
                return BadRequest(ModelState);
            }

            var ticket = _mapper.Map<Ticket>(ticketDTO);
            var isSuccess = await _ticketRepository.Create(ticket);
            if (!isSuccess) return InternalError($"{location}: Creation failed");
            if (!string.IsNullOrEmpty(ticketDTO.File))
            {
                var imgPath = GetImagePath(ticketDTO.Image);
                var imageBytes = Convert.FromBase64String(ticketDTO.File);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);
            }

            _logger.LogInfo($"{location}: Creation was successful");
            return Created("Create", new { ticket });
        }
        catch (Exception e)
        {
            return InternalError($"{location}: {e.Message} - {e.InnerException}");
        }
    }

    /// <summary>
    ///     Update a Ticket by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ticketDTO"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] TicketUpdateDTO ticketDTO)
    {
        var location = GetControllerActionNames();
        try
        {
            _logger.LogInfo($"{location}: Update Attempted on record with id: {id} ");
            if (id < 1 || ticketDTO == null || id != ticketDTO.Id)
            {
                _logger.LogWarn($"{location}: Update failed with bad data - id: {id}");
                return BadRequest();
            }

            var isExists = await _ticketRepository.isExists(id);

            if (!isExists)
            {
                _logger.LogWarn($"{location}: Failed to retrieve record with id: {id}");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn($"{location}: Data was Incomplete");
                return BadRequest(ModelState);
            }

            var oldImage = await _ticketRepository.GetImageFileName(id);
            var ticket = _mapper.Map<Ticket>(ticketDTO);
            var isSuccess = await _ticketRepository.Update(ticket);
            if (!isSuccess) return InternalError($"{location}: Update failed for record with id: {id}");

            if (!ticketDTO.Image.Equals(oldImage))
                if (System.IO.File.Exists(GetImagePath(oldImage)))
                    System.IO.File.Delete(GetImagePath(oldImage));

            if (!string.IsNullOrEmpty(ticketDTO.File))
            {
                var imageBytes = Convert.FromBase64String(ticketDTO.File);
                System.IO.File.WriteAllBytes(GetImagePath(ticketDTO.Image), imageBytes);
            }

            _logger.LogInfo($"{location}: Record with id: {id} successfully updated");
            return NoContent();
        }
        catch (Exception e)
        {
            return InternalError($"{location}: {e.Message} - {e.InnerException}");
        }
    }

    /// <summary>
    ///     Removes an ticket by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var location = GetControllerActionNames();
        try
        {
            _logger.LogInfo($"{location}: Delete Attempted on record with id: {id} ");
            if (id < 1)
            {
                _logger.LogWarn($"{location}: Delete failed with bad data - id: {id}");
                return BadRequest();
            }

            var isExists = await _ticketRepository.isExists(id);
            if (!isExists)
            {
                _logger.LogWarn($"{location}: Failed to retrieve record with id: {id}");
                return NotFound();
            }

            var ticket = await _ticketRepository.FindById(id);
            var isSuccess = await _ticketRepository.Delete(ticket);
            if (!isSuccess) return InternalError($"{location}: Delete failed for record with id: {id}");
            _logger.LogInfo($"{location}: Record with id: {id} successfully deleted");
            return NoContent();
        }
        catch (Exception e)
        {
            return InternalError($"{location}: {e.Message} - {e.InnerException}");
        }
    }

    private string GetControllerActionNames()
    {
        var controller = ControllerContext.ActionDescriptor.ControllerName;
        var action = ControllerContext.ActionDescriptor.ActionName;

        return $"{controller} - {action}";
    }

    private ObjectResult InternalError(string message)
    {
        _logger.LogError(message);
        return StatusCode(500, "Something went wrong. Please contact the Administrator");
    }
}