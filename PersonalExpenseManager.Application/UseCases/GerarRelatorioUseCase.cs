using System;
using PersonalExpanseManager.Interfaces;
using PersonalExpenseManager.Application.DTO;

namespace PersonalExpenseManager.Application.UseCases;

public class GerarRelatorioUseCase
{
    private readonly IDespesaRepository _repository;
    public GerarRelatorioUseCase(IDespesaRepository repository) => _repository = repository;

    public RelatorioDTO Executar(int mes, int ano)
    {
        var despesas = _repository.BuscarPorPeriodo(mes, ano);

        if (despesas.Count == 0)
            throw new InvalidOperationException("Nenhuma despesa encontrada para o período informado.");

        var totalGeral = despesas.Sum(d => d.Valor.Valor); // Soma os valores totais
        var totalPorCategoria = despesas
            .GroupBy(d => d.Categoria)
            .ToDictionary(g => g.Key, g => g.Sum(d => d.Valor.Valor)); // Agrupa por categoria e soma os valores
        
        return new RelatorioDTO(mes, ano, totalGeral, totalPorCategoria); // Cria o DTO do relatório
    }
}
