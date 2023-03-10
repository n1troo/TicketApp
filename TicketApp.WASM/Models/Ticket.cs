namespace TicketApp_UI.WASM.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Image { get; set; }
    public string ContactPerson { get; set; }
    public double? Time { get; set; }

    public string File { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}