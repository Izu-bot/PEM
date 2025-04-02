using System;
using PersonalExpanseManager.Enums;
using PersonalExpanseManager.Interfaces;
using PersonalExpenseManager.Application.DTO;

namespace PersonalExpenseManager.Application.UseCases;

public class FiltrarDesepsasUseCase
{
    private readonly IDespesaRepository _repository;

    public FiltrarDesepsasUseCase(IDespesaRepository repository) => _repository = repository;

    public List<VisualizacaoDespesaDTO> Executar(string categoria)
    {
        if (!Enum.TryParse<Categoria>(categoria, true, out var categoriaValida)) 
            throw new ArgumentException("Categoria inválida.");

        var despesas = _repository.BuscarPorCategoria(categoriaValida);

        return despesas.Select(d => new VisualizacaoDespesaDTO(d.Id, d.Valor.Valor, d.Categoria.ToString(), d.Data)).ToList();
    }
}
