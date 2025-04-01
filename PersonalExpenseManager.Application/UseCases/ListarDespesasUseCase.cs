using System;
using PersonalExpanseManager.Interfaces;
using PersonalExpenseManager.Application.DTO;

namespace PersonalExpenseManager.Application.UseCases;

public class ListarDespesasUseCase
{
    private readonly IDespesaRepository _repository;

    public ListarDespesasUseCase(IDespesaRepository repository) => _repository = repository;

    public List<DespesaDTO> Executar()
    {
        var despesas = _repository.BuscarTodas();

        return despesas.Select(d => new DespesaDTO(d.Id, d.Valor.Valor, d.Categoria.ToString(), d.Data)).ToList();
    }
}
