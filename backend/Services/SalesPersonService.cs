using BakeSale.API.Models;
using BakeSale.API.Repositories;

namespace BakeSale.API.Services;

public class SalesPersonService
{
    private readonly SalespersonRepository _salespersonRepository;

    public SalesPersonService(SalespersonRepository salespersonRepository)
    {
        _salespersonRepository = salespersonRepository;
    }

    public async Task<IEnumerable<Salesperson>> GetSalespersonsAsync()
    {
        return await _salespersonRepository.GetAllSalespersonsAsync();
    }

    public async Task<Salesperson?> GetSalespersonByIdAsync(int id)
    {
        return await _salespersonRepository.GetSalespersonByIdAsync(id);
    }

    public async Task<Salesperson> CreateSalespersonAsync(Salesperson salesperson)
    {
        return await _salespersonRepository.AddSalespersonAsync(salesperson);
    }

    public async Task UpdateSalespersonAsync(int id, Salesperson updatedSalesperson)
    {
        updatedSalesperson.Id = id;
        await _salespersonRepository.UpdateSalespersonAsync(updatedSalesperson);
    }

    public async Task DeleteSalespersonAsync(int id)
    {
        await _salespersonRepository.DeleteSalespersonAsync(id);
    }
}