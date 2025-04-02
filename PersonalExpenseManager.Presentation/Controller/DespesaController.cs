using System;
using PersonalExpenseManager.Application.DTO;
using PersonalExpenseManager.Application.UseCases;
using Spectre.Console;

namespace PersonalExpenseManager.Presentation.Controller;

public class DespesaController
{
    private readonly AdicionarDespesaUseCase _adicionarDespesaUseCase;
    private readonly ListarDespesasUseCase _listarDespesasUseCase;
    private readonly FiltrarDesepsasUseCase _filtrarDesepsasUseCase;
    private readonly RemoverDespesaUseCase _removerDespesaUseCase;
    private readonly GerarRelatorioUseCase _gerarRelatorioUseCase;

    public DespesaController(
        AdicionarDespesaUseCase adicionarDespesaUseCase,
        ListarDespesasUseCase listarDespesasUseCase,
        FiltrarDesepsasUseCase filtrarDesepsasUseCase,
        RemoverDespesaUseCase removerDespesaUseCase,
        GerarRelatorioUseCase gerarRelatorioUseCase
    )
    {
        _adicionarDespesaUseCase = adicionarDespesaUseCase;
        _listarDespesasUseCase = listarDespesasUseCase;
        _filtrarDesepsasUseCase = filtrarDesepsasUseCase;
        _removerDespesaUseCase = removerDespesaUseCase;
        _gerarRelatorioUseCase = gerarRelatorioUseCase;
    }

    public void AdicionarDespesa(decimal valor, string categoria, DateOnly data)
    {
        var despesaDTO = new DespesaDTO(valor, categoria, data);
        _adicionarDespesaUseCase.Executar(despesaDTO);
        AnsiConsole.MarkupLine("[bold green]Despesa adicionada com sucesso![/]");
    }


    public List<VisualizacaoDespesaDTO> ListarDespesa() => _listarDespesasUseCase.Executar();

    public List<VisualizacaoDespesaDTO> FiltrarDespesas(string categoria) => _filtrarDesepsasUseCase.Executar(categoria);

    public void RemoverDespesa(Guid id)
    {
        _removerDespesaUseCase.Executar(id);
        Console.WriteLine("Despesa removida com sucesso!");
    }

    public RelatorioDTO GerarRelatorio(int mes, int ano) => _gerarRelatorioUseCase.Executar(mes, ano);    
}
