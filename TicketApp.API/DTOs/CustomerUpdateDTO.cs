using System.ComponentModel.DataAnnotations;

namespace TicketApp_API.DTOs;

public class CustomerUpdateDTO
{
    public int Id { get; set; }

    [Required] public string City { get; set; }

    [Required] public string Address { get; set; }
}