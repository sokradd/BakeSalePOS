using BakeSale.API.Data;
using BakeSale.API.Repositories;
using BakeSale.API.Repository;
using BakeSale.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Host=localhost;Port=5432;Database=BakeSaleDB;Username=postgres;Password=password";
builder.Services.AddDbContext<BakeSaleContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductRepository, ProductRepository>();
builder.Services.AddScoped<SecondHandItemRepository, SecondHandItemRepository>();
builder.Services.AddScoped<InventoryService, InventoryService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BakeSaleContext>();

    await dbContext.Database.MigrateAsync();

    var seeder = new DataSeeder(dbContext);
    await seeder.SeedAsync();
}

app.MapControllers();

app.Run();