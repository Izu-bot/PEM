using System;

namespace PersonalExpanseManager.ValueObjects;

public record Periodo(DateOnly DataInicial, DateOnly DataFinal)
{
    public bool Contem(DateOnly data) => data >= DataInicial && data <= DataFinal;

    public override string ToString() => $"{DataInicial:dd/MM/yyyy} - {DataFinal:dd/MM/yyyy}";
}
