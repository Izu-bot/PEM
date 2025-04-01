using System;
using PersonalExpanseManager.Enums;

namespace PersonalExpenseManager.Application.DTO;

public record RelatorioDTO(int Mes, int Ano, decimal TotalGeral, Dictionary<Categoria, decimal> TotaisPorCategoria);