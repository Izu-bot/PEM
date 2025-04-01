using PersonalExpanseManager.Enums;
using PersonalExpanseManager.ValueObjects;

namespace PersonalExpanseManager.Entities;

public class Despesa
{

    public Guid Id { get; } = Guid.NewGuid();
    public Dinheiro Valor { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public Categoria Categoria { get; private set; } = new Categoria();
    public DateOnly Data { get; private set; }

    public Despesa(decimal valor, Categoria categoria, DateOnly data)
    {
        Id = Guid.NewGuid();
        Valor = new Dinheiro(valor);
        Categoria = categoria;
        Data = data;
    }
}
