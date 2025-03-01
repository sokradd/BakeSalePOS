using BakeSale.API.DTOs;
using BakeSale.API.Models;
using BakeSale.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalespersonController : ControllerBase
    {
        private readonly SalesPersonService _salesPersonService;

        public SalespersonController(SalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }

        //POST : api/createSalesPerson
        [HttpPost("createSalesPerson")]
        public async Task<ActionResult<SalespersonDto>> CreateSalesPerson([FromBody] SalespersonDto salespersonDto)
        {
            try
            {
                var salesperson = new Salesperson
                {
                    Name = salespersonDto.Name
                };
                var created = await _salesPersonService.CreateSalespersonAsync(salesperson);
                var resultDto = new SalespersonDto
                {
                    Id = created.Id,
                    Name = created.Name
                };
                return CreatedAtAction(nameof(GetSalesPersonById), new { id = resultDto.Id }, resultDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        //GET : api/getAllSalesPersons
        [HttpGet("getAllSalesPersons")]
        public async Task<ActionResult<IEnumerable<SalespersonDto>>> GetAllSalesPersons()
        {
            var salespersons = await _salesPersonService.GetSalespersonsAsync();
            var dtoList = salespersons.Select(s => new SalespersonDto
            {
                Id = s.Id,
                Name = s.Name
            });
            return Ok(dtoList);
        }

        //GET : api/getSalesPersonById/{id}
        [HttpGet("getSalesPersonById/{id}")]
        public async Task<ActionResult<SalespersonDto>> GetSalesPersonById(int id)
        {
            var salesperson = await _salesPersonService.GetSalespersonByIdAsync(id);
            if (salesperson == null)
                return NotFound();

            var dto = new SalespersonDto
            {
                Id = salesperson.Id,
                Name = salesperson.Name
            };
            return Ok(dto);
        }
        
    }
}