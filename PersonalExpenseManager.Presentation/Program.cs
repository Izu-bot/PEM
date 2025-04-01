using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalExpanseManager.Interfaces;
using PersonalExpenseManager.Application.UseCases;
using PersonalExpenseManager.Infrastructure.Data;
using PersonalExpenseManager.Infrastructure.Persistence;

var builder = new ServiceCollection();

// Configura o banco de dados
builder.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Souce=despesas.db"));

// Registrar o repositório e os casos de uso
builder.AddScoped<IDespesaRepository, DespesaRepository>();
builder.AddScoped<AdicionarDespesaUseCase>();
builder.AddScoped<ListarDespesasUseCase>();
builder.AddScoped<FiltrarDesepsasUseCase>();
builder.AddScoped<RemoverDespesaUseCase>();
builder.AddScoped<GerarRelatorioUseCase>();

// Criar o provedor de serviços
var serviceProvider = builder.BuildServiceProvider();

// Criar o banco de dados se ele não existir
using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}