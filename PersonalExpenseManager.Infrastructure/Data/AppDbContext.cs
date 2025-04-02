using System;
using Microsoft.EntityFrameworkCore;
using PersonalExpanseManager.Entities;
using PersonalExpanseManager.ValueObjects;

namespace PersonalExpenseManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Despesa> Despesa { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.ToTable("Despesa");

            entity.HasKey(d => d.Id);

            entity.Property(d => d.Valor)
                .HasConversion(
                    v => v.Valor,
                    v => new Dinheiro(v)
                );
            
            entity.Property(d => d.Data);

            entity.Property(d => d.Categoria)
                .HasConversion<string>(); // Converte o enum para uma string
        });

        base.OnModelCreating(modelBuilder);
    }
}
