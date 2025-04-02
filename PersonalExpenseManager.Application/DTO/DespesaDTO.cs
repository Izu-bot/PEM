using System;

namespace PersonalExpenseManager.Application.DTO;

public record DespesaDTO(decimal Valor, string Categoria, DateOnly Data);
