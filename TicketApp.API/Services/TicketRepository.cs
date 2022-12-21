using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketApp_API.Contracts;
using TicketApp_API.Data;

namespace TicketApp_API.Services;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _db;

    public TicketRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Ticket entity)
    {
        await _db.Tickets.AddAsync(entity);
        return await Save();
    }

    public async Task<bool> Delete(Ticket entity)
    {
        _db.Tickets.Remove(entity);
        return await Save();
    }

    public async Task<IList<Ticket>> FindAll()
    {
        var tickets = await _db.Tickets
            .Include(q => q.Customer)
            .ToListAsync();
        return tickets;
    }

    public async Task<Ticket> FindById(int id)
    {
        var ticket = await _db.Tickets
            .Include(q => q.Customer)
            .FirstOrDefaultAsync(q => q.Id == id);
        return ticket;
    }

    public async Task<string> GetImageFileName(int id)
    {
        var ticket = await _db.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
        return ticket.Image;
    }

    public async Task<bool> isExists(int id)
    {
        var isExists = await _db.Tickets.AnyAsync(q => q.Id == id);
        return isExists;
    }

    public async Task<bool> Save()
    {
        var changes = await _db.SaveChangesAsync();
        return changes > 0;
    }

    public async Task<bool> Update(Ticket entity)
    {
        _db.Tickets.Update(entity);
        return await Save();
    }
}