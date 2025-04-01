using System;
using PersonalExpanseManager.Interfaces;

namespace PersonalExpenseManager.Application.UseCases;

public class RemoverDespesaUseCase
{
    private readonly IDespesaRepository _repository;
    public RemoverDespesaUseCase(IDespesaRepository repository) => _repository = repository;

    public string Executar(Guid id)
    {
        var despesa = _repository.BuscarPorId(id);

        if (despesa == null) 
            throw new ArgumentException("Despesa não encontrada.");
        
        var sucesso = _repository.Deletar(id);

        return sucesso ? "Despesa removida com sucesso." : "Erro ao remover despesa.";
    }
}
