using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repositories
{
    public class SalespersonRepository
    {
        private readonly BakeSaleContext _context;

        public SalespersonRepository(BakeSaleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salesperson>> GetAllSalespersonsAsync()
        {
            return await _context.Salespersons.ToListAsync();
        }

        public async Task<Salesperson> AddSalespersonAsync(Salesperson salesperson)
        {
            _context.Salespersons.Add(salesperson);
            await _context.SaveChangesAsync();
            return salesperson;
        }

        public async Task<Salesperson?> GetSalespersonByIdAsync(int id)
        {
            return await _context.Salespersons.FindAsync(id);
        }

        public async Task UpdateSalespersonAsync(Salesperson salesperson)
        {
            _context.Entry(salesperson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalespersonAsync(int id)
        {
            var salesperson = await _context.Salespersons.FindAsync(id);
            if (salesperson != null)
            {
                _context.Salespersons.Remove(salesperson);
                await _context.SaveChangesAsync();
            }
        }
    }
}