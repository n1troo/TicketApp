using System;

namespace TicketApp_API.Data
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}
