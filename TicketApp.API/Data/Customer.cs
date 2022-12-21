using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp_API.Data;

[Table("AppCustomers")]
public class Customer : BaseModel
{
    public string Name { get; set; }
    public string NIP {get;set;}
    public string Address { get; set; }
    public string City { get; set; }

    public string Description { get; set; }

    public virtual IList<Ticket> Tickets { get; set; }
}