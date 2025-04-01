using System;
using PersonalExpanseManager.Entities;
using PersonalExpanseManager.Enums;
using PersonalExpanseManager.Interfaces;
using PersonalExpenseManager.Application.DTO;

namespace PersonalExpenseManager.Application.UseCases;

public class AdicionarDespesaUseCase
{
    private readonly IDespesaRepository _repository;

    public AdicionarDespesaUseCase(IDespesaRepository repository) => _repository = repository;
    
    public void Executar(DespesaDTO despesaDTO)
    {
        if (!Enum.TryParse<Categoria>(despesaDTO.Categoria, true, out var categoria))
            throw new ArgumentException("Categoria inválida.");

        var despesa = new Despesa(despesaDTO.Valor, categoria, despesaDTO.Data);
        _repository.Salvar(despesa);
    }
}
