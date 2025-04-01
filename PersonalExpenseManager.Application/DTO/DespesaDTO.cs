using System;

namespace PersonalExpenseManager.Application.DTO;

public record DespesaDTO(Guid Id, decimal Valor, string Categoria, DateOnly Data);
