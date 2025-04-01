using System;
using PersonalExpanseManager.Entities;
using PersonalExpanseManager.Enums;
using PersonalExpanseManager.Interfaces;

namespace PersonalExpenseManager.Infrastructure.Persistence;

public class DespesaRepository : IDespesaRepository
{
    private readonly List<Despesa> _despesa = new();

    public bool Salvar(Despesa despesa)
    {
        _despesa.Add(despesa);
        return true;
    }

    public List<Despesa> BuscarTodas() => _despesa;

    public List<Despesa> BuscarPorCategoria(Categoria categoria) => _despesa.Where(d => d.Categoria == categoria).ToList();

    public List<Despesa> BuscarPorPeriodo(int mes, int ano) => _despesa.Where(d => d.Data.Month == mes && d.Data.Year == ano).ToList();

    public bool Deletar(Guid id)
    {
        var despesa = BuscarPorId(id);

        if (despesa == null) return false;

        _despesa.Remove(despesa);
        return true;
    }

    public Despesa? BuscarPorId(Guid id) => _despesa.FirstOrDefault(d => d.Id == id);
}
