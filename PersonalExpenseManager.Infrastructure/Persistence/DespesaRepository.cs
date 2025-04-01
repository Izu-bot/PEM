using System;
using PersonalExpanseManager.Entities;
using PersonalExpanseManager.Enums;
using PersonalExpanseManager.Interfaces;
using PersonalExpenseManager.Infrastructure.Data;

namespace PersonalExpenseManager.Infrastructure.Persistence;

public class DespesaRepository : IDespesaRepository
{
    private readonly AppDbContext _context;

    public DespesaRepository(AppDbContext context) => _context = context;

    public bool Salvar(Despesa despesa)
    {
        _context.Despesas.Add(despesa);
        return _context.SaveChanges() > 0;
    }

    public List<Despesa> BuscarTodas() => _context.Despesas.ToList();

    public List<Despesa> BuscarPorCategoria(Categoria categoria) => _context.Despesas.Where(d => d.Categoria == categoria).ToList();

    public List<Despesa> BuscarPorPeriodo(int mes, int ano) => _context.Despesas.Where(d => d.Data.Month == mes && d.Data.Year == ano).ToList();

    public bool Deletar(Guid id)
    {
        var despesa = BuscarPorId(id);

        if (despesa == null) return false;

        _context.Despesas.Remove(despesa);
        return _context.SaveChanges() > 0;
    }

    public Despesa? BuscarPorId(Guid id) => _context.Despesas.FirstOrDefault(d => d.Id == id);
}
