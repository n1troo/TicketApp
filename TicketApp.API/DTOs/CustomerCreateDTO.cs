using System.ComponentModel.DataAnnotations;

namespace TicketApp_API.DTOs;

public class CustomerCreateDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string NIP { get; set; }
    public string City { get; set; }
}