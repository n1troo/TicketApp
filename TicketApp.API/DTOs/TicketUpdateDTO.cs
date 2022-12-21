using System.ComponentModel.DataAnnotations;

namespace TicketApp_API.DTOs;

public class TicketUpdateDTO
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public double? Time { get; set; }
    public string Image { get; set; }
        
    public string File { get; set; }


}