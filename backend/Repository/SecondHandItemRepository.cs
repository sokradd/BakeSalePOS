using BakeSale.API.Data;
using BakeSale.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeSale.API.Repository;

public class SecondHandItemRepository
{
    private readonly BakeSaleContext _context;
    
    public SecondHandItemRepository(BakeSaleContext context)
    {
        _context = context;
    }

    public async Task<SecondHandItem> AddSecondHandItemAsync(SecondHandItem secondHandItem)
    {
        _context.SecondHandItems.Add(secondHandItem);
        await _context.SaveChangesAsync();
        return secondHandItem;
    }

    public async Task<IEnumerable<SecondHandItem>> GetAllSecondHandItemsAsync()
    {
        return await _context.SecondHandItems.AsNoTracking().ToListAsync();
    }

    public async Task<SecondHandItem?> GetSecondHandItemByIdAsync(int id)
    {
        return await _context.SecondHandItems.FindAsync(id);
    }

    public async Task UpdateSecondHandItemAsync(SecondHandItem secondHandItem)
    {
        _context.Entry(secondHandItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}