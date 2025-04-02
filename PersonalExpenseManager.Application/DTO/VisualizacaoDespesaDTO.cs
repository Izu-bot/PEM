using System;

namespace PersonalExpenseManager.Application.DTO;

public record VisualizacaoDespesaDTO(Guid Id, decimal Valor, string Categoria, DateOnly Data);
