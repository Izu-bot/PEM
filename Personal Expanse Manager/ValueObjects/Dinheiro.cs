using System;

namespace PersonalExpanseManager.ValueObjects;

public record Dinheiro
{
    public decimal Valor { get; }

    public Dinheiro(decimal valor)
    {
        if (valor < 0)
            throw new ArgumentException("Valor não pode ser negativo.", nameof(valor));

        Valor = valor;
    }

    public override string ToString() => $"R$: {Valor:F2}";
}
