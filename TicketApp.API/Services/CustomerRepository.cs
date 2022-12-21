using TicketApp_API.Contracts;
using TicketApp_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp_API.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Customer entity)
        {
            await _db.Customers.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Customer entity)
        {
            _db.Customers.Remove(entity);
            return await Save();
        }

        public async Task<IList<Customer>> FindAll()
        {
            var customers = await _db.Customers
                .Include(q => q.Tickets)
                .ToListAsync();
            return customers;
        }

        public async Task<Customer> FindById(int id)
        {
            var author = await _db.Customers
                .Include(q => q.Tickets)
                .FirstOrDefaultAsync(q => q.Id == id);
            return author;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Customers.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Customer entity)
        {
            _db.Customers.Update(entity);
            return await Save();
        }
    }
}
