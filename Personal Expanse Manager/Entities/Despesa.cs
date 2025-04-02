using PersonalExpanseManager.Enums;
using PersonalExpanseManager.ValueObjects;

namespace PersonalExpanseManager.Entities;

public class Despesa
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Dinheiro Valor { get; private set; }
    public Categoria Categoria { get; private set; } = new Categoria();
    public DateOnly Data { get; private set; }

    public Despesa(decimal valor, Categoria categoria, DateOnly data)
    {
        Valor = new Dinheiro(valor);
        Categoria = categoria;
        Data = data;
    }

    private Despesa() 
    {
        Valor = new Dinheiro(0);
    }
}
