using System;
using PersonalExpanseManager.Entities;
using PersonalExpanseManager.Enums;

namespace PersonalExpanseManager.Interfaces;

public interface IDespesaRepository
{
    public bool Salvar(Despesa despesa);
    public List<Despesa> BuscarTodas();
    public List<Despesa> BuscarPorCategoria(Categoria categoria);
    public List<Despesa> BuscarPorPeriodo(int mes, int ano);
    Despesa? BuscarPorId(Guid id);
    public bool Deletar(Guid id);
}
