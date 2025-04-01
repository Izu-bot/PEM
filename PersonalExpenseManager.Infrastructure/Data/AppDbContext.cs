using System;
using Microsoft.EntityFrameworkCore;
using PersonalExpanseManager.Entities;
using PersonalExpanseManager.ValueObjects;

namespace PersonalExpenseManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Despesa> Despesas { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Despesa>()
            .Property(d => d.Valor)
            .HasConversion(
                v => v.Valor, // Converte ValueObject Dinheiro para decimal
                v => new Dinheiro(v) // Converte de decimal para Dinheiro ao ler do BD
            );
    }
}
