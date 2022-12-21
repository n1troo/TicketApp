using System.ComponentModel.DataAnnotations;
using TicketApp_API.Data;

namespace TicketApp_API.DTOs;

public class TicketCreateDTO
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Summary { get; set; }
    public string Image { get; set; }
    [Required]
    public string ContactPerson { get; set; }

    public int CustomerId { get; set; }
    public string File { get; internal set; }
    public virtual Customer Customer { get; set; }
}